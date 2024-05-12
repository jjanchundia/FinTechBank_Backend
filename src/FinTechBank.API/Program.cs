using FinTechBank.Persistence;
using FinTechBank.Application.Dtos;
using FinTechBank.Application.UseCases.Clientes;
using FinTechBank.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Cliente.Services.RemoteInterface;
using Cliente.Services.RemoteServices;

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

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:SecretKey") ?? string.Empty));
// Configurar la autenticación con JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = key,
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de nuestra BD en PG
var connectionString = builder.Configuration.GetConnectionString("ConexionPG");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Registro de servicio MediatR
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

//Inyectamos los servicios a nuestra clase program.cs
builder.Services.AddScoped<IRequestHandler<ConsultarCliente.ConsultarClienteRequest, Result<List<ClienteDto>>>, ConsultarCliente.Handler>();
builder.Services.AddScoped<IRequestHandler<ConsultarClientePorId.ConsultarClientePorIdCommand, Result<ClienteDto>>, ConsultarClientePorId.Handler>();
builder.Services.AddScoped<IRequestHandler<AgregarCliente.AgregarClienteCommand, Result<ClienteDto>>, AgregarCliente.Handler>();
builder.Services.AddScoped<IRequestHandler<EliminarCliente.EliminarClienteCommand, Result<string>>, EliminarCliente.Handler>();
builder.Services.AddScoped<IRequestHandler<EditarCliente.EditarClienteCommand, Result<ClienteDto>>, EditarCliente.Handler>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddHttpClient("Usuario", config =>
{
    config.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Services:Usuario") ?? string.Empty);
});

// Configuración de Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microservicio Cliente API", Version = "v1" });

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

// Configurar la autorización
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireLoggedIn", policy =>
    {
        policy.RequireAuthenticatedUser();
    });

    options.AddPolicy("RequireAdminRole", policy =>
    {
        policy.RequireRole("Admin");
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
