

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistance;

namespace Restaurants.Infrastructure.Seeders
{
    public interface IRestaurantSeeder
    {
        Task Seed();
    }

    internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Restaurants.Any())
                {
                    var restuarants = GetRestaurants();
                    dbContext.Restaurants.AddRange(restuarants);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }

            }
        }

        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles = new List<IdentityRole>()
            {
                new (UserRoles.User)
                {
                    NormalizedName = UserRoles.User.ToUpper()
                },
                new (UserRoles.Owner)
                {
                    NormalizedName = UserRoles.Owner.ToUpper()
                },
                new (UserRoles.Admin)
                {
                    NormalizedName = UserRoles.Admin.ToUpper()
                }
            };
            
            return roles;
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Name = "Gourmet Bistro",
                    Description = "A cozy bistro offering a mix of French and Italian cuisine.",
                    Category = "Fine Dining",
                    HasDelivery = true,
                    ContactEmail = "contact@gourmetbistro.com",
                    ContactNumber = "123-456-7890",
                    Address = new Address
                    {
                        City = "San Francisco",
                        Street = "123 Main Street",
                        PostalCode = "94105"
                    },
                    Dishes = new List<Dish>
                    {
                        new Dish
                        {
                            Name = "Truffle Pasta",
                            Description = "Fresh pasta with creamy truffle sauce.",
                            Price = 18.99m,
                        },
                        new Dish
                        {
                            Name = "Crème Brûlée",
                            Description = "Classic French dessert with a caramelized top.",
                            Price = 7.50m,
                        }
                    }
                },
                new Restaurant
                {
                    Name = "Sushi Haven",
                    Description = "A modern sushi bar with fresh, high-quality ingredients.",
                    Category = "Japanese",
                    HasDelivery = false,
                    ContactEmail = "info@sushihaven.com",
                    ContactNumber = "987-654-3210",
                    Address = new Address
                    {
                        City = "Los Angeles",
                        Street = "456 Ocean Drive",
                        PostalCode = "90210"
                    }
                }
            };

            return restaurants;
        }
    }
}
