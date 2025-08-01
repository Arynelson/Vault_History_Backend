// src/VaultHistory.Core/Interfaces/IGeocodingService.cs

namespace VaultHistory.Core.Interfaces;

public interface IGeocodingService
{
    Task<string> GetLocationNameFromCoordinatesAsync(double latitude, double longitude, CancellationToken cancellationToken = default);
}

//Análise:
//Este contrato define um único método que recebe latitude e longitude e retorna o nome do local (ex: "São Paulo, Brasil") como uma string.
