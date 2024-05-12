using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using FinTechBank.Usuario.Application.UseCases.Usuario;
using FinTechBank.Usuario.Dtos;
using FinTechBank.Usuario.Domain;
using FinTechBank.Usuario.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Agregamos politicas CORS para uso de endpoint localmente
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de nuestra BD en SQL
var connectionString = builder.Configuration.GetConnectionString("ConexionSQL");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Registro de servicio MediatR
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

//Inyectamos los servicios a nuestra clase program.cs
builder.Services.AddScoped<IRequestHandler<CrearUsuario.CrearUsuarioCommand, Result<UsuarioDto>>, CrearUsuario.Handler>();

// Configuración de Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microservicio Usuario API", Version = "v1" });

    // Configuración de la autenticación en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        //Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        //Name = "Authorization",
        //In = ParameterLocation.Header,
        //Type = SecuritySchemeType.ApiKey
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                //new string[] { }
                Array.Empty<string>()
            }
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplica la política CORS a todas las solicitudes
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
