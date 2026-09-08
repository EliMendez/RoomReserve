using BookingService.Application.Behaviors;
using BookingService.Application.Features.Bookings.CheckAvailability;
using BookingService.Application.Features.Bookings.CreateBooking;
using BookingService.Application.Interfaces;
using BookingService.Application.Interfaces.Publisher;
using BookingService.Application.Interfaces.Repository;
using BookingService.Application.Interfaces.Service;
using BookingService.Application.Interfaces.ServiceClient;
using BookingService.Application.Mapping;
using BookingService.Application.Service;
using BookingService.Infrastructure.Clients;
using BookingService.Infrastructure.Data;
using BookingService.Infrastructure.Messaging;
using BookingService.Infrastructure.Repository;
using FluentValidation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// ============================
// Application
// ============================

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(
        typeof(CreateBookingCommand).Assembly);

    cfg.AddOpenBehavior(
        typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(
    typeof(CreateBookingCommand).Assembly);

builder.Services.AddAutoMapper(
    cfg => { },
    typeof(BookingProfile).Assembly
);

// ============================
// Infrastructure
// ============================

// EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Dapper
builder.Services.AddScoped<IDbConnection>(options =>
    new SqlConnection(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repositories
builder.Services.AddScoped<IBookingCommandRepository, BookingCommandRepository>();
builder.Services.AddScoped<IBookingQueryRepository, BookingQueryRepository>();
builder.Services.AddScoped<IAvailabilityBookingService, AvailabilityBookingService>();

// RabbitMQ
builder.Services.AddScoped<IMessagePublisher, RabbitMqPublisher>();

// RoomService Client
builder.Services.AddHttpClient<IRoomServiceClient, RoomServiceClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5273/");
});

builder.Services.AddHttpClient<IRoomRateServiceClient, RoomRateServiceClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5273/");
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
