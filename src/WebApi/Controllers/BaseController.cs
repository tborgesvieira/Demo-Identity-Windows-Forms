using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
    public class BaseController : Controller
    {
        public List<Erro> NotificarErros()
        {
            var erros = new List<Erro>(ModelState.ErrorCount);

            foreach (var item in ModelState.Values.SelectMany(e => e.Errors))
            {
                erros.Add(new Erro()
                {
                    MensageErro = item.ErrorMessage
                });
            }

            return erros;
        }
    }
}