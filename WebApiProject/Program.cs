using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Caching;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Resilience;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DB Context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerSaleService, CustomerSaleService>();
builder.Services.AddScoped<ICustomerSaleRepository, CustomerSaleRepository>();

//Add ratelimit, curcuit bracker and retry pattern, you need to pass DefaultClient in "_client = factory.CreateClient("DefaultClient");" for calling apis
builder.Services.AddHttpClient("DefaultClient")
    .AddPolicyHandler(ResiliencePolicies.GetRateLimitPolicy())
    .AddPolicyHandler(ResiliencePolicies.GetRetryPolicy())
    .AddPolicyHandler(ResiliencePolicies.GetCircuitBrackerPolicy());

//Add redis cache
builder.Services.AddScoped<ICachingService, RedisCacheService>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.InstanceName = "redis cache";
    options.Configuration = "localhost:6379";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseAuthorization();

app.MapControllers();

app.Run();
