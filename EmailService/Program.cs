using EmailService;
using EmailService.Consumers;
using EmailService.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<BookingCreatedConsumer>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
