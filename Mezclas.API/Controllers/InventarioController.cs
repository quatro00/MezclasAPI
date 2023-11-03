using Mezclas.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http.Headers;

namespace Mezclas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly ICatalogoArticuloRepository catalogoArticuloRepository;

        public InventarioController(ICatalogoArticuloRepository catalogoArticuloRepository)
        {
            this.catalogoArticuloRepository = catalogoArticuloRepository;
        }

        [HttpPost("ActualizaCatalogo"), DisableRequestSizeLimit]
        [Authorize("Mezclas-Admin")]
        public async Task<IActionResult> ActualizaCatalogo()
        {
            //var response = await categoriaRepository.CreateAsync(request);
            //var ticketArchivoDtos = new List<string>();
            var formCollection = await Request.ReadFormAsync();
            var files = formCollection.Files;
            //var Name = "";
            //var Extension = "";
            //var folderName = Path.Combine("Resources");
            //var tempFolderName = Path.Combine("Temp");
           // var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), tempFolderName);


            if (files.Any(f => f.Length == 0))
            {
                return BadRequest();
            }

            var x = 1;
            foreach (var file in files)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fileNameSplit = fileName.Split(".");

                //si no tiene extension
                if (fileNameSplit.Length == 1)
                {
                    ModelState.AddModelError("error", "El archivo no tiene extensión valida.");
                    return ValidationProblem(ModelState);
                }

                if (fileNameSplit[fileNameSplit.Length - 1].ToUpper() != "csv".ToUpper() )
                {
                    ModelState.AddModelError("error", "Solo se aceptan archivos CSV");
                    return ValidationProblem(ModelState);
                }

                var response = await catalogoArticuloRepository.ActualizaCatalogo(file.OpenReadStream());

                if (!response.response)
                {
                    ModelState.AddModelError("error", response.message);
                    return ValidationProblem(ModelState);
                }

                return Ok(response.result);
            }
            return Ok();
        }
    }
}
