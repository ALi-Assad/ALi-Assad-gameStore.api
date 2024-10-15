using System;
using Microsoft.AspNetCore.Authentication;

namespace gameStore.api.Entities;

public class Genre
{

    public int Id { get; set; }

    public required string Name { get; set; }

}
