using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models.Mappings;
using QuizWebAPI.ServiceInstallers;
using System.Text;

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

/*builder.Services.AddAuthentication("Bearer")
    .AddIdentityServerAuthentication(options =>
    {
        options.Authority = "https://localhost:7007"; // Replace with your IdentityServer URL
        options.ApiSecret = "71947ab71fe401a6af4cf23daa21b337e7c08c52589b535f32099572e22ed094";
        options.RequireHttpsMetadata = true; // Set to true in production
        options.ApiName = "IdentityServer"; // Replace with your API resource name
    });*/

builder.Services.AddAuthorization();
builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("71947ab71fe401a6af4cf23daa21b337e7c08c52589b535f32099572e22ed094"));
        options.SaveToken = true;
        options.Authority = "https://localhost:7007";
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = key
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
