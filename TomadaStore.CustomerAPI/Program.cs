using TomadaStore.CustomerAPI.Data;
using TomadaStore.CustomerAPI.Repository;
using TomadaStore.CustomerAPI.Repository.Interface;
using TomadaStore.CustomerAPI.Service;
using TomadaStore.CustomerAPI.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<ConnectionDB>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
