using ManejoPresupuesto.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManejoPresupuesto.Controllers
{
    public class TiposCuentasController: Controller // Ctrl . para importar AspNetcore
    {
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(TipoCuentas tipoCuentas)
        {
            if (!ModelState.IsValid) // si el modelo es ! no valido 
            {
                return View(tipoCuentas); //Que retorne de nuevo, y que no borre los datos
            }


            return View();
        }
    }
}
