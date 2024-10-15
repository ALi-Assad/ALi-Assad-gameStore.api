using gameStore.api.Data;
using gameStore.api.Dtos;
using gameStore.api.EndPoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MapGamesEndPoints();
app.MapGeneresEndPoints();

await app.MigrateDbAsync();

app.Run();
