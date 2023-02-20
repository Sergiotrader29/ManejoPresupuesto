using ManejoPresupuesto.Models;
using ManejoPresupuesto.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace ManejoPresupuesto.Controllers
{
    public class TiposCuentasController: Controller // Ctrl . para importar AspNetcore
    {
        private readonly IrepositoriosTipoCuentas repositoriosTipoCuentas;

        public TiposCuentasController(IrepositoriosTipoCuentas repositoriosTipoCuentas)
        {
            this.repositoriosTipoCuentas = repositoriosTipoCuentas;
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(TipoCuentas tipoCuentas)
        {
            if (!ModelState.IsValid) // si el modelo es ! no valido 
            {
                return View(tipoCuentas); //Que retorne de nuevo, y que no borre los datos
            }

            tipoCuentas.UsuarioId = 1;

            var yaExisteTipoCuenta = await repositoriosTipoCuentas.Existe(tipoCuentas.Nombre, tipoCuentas.UsuarioId);

            if (yaExisteTipoCuenta)
            {
                ModelState.AddModelError(nameof(tipoCuentas.Nombre), $"El nombre{tipoCuentas.Nombre} ya existe. ");
                return View(tipoCuentas);
            }

           
            await repositoriosTipoCuentas.Crear(tipoCuentas);

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> VerificarExisteTipoCuenta(string nombre)
        {
            var usuarioId = 1;
            var yaExisteTipoCuenta = await repositoriosTipoCuentas.Existe(nombre, usuarioId);

            if (yaExisteTipoCuenta)
            {
                return Json($"El nombre {nombre} ya existe");
            }

            return Json(true);
        }
    }
}
