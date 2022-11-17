using Microsoft.AspNetCore.Identity;
using xyzpharmacy.Data.Enums;
using xyzpharmacy.Data.Static;
using xyzpharmacy.Models;

namespace xyzpharmacy.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var servicescope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = servicescope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();
                if (!context.Suppliers.Any())
                {
                    context.Suppliers.AddRange(new List<Supplier>()
                    {
                        new Supplier()
                        {
                            
                            SupplierName="MMNZ ltd",
                            SupplierAddress = "Salem, TamilNadu",
                            
                        },
                        new Supplier()
                        {
                            
                            SupplierName="MKS Ltd",
                            SupplierAddress="Bangalore, Karnataka"
                        }


                    });
                    context.SaveChanges();

                }
                if (!context.Products.Any())

                    context.Products.AddRange(new List<Product>()
                    {
                        new Product()
                        {
                            ProductName = "Dolo650",
                            ProductPrice=4,
                            CategoryName = Category.Tablet,
                            MedicinalUse = MedicinalUse.Fever,
                            ProductImage="~/Image/1.jpeg",
                            SupplierId = 1,
                            ExpiryDate = new DateTime(2025,10,27),
                            MedicineDesc = "Dolo 650 Tablet helps relieve pain and fever by blocking the" +
                            " release of certain chemical messengers responsible for fever and pain."
                        },
                        new Product()
                        {
                            ProductName = "Safi",
                            ProductPrice=4,
                            CategoryName = Category.Syrup,
                            MedicinalUse = MedicinalUse.Cold,
                            ProductImage="~/Image/2.jpeg",
                            SupplierId = 2,
                            ExpiryDate = new DateTime(2035,11,07),
                            MedicineDesc = "Safi syrup is a non toxic blood purifier. Unique ayurvedic " +
                            "formulation for acne, pimples, blemishes, skin boils, skin rashes and other skin infections."
                        }


                    }) ;
                context.SaveChanges();

                
                
            }
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@xyz.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Contact = 9344290177.ToString(),
                        Gender = Gender.Female,
                        Age = 22,
                        DateOfBirth = new DateTime(1995, 11, 07)
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@xyz.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Contact=8754047956.ToString(),
                        Gender=Gender.Male,
                        Age=25,
                        DateOfBirth=new DateTime(1995,10,27)
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
