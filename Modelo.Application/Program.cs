using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Modelo.Application.Models;
using Modelo.Domain.Entities;
using Modelo.Domain.Interfaces;
using Modelo.Infra.Data.Context;
using Modelo.Infra.Data.Repository;
using Modelo.Service.Services;
using System.Text;
using static Modelo.Infra.CrossCutting.Utils.Key;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<SqlContext>(options =>
options.UseSqlServer(
                  builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Configuration.AddJsonFile("appsettings.json", false, true);

builder.Services.AddCors(options => options.AddPolicy("Cors", options => options.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()));

builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
}).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin, User", policy => policy.RequireAssertion(context =>
    context.User.HasClaim("ApiSl", "User") || context.User.HasClaim("ApiSl", "Admin")));
    options.AddPolicy("Admin", policy => policy.RequireClaim("ApiSl", "Admin"));
});

var key = Encoding.ASCII.GetBytes(Settings.Secret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
builder.Services.AddScoped<IBaseService<User>, BaseService<User>>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddSingleton(new MapperConfiguration(config =>
{
    config.CreateMap<LoginUserModel, User>();
    config.CreateMap<UpdateUserModel, User>();
    config.CreateMap<CreateUserModel, User>();
    config.CreateMap<User, UserModel>();

}).CreateMapper());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddControllersWithViews()
               .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling =
                   Newtonsoft.Json.ReferenceLoopHandling.Ignore);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
