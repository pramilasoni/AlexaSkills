using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AlexaSkill.Data;

namespace AlexaSkill.Controllers
{
    public class CompetitionWinnersController : Controller
    {
        private readonly alexaskilldemoEntities db = new alexaskilldemoEntities();

        // GET: CompetitionWinners
        public ActionResult Index()
        {
            return View(db.CompetitionWinners.ToList());
        }

        // GET: CompetitionWinners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var competitionWinner = db.CompetitionWinners.Find(id);
            if (competitionWinner == null) return HttpNotFound();
            return View(competitionWinner);
        }

        // GET: CompetitionWinners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompetitionWinners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Tweet,ProfileImageUrl,CreatedDate,UpdatedDate")]
            CompetitionWinner competitionWinner)
        {
            if (ModelState.IsValid)
            {
                db.CompetitionWinners.Add(competitionWinner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(competitionWinner);
        }

        // GET: CompetitionWinners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var competitionWinner = db.CompetitionWinners.Find(id);
            if (competitionWinner == null) return HttpNotFound();
            return View(competitionWinner);
        }

        // POST: CompetitionWinners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Tweet,ProfileImageUrl,CreatedDate,UpdatedDate")]
            CompetitionWinner competitionWinner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(competitionWinner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(competitionWinner);
        }

        // GET: CompetitionWinners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var competitionWinner = db.CompetitionWinners.Find(id);
            if (competitionWinner == null) return HttpNotFound();
            return View(competitionWinner);
        }

        // POST: CompetitionWinners/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var competitionWinner = db.CompetitionWinners.Find(id);
            db.CompetitionWinners.Remove(competitionWinner);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}