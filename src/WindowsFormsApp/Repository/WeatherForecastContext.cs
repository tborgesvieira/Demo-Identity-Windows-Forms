using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp.Model;
using WindowsFormsApp.Repository.Context;
using WindowsFormsApp.Repository.Interfaces;

namespace WindowsFormsApp.Repository
{
    public class WeatherForecastContext : IWeatherForecastContext
    {
        private readonly ApiContext _apiContext;

        public WeatherForecastContext()
        {
            _apiContext = new ApiContext();
        }

        public void Dispose()
        {
            _apiContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<Temperatura>> ObterTemepraturas(string token)
        {
            List<Temperatura> temperaturas = null;

            var get = await _apiContext.Get("WeatherForecast", token);

            if (get.IsSuccessStatusCode)
            {
                var retorno = await get.Content.ReadAsStringAsync();

                temperaturas = ToListViewModel(JsonConvert.DeserializeObject<Retorno>(retorno));
            }
            else
            {
                if (get.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Acesso não autorizado");
                }
            }

            return temperaturas;
        }

        //Camada de anticorrupção
        private List<Temperatura> ToListViewModel(Retorno retorno)
        {
            if (retorno.Success)
            {
                var temperaturas = new List<Temperatura>(retorno.Lista.Count);

                retorno.Lista.ForEach(c =>
                {
                    temperaturas.Add(new Temperatura()
                    {
                        Date = c.Date,
                        TemperaturaC = c.TemperatureC,
                        TemperaturaF = c.TemperatureF,
                        Sumarry = c.Summary
                    });
                });

                return temperaturas;
            }
            else
            {
                return null;
            }
        }

        class WeatherForecast
        {
            public DateTime Date { get; set; }

            public int TemperatureC { get; set; }

            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

            public string Summary { get; set; }
        }

        class Retorno
        {
            public bool Success { get; set; }
            public List<WeatherForecast> Lista { get; set; }
        }
    }
}
