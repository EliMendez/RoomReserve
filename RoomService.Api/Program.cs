using FluentValidation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RoomService.Application.Behaviors;
using RoomService.Application.Features.Rooms.CreateRoom;
using RoomService.Application.Interface;
using RoomService.Application.Interfaces.RoomRates;
using RoomService.Application.Interfaces.Rooms;
using RoomService.Application.Mapping;
using RoomService.Infrastructure.Data;
using RoomService.Infrastructure.Repository.RoomRates;
using RoomService.Infrastructure.Repository.Rooms;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// ============================
// Application
// ============================

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(
        typeof(CreateRoomCommand).Assembly);

    cfg.AddOpenBehavior(
        typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(
    typeof(CreateRoomCommand).Assembly);

builder.Services.AddAutoMapper(
    cfg => { },
    typeof(RoomProfile).Assembly);

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
builder.Services.AddScoped<IRoomCommandRepository, RoomCommandRepository>();
builder.Services.AddScoped<IRoomQueryRepository, RoomQueryRepository>();
builder.Services.AddScoped<IRoomRateCommandRepository, RoomRateCommandRepository>();
builder.Services.AddScoped<IRoomRateQueryRepository, RoomRateQueryRepository>();


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
