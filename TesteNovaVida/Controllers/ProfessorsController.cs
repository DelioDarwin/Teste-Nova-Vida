using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using TesteNovaVida.Data;
using TesteNovaVida.Models;


namespace TesteNovaVida.Controllers
{
    public class ProfessorsController : Controller
    {
        private readonly TesteNovaVidaContext _context;

        public ProfessorsController(TesteNovaVidaContext context)
        {
            _context = context;
        }

        // GET: Professors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Professor.ToListAsync());
        }

        // GET: Professors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor
                .FirstOrDefaultAsync(m => m.IdProfessor == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProfessor,NomeProfessor")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(professor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(professor);
        }

        // GET: Professors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProfessor,NomeProfessor")] Professor professor)
        {
            if (id != professor.IdProfessor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(professor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessorExists(professor.IdProfessor))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(professor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor
                .FirstOrDefaultAsync(m => m.IdProfessor == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professor = await _context.Professor.FindAsync(id);
            if (professor != null)
            {
                _context.Professor.Remove(professor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessorExists(int id)
        {
            return _context.Professor.Any(e => e.IdProfessor == id);
        }


        public async Task<IActionResult> ListarAlunosProfessor(int? id, string? nome)
        {
            if (id == null)
            {
                return NotFound();
            }

            IEnumerable<object> nomeRet = null;

            List<Aluno> aluno =  _context.Aluno
                .Include(a => a.Professor)
               .Where(m => m.IdProfessor == id)
                .ToListAsync().Result;

            if (aluno == null)
            {
                return NotFound();
            }
         

            return View(aluno);
        }


        [HttpPost, ActionName("DeleteDirect")]
        public async Task DeleteDirect(int id)
        {

            if (ModelState.IsValid)
            {
                var alunos = _context.Aluno.Where(c => c.IdProfessor == id).ToList();
                foreach (var x in alunos)
                {
                    var com = x;
                    _context.Aluno.Remove(com);
                }

                await _context.SaveChangesAsync();

                var profesor = await _context.Professor.FindAsync(id);
                _context.Professor.Remove(profesor);

                await _context.SaveChangesAsync();
            }
            else
            {
                BadRequest(ModelState);
            }
            
        }
    }
}
