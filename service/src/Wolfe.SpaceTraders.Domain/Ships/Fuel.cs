﻿using System.Text.Json.Serialization;

namespace Wolfe.SpaceTraders.Domain.Ships;

[JsonConverter(typeof(FuelJsonConverter))]
public record Fuel(int Value)
{
    public static readonly Fuel Zero = new(0);

    public static decimal operator /(Fuel left, Fuel right) => (decimal)left.Value / right.Value;

    public virtual bool Equals(Fuel? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}