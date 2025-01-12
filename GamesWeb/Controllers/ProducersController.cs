using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Interfaces;

namespace GamesWeb.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IDAO _dao;

        public ProducersController(BLC.BLC blc)
        {
            _dao = blc.Dao;
        }

        // GET: Producers
        public async Task<IActionResult> Index()
        {
            var Producers = _dao.GetAllProducers();
            return View(Producers);
        }

        // GET: Producers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = _dao.GetAllProducers().FirstOrDefault(x => x.Id == id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // GET: Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id,string name, int estYear, int continent)
        {
            IProducer producer = _dao.CreateNewProducer();
            producer.Id = id;
            producer.Name = name;
            producer.EstYear = estYear;
            producer.Continent = (Continent) continent;

            ModelState.Clear();
            TryValidateModel(producer);
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            if (ModelState.IsValid)
            {
                _dao.AddProducer(producer);
                _dao.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        // GET: Producers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = _dao.GetAllProducers().FirstOrDefault(x => x.Id == id);
            if (producer == null)
            {
                return NotFound();
            }
            return View(producer);
        }

        // POST: Producers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string name, int estYear, int continent)
        {
            var producer = _dao.GetAllProducers().FirstOrDefault(x => x.Id == id);
            var producerTemp = _dao.CreateNewProducer();


            if (id != producer.Id)
            {
                return NotFound();
            }

            producerTemp.Id = id;
            producerTemp.Name = name;
            producerTemp.EstYear = estYear;
            producerTemp.Continent = (Continent)continent;

            ModelState.Clear();
            this.TryValidateModel(producerTemp);

            if (ModelState.IsValid)
            {
                producer.Id = producerTemp.Id;
                producer.Name = producerTemp.Name;
                producer.EstYear = producerTemp.EstYear;
                producer.Continent = producerTemp.Continent;

                try
                {
                    _dao.SaveChanges();
                }
                catch (Exception e)
                {
                    if (!ProducerExists(producer.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        // GET: Producers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = _dao.GetAllProducers().FirstOrDefault(x => x.Id == id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // POST: Producers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producer = _dao.GetAllProducers().FirstOrDefault(x => x.Id == id);
            if (producer != null)
            {
                _dao.RemoveProducer(producer);
            }

            _dao.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ProducerExists(int id)
        {
            return _dao.GetAllProducers().Any(e => e.Id == id);
        }
    }
}
