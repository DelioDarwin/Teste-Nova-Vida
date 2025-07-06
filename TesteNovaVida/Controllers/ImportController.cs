using TesteNovaVida.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Mvc;
using TesteNovaVida.Data;
using System.Text;


namespace TesteNovaVida.Controllers
{
    public class ImportController : Controller
    {
        private readonly TesteNovaVidaContext _context;
        private IHostingEnvironment Environment;        
        private readonly IConfiguration _configuration;

        public ImportController(TesteNovaVidaContext context, IHostingEnvironment _environment, IConfiguration configuration)
        {
             _context = context;
             Environment = _environment;
             _configuration = configuration;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View(new List<Aluno>());
        }

        [HttpPost]
        public  IActionResult Index(string id, string nome, IFormFile postedFile)
        {
            //Trata e Verificação configuração do intervalo para uma nova importação

            TimeZoneInfo Standard_Time = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            DateTime dataHoraLocal = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Standard_Time);
            UltimaImportacao primeiraImportacao = new UltimaImportacao();

            UltimaImportacao ultimaDataHoraGravada = _context.UltimaImportacao.SingleOrDefault();
            if (ultimaDataHoraGravada != null)
            {               
                DateTime dataTermino = ultimaDataHoraGravada.DataHora.AddMinutes(_configuration.GetValue<int>("IntervaloMinutosNovaImportacao"));
                TimeSpan tempoRestante = dataTermino - dataHoraLocal;

                if (tempoRestante.Minutes > 0) //Caso não tenha expirado o limite de minutos para uma nova importação
                {
                    ViewData["Message"] = "Importação Negada! Ainda não expirou o tempo limite permitido para que possa fazer uma nova importação, aguarde: #" + tempoRestante.Minutes.ToString() + " minutos" + "#" + StatusCodes.Status403Forbidden + "#" + nome;
                    return View();
                }
                else //Caso o limite já tenha sido expirado grava a nova data e hora desta importação (Update)
                { 
                    ultimaDataHoraGravada.DataHora = dataHoraLocal;
                    _context.UltimaImportacao.Update(ultimaDataHoraGravada);
                    _context.SaveChanges();
                }
            }
            else //Primeira Importação faz o insert (apenas uma linha tabela)
            {
                primeiraImportacao.DataHora = dataHoraLocal;                
                _context.UltimaImportacao.Add(primeiraImportacao);
                _context.SaveChanges();
            }
        
            //Importação
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
                    string csvData = System.IO.File.ReadAllText(filePath,Encoding.Latin1);

                    //Execute a loop over the rows.
                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            Aluno aluno = new Aluno();
                            aluno.IdProfessor = Convert.ToInt32(id);
                            aluno.NomeAluno = row.Split(',')[0];
                            aluno.ValorMensalidade = Convert.ToDecimal(row.Split(',')[1].Replace(".", ","));
                            aluno.DataVencimento = Convert.ToDateTime(row.Split(',')[2]);

                            alunos.Add(aluno);

                            _context.Add(aluno);
                            _context.SaveChanges();
                        }
                    }
                }

            }

         
            return RedirectToAction("ListarAlunosProfessor", "Professors", new { id = id, nome = nome, import = "s", total = alunos.Count.ToString() });
        }

    }
}