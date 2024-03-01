using Microsoft.AspNetCore.Identity;
using System.Runtime.Intrinsics.X86;
using vet_backend.Models;


namespace vet_backend.Helpers
{
    public class DatosIniciales
    {
        IServiceScope _scope;
        public UserManager<User> _userManager;
        public RoleManager<IdentityRole> _roleManager;
        public DatosIniciales(IServiceScope scope) {
            _scope = scope;
            _userManager = _scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            _roleManager = _scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        }

        public void Crear() { 

            var roles = new[] { "Administrador", "Secretario" };



            foreach (var role in roles)
            {
                if (!_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                }
            }



            var usuarios = new[] { "leandro", "rosa", "felipe", "romina" };


            string password;
            User user;
            User usuarioexiste;

            foreach (var usuario in usuarios)
            {
                password = usuario + "Xx123!";
                user = new User { UserName = usuario, Email = usuario + "@gmail.com", Nombre = usuario, Apellido = "Garcia", Password = password };
                usuarioexiste = _userManager.FindByNameAsync(usuario).GetAwaiter().GetResult();
                if (usuarioexiste == null)
                {
                    _userManager.CreateAsync(user, password).GetAwaiter().GetResult();
                    //_userManager.AddToRoleAsync(user, "Veterinario").GetAwaiter().GetResult();
                }

            }

            var usuarios_admin = new[] { "leandro", "rosa"};

            foreach (var usuario in usuarios_admin)
            {
                usuarioexiste = _userManager.FindByNameAsync(usuario).GetAwaiter().GetResult();
                if (usuarioexiste != null)
                {
                    _userManager.AddToRoleAsync(usuarioexiste, "Administrador").GetAwaiter().GetResult();
                }

            }

            var usuarios_secret = new[] { "felipe", "romina" };

            foreach (var usuario in usuarios_secret)
            {
                usuarioexiste = _userManager.FindByNameAsync(usuario).GetAwaiter().GetResult();
                if (usuarioexiste != null)
                {
                    _userManager.AddToRoleAsync(usuarioexiste, "Secretario").GetAwaiter().GetResult();
                }

            }

        }
    }
}
