using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RuiMoraes.DocWebUpload.Models;
using RuiMoraes.DocWebUpload.Repositories.Interfaces;
using System.IO;

namespace RuiMoraes.DocWebUpload.Controllers
{
    public class DocumentoController : Controller
    {
        private readonly IDocumentoRepository _documentoRepository;
        private readonly IWebHostEnvironment _environment;

        public DocumentoController(IDocumentoRepository documentoRepository, IWebHostEnvironment environment)
        {
            _documentoRepository = documentoRepository;
            _environment = environment;
        }

        // GET: DocumentoController
        public ActionResult Index()
        {
            var documentos = _documentoRepository.BuscaTodos();
            return View(documentos);
        }

        // GET: DocumentoController/Details/5
        public ActionResult Detalhes(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documento = _documentoRepository.BuscaPorId(id);
            if (documento == null)
            {
                return NotFound();
            }

            return View(documento);
        }

        // GET: DocumentoController/Create
        public ActionResult Adicionar()
        {
            return View();
        }

        // POST: DocumentoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(Documento documento, IFormFile file)
        {

            if (file.Length > 0)
            {
                //string pasta = "\\arquivos\\";
                //string nomeArquivo = "doc_criado_" + DateTime.Now.ToShortTimeString();
                string rootPath = _environment.WebRootPath;
                string caminhoUpload = "C:\\Temp\\DocUpload\\";// + nomeArquivo;
                //string caminhoUpload = rootPath + pasta;// + file.FileName;

                if (!Directory.Exists(caminhoUpload))
                {
                    Directory.CreateDirectory(caminhoUpload);
                }
                using (var fileStream = new FileStream(caminhoUpload + file.FileName, FileMode.Create))
                {
                    try
                    {
                        file.CopyTo(fileStream);
                    }
                    catch (Exception ex)
                    {
                        var message = ex.Message;

                    }
                }

                documento.NomeDocumento = file.FileName;
                documento.Caminho = caminhoUpload + file.FileName;
                documento.DataCriado = DateTime.Now;

                _documentoRepository.Adicionar(documento);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: DocumentoController/Edit/5
        public ActionResult Editar(int id)
        {
            if (id == null) return NotFound();
            var documento = _documentoRepository.BuscaPorId(id);
            if (documento == null) return NotFound();
            return View(documento);
        }

        // POST: DocumentoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, Documento documento, IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {

                    //string pasta = "\\arquivos\\";
                    //string nomeArquivo = "doc_criado_" + DateTime.Now.ToShortTimeString();
                    //string rootPath = _environment.WebRootPath;
                    string caminhoUpload = "C:\\Temp\\DocUpload\\";// + nomeArquivo;
                                                                   //string caminhoUpload = rootPath + pasta;// + file.FileName;

                    if (!Directory.Exists(caminhoUpload))
                    {
                        Directory.CreateDirectory(caminhoUpload);
                    }
                    using (var fileStream = new FileStream(caminhoUpload + file.FileName, FileMode.Create))
                    {
                        try
                        {
                            file.CopyTo(fileStream);
                        }
                        catch (Exception ex)
                        {
                            var message = ex.Message;

                        }
                    }
                    documento.NomeDocumento = file.FileName;
                    documento.Caminho = caminhoUpload + file.FileName;
                    documento.DataCriado = DateTime.Now;

                    _documentoRepository.Editar(documento);
                }

            }
            catch (Exception ex)
            {
                var message = ex.Message;
                throw;
            }
            return RedirectToAction(nameof(Index));


        }

        // GET: DocumentoController/Delete/5
        public ActionResult Excluir(int? id)
        {
            if (id == null)
                return NotFound();

            var documento = _documentoRepository.BuscaPorId(id);

            if (documento == null) return NotFound();

            if (System.IO.File.Exists(documento.Caminho))
                System.IO.File.Delete(documento.Caminho);
            else
                ViewBag.Mensagem = "Falha ao excluir o arquivo";

            _documentoRepository.Remover(id);
            return RedirectToAction("Index");
        }

        public FileResult ArquivoDownload(string nomeArquivo)
        {           
            string path = Path.Combine(nomeArquivo);
                       
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            
            return File(bytes, "application/octet-stream", nomeArquivo);
        }
    }
}
