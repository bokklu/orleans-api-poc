using Orleans;
using Orleans.Api.Application;
using Orleans.Api.Application.Interfaces;
using Orleans.Api.Grains;
using Orleans.Api.Infra.Controllers;
using Orleans.Configuration;
using Orleans.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc()
    .AddApplicationPart(typeof(PlayerController).Assembly)
    .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);

builder.Services.AddSingleton<IPlayerService, PlayerService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseOrleans(siloBuilder =>
{
    siloBuilder.UseLocalhostClustering().Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "dev";
        options.ServiceId = "Orleans.Api";
    })
    .AddMemoryGrainStorageAsDefault();

    siloBuilder.ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(PlayerGrain).Assembly).WithReferences());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
