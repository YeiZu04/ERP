using ERP_API.Models;
using ERP_API.Services;
using ERP_API.Services.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Add services to the container.
// Registro de servicios
builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
builder.Services.AddScoped<BearerCode>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<PasswordHash>();
builder.Services.AddScoped<SendEmail>();
builder.Services.AddScoped<RandomGenerator>();
builder.Services.AddScoped<Api_Response>();
builder.Services.AddScoped<CompanyService>();
builder.Services.AddScoped<PermissionService>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<PersonService>();

// Configuración de Swagger para JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ERP", Version = "v1" });

    // Configuración de JWT en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Introduce 'Bearer' [space] y luego tu token en el campo de texto.\r\n\r\nEjemplo: \"Bearer abcdefgh12345678\""
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
            new string[] {}
        }
    });
});

// Configuración de JWT
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Configuración de DbContext
builder.Services.AddDbContext<ERPDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Usar CORS
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Usar middleware de manejo de errores personalizado
app.UseMiddleware<ErrorHandlerMiddleware>();

// Middleware de autenticación y autorización
app.UseAuthentication(); // Esto valida el JWT y autentica al usuario
app.UseAuthorization();  // Esto asegura que solo se acceda a los endpoints con la autorización correcta

app.MapControllers();

app.Run();
