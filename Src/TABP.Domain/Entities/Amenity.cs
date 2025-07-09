﻿namespace TABP.Domain.Entities;

public class Amenity
{
    public int Id { get; set; }

    public required string Name { get; set; }
    public string? Description { get; set; }
}