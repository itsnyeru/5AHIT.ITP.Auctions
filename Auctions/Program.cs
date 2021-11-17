using Auctions.Services;
using Domain.Repository;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<AuctionDbContext>(
    options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new System.Version("8.0.25")))
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors()
);

builder.Services.AddScoped<IUserRepository, UserRepository>();

//builder.Services.AddScoped<Authentication>();
//builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<Authentication>());

var app = builder.Build();

using (var context = app.Services.CreateScope().ServiceProvider.GetService<AuctionDbContext>()) {
    if (context == null) return;
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseStaticFiles();
app.UseCookiePolicy();
app.UseAuthentication();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
