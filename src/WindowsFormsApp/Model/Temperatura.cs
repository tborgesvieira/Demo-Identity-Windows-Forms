using System;

namespace WindowsFormsApp.Model
{
    public class Temperatura : Entity
    {
        public DateTime Date { get; set; }
        public double TemperaturaC { get; set; }
        public double TemperaturaF { get; set; }
        public string Sumarry { get; set; }

    }
}
