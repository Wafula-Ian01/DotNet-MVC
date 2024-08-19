using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Records_Master.Data;
using Records_Master.Models;


namespace Records_Master.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Model data
        private IEnumerable<Patient> GetPatientsData()
        { 
            return _context.Patient.ToList();
        }

        // GET: Patients
       /* public async Task<IActionResult> Index()
        {
            return View(await _context.Patient.ToListAsync());
        }*/
        public ViewResult Index(string sortOrder, string currentFilter,string searchString)
        {
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["RegionSortParam"] = sortOrder == "Region" ? "Region" : "region_desc";
            ViewData["CurrentFilter"] = searchString;
            var patient = from p in _context.Patient select p;


            ViewBag.CurrentFilter= searchString;


            if(!String.IsNullOrEmpty(searchString))
            {
                patient=patient.Where(p=>p.LastName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    patient = patient.OrderByDescending(p => p.LastName);
                    break;

                case "Region":
                    patient = patient.OrderBy(p => p.Region);
                    break;

                case "region_desc":
                    patient = patient.OrderByDescending(p => p.Region);
                    break;

                default:
                    patient = patient.OrderByDescending(p => p.LastName);
                    break;
            }

            return View(patient);
        }


        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        //GET: By Region
        public async Task<IActionResult> ByRegion()
        {
            var patient = _context.Patient.ToList();
            var patientGroup= patient
                .GroupBy(p=>p.Region)
                .ToDictionary(g=>g.Key, g=>g.ToList());

            return View(patientGroup);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            ViewData["RegionOptions"] = Patient.RegionOptions;
            ViewData["StatusOptions"] = Patient.StatusOptions;
            ViewData["GenderOptions"] = Patient.GenderOptions;
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PhoneNumber,City,Region,RegNumber,Status,Gender")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegionOptions"]=Patient.RegionOptions;
            ViewData["StatusOptions"]=Patient.StatusOptions;
            ViewData["GenderOptions"] = Patient.GenderOptions;
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            ViewData["RegionOptions"] = Patient.RegionOptions;
            ViewData["StatusOptions"] = Patient.StatusOptions;
            ViewData["GenderOptions"] = Patient.GenderOptions;

            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhoneNumber,City,Region,RegNumber,Status,Gender")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
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
            ViewData["RegionOptions"] = Patient.RegionOptions;
            ViewData["StatusOptions"] = Patient.StatusOptions;
            ViewData["GenderOptions"] = Patient.GenderOptions;

            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _context.Patient.FindAsync(id);
            if (patient != null)
            {
                _context.Patient.Remove(patient);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
            return _context.Patient.Any(e => e.Id == id);
        }

    }
}
