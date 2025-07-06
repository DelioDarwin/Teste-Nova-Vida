using TesteNovaVida.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Mvc;
using TesteNovaVida.Data;


namespace TesteNovaVida.Controllers
{
    public class ImportController : Controller
    {

        private IHostingEnvironment Environment;

        private readonly TesteNovaVidaContext _context;

        public ImportController(TesteNovaVidaContext context, IHostingEnvironment _environment)
        {
             _context = context;
            Environment = _environment;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View(new List<Aluno>());
        }

        [HttpPost]
        public ActionResult Index(string id, string nome, IFormFile postedFile)
        {
            List<Aluno> alunos = new List<Aluno>();

            string filePath = string.Empty;
            if (postedFile != null)
            {
                string wwwPath = this.Environment.WebRootPath;
                string contentPath = this.Environment.ContentRootPath;

                string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
                string fileName = Path.GetFileName(postedFile.FileName);
                filePath = path + "\\" + fileName;
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    stream.Dispose();

                    //Read the contents of CSV file.
                    string csvData = System.IO.File.ReadAllText(filePath);

                    //Execute a loop over the rows.
                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            Aluno aluno = new Aluno();
                            aluno.IdProfessor = Convert.ToInt32(id);
                            aluno.NomeAluno = row.Split(',')[0];
                            aluno.ValorMensalidade = Convert.ToDecimal(row.Split(',')[1]);
                            aluno.DataVencimento = Convert.ToDateTime(row.Split(',')[2]);

                            alunos.Add(aluno);

                            _context.Add(aluno);
                           _context.SaveChanges();
                        }
                    }

                      
                }
            
            }
            return RedirectToAction("ListarAlunosProfessor", "Professors", new { id = id, nome = nome });
        }

    }
}