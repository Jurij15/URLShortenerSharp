using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using URLShortenerSharp.Components;
using URLShortenerSharp.DB;
using URLShortenerSharp.Helpers;
using URLShortenerSharp.Models;
using URLShortenerSharp.ServiceDefaults.Extensions;

namespace URLShortenerSharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            //add db
            builder.Services.AddDbContext<AppDBContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DBConnection")));

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddFluentUIComponents();

            builder.Services.AddHttpClient();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            //map urls
            app.MapPost("/shorten", async (HttpContext httpContext, AppDBContext db, [FromQuery] string FullURL) =>
            {
                if (FullURL.IsNullOrEmptyOrWhiteSpace())
                {
                    return Results.BadRequest("FullUrl is null or empty");
                }
                if (!FullURL.IsUrl())
                {
                    return Results.BadRequest("FullUrl is not an url!");
                }

                URL url = new();

                url.Id = new();
                url.ShortAddress = StringHasher.ShortHash(FullURL);
                url.CreationDateTime = DateTime.Now;
                url.ClicksCount = 0;

                url.FullURL = FullURL;

                if (!db.URLs.Any(u => u.ShortAddress == url.ShortAddress))
                {
                    //only save urls if unique
                    await db.URLs.AddAsync(url);

                    await db.SaveChangesAsync();
                }

                return Results.Ok(url.ShortAddress);
            });

            app.MapGet("/r/{shortAddress}", async (HttpContext httpContext, AppDBContext db, string shortAddress) =>
            {
                if (shortAddress.IsNullOrEmptyOrWhiteSpace())
                {
                    return Results.BadRequest("shortAddress is null or empty");
                }

                URL? url = await db.URLs.Where(x => x.ShortAddress == shortAddress).FirstOrDefaultAsync();

                if (url is null)
                {
                    return Results.Redirect("/NotFound");
                }

                url.ClicksCount += 1;

                return Results.Redirect(url.FullURL);
            });

            app.Run();
        }
    }
}
