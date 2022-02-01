using Microsoft.Extensions.Caching.Memory;
using RestCacheService.Constants;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/cache", (IMemoryCache memoryCache, KeyValue keyValue) =>
{
    memoryCache.Set(keyValue.Key, keyValue.Value, ClientConstants.MemoryCacheEntryOptions);
})
.WithName("SetCache");

app.MapGet("/cache/{key}", (IMemoryCache memoryCache, string key) =>
{
    return memoryCache.Get<string>(key);
})
.WithName("GetCache");

app.MapDelete("/cache/{key}", (IMemoryCache memoryCache, string key) =>
{
    memoryCache.Remove(key);
})
.WithName("RemoveCache");

app.Run();

internal record KeyValue(string Key, string Value);