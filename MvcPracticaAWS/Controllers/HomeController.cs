using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcPracticaAWS.Models;
using MvcPracticaAWS.Services;

namespace MvcPracticaAWS.Controllers {
    public class HomeController : Controller {

        public ServiceAWSDynamoDb serviceDynamo;

        public HomeController (ServiceAWSDynamoDb serviceD) {
            this.serviceDynamo = serviceD;
        }

        public async Task<IActionResult> Index() {
            return View(await this.serviceDynamo.GetUsuariosAsync());
        }

        public async Task<IActionResult> Details (int idUsuario) {
            return View(await this.serviceDynamo.GetUsuarioAsync(idUsuario));
        }

        [HttpPost]
        public async Task<IActionResult> Details (int idUsuario, String titulo, IFormFile imagen) {
            Usuario usuario = await this.serviceDynamo.GetUsuarioAsync(idUsuario);
            if(usuario.Fotos == null) {
                usuario.Fotos = new List<Foto>();
            }
            usuario.Fotos.Add(new Foto(titulo, imagen.FileName));
            await this.serviceDynamo.UpdateUsuarioAsync(usuario);
            return RedirectToAction("Details", new { idUsuario = idUsuario });
        }

        public IActionResult Create () {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create (Usuario usuario) {
            usuario.FechaAlta = DateTime.Now;
            await this.serviceDynamo.CreateUsuarioAsync(usuario);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update (int idUsuario) {
            return View(await this.serviceDynamo.GetUsuarioAsync(idUsuario));
        }

        [HttpPost]
        public async Task<IActionResult> Update (Usuario usuario) {
            Usuario user = await this.serviceDynamo.GetUsuarioAsync(usuario.IdUsuario);
            usuario.FechaAlta = user.FechaAlta;
            usuario.Fotos = user.Fotos;
            await this.serviceDynamo.UpdateUsuarioAsync(usuario);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete (int idUsuario) {
            await this.serviceDynamo.DeleteUsuarioAsync(idUsuario);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteFoto (String imagen, int idUsuario) {
            Usuario usuario = await this.serviceDynamo.GetUsuarioAsync(idUsuario);
            Foto foto = usuario.Fotos.SingleOrDefault(x => x.Imagen == imagen);
            usuario.Fotos.Remove(foto);
            await this.serviceDynamo.UpdateUsuarioAsync(usuario);
            return RedirectToAction("Details", new { idUsuario = idUsuario });
        }
    }
}
