using NotificationService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(builder => builder.AddConsole());
builder.Services.AddSingleton<ConsumerService>();

var app = builder.Build();

//Instantiate consumer
app.Services.GetService<ConsumerService>();
app.Run();
