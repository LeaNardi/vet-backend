using Microsoft.EntityFrameworkCore;
using vet_backend.Context;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Identity;

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
                            Description = "Ac� pegar el token generado al loguearse."
                        });

                        setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "VeterinariaBearerAuth" } //Tiene que coincidir con el id seteado arriba en la definici�n
                        }, new List<string>() }
            });
                    });

            // Add context
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion01"));
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




            // Cors
            builder.Services.AddCors(options => options.AddPolicy("AllowWebApp",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
                ));


            ////Roles
            //builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddUserStore<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();





            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            //using (var scope = app.Services.CreateScope())
            //{
            //    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //    var roles = new[] { "Admin", "Manager", "Member" };

            //    foreach (var role in roles)
            //    {
            //        if (!roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
            //        {
            //            roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
            //        }
            //    }
            //}



            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors("AllowWebApp");

            app.MapControllers();

            app.Run();
        }
    }
}