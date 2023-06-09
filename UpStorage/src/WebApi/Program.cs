﻿using Application;
using Application.Common.Interfaces;
using Domain.Settings;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Globalization;
using System.Text;
using WebApi.Filters;
using WebApi.Hubs;
using WebApi.Services;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();
    // Add services to the container.

    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    builder.Services.AddScoped<ICurrentUserService, CurrentUserManager>();

    builder.Services.AddControllers(opt =>
    {
        // opt.Filters.Add<ValidationFilter>();
        opt.Filters.Add<GlobalExceptionFilter>();
    });

    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

    //Apibehaviours
    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(setupAction =>
    {
        setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = $"Input your Bearer token in this format - Bearer token to access this API",
        });
        setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        }, new List<string>()
                    },
                });
    });

    // Add services to the container.
    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructure(builder.Configuration, builder.Environment.WebRootPath);

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
                    .AddJwtBearer(o =>
                    {
                        o.RequireHttpsMetadata = false;
                        o.SaveToken = false;
                        o.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero,
                            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                            ValidAudience = builder.Configuration["JwtSettings:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
                        };
                    });


    //Localization Files path
    builder.Services.AddLocalization(options =>

        options.ResourcesPath = "Resources"

    );

    builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
        var defaultCulture = new CultureInfo("en-GB");

        List<CultureInfo> cultureInfos = new List<CultureInfo>
    {
        defaultCulture , //en-GB
        new ("tr-TR")
    };

        options.SupportedCultures = cultureInfos;

        options.SupportedUICultures = cultureInfos;

        options.DefaultRequestCulture = new RequestCulture(defaultCulture);

        options.ApplyCurrentCultureToResponseHeaders = true;

    });

    builder.Services.AddSignalR();

    builder.Services.AddScoped<IAccountHubService, AccountHubManager>();

    builder.Services.AddMemoryCache();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            builder => builder
                .AllowAnyMethod()
                .AllowCredentials()
                .SetIsOriginAllowed((host) => true)
                .AllowAnyHeader());
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseStaticFiles();

    // Localization
    var requestLocalizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
    if (requestLocalizationOptions is not null) app.UseRequestLocalization(requestLocalizationOptions.Value);

    app.UseHttpsRedirection();

    app.UseCors("AllowAll");

    app.UseAuthentication();

    app.UseAuthorization();



    app.MapControllers();

    app.MapHub<ChatHub>("/Hubs/ChatHub");


   // throw new NullReferenceException("The package is not installed");

    app.Run();

  //  throw new AbandonedMutexException("jESUS cGRİST");

}
catch (Exception ex )
{

    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

