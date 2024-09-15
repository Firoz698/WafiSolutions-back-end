using Microsoft.EntityFrameworkCore;
using WafiSolutions.infracturcture;
using WafiSolutions.service.Interface;
using WafiSolutions.service.Manager;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefultConnection"));
    option.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

});
// Add services to the container.

builder.Services.AddScoped<IEmployeeInterface, EmployeeManager>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("Corsploicy", builder =>
{
    builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Corsploicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
