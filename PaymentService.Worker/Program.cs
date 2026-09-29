using Microsoft.EntityFrameworkCore;
using PaymentService.Application.Interfaces;
using PaymentService.Application.Interfaces.Publisher;
using PaymentService.Application.Services;
using PaymentService.Infrastructure.Data;
using PaymentService.Infrastructure.Messaging;
using PaymentService.Infrastructure.Repository;
using PaymentService.Worker;
using PaymentService.Worker.Consumers;

var builder = Host.CreateApplicationBuilder(args);

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Application
builder.Services.AddScoped<IPaymentProcessor, PaymentProcessor>();
builder.Services.AddScoped<IRabbitMqPublisher, RabbitMqPublisher>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

// Consumer
builder.Services.AddScoped<BookingCreatedConsumer>();

// Worker
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
