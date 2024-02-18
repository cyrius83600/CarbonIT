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
            var entree = await ReadFile(file);
            var listeSortie = await GetLignes(entree);
            return await EcrireFichier(listeSortie);
        }

        private async Task<List<string>?> GetLignes(List<string>? entree)
        {
            var carte = new Carte();
            carte.LireCarte(entree);
            carte.RemplirCarte();
            var listeSortie = CarteEnTete(carte);
            carte.DessinerCarte().ForEach(p => listeSortie.Add(p));
            return listeSortie;
        }

        private async Task<IActionResult> EcrireFichier(List<string>? listeSortie)
        {
            var filename = "carte.txt";
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);
            using (var stream = new FileStream(exactpath, FileMode.Create))
            {
                using (var streamWriter = new StreamWriter(stream))
                {
                    foreach (var ligne in listeSortie)
                    {
                        streamWriter.WriteLine(ligne);
                    }
                }
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(exactpath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(exactpath);
            return File(bytes, contenttype, Path.GetFileName(exactpath));
        }

        private List<string>? CarteEnTete(Carte carte)
        {
            var listeSortie = new List<string>();
            listeSortie.Add("C - " + carte.Largeur + " - " + carte.Hauteur);
            foreach (var montagne in carte.Montagnes)
            {
                listeSortie.Add("M - " + montagne.Y + " - " + montagne.X);
            }
            listeSortie.Add("# {T comme Trésor} - {Axe horizontal} - {Axe vertical} - {Nb. de trésors restants}");
            foreach (var tresor in carte.Tresors)
            {
                listeSortie.Add("T - " + tresor.Y + " - " + tresor.X + " - " + tresor.Quantite);
            }
            listeSortie.Add("# {T comme Trésor} - {Axe horizontal} - {Axe vertical} - {Nb. de trésors restants}");
            foreach (var tresor in carte.Tresors)
            {
                listeSortie.Add("T - " + tresor.Y + " - " + tresor.X + " - " + tresor.Quantite);
            }
            listeSortie.Add("# {A comme Aventurier} - {Nom de l’aventurier} - {Axe horizontal} - {Axe vertical} - {Orientation} - {Nb. trésors ramassés}");
            foreach (var aventurier in carte.Aventuriers)
            {
                listeSortie.Add("A - " + aventurier.Nom + " - " + aventurier.Y + " - " + aventurier.X + " - " + aventurier.Orientation + " - " + aventurier.Tresor);
            }
            listeSortie.Add("\n");
            
            return listeSortie;
        }

        // Lecture et mise en forme du fichier d'entrée
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

    }
}
