using Microsoft.EntityFrameworkCore;
using openAPI.Controllers;
using openAPI.Models;
using openAPI.Services;
using openAPI.Staging;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HKContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("HK")));
builder.Services.AddScoped<EmbeddingController>();
builder.Services.AddScoped<TurboController>();
builder.Services.AddScoped<AnswerService>();
builder.Services.AddCors(options => options.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
}));
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAll");
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
