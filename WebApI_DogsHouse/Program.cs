using AspNetCoreRateLimit;
using BLL_DogsHouse.Interfaces;
using BLL_DogsHouse.Services;
using DAL_DogsHouse;
using DAL_DogsHouse.Interfaces;
using DAL_DogsHouse.Repositories;
using Microsoft.EntityFrameworkCore;
using WebApI_DogsHouse.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DogsHouseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("constring")));

builder.Services.AddAutoMapper();

builder.Services.AddTransient<IDogRepository, DogRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IDogService, DogService>();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddInMemoryRateLimiting();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseIpRateLimiting();

app.UseAuthorization();

app.MapControllers();

app.Run();
