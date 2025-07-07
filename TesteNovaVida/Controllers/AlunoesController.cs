using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TesteNovaVida.Data;
using TesteNovaVida.Models;

namespace TesteNovaVida.Controllers
{
    public class AlunoesController : Controller
    {
        private readonly TesteNovaVidaContext _context;

        public AlunoesController(TesteNovaVidaContext context)
        {
            _context = context;
        }

        // GET: Alunoes
        public async Task<IActionResult> Index()
        {
            var TesteNovaVidaContext = _context.Aluno.Include(a => a.Professor);
            return View(await TesteNovaVidaContext.ToListAsync());
        }
          

        public IActionResult Create(string? nome)
        {
            if (nome != null)
                ViewData["nomeProf"] = nome;

            ViewData["IdProfessor"] = new SelectList(_context.Professor, "IdProfessor", "NomeProfessor");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAluno,IdProfessor,NomeAluno,ValorMensalidade,DataVencimento")] Aluno aluno , string? nome)
        {
            if (ModelState.IsValid)
            {                
                _context.Add(aluno);
                await _context.SaveChangesAsync();
               
                if (nome == null) 
                     return RedirectToAction(nameof(Index));
                else
                     return RedirectToAction("ListarAlunosProfessor", "Professors", new { id = aluno.IdProfessor, nome = nome });

            }
            ViewData["IdProfessor"] = new SelectList(_context.Professor, "IdProfessor", "NomeProfessor", aluno.IdProfessor);
            return View(aluno);
        }

        // GET: Alunoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            ViewData["IdProfessor"] = new SelectList(_context.Professor, "IdProfessor", "NomeProfessor", aluno.IdProfessor);
            return View(aluno);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAluno,IdProfessor,NomeAluno,ValorMensalidade,DataVencimento")] Aluno aluno, string? nome)
        {
            if (id != aluno.IdAluno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(aluno.IdAluno))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (nome == null) 
                     return RedirectToAction(nameof(Index));
                else
                     return RedirectToAction("ListarAlunosProfessor", "Professors", new { id = aluno.IdProfessor, nome = nome });
              
            }

            ViewData["IdProfessor"] = new SelectList(_context.Professor, "IdProfessor", "NomeProfessor", aluno.IdProfessor);

            return View(aluno);


        }

        // GET: Alunoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno
                .Include(a => a.Professor)
                .FirstOrDefaultAsync(m => m.IdAluno == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // POST: Alunoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aluno = await _context.Aluno.FindAsync(id);
            if (aluno != null)
            {
                _context.Aluno.Remove(aluno);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlunoExists(int id)
        {
            return _context.Aluno.Any(e => e.IdAluno == id);
        }


        [HttpPost, ActionName("DeleteDirect")]
        public async Task DeleteDirect(int id)
        {
            var aluno = await _context.Aluno.FindAsync(id);
            if (aluno != null)
            {
                _context.Aluno.Remove(aluno);
            }

            await _context.SaveChangesAsync();
        }
    }
}
