using FluentAssertions.Execution;
using System.Reactive.Linq;
using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.General;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Ships.Commands;
using Wolfe.SpaceTraders.Domain.Ships.Results;

namespace Wolfe.SpaceTraders.Domain.Tests.Unit.Ships;

public class ShipTests
{
    private readonly Mock<IShipClient> _client = new();
    private readonly Mock<IShipFuelBase> _fuel = new();
    private readonly Mock<IShipNavigation> _navigation = new();
    private readonly Mock<IShipCargo> _cargo = new();

    public ShipTests()
    {
        _client
            .Setup(c => c.Refuel(It.IsAny<ShipId>(), default))
            .ReturnsAsync(new ShipRefuelResult { NewValue = new Fuel(100), Cost = new Credits(100) });

        _client
            .Setup(c => c.Navigate(It.IsAny<ShipId>(), It.IsAny<ShipNavigateCommand>(), default))
            .ReturnsAsync((ShipId _, ShipNavigateCommand command, CancellationToken _) => new ShipNavigateResult
            {
                FuelRemaining = new Fuel(50),
                Destination = new ShipNavigationDestination
                {
                    WaypointId = command.WaypointId,
                    Arrival = DateTimeOffset.UtcNow,
                    Location = Point.Zero,
                }
            });

        _fuel.SetupGet(f => f.Capacity).Returns(new Fuel(100));
        _cargo.SetupGet(c => c.Items).Returns(Array.Empty<ShipCargoItem>());
    }

    [Fact]
    public async Task Dock_WhenInOrbit_Docks()
    {
        // Arrange
        _navigation.SetupGet(n => n.Status).Returns(ShipNavigationStatus.InOrbit);

        // Act
        var sut = CreateShip();
        await sut.Dock();

        // Assert
        _client.Verify(c => c.Dock(sut.Id, CancellationToken.None), Times.Once);
        sut.Navigation.Status.Should().Be(ShipNavigationStatus.Docked);
    }

    [Fact]
    public async Task Dock_WhenDocked_RemainsDocked()
    {
        // Arrange
        _navigation.SetupGet(n => n.Status).Returns(ShipNavigationStatus.Docked);

        // Act
        var sut = CreateShip();
        await sut.Dock();

        // Assert
        _client.Verify(c => c.Dock(sut.Id, CancellationToken.None), Times.Never);
        sut.Navigation.Status.Should().Be(ShipNavigationStatus.Docked);
    }

    [Fact]
    public async Task Dock_WhenInTransit_Throws()
    {
        // Arrange
        _navigation.SetupGet(n => n.Status).Returns(ShipNavigationStatus.InTransit);

        // Act
        var sut = CreateShip();
        var act = () => sut.Dock().AsTask();

        // Assert
        using (new AssertionScope())
        {
            await act.Should().ThrowAsync<InvalidOperationException>();
            _client.Verify(c => c.Dock(sut.Id, CancellationToken.None), Times.Never);
            sut.Navigation.Status.Should().Be(ShipNavigationStatus.InTransit);
        }
    }

    [Fact]
    public async Task Orbit_WhenInOrbit_RemainsInOrbit()
    {
        // Arrange
        _navigation.SetupGet(n => n.Status).Returns(ShipNavigationStatus.InOrbit);

        // Act
        var sut = CreateShip();
        await sut.Orbit();

        // Assert
        _client.Verify(c => c.Orbit(sut.Id, CancellationToken.None), Times.Never);
        sut.Navigation.Status.Should().Be(ShipNavigationStatus.InOrbit);
    }

    [Fact]
    public async Task Orbit_WhenDocked_GoesToOrbit()
    {
        // Arrange
        _navigation.SetupGet(n => n.Status).Returns(ShipNavigationStatus.Docked);

        // Act
        var sut = CreateShip();
        await sut.Orbit();

        // Assert
        _client.Verify(c => c.Orbit(sut.Id, CancellationToken.None), Times.Once);
        sut.Navigation.Status.Should().Be(ShipNavigationStatus.InOrbit);
    }

    [Fact]
    public async Task Orbit_WhenInTransit_Throws()
    {
        // Arrange
        _navigation.SetupGet(n => n.Status).Returns(ShipNavigationStatus.InTransit);

        // Act
        var sut = CreateShip();
        var act = () => sut.Orbit().AsTask();

        // Assert
        using (new AssertionScope())
        {
            await act.Should().ThrowAsync<InvalidOperationException>();
            _client.Verify(c => c.Orbit(sut.Id, CancellationToken.None), Times.Never);
            sut.Navigation.Status.Should().Be(ShipNavigationStatus.InTransit);
        }
    }

    [Fact]
    public async Task Refuel_WhenInTransit_Throws()
    {
        // Arrange
        _fuel.SetupGet(f => f.Current).Returns(Fuel.Zero);
        _navigation.SetupGet(n => n.Status).Returns(ShipNavigationStatus.InTransit);

        // Act
        var sut = CreateShip();
        var act = () => sut.Refuel().AsTask();

        // Assert
        using (new AssertionScope())
        {
            await act.Should().ThrowAsync<InvalidOperationException>();
            _client.Verify(c => c.Refuel(sut.Id, default), Times.Never);
            sut.Navigation.Status.Should().Be(ShipNavigationStatus.InTransit);
            sut.Fuel.Current.Should().Be(Fuel.Zero);
        }
    }

    [Fact]
    public async Task Refuel_WhenInOrbit_DocksFirst()
    {
        // Arrange
        _fuel.SetupGet(f => f.Current).Returns(Fuel.Zero);
        _navigation.SetupGet(n => n.Status).Returns(ShipNavigationStatus.InOrbit);

        // Act
        var sut = CreateShip();
        await sut.Refuel().AsTask();

        // Assert
        using (new AssertionScope())
        {
            _client.Verify(c => c.Dock(sut.Id, default), Times.Once);
            _client.Verify(c => c.Refuel(sut.Id, default), Times.Once);
            sut.Navigation.Status.Should().Be(ShipNavigationStatus.Docked);
            sut.Fuel.Current.Should().Be(sut.Fuel.Capacity);
        }
    }

    [Fact]
    public async Task Refuel_WhenDocked_Refuels()
    {
        // Arrange
        _fuel.SetupGet(f => f.Current).Returns(Fuel.Zero);
        _navigation.SetupGet(n => n.Status).Returns(ShipNavigationStatus.Docked);

        // Act
        var sut = CreateShip();
        await sut.Refuel();

        // Assert
        using (new AssertionScope())
        {
            _client.Verify(c => c.Refuel(sut.Id, default), Times.Once);
            sut.Fuel.Current.Should().Be(sut.Fuel.Capacity);
        }
    }

    [Fact]
    public async Task NavigateTo_WithSpeed_SetsSpeed()
    {
        // Arrange
        _navigation.SetupGet(n => n.Speed).Returns(ShipSpeed.Cruise);

        // Act
        var sut = CreateShip();
        await sut.NavigateTo(WaypointId.Empty, ShipSpeed.Burn);

        // Assert
        using (new AssertionScope())
        {
            _client.Verify(c => c.SetSpeed(sut.Id, ShipSpeed.Burn, default), Times.Once);
            sut.Navigation.Speed.Should().Be(ShipSpeed.Burn);
        }
    }

    [Fact]
    public async Task NavigateTo_WithoutSpeed_DoesNotSetSpeed()
    {
        // Arrange
        _navigation.SetupGet(n => n.Speed).Returns(ShipSpeed.Stealth);

        // Act
        var sut = CreateShip();
        await sut.NavigateTo(WaypointId.Empty);

        // Assert
        using (new AssertionScope())
        {
            _client.Verify(c => c.SetSpeed(sut.Id, It.IsAny<ShipSpeed>(), default), Times.Never);
            sut.Navigation.Speed.Should().Be(ShipSpeed.Stealth);
        }
    }

    [Fact]
    public async Task NavigateTo_WhenDocked_ReturnsToOrbitFirst()
    {
        // Arrange
        _navigation.SetupGet(n => n.Status).Returns(ShipNavigationStatus.Docked);

        // Act
        var sut = CreateShip();
        await sut.NavigateTo(WaypointId.Empty);

        // Assert
        using (new AssertionScope())
        {
            _client.Verify(c => c.Orbit(sut.Id, default), Times.Once);
        }
    }

    [Fact]
    public async Task NavigateTo_WhenInTransit_Throws()
    {
        // Arrange
        _navigation.SetupGet(n => n.Status).Returns(ShipNavigationStatus.InTransit);

        // Act
        var sut = CreateShip();
        var act = () => sut.NavigateTo(WaypointId.Empty).AsTask();

        // Assert
        using (new AssertionScope())
        {
            await act.Should().ThrowAsync<InvalidOperationException>();
            _client.Verify(c => c.Navigate(sut.Id, It.IsAny<ShipNavigateCommand>(), default), Times.Never);
        }
    }

    [Fact]
    public async Task NavigateTo_WhenAtDestination_Returns()
    {
        // Arrange
        var location = WaypointId.Parse("LOCATION");
        _navigation.SetupGet(n => n.WaypointId).Returns(location);
        _navigation.SetupGet(n => n.Status).Returns(ShipNavigationStatus.InOrbit);

        // Act
        var sut = CreateShip();
        await sut.NavigateTo(location);

        // Assert
        using (new AssertionScope())
        {
            _client.Verify(c => c.Navigate(sut.Id, It.IsAny<ShipNavigateCommand>(), default), Times.Never);
        }
    }

    [Fact]
    public async Task NavigateTo_NewDestination_BeginsNavigation()
    {
        // Arrange
        var home = WaypointId.Parse("HOME");
        var location = WaypointId.Parse("LOCATION");

        _navigation.SetupGet(n => n.WaypointId).Returns(home);
        _navigation.SetupGet(n => n.Status).Returns(ShipNavigationStatus.InOrbit);

        // Act
        var sut = CreateShip();
        await sut.NavigateTo(location);

        // Assert
        using (new AssertionScope())
        {
            _client.Verify(c => c.Navigate(sut.Id, It.IsAny<ShipNavigateCommand>(), default), Times.Once);
        }
    }

    [Fact]
    public async Task NavigateTo_NewDestination_UpdatesDetails()
    {
        // Arrange
        var home = WaypointId.Parse("HOME");
        var destination = WaypointId.Parse("LOCATION");
        var fuelRemaining = new Fuel(50);
        var destinationPoint = new Point(15, 20);
        var arrivalAt = DateTimeOffset.UtcNow + TimeSpan.FromHours(1);

        _navigation.SetupGet(n => n.WaypointId).Returns(home);
        _navigation.SetupGet(n => n.Status).Returns(ShipNavigationStatus.InOrbit);

        _client
            .Setup(c => c.Navigate(It.IsAny<ShipId>(), It.IsAny<ShipNavigateCommand>(), default))
            .ReturnsAsync((ShipId _, ShipNavigateCommand command, CancellationToken _) => new ShipNavigateResult
            {
                FuelRemaining = fuelRemaining,
                Destination = new ShipNavigationDestination
                {
                    WaypointId = command.WaypointId,
                    Arrival = arrivalAt,
                    Location = destinationPoint,
                }
            });

        // Act
        var sut = CreateShip();
        await sut.NavigateTo(destination);

        // Assert
        using (new AssertionScope())
        {
            _client.Verify(c => c.Navigate(sut.Id, It.IsAny<ShipNavigateCommand>(), default), Times.Once);
            sut.Navigation.Status.Should().Be(ShipNavigationStatus.InTransit);
            sut.Navigation.Destination.Should().NotBeNull();
            sut.Navigation.Destination?.WaypointId.Should().Be(destination);
            sut.Navigation.Destination?.Location.Should().Be(destinationPoint);
            sut.Navigation.Destination?.Arrival.Should().Be(arrivalAt);
            sut.Fuel.Current.Should().Be(fuelRemaining);
        }
    }

    [Fact]
    public async Task OnArrival_NewDestination_EmitsArrivedEvent()
    {
        // Arrange
        var destination = WaypointId.Parse("DESTINATION");
        var destinationPoint = new Point(15, 20);

        _navigation.SetupGet(n => n.WaypointId).Returns(WaypointId.Parse("HOME"));
        _navigation.SetupGet(n => n.Status).Returns(ShipNavigationStatus.InOrbit);

        var updatedNavigation = new Mock<IShipNavigation>();
        updatedNavigation.SetupGet(n => n.WaypointId).Returns(destination);
        updatedNavigation.SetupGet(n => n.Status).Returns(ShipNavigationStatus.InOrbit);
        updatedNavigation.SetupGet(n => n.Location).Returns(destinationPoint);

        _client
            .Setup(c => c.GetNavigationStatus(It.IsAny<ShipId>(), default))
            .ReturnsAsync(updatedNavigation.Object);

        _client
            .Setup(c => c.Navigate(It.IsAny<ShipId>(), It.IsAny<ShipNavigateCommand>(), default))
            .ReturnsAsync((ShipId _, ShipNavigateCommand command, CancellationToken _) => new ShipNavigateResult
            {
                FuelRemaining = new Fuel(50),
                Destination = new ShipNavigationDestination
                {
                    WaypointId = command.WaypointId,
                    Arrival = DateTimeOffset.UtcNow + TimeSpan.FromSeconds(5),
                    Location = destinationPoint,
                }
            });

        // Act
        var sut = CreateShip();
        await sut.NavigateTo(destination);
        var result = await sut.Arrived.Take(1);

        // Assert
        using (new AssertionScope())
        {
            _client.Verify(c => c.Navigate(sut.Id, It.IsAny<ShipNavigateCommand>(), default), Times.Once);
            _client.Verify(c => c.GetNavigationStatus(sut.Id, default), Times.Once);

            result.Should().Be(destination);
            sut.Navigation.Location.Should().Be(destinationPoint);
            sut.Navigation.Status.Should().Be(ShipNavigationStatus.InOrbit);
            sut.Navigation.WaypointId.Should().Be(destination);
            sut.Navigation.Destination.Should().BeNull();
        }
    }

    // TODO: Test Sell
    // TODO: Test Extract
    // TODO: Test Jettison
    // TODO: Test NavigateTo

    private Ship CreateShip() => Ship.Create(
        _client.Object,
        new ShipId("SHIP"),
        new AgentId("AGENT"),
        "SHIP",
        ShipRole.Command,
        _fuel.Object,
        _navigation.Object,
        _cargo.Object
    );
}