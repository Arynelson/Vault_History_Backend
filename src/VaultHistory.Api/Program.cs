// src/VaultHistory.Api/Program.cs

using VaultHistory.Core.Interfaces;
using VaultHistory.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// --- Seção de Configuração de Serviços (Injeção de Dependência) ---

// 1. Adiciona os serviços de controller da API
builder.Services.AddControllers();

// 2. Registra nosso serviço de geocodificação
// Adicionamos como "Typed HttpClient" para que ele receba um HttpClient gerenciado
builder.Services.AddHttpClient<IGeocodingService, NominatimGeocodingService>();

// 3. Registra nosso serviço da Wikipédia (usando a versão Fake por enquanto)
// Usamos AddScoped para que uma nova instância seja criada para cada requisição web.
builder.Services.AddScoped<IWikipediaService, FakeWikipediaService>();


// --- Fim da Seção de Configuração ---


var app = builder.Build();

// --- Seção de Configuração do Pipeline HTTP ---

// Redireciona chamadas HTTP para HTTPS
app.UseHttpsRedirection();

// Habilita o roteamento para os controllers
app.MapControllers();

// --- Fim da Seção de Pipeline ---

app.Run();
