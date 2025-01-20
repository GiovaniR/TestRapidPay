using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RP.Application;
using RP.External;
using RP.Infrastructure;
using System.Data.Common;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddSingleton<DbConnection>(container => {
    var connection = new SqliteConnection("DataSource=:memory:");
    connection.Open();

    return connection;
});

services.AddDbContext<ApplicationDbContext>((container, options) => {
    var connection = container.GetRequiredService<DbConnection>();
    options.UseSqlite(connection);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Add security definition for JWT Bearer
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

//Request specific storage
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

//Save In-Memory for entire application
builder.Services.AddSingleton<IUniversalFeeExchangeService ,UniversalFeeExchangeService>(provider => new UniversalFeeExchangeService(TimeSpan.FromMinutes(60)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{ 
    var container = scope.ServiceProvider;
    var db = container.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
}

app.Run();

public partial class Program { }