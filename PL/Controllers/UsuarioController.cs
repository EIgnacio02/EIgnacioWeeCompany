using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public UsuarioController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Form(int? IdUsuario)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Form(ML.Usuario usuario)
        {

            if (usuario.IdUsuario==0)
            {
                ML.Result result = new ML.Result();
                //ML.Result result = BL.Usuario.Add(usuario);
                try
                {
                    string urlAPI = _configuration["UrlAPI"];
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(urlAPI);
                        var postTask = client.PostAsJsonAsync("Add", usuario);
                        postTask.Wait();

                        var resultServicio = postTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            result.Correct = true;
                            result.Message = "Los datos se ingresaron correctamente";

                        }
                    }
                }
                catch (Exception ex)
                {

                }

                if (result.Correct)
                {
                    ViewBag.Message = result.Message;
                }
                else
                {
                    ViewBag.Mensaje = "No ha registrado el usuario";
                }
            }
            return PartialView("Modal");
        
        }
    }
}
