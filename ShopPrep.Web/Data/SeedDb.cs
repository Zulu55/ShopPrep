namespace ShopPrep.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Models;
    using Microsoft.AspNetCore.Identity;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private Random random;

        public SeedDb(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            await this.CheckRole("Admin");
            await this.CheckRole("Customer");

            var user = await this.userManager.FindByEmailAsync("jzuluaga55@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Juan",
                    LastName = "Zuluaga",
                    Email = "jzuluaga55@gmail.com",
                    UserName = "jzuluaga55@gmail.com"
                };

                var result = await this.userManager.CreateAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await this.userManager.AddToRoleAsync(user, "Admin");
            }

            var isInRole = await this.userManager.IsInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await this.userManager.AddToRoleAsync(user, "Admin");
            }

            if (!this.context.Products.Any())
            {
                this.AddProduct("First Product", user);
                this.AddProduct("Second Product", user);
                this.AddProduct("Third Product", user);
                await this.context.SaveChangesAsync();
            }
        }

        private async Task CheckRole(string roleName)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await this.roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        private void AddProduct(string name, User user)
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = this.random.Next(100),
                IsAvailabe = true,
                Stock = this.random.Next(100),
                User = user
            });
        }
    }
}