using System.Collections.Immutable;
using AlatorX.Server.Management.Data.DbContexts;
using AlatorX.Server.Management.Extensions;
using AlatorX.Server.Management.Middlewares;
using AlatorX.Server.Management.Service.Mappers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddCustomServices();
builder.Services.AddJwtService(builder.Configuration);
builder.Services.ConfigureCors();
builder.Services.ConfigureSwagger();

var app = builder.Build();

app.InitAccessor();
app.InitEnvironment();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors("AllowAll");
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
