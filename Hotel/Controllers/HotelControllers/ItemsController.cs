using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Hotel.Data.DataContext.DataContext;
using Hotel.Data.Objects.Entities;
using Hotel.Data.Service.Enum;

namespace Hotel.Controllers.HotelControllers
{
    public class ItemsController : Controller
    {
        private readonly ItemDataContext _db = new ItemDataContext();

        // GET: Items
        public ActionResult Index()
        {
            ViewBag.ItemCategoryId = new SelectList(_db.ItemCategories, "ItemCategoryId", "Name");
            var items = _db.Items.Include(i => i.ItemCategory);
            return View(items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }
        // GET: Items/AddItemsToStock/5
        public ActionResult AddItemsToStock(long? id,FormCollection collectedValues)
        {
            var loggedinuser = Session["hotelloggedinuser"] as AppUser;
            Item item = _db.Items.Find(id);
            ItemLog log= new ItemLog();
            long quantity = Convert.ToInt64(collectedValues["Quantity"]);
            if (item != null)
            {
                item.Quantity = quantity;
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();

                //create a new log record
                log.ItemId = id;
                log.Quantity = quantity;
                log.TransactionType = TransactionType.Credit.ToString();
                log.DateCreated = DateTime.Now;
                log.DateLastModified = DateTime.Now;
                if (loggedinuser != null) log.CreatedBy = loggedinuser.AppUserId;
                if (loggedinuser != null) log.LastModifiedBy = loggedinuser.AppUserId;
                log.TotalAmount = quantity*item.Amount;

                _db.ItemLogs.Add(log);
                _db.SaveChanges();
                TempData["display"] = "Your have successfully added more stock items to the Inventory!";
                TempData["notificationtype"] = NotificationType.Info.ToString();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        // GET: Items/RemoveItemsFromStock/5
        public ActionResult RemoveItemsFromStock(long? id, FormCollection collectedValues)
        {
            var loggedinuser = Session["hotelloggedinuser"] as AppUser;
            Item item = _db.Items.Find(id);
            ItemLog log = new ItemLog();
            long quantity = Convert.ToInt64(collectedValues["Quantity"]);
            if (item != null)
            {
                item.Quantity = quantity;
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();

                //create a new log record
                log.ItemId = id;
                log.Quantity = quantity;
                log.TransactionType = TransactionType.Debit.ToString();
                log.DateCreated = DateTime.Now;
                log.DateLastModified = DateTime.Now;
                if (loggedinuser != null) log.CreatedBy = loggedinuser.AppUserId;
                if (loggedinuser != null) log.LastModifiedBy = loggedinuser.AppUserId;
                log.TotalAmount = quantity * item.Amount;

                _db.ItemLogs.Add(log);
                _db.SaveChanges();
                TempData["display"] = "Your have successfully deducted more stock items from the Inventory!";
                TempData["notificationtype"] = NotificationType.Info.ToString();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.ItemCategoryId = new SelectList(_db.ItemCategories, "ItemCategoryId", "Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,Name,Description,Quantity,Amount,ItemCategoryId")] Item item)
        {
            if (ModelState.IsValid)
            {
                var loggedinuser = Session["hotelloggedinuser"] as AppUser;
                item.DateCreated = DateTime.Now;
                item.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    item.LastModifiedBy = loggedinuser.AppUserId;
                    item.CreatedBy = loggedinuser.AppUserId;
                }
                else
                {
                    TempData["login"] = "Your session has expired, Login again!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                }
                _db.Items.Add(item);
                _db.SaveChanges();
                TempData["display"] = "Your have successfully added a new item!";
                TempData["notificationtype"] = NotificationType.Info.ToString();
                return RedirectToAction("Index");
            }

            ViewBag.ItemCategoryId = new SelectList(_db.ItemCategories, "ItemCategoryId", "Name", item.ItemCategoryId);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemCategoryId = new SelectList(_db.ItemCategories, "ItemCategoryId", "Name", item.ItemCategoryId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,Name,Description,Quantity,Amount,ItemCategoryId,CreatedBy,DateCreated")] Item item)
        {
            if (ModelState.IsValid)
            {
                var loggedinuser = Session["hotelloggedinuser"] as AppUser;
                item.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    item.LastModifiedBy = loggedinuser.AppUserId;
                }
                else
                {
                    TempData["login"] = "Your session has expired, Login again!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                }
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["display"] = "Your have successfully modified an item!";
                TempData["notificationtype"] = NotificationType.Info.ToString();
                return RedirectToAction("Index");
            }
            ViewBag.ItemCategoryId = new SelectList(_db.ItemCategories, "ItemCategoryId", "Name", item.ItemCategoryId);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Item item = _db.Items.Find(id);
            _db.Items.Remove(item);
            _db.SaveChanges();
            TempData["display"] = "Your have successfully deleted an item!";
            TempData["notificationtype"] = NotificationType.Info.ToString();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
