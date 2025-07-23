﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TABP.Infrastructure.contexts;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // Use your actual connection string here
        optionsBuilder.UseSqlServer("YourConnectionStringHere");

        return new AppDbContext(optionsBuilder.Options);
    }
}