using Wolfe.SpaceTraders.Domain.Marketplaces;

namespace Wolfe.SpaceTraders.Domain.Ships;

internal class ShipCargo : IShipCargo
{
    public ShipCargo(IShipCargoBase cargo)
    {
        _items.AddRange(cargo.Items);
        Capacity = cargo.Capacity;
    }

    private readonly List<ShipCargoItem> _items = [];

    public int Capacity { get; set; }
    public int Quantity => Items.Sum(i => i.Quantity);
    public decimal PercentRemaining => Capacity == 0 ? 0 : (decimal)Quantity / Capacity * 100m;
    public IReadOnlyCollection<ShipCargoItem> Items => _items;

    public void Remove(ItemId itemId, int quantity)
    {
        var item = _items.First(i => i.Id == itemId);
        var remainingQuantity = _items.First(i => i.Id == itemId).Quantity - quantity;
        _items.Remove(item);
        if (remainingQuantity > 0)
        {
            _items.Add(new ShipCargoItem
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Quantity = remainingQuantity
            });
        }
    }

    public bool Contains(ItemId itemId, int quantity)
    {
        return Items.FirstOrDefault(i => i.Id == itemId)?.Quantity >= quantity;
    }

    public void Add(ShipCargoItem item)
    {
        var existingItem = _items.FirstOrDefault(i => i.Id == item.Id);
        var existingQty = existingItem?.Quantity ?? 0;

        if (existingItem != null)
        {
            _items.Remove(existingItem);
        }

        _items.Add(new ShipCargoItem
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Quantity = existingQty + item.Quantity
        });
    }
}