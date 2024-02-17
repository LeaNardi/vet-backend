using Microsoft.EntityFrameworkCore;
using vet_backend.Context;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace vet_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(setupAction =>
                    {
                        setupAction.AddSecurityDefinition("VeterinariaBearerAuth", new OpenApiSecurityScheme() //Esto va a permitir usar swagger con el token.
                        {
                            Type = SecuritySchemeType.Http,
                            Scheme = "Bearer",
                            Description = "Acá pegar el token generado al loguearse."
                        });

                        setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "VeterinariaBearerAuth" } //Tiene que coincidir con el id seteado arriba en la definición
                        }, new List<string>() }
            });
                    });

            // Add authentication
            builder.Services.AddAuthentication("Bearer") 
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Authentication:Issuer"],
                        ValidAudience = builder.Configuration["Authentication:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
                    };
                }
            );

            // Add context
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion01"));
            });

            

            // Cors
            builder.Services.AddCors(options => options.AddPolicy("AllowWebApp",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
                ));



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

            app.UseCors("AllowWebApp");

            app.MapControllers();

            app.Run();
        }
    }
}