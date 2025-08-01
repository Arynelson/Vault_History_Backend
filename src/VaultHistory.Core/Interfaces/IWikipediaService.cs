// src/VaultHistory.Core/Interfaces/IWikipediaService.cs

using VaultHistory.Core.Entities;

namespace VaultHistory.Core.Interfaces;

public interface IWikipediaService
{
    Task<IEnumerable<HistoricalEvent>> GetEventsOnThisDayAsync(CancellationToken cancellationToken = default);
    
    Task<string> GetSummaryForLocationAsync(string locationName, CancellationToken cancellationToken = default);
}

// Análise:
// public interface IWikipediaService: Define o contrato.
// Task<IEnumerable<HistoricalEvent>> GetEventsOnThisDayAsync(...): Define um método que deve retornar uma tarefa (Task) que, 
//quando concluída, resultará em uma coleção (IEnumerable) de objetos HistoricalEvent. Este método buscará os fatos do dia.
// Task<string> GetSummaryForLocationAsync(...): Define um método que busca o resumo histórico para um local específico.
// CancellationToken cancellationToken = default: Este é um parâmetro muito importante para boas práticas em código assíncrono.
// Ele permite que uma operação longa (como uma chamada de API) seja cancelada, por exemplo, se o usuário fechar o app ou
// a requisição atingir um timeout.