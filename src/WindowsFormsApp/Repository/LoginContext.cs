using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WindowsFormsApp.Model;
using WindowsFormsApp.Repository.Context;
using WindowsFormsApp.Repository.Interfaces;

namespace WindowsFormsApp.Repository
{
    public class LoginContext : ILoginContext
    {       
        private readonly ApiContext _apiContext;

        public LoginContext()
        {
            _apiContext = new ApiContext();
        }

        public void Dispose()
        {
            _apiContext?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<Usuario> EfetuarLogin(Usuario usuario)
        {
            var post = await _apiContext.Post("Login", ToApiModel(usuario));

            var retorno = await post.Content.ReadAsStringAsync();

            if (post.IsSuccessStatusCode)
            {
                usuario.DefinirRetorno(true);
                usuario.Token = JsonConvert.DeserializeObject<Login>(retorno).Token;
            }
            else
            {
                var falha = JsonConvert.DeserializeObject<Falha>(retorno);

                usuario.DefinirRetorno(falha.Success, falha.Erros.Select(c=>c.MensageErro).ToList());

                usuario.Token = null;
            }

            return usuario;
        }

        //Camada de anticorrupção
        private UserModel ToApiModel(Usuario usuario)
        {
            return new UserModel()
            {
                Username = usuario.Nomeusuario,
                Password = usuario.Password,                
            };
        }

        class UserModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        class Login
        {
            public bool Success { get; set; }
            public string Token { get; set; }
        }
    }
}
