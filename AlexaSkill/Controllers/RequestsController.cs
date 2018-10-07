using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AlexaSkill.Data;

namespace AlexaSkill.Controllers
{
    public class RequestsController : Controller
    {
        private readonly alexaskilldemoEntities db = new alexaskilldemoEntities();

        // GET: Requests
        public ActionResult Index()
        {
            var requests = db.Requests.Include(r => r.Member);
            return View(requests.ToList());
        }

        // GET: Requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var request = db.Requests.Find(id);
            if (request == null) return HttpNotFound();
            return View(request);
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
            ViewBag.MemberId = new SelectList(db.Members, "id", "AlexaUserId");
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include =
                "id,MemberId,SessionId,AppId,RequestId,UserId,TimeStamp,Intent,Slots,IsNew,Version,Type,Reason,DateCreated")]
            Request request)
        {
            if (ModelState.IsValid)
            {
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.Members, "id", "AlexaUserId", request.MemberId);
            return View(request);
        }

        // GET: Requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var request = db.Requests.Find(id);
            if (request == null) return HttpNotFound();
            ViewBag.MemberId = new SelectList(db.Members, "id", "AlexaUserId", request.MemberId);
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =
                "id,MemberId,SessionId,AppId,RequestId,UserId,TimeStamp,Intent,Slots,IsNew,Version,Type,Reason,DateCreated")]
            Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.Members, "id", "AlexaUserId", request.MemberId);
            return View(request);
        }

        // GET: Requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var request = db.Requests.Find(id);
            if (request == null) return HttpNotFound();
            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var request = db.Requests.Find(id);
            db.Requests.Remove(request);
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