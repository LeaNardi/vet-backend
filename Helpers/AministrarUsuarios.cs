using Microsoft.AspNetCore.Identity;
using vet_backend.Models;


namespace vet_backend.Helpers
{
    public class AministrarUsuarios
    {
        IServiceScope _scope;
        public UserManager<User> _userManager;
        public RoleManager<IdentityRole> _roleManager;
        public AministrarUsuarios(IServiceScope scope) {
            _scope = scope;
            _userManager = _scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            _roleManager = _scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        }

        public void Crear() { 

            var roles = new[] { "Administrador", "Secretario", "Veterinario", "Usuario" };



            foreach (var role in roles)
            {
                if (!_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                }
            }

            //IdentityResult result;

            //var password = "Pablo123!";
            //var user2 = new User { UserName = "pablo", Email = "pablo@gmail.com", Nombre = "Pablo", Apellido = "Gomez", Password = password };

            //if (_userManager.FindByNameAsync("pablo").GetAwaiter().GetResult() != null)
            //{
            //    result = _userManager.CreateAsync(user2, password).GetAwaiter().GetResult();

            //}

            //_userManager.AddToRoleAsync(user2, "Secretario").GetAwaiter().GetResult();
            //_userManager.AddToRoleAsync(user2, "Administrador").GetAwaiter().GetResult();




            //password = "Agustin123!";
            //var user3 = new User { UserName = "Agustin", Email = "Agustin@gmail.com", Nombre = "Agustin", Apellido = "Jackson", Password = password };

            //Console.WriteLine("Agustin:");
            //System.Diagnostics.Debug.WriteLine("Agustin:");
            //Console.WriteLine(_userManager.FindByNameAsync("Agustin").GetAwaiter().GetResult());
            //System.Diagnostics.Debug.WriteLine(_userManager.FindByNameAsync("Agustin").GetAwaiter().GetResult());

            //if (_userManager.FindByNameAsync("Agustin").GetAwaiter().GetResult() != null)
            //{
            //    result = _userManager.CreateAsync(user3, password).GetAwaiter().GetResult();

            //}
            //else
            //{
            //    result = _userManager.CreateAsync(user3, password).GetAwaiter().GetResult();
            //}

            //_userManager.AddToRoleAsync(user3, "Veterinario").GetAwaiter().GetResult();
            //_userManager.AddToRoleAsync(user3, "Administrador").GetAwaiter().GetResult();


        }
    }
}
