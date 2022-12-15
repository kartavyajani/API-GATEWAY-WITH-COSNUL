using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Consul;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddOcelot(builder.Configuration).AddConsul();
// builder.Services.AddConsulConfig(builder.Configuration);
builder.Services.AddOcelot().AddConsul();

builder.WebHost.ConfigureKestrel(options=>options.Listen(System.Net.IPAddress.Parse("192.168.11.62"),9050));
  builder.Services.AddServiceDiscovery(opt => opt.UseConsul());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseOcelot().Wait();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
