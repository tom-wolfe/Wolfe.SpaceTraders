using FluentAssertions.Execution;
using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Tests.Unit.Ships;

public class ShipTests
{
    private readonly Mock<IShipClient> _client = new();
    private readonly Mock<IShipFuelBase> _fuel = new();
    private readonly Mock<IShipNavigation> _navigation = new();
    private readonly Mock<IShipCargo> _cargo = new();

    public ShipTests()
    {
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