using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using vet_backend.Context;
using vet_backend.Models;
using vet_backend.Helpers;

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

            // Add context
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion01"));
            })
                .AddIdentityCore<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            ;


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

            //Roles
            var scope = app.Services.CreateScope();
            var administrarusuarios = new AministrarUsuarios(scope);
            administrarusuarios.Crear();


            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors("AllowWebApp");

            app.MapControllers();


            Console.WriteLine("Programa corriendo");
            //var newuser = new User { UserName = "lolo", Email = "lolo@gmail.com", Nombre = "Lolo", Apellido = "Gomez", Password = "pass" };
            //Console.WriteLine($"usuario: {newuser}");
            //var usuario = administrarusuarios._userManager.FindByNameAsync("Agustin").GetAwaiter().GetResult();
            //Console.WriteLine($"usuario: {usuario}");
            //var roles = administrarusuarios._userManager.GetRolesAsync(usuario).GetAwaiter().GetResult();
            //foreach (var role in roles)
            //{
            //    Console.WriteLine($"role: {role}");
            //}

            //Console.WriteLine($"Administrador: {administrarusuarios._userManager.IsInRoleAsync(usuario, "Administrador").GetAwaiter().GetResult()}");
            //Console.WriteLine($"Secretario: {administrarusuarios._userManager.IsInRoleAsync(usuario, "Secretario").GetAwaiter().GetResult()}");


            var usuario2 = administrarusuarios._userManager.FindByNameAsync("Benjamin").GetAwaiter().GetResult();
            if (usuario2 != null)
            {
                Console.WriteLine("Usuario Benjamin existe");

            }
            else
            {
                Console.WriteLine("NOOOO existe Benjamin");

            }
            Console.WriteLine($"usuario2: {usuario2}");

            app.Run();


        }
    }
}