using RestCacheClient.Constants;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient(ClientConstants.CacheServiceHttpClientName, c => c.BaseAddress = new Uri(ClientConstants.CacheServiceBaseAddress));

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/restclienttest", async (IHttpClientFactory httpClientFactory) =>
{
    HttpClient client = httpClientFactory.CreateClient(ClientConstants.CacheServiceHttpClientName);

    string key = Guid.NewGuid().ToString();
    await client.PostAsJsonAsync(ClientConstants.CacheServiceRequestUri, new KeyValue(key, ClientConstants.TestString));
    await client.GetStringAsync($"{ClientConstants.CacheServiceRequestUri}/{key}");
    await client.DeleteAsync($"{ClientConstants.CacheServiceRequestUri}/{key}");
})
.WithName("RestClientTest");

app.Run();

internal record KeyValue(string Key, string Value);