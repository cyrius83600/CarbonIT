using CarteTresors.BO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;
using System.IO.Pipelines;
using System.Reflection.PortableExecutable;
using System.Text;
using static System.Net.WebRequestMethods;

namespace CarteTresors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost]
        [Route("UploadFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            //var result = await WriteFile(file);
            //var filename = file.FileName;
            //var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");
            //if (!Directory.Exists(filepath))
            //{
            //    Directory.CreateDirectory(filepath);
            //}
            //var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);
            //using (var stream = new FileStream(exactpath, FileMode.Create))
            //{
            //    await file.CopyToAsync(stream);
            //}
            //var lignes = System.IO.File.ReadAllLines(exactpath, Encoding.UTF8).ToList();
            var carte = new Carte().LireCarte(await ReadFile(file));

            return Ok("Ca marche");
        }

        private async Task<List<string>?> ReadFile(IFormFile file)
        {
            var filename = file.FileName;
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);
            using (var stream = new FileStream(exactpath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return System.IO.File.ReadAllLines(exactpath, Encoding.UTF8).ToList();
        }


        [HttpGet]
        [Route("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string filename)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contenttype, Path.GetFileName(filepath));
        }
    }
}
