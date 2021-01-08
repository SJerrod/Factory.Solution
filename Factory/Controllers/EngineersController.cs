using Factory.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers
{
    public class EngineersController : Controller
    {
        private readonly FactoryContext _db;
        public EngineersController(FactoryContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View(_db.Engineers.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Engineer Engineer)
        {
            _db.Engineers.Add(Engineer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // add to create?
//  List<int> machines
//         if (machines.Count != 0)
//             {
//                 foreach (int machine in machines)
//                 {
//                     _db.EngineerMachine.Add(new EngineerMachine() { MachineId = machine, EngineerId = Engineer.EngineerId });
//                 }
//             }

        public ActionResult Details(int id)
        {
            var thisEngineer = _db.Engineers
                .Include(engineer => engineer.JoinEntries)
                .ThenInclude(join => join.Machine)
                .FirstOrDefault(Engineer => Engineer.EngineerId == id);
            return View(thisEngineer);
        }

        public ActionResult Edit(int id)
        {
            var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
            ViewBag.Machines = _db.Machines.ToList();
            return View(thisEngineer);
        }

        [HttpPost]
        public ActionResult Edit(Engineer Engineer, List<int> machines)
        {
            if (machines.Count != 0)
            {
                foreach (int machine in machines)
                {
                    _db.EngineerMachine.Add(new EngineerMachine() { MachineId = machine, EngineerId = Engineer.EngineerId });
                }
            }
            _db.Entry(Engineer).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddMachine(int id)
        {
            var thisEngineer = _db.Engineers.FirstOrDefault(EngineersController => EngineersController.EngineerId == id);
            ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
            return View(thisEngineer);
        }

        [HttpPost]
        public ActionResult AddMachine(Engineer engineer, int MachineId)
        {
            if (MachineId != 0)
            {
                _db.EngineerMachine.Add(new EngineerMachine() { MachineId = MachineId, EngineerId = engineer.EngineerId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
          var thisEngineer = _db.Engineers.FirstOrDefault(engineers => engineers.EngineerId == id);
          return View(thisEngineer);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
          var thisEngineer = _db.Engineers.FirstOrDefault(engineers => engineers.EngineerId == id);
          _db.Engineers.Remove(thisEngineer);
          _db.SaveChanges();
          return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteEngineer(int joinId)
        {
          var joinEntry = _db.EngineerMachine.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
          _db.EngineerMachine.Remove(joinEntry);
          _db.SaveChanges();
          return RedirectToAction("Index");
        }
    }
}