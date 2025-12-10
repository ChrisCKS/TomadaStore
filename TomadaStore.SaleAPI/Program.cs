using TomadaStore.ProductAPI.Data;
using TomadaStore.SaleAPI.Data;
using TomadaStore.SaleAPI.Repository;
using TomadaStore.SaleAPI.Repository.Interface;
using TomadaStore.SaleAPI.Service.v1;
using TomadaStore.SaleAPI.Service.v1.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));

builder.Services.AddSingleton<ConnectionDB>();

builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISaleService, SaleService>();

builder.Services.AddHttpClient("CustomerApi", client =>                                 /**/
{
    client.BaseAddress = new Uri("https://localhost:5001/api/v1/Customer/");        
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient("ProductApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:6001/api/v1/Product/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");                     /**/
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
