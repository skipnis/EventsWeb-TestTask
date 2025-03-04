using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddControllers();  

var app = builder.Build();

app.MapControllers();

app.Run();