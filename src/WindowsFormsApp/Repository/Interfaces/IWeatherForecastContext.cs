using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WindowsFormsApp.Model;

namespace WindowsFormsApp.Repository.Interfaces
{
    public interface IWeatherForecastContext : IDisposable
    {
        Task<IEnumerable<Temperatura>> ObterTemepraturas(string token);
    }
}
