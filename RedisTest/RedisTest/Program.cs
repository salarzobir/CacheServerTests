using Microsoft.Extensions.Caching.Distributed;
using RedisTest.Constants;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(option =>
{
    option.Configuration = "localhost:7001";
});

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/redisclienttest", async (IDistributedCache distributedCache) =>
{
    string key = Guid.NewGuid().ToString();
    await distributedCache.SetStringAsync(key, ClientConstants.TestString);
    await distributedCache.GetStringAsync(key);
    await distributedCache.RemoveAsync(key);
})
.WithName("RedisClientTest");

app.Run();