using System;
using gameStore.api.Data;
using gameStore.api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace gameStore.api.EndPoints;

public static class GenresEndPoints
{

    public static void MapGeneresEndPoints(this WebApplication app)
    {
        RouteGroupBuilder group = app.MapGroup("genres")
      .WithParameterValidation();

        group.MapGet("", async (GameStoreContext dbContext) =>
            await dbContext.Genres
                    .Select(genre => genre.ToDto())
                    .AsNoTracking()
                    .ToListAsync()
        );
    }

}
