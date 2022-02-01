using GrpcCacheClient.Constants;
using GrpcCacheService;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGrpcClient<Cache.CacheClient>(options =>
{
    options.Address = new Uri(ClientConstants.GrpcServiceAddress);
});

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/grpcclienttest", async (Cache.CacheClient cacheClient) =>
{
    string key = Guid.NewGuid().ToString();
    await cacheClient.SetAsync(new SetRequest { Key = key, Value = ClientConstants.TestString });
    await cacheClient.GetAsync(new GetRequest { Key = key });
    await cacheClient.RemoveAsync(new RemoveRequest { Key = key });
})
.WithName("GrpcClientTest");

app.Run();