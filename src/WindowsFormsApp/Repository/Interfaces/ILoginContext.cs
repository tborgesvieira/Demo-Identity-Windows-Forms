using System;
using System.Threading.Tasks;
using WindowsFormsApp.Model;

namespace WindowsFormsApp.Repository.Interfaces
{
    public interface ILoginContext : IDisposable
    {
        Task<Usuario> EfetuarLogin(Usuario usuario);
    }
}
