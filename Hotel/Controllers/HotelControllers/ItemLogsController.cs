using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Hotel.Data.DataContext.DataContext;
using Hotel.Data.Objects.Entities;

namespace Hotel.Controllers.HotelControllers
{
    public class ItemLogsController : Controller
    {
        private ItemLogDataContext db = new ItemLogDataContext();

        // GET: ItemLogs
        public ActionResult Index()
        {
            var itemLogs = db.ItemLogs.Include(i => i.Item);
            return View(itemLogs.ToList());
        }

        // GET: ItemLogs/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemLog itemLog = db.ItemLogs.Find(id);
            if (itemLog == null)
            {
                return HttpNotFound();
            }
            return View(itemLog);
        }

        // GET: ItemLogs/Create
        public ActionResult Create()
        {
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Name");
            return View();
        }

        // POST: ItemLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemLogId,ItemId,TransactionType,Quantity,TotalAmount,CreatedBy,DateCreated,DateLastModified,LastModifiedBy")] ItemLog itemLog)
        {
            if (ModelState.IsValid)
            {
                db.ItemLogs.Add(itemLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Name", itemLog.ItemId);
            return View(itemLog);
        }

        // GET: ItemLogs/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemLog itemLog = db.ItemLogs.Find(id);
            if (itemLog == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Name", itemLog.ItemId);
            return View(itemLog);
        }

        // POST: ItemLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemLogId,ItemId,TransactionType,Quantity,TotalAmount,CreatedBy,DateCreated,DateLastModified,LastModifiedBy")] ItemLog itemLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Name", itemLog.ItemId);
            return View(itemLog);
        }

        // GET: ItemLogs/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemLog itemLog = db.ItemLogs.Find(id);
            if (itemLog == null)
            {
                return HttpNotFound();
            }
            return View(itemLog);
        }

        // POST: ItemLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ItemLog itemLog = db.ItemLogs.Find(id);
            db.ItemLogs.Remove(itemLog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
