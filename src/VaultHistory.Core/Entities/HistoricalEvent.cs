// src/VaultHistory.Core/Entities/HistoricalEvent.cs

namespace VaultHistory.Core.Entities;

public class HistoricalEvent
{
    public required string Year { get; set; }
    public required string Description { get; set; }
}
