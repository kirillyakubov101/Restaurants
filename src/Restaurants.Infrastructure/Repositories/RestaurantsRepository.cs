﻿using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistance;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantsRepository(RestaurantsDbContext dbContext)
    : IRestaurantRepository
{
    public async Task<int> Create(Restaurant entity)
    {
        await dbContext.Restaurants.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task Delete(Restaurant entity)
    {
        dbContext.Restaurants.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants.ToListAsync();
        return restaurants;
    }

    public async Task<(IEnumerable<Restaurant>,int)> GetAllMatchingAsync(string? searchPhrase,int pageSize,int PageNumber,string? sortBy,SortDirection sortDirection)
    {
        string? lowercaseSearchPhrase = searchPhrase?.ToLower();

        var baseQuery = dbContext
         .Restaurants
         .Where(r => lowercaseSearchPhrase == null || (r.Name.ToLower().Contains(lowercaseSearchPhrase)
                                          || r.Description.ToLower().Contains(lowercaseSearchPhrase)));

        var totalCount = await baseQuery.CountAsync();

        if(sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                {nameof(Restaurant.Name),r => r.Name},
                {nameof(Restaurant.Description),r => r.Description},
                {nameof(Restaurant.Category),r => r.Category},
             };

            var selectedColumn = columnsSelector[sortBy];

            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var restaurants = await baseQuery
            .Skip(pageSize * (PageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (restaurants,totalCount);
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        var restaurant = await dbContext.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(x => x.Id == id);
        return restaurant;
    }

    public Task SaveChanges()
    {
        return dbContext.SaveChangesAsync();
    }
}