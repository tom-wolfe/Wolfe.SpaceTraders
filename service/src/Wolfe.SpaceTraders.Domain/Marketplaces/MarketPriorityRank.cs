﻿using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.General;

namespace Wolfe.SpaceTraders.Domain.Marketplaces;

public record MarketPriorityRank(WaypointId MarketId, Point Location, Distance Distance, TimeSpan? DataAge, double Volatility, double Rank);
