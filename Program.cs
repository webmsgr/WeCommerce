using Microsoft.EntityFrameworkCore;
using WeCommerce.Data;
using WeCommerce.Models;
using WeCommerce.Util;

namespace WeCommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // add stuff for logging in
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });


            

            // add database
            builder.Services.AddDbContext<WeCommerceContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseSession();
            app.UseRouting();


            app.Use(async (context, next) =>
            {

                if (context.Session.GetInt32("User") != null && context.Request.Path != "/Users/ForceChangePassword")
                {
                    // we're logged in, somehow get a context?
                    // stackoverflow dont fail me now! https://stackoverflow.com/a/74071461
                    var dbContext = context.RequestServices.GetRequiredService<WeCommerceContext>();
                    if (dbContext != null)
                    {
                        var user = await dbContext.Users
                            .FindAsync(context.Session.GetInt32("User"));
                        if (user == null)
                        {
                            // they were probably logged in as a deleted user. log em out
                            context.Session.Remove("User");
                        }
                        else
                        {
                            if (user.ForceChangePassword)
                            {
                                context.Response.Redirect($"/Users/ForceChangePassword"); // bad but idk how else
                            }
                        }
                    }
                }
                await next.Invoke();
            });

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            using (var scope = app.Services.CreateScope())
            {
                // create our default admin user if it doesn't already exist
                // this might cause concurrency issues with scaling but who in the world will be scaling this
                var dbContext = scope.ServiceProvider.GetRequiredService<WeCommerceContext>();
                if (dbContext.Users.FirstOrDefault(u => u.Username == "admin") == null)
                {
                    // create the default admin user
                    dbContext.Users.Add(new User
                    {
                        Email = "admin@localhost",
                        Username = "admin",
                        PasswordHash = Crypto.HashPassword("password"), // very bad but you're forced to change it after
                        ForceChangePassword = true,
                        IsAdmin = true
                    });
                    dbContext.SaveChanges();
                }
            }


            app.Run();
        }
    }
}
