using ManejoPresupuesto.Models;
using ManejoPresupuesto.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace ManejoPresupuesto.Controllers
{
    public class TiposCuentasController: Controller // Ctrl . para importar AspNetcore
    {
        private readonly IrepositoriosTipoCuentas repositoriosTipoCuentas;
        private readonly IserviciosUsuarios serviciosUsuarios;

        public TiposCuentasController(IrepositoriosTipoCuentas repositoriosTipoCuentas,
            IserviciosUsuarios serviciosUsuarios)
        {
            this.repositoriosTipoCuentas = repositoriosTipoCuentas;
            this.serviciosUsuarios = serviciosUsuarios;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();
            var tipoCuentas = await repositoriosTipoCuentas.Obtener(usuarioId);
            return View(tipoCuentas);
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

            tipoCuentas.UsuarioId = serviciosUsuarios.ObtenerUsuarioId();

            var yaExisteTipoCuenta = await repositoriosTipoCuentas.Existe(tipoCuentas.Nombre, tipoCuentas.UsuarioId);

            if (yaExisteTipoCuenta)
            {
                ModelState.AddModelError(nameof(tipoCuentas.Nombre), $"El nombre{tipoCuentas.Nombre} ya existe. ");
                return View(tipoCuentas);
            }


            await repositoriosTipoCuentas.Crear(tipoCuentas);

            return RedirectToAction("Index");
        }
            [HttpGet]
        public async Task<IActionResult> VerificarExisteTipoCuenta(string nombre)
        {
            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();
            var yaExisteTipoCuenta = await repositoriosTipoCuentas.Existe(nombre, usuarioId);

            if (yaExisteTipoCuenta)
            {
                return Json($"El nombre {nombre} ya existe");
            }

            return Json(true);
        }


    }
}
