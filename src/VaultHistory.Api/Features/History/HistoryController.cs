// src/VaultHistory.Api/Features/History/HistoryController.cs

using Microsoft.AspNetCore.Mvc;
using VaultHistory.Core.Interfaces;

namespace VaultHistory.Api.Features.History;

[ApiController]
[Route("api/v1/[controller]")] // Define a rota base: /api/v1/history
public class HistoryController : ControllerBase
{
    private readonly IGeocodingService _geocodingService;
    private readonly IWikipediaService _wikipediaService;

    // Os serviços são injetados aqui pelo container de DI
    public HistoryController(IGeocodingService geocodingService, IWikipediaService wikipediaService)
    {
        _geocodingService = geocodingService;
        _wikipediaService = wikipediaService;
    }

    [HttpGet("here")] // Define a sub-rota: /here -> /api/v1/history/here
    public async Task<IActionResult> GetHistoryHere([FromQuery] double lat, [FromQuery] double lon)
    {
        // Validação básica de entrada
        if (lat is < -90 or > 90 || lon is < -180 or > 180)
        {
            return BadRequest("Latitude ou longitude inválida.");
        }

        // 1. Orquestra as chamadas aos serviços em paralelo para mais eficiência
        var locationNameTask = _geocodingService.GetLocationNameFromCoordinatesAsync(lat, lon);
        var eventsTask = _wikipediaService.GetEventsOnThisDayAsync();

        await Task.WhenAll(locationNameTask, eventsTask);

        var locationName = await locationNameTask;
        if (string.IsNullOrEmpty(locationName))
        {
            return NotFound("Não foi possível determinar o nome do local para as coordenadas fornecidas.");
        }

        var summaryTask = _wikipediaService.GetSummaryForLocationAsync(locationName);
        
        var events = await eventsTask;
        var summary = await summaryTask;

        // 2. Monta o DTO de resposta com os dados coletados
        var response = new HistoryResponseDto(
            LocationContext: new LocationContextDto(
                PlaceName: locationName,
                Summary: summary
            ),
            EventsOnThisDay: new EventsOnThisDayDto(
                Date: DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Events: events.Select(e => new HistoricalEventDto(e.Year, e.Description))
            )
        );

        // 3. Retorna a resposta de sucesso com o JSON
        return Ok(response);
    }
}
