using Auth0.AspNetCore.Authentication;
using DataAccess.Context;
using DataAccess.Services;
using Drink_Book_App.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Default")
	?? throw new NullReferenceException("No connection String Found");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAuth0WebAppAuthentication(
	options => {

		options.Domain = builder.Configuration["Auth0:Domain"];
		options.ClientId = builder.Configuration["Auth0:ClientId"];

    });
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<DashBoardDataTools>();
builder.Services.AddTransient<DrinkRepository>();
builder.Services.AddDbContextFactory<DrinkDBContext>(
	(DbContextOptionsBuilder options) =>
	options.UseNpgsql(connectionString).AddInterceptors(new SoftDeleteInterceptor()).AddInterceptors(new LoggingInterceptor())
    );

  

builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
	//app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	//app.UseHsts();
//}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
