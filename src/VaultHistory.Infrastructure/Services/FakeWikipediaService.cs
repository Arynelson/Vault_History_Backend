// src/VaultHistory.Infrastructure/Services/FakeWikipediaService.cs

using VaultHistory.Core.Entities;
using VaultHistory.Core.Interfaces;

namespace VaultHistory.Infrastructure.Services;

public class FakeWikipediaService : IWikipediaService
{
    public Task<IEnumerable<HistoricalEvent>> GetEventsOnThisDayAsync(CancellationToken cancellationToken = default)
    {
        // Retorna uma lista de dados falsos, mas com a estrutura correta.
        var fakeEvents = new List<HistoricalEvent>
        {
            new() { Year = "1969", Description = "A Apollo 11 pousa na Lua, e Neil Armstrong se torna o primeiro humano a caminhar na superfície lunar." },
            new() { Year = "1881", Description = "O famoso pistoleiro Billy the Kid é morto pelo xerife Pat Garrett." },
            new() { Year = "2012", Description = "O filme 'O Cavaleiro das Trevas Ressurge' é lançado, concluindo a trilogia de Christopher Nolan." }
        };

        // Envolve a lista em uma Task para simular uma operação assíncrona.
        return Task.FromResult<IEnumerable<HistoricalEvent>>(fakeEvents);
    }

    public Task<string> GetSummaryForLocationAsync(string locationName, CancellationToken cancellationToken = default)
    {
        // Retorna um resumo falso baseado no nome do local.
        var summary = $"Este é um resumo histórico gerado para '{locationName}'. É um lugar com uma história rica e fascinante, conhecido por seus marcos culturais e contribuições significativas para a região ao longo dos séculos.";

        return Task.FromResult(summary);
    }
}
