﻿using Bhasha.Areas.Identity;
using Bhasha.Domain.Interfaces;
using Bhasha.Identity;
using Bhasha.Identity.Extensions;
using Bhasha.Infrastructure.EntityFramework;
using Bhasha.Services;
using Bhasha.Shared.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

try
{
    var builder = WebApplication
        .CreateBuilder(args);

    builder
        .Configuration
        .AddJsonFile("config/appsettings.json", optional: true, reloadOnChange: true);
    
    ////////////////////
    // DB & Identity
    ////////////////////
    
    var services = builder.Services;
    var connectionString = builder.Configuration.GetConnectionString("postgres");

    services.AddAuthentication();
    services.AddAuthorizationBuilder();
    services.AddDbContext<AppDbContext>(db => db
        .EnableSensitiveDataLogging()
        .UseNpgsql(connectionString)); 

    services.AddScoped<IChapterRepository, EntityFrameworkChapterRepository>();
    services.AddScoped<IExpressionRepository, EntityFrameworkExpressionRepository>();
    services.AddScoped<IProfileRepository, EntityFrameworkProfileRepository>();
    services.AddScoped<ITranslationRepository, EntityFrameworkTranslationRepository>();
    
    services
        .AddIdentity<AppUser, AppRole>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddApiEndpoints()
        .AddDefaultTokenProviders()
        .AddDefaultUI();
    
    services
        .AddSingleton<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<AppUser>>();

    ////////////////////
    // Blazor
    ////////////////////

    services.Configure<RazorPagesOptions>(options => options.RootDirectory = "/Web/Pages");
    services.AddRazorPages();
    services.AddServerSideBlazor();
    services.AddMudServices();

    ////////////////////
    // Application
    ////////////////////

    services.AddScoped<IValidator, Validator>();
    services.AddScoped<IPageFactory, PageFactory>();
    services.AddScoped<IMultipleChoicePageFactory, MultipleChoicePageFactory>();
    services.AddScoped<IChapterSummariesProvider, ChapterSummariesProvider>();
    services.AddScoped<IChapterProvider, ChapterProvider>();
    services.AddScoped<IAuthoringService, AuthoringService>();
    services.AddScoped<IStudyingService, StudyingService>();

    var app = builder.Build();

    ////////////////////
    // HTTP pipeline
    ////////////////////

    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();
    
    var identitySettings = IdentitySettings.From(app.Configuration);
    var scope = ((IApplicationBuilder)app).ApplicationServices.CreateScope();
    var serviceProvider = scope.ServiceProvider;

    await serviceProvider.UseIdentitySettings(identitySettings);

    app.MapIdentityApi<AppUser>();
    app.MapControllers();
    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");

    app.Run();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

