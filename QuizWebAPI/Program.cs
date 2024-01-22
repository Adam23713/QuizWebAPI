using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models.Mappings;
using QuizWebAPI.ServiceInstallers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Get service installers from the ServiceInstallers directory
var servicesInstallers = typeof(Program).Assembly.ExportedTypes.Where(x =>
typeof(IServiceInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).
Select(Activator.CreateInstance).Cast<IServiceInstaller>().ToList();

//Add services
servicesInstallers.ForEach(installer => installer.InstallServices(builder.Services, builder.Configuration));


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
