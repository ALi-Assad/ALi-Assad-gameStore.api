using System;
using gameStore.api.Dtos;
using gameStore.api.Entities;

namespace gameStore.api.Mapping;

public static class GenreMapping
{

    public static GenreDto ToDto(this Genre genre)
    {
        return new(
            genre.Id,
            genre.Name
        );
    }

}
