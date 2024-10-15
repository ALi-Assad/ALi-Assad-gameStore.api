using System;
using gameStore.api.Data;
using gameStore.api.Dtos;
using gameStore.api.Entities;
using gameStore.api.Mapping;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace gameStore.api.EndPoints;

public static class GamesEndPoints
{

    const string gatGameEndPointName = "GetGame";

    public static void MapGamesEndPoints(this WebApplication app)
    {

        RouteGroupBuilder group = app.MapGroup("games")
        .WithParameterValidation();

        group.MapGet("", async (GameStoreContext dbContext) =>
                         await dbContext
                            .Games
                            .Include(game => game.Genre)
                            .Select(game => game.ToGameSummaryDto())
                            .AsNoTracking()
                            .ToListAsync()
        );

        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {

            Game? game = await dbContext.Games.FindAsync(id);
            return game is null
            ? Results.NotFound()
            : Results.Ok(game.ToGameDetailsDto());

        }
        ).WithName(gatGameEndPointName);

        group.MapPost("", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntitiy();

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(gatGameEndPointName,
             new { id = game.Id },
              game.ToGameDetailsDto()
            );
        });

        group.MapPut("/{id}", async (int id, UpdateGameDto updateGameDto, GameStoreContext dbContext) =>
        {

            Game? game = await dbContext.Games.FindAsync(id);

            if (game is null)
            {
                return Results.NotFound();
            }

            dbContext
            .Entry(game)
            .CurrentValues
            .SetValues(updateGameDto.ToEntitiy(id));
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
            {
                await dbContext.Games.Where(game => game.Id == id)
                .ExecuteDeleteAsync();

                return Results.NoContent();
            }
        );

    }

}
