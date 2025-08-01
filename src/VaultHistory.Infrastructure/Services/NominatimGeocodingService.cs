// src/VaultHistory.Infrastructure/Services/NominatimGeocodingService.cs

using System.Net.Http.Json;
using System.Text.Json.Serialization;
using VaultHistory.Core.Interfaces;

namespace VaultHistory.Infrastructure.Services;

// Classe interna para mapear apenas os campos que nos interessam da resposta da API
internal class NominatimResponse
{
    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = string.Empty;
}

public class NominatimGeocodingService : IGeocodingService
{
    private readonly HttpClient _httpClient;

    // Usamos injeção de dependência para receber um HttpClient configurado.
    public NominatimGeocodingService(HttpClient httpClient )
    {
        _httpClient = httpClient;
        // É uma boa prática definir um User-Agent para APIs públicas.
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("VaultHistoryApp/1.0" );
    }

    public async Task<string> GetLocationNameFromCoordinatesAsync(double latitude, double longitude, CancellationToken cancellationToken = default)
    {
        // Formata a URL da API do Nominatim com as coordenadas.
        // Usamos InvariantCulture para garantir que o ponto decimal seja sempre '.'
        var requestUri = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={latitude.ToString(System.Globalization.CultureInfo.InvariantCulture )}&lon={longitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}";

        try
        {
            // Faz a chamada GET e pede para o .NET converter o JSON da resposta
            // diretamente para o nosso objeto NominatimResponse.
            var response = await _httpClient.GetFromJsonAsync<NominatimResponse>(requestUri, cancellationToken );

            // Retorna o nome do local se a resposta for válida, ou uma string vazia caso contrário.
            return response?.DisplayName ?? string.Empty;
        }
        catch (HttpRequestException ex)
        {
            // Em um cenário real, adicionaríamos um log aqui para registrar o erro.
            // Por enquanto, retornamos uma string vazia para indicar a falha.
            Console.WriteLine($"Erro ao chamar a API do Nominatim: {ex.Message}");
            return string.Empty;
        }
    }
}
