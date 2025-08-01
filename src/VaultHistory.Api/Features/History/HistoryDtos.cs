// src/VaultHistory.Api/Features/History/HistoryDtos.cs

namespace VaultHistory.Api.Features.History;

// DTO para um único evento histórico na resposta
public record HistoricalEventDto(string Year, string Description);

// DTO para a resposta completa da API em caso de sucesso
public record HistoryResponseDto(
    LocationContextDto LocationContext,
    EventsOnThisDayDto EventsOnThisDay
);

public record LocationContextDto(string PlaceName, string Summary);

public record EventsOnThisDayDto(string Date, IEnumerable<HistoricalEventDto> Events);
