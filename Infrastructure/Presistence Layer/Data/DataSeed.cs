using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entitys.IdentityModels;
using DomainLayer.Models.OrderModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Presistence_Layer.Data
{
    #region Old seeding
    // public class DataSeed(StoreDbContext _dbContext) : IDataSeed
    //{
    //    public void SeedDataAsync()
    //    {
    //        try
    //        {

    //            //if there are any pending migrations, apply them on database
    //            if (_dbContext.Database.GetPendingMigrations().Any())
    //            {
    //                _dbContext.Database.Migrate();
    //            }
    //            //Must begin seeding with ProductBrands or ProductTypes because Products has foreign keys to them
    //            if (!_dbContext.ProductBrands.Any())
    //            {

    //                var productBrandData = File.ReadAllText("..\\Infrastructure\\Presistence Layer\\Data\\DataSeed\\brands.json");
    //                //Convert json data to C# object by using Sel
    //                var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandData);
    //                if (ProductBrands is not null && ProductBrands.Any())
    //                {
    //                    _dbContext.ProductBrands.AddRange(ProductBrands);

    //                }
    //            }

    //            if (!_dbContext.ProductTypes.Any())
    //            {

    //                var productTypeData = File.ReadAllText("..\\Infrastructure\\Presistence Layer\\Data\\DataSeed\\types.json");
    //                //Convert json data to C# object by using Sel
    //                var productType = JsonSerializer.Deserialize<List<ProductType>>(productTypeData);
    //                if (productType is not null && productType.Any())
    //                {
    //                    _dbContext.ProductTypes.AddRange(productType);

    //                }
    //            }


    //            if (!_dbContext.Products.Any())
    //            {

    //                var productsData = File.ReadAllText("..\\Infrastructure\\Presistence Layer\\Data\\DataSeed\\Products.json");
    //                //Convert json data to C# object by using Sel
    //                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
    //                if (products is not null && products.Any())
    //                {
    //                    _dbContext.Products.AddRange(products);

    //                }
    //            }

    //            _dbContext.SaveChanges();
    //        }
    //        catch (Exception ex)
    //        {

    //        }
    //  } 
    #endregion


 
    public class DataSeed(StoreDbContext _dbContext,UserManager<ApplicationUser> _userManager,RoleManager<IdentityRole> _roleManager) : IDataSeed
    {
        #region IdentityDataSeeding
        public async Task IdentityDataSeeding()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_userManager.Users.Any())
                {
                    var user1 = new ApplicationUser()
                    {
                        Email = "MohamedEssam@gmail.com",
                        DisplayName = "Mohamed Essam",
                        PhoneNumber = "01011234329",
                        UserName = "MohamedEssam"
                    };
                    var user2 = new ApplicationUser()
                    {
                        Email = "MahmoudSalah@gmail.com",
                        DisplayName = "Mahmoud Salah",
                        PhoneNumber = "01201234329",
                        UserName = "MahmoudSalah"
                    };
                    await _userManager.CreateAsync(user1, "P@ssw0rd");
                    await _userManager.CreateAsync(user2, "P@ssw0rd");

                    await _userManager.AddToRoleAsync(user1, "SuperAdmin");
                    await _userManager.AddToRoleAsync(user2, "Admin");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Async Seeding
        //async => method that performs asynchronous method with another method in the same time without blocking the following code  and can be awaited to get the result when it is done
        public async Task SeedDataAsync()
        {
            try
            {

                //if there are any pending migrations, apply them on database
                if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())//await must be used here because the returned value is Task<IEnumerable<string>> then any() is called on it
                {
                    await _dbContext.Database.MigrateAsync();
                }
                //Must begin seeding with ProductBrands or ProductTypes because Products has foreign keys to them
                if (!_dbContext.ProductBrands.Any())
                {

                    var productBrandData = File.OpenRead("..\\Infrastructure\\Presistence Layer\\Data\\DataSeed\\brands.json");
                    //Convert json data to C# object by using Sel
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrandData);
                    if (ProductBrands is not null && ProductBrands.Any())
                    {
                        await _dbContext.ProductBrands.AddRangeAsync(ProductBrands);

                    }
                }

                if (!_dbContext.ProductTypes.Any())
                {

                    var productTypeData = File.OpenRead("..\\Infrastructure\\Presistence Layer\\Data\\DataSeed\\types.json");
                    //Convert json data to C# object by using Sel
                    var productType = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypeData);
                    if (productType is not null && productType.Any())
                    {
                        await _dbContext.ProductTypes.AddRangeAsync(productType);

                    }
                }


                if (!_dbContext.Products.Any())
                {

                    var productsData = File.OpenRead("..\\Infrastructure\\Presistence Layer\\Data\\DataSeed\\Products.json");
                    //Convert json data to C# object by using Sel
                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(productsData);
                    if (products is not null && products.Any())
                    {
                        await _dbContext.Products.AddRangeAsync(products);

                    }
                }

                if (!_dbContext.Set<DeliveryMethod>().Any())
                {

                    var deliveryMethod = File.OpenRead("..\\Infrastructure\\Presistence Layer\\Data\\DataSeed\\delivery.json");
                    //Convert json data to C# object by using Sel
                    var deliveryMethodObjs = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(deliveryMethod);
                    if (deliveryMethodObjs is not null && deliveryMethodObjs.Any())
                    {
                        await _dbContext.AddRangeAsync(deliveryMethodObjs);

                    }
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        } 
        #endregion

    
    }
}

