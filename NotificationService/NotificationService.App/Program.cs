using NotificationService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();
builder.Services.AddSingleton<ConsumerService>();

var app = builder.Build();

app.Run();
