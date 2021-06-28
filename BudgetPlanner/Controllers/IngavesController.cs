using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BudgetPlanner.Database;
using BudgetPlanner.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using StoreAccountingApp.CustomMethods;

namespace BudgetPlanner.Controllers
{
    public class IngavesController : Controller
    {
        private readonly BudgetPlannerContext _context;
        private readonly IWebHostEnvironment hostEnvironment;

        public IngavesController(BudgetPlannerContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.hostEnvironment = hostEnvironment;
        }

        [Route(""), Route("/Budget")]
        // GET: Ingaves
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ingaves.ToListAsync());
        }

        // GET: Ingaves/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingave = await _context.Ingaves
                .FirstOrDefaultAsync(m => m.IngaveId == id);
            if (ingave == null)
            {
                return NotFound();
            }

            return View(ingave);
        }

        // GET: Ingaves/Create
        public IActionResult Create()
        {
            return View(new IngaveViewModel());
        }

        // POST: Ingaves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IngaveId,Bedrag,Datum,Omschrijving,ProductImage,Bedrijf,Status,ProductNewImage")] IngaveViewModel ingaveVM)
        {
            if (ModelState.IsValid)
            {
                Ingave ingave = new Ingave();
                ViewModelToModel(ingaveVM, ingave);
                _context.Add(ingave);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ingaveVM);
        }

        // GET: Ingaves/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingave = await _context.Ingaves.FindAsync(id);
            if (ingave == null)
            {
                return NotFound();
            }
            return View(ModelToViewModel(ingave));
        }

        // POST: Ingaves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IngaveId,Bedrag,Datum,Omschrijving,ProductImage,Bedrijf,Status,ProductNewImage")] IngaveViewModel ingaveVM)
        {
            if (id != ingaveVM.IngaveId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var ingave = _context.Find<Ingave>(id);
                    if (ingave != null)
                    {
                        ViewModelToModel(ingaveVM, ingave);
                    }
                    _context.Update(ingave);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngaveExists(ingaveVM.IngaveId))
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
            return View(ingaveVM);
        }

        // GET: Ingaves/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingave = await _context.Ingaves
                .FirstOrDefaultAsync(m => m.IngaveId == id);
            if (ingave == null)
            {
                return NotFound();
            }

            return View(ingave);
        }

        // POST: Ingaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingave = await _context.Ingaves.FindAsync(id);
            _context.Ingaves.Remove(ingave);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngaveExists(int id)
        {
            return _context.Ingaves.Any(e => e.IngaveId == id);
        }
        private IngaveViewModel ModelToViewModel(Ingave ingave)
        {
            return ObjMethods.CopyProperties<Ingave, IngaveViewModel>(ingave);
        }
        //private TU ModelToViewModel<TU,T>(T ingave)
        //    where TU : new ()
        //{
        //    return ObjMethods.CopyProperties<T, TU>(ingave);
        //}
        private void ViewModelToModel(IngaveViewModel ingaveVM, Ingave ingave)
        {
            ObjMethods.EditProperties(ingaveVM, ingave);
            if (ingaveVM.ProductNewImage != null)
            {
                if (!String.IsNullOrEmpty(ingave.ProductImage))
                    DeleteFoto(ingave.ProductImage);
                ingave.ProductImage = "/" + Path.Combine("Images", UploadFoto(ingaveVM.ProductNewImage));
            }
        }

        private string UploadFoto(IFormFile foto)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
            string pathName = Path.Combine(hostEnvironment.WebRootPath, "Images");
            string fileNameWithPath = Path.Combine(pathName, uniqueFileName);
            foto.CopyTo(new FileStream(fileNameWithPath, FileMode.Create));
            return uniqueFileName;
        }
        private void DeleteFoto(string filename)
        {
            string path = Path.Combine(hostEnvironment.WebRootPath, filename.Substring(1));
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }
    }
}
