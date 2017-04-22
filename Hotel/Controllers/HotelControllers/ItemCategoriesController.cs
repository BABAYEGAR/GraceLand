using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hotel.Data.DataContext.DataContext;
using Hotel.Data.Objects.Entities;
using Hotel.Data.Service.Enum;

namespace Hotel.Controllers.HotelControllers
{
    public class ItemCategoriesController : Controller
    {
        private ItemCategoryDataContext db = new ItemCategoryDataContext();

        // GET: ItemCategories
        public ActionResult Index()
        {
            return View(db.ItemCategories.ToList());
        }

        // GET: ItemCategories/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCategory itemCategory = db.ItemCategories.Find(id);
            if (itemCategory == null)
            {
                return HttpNotFound();
            }
            return View(itemCategory);
        }

        // GET: ItemCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemCategoryId,Name")] ItemCategory itemCategory)
        {
            if (ModelState.IsValid)
            {
                var loggedinuser = Session["hotelloggedinuser"] as AppUser;
                itemCategory.DateCreated = DateTime.Now;
                itemCategory.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    itemCategory.LastModifiedBy = loggedinuser.AppUserId;
                    itemCategory.CreatedBy = loggedinuser.AppUserId;
                }
                else
                {
                    TempData["login"] = "Your session has expired, Login again!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                }
                db.ItemCategories.Add(itemCategory);
                db.SaveChanges();
                TempData["display"] = "Your have successfully added a new item category!";
                TempData["notificationtype"] = NotificationType.Info.ToString();
                return RedirectToAction("Index");
            }

            return View(itemCategory);
        }

        // GET: ItemCategories/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCategory itemCategory = db.ItemCategories.Find(id);
            if (itemCategory == null)
            {
                return HttpNotFound();
            }
            return View(itemCategory);
        }

        // POST: ItemCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemCategoryId,Name,CreatedBy,DateCreated")] ItemCategory itemCategory)
        {
            if (ModelState.IsValid)
            {
                var loggedinuser = Session["hotelloggedinuser"] as AppUser;
                itemCategory.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    itemCategory.LastModifiedBy = loggedinuser.AppUserId;
                }
                else
                {
                    TempData["login"] = "Your session has expired, Login again!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                }
                db.Entry(itemCategory).State = EntityState.Modified;
                db.SaveChanges();
                TempData["display"] = "Your have successfully modified an item category!";
                TempData["notificationtype"] = NotificationType.Info.ToString();
                return RedirectToAction("Index");
            }
            return View(itemCategory);
        }

        // GET: ItemCategories/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCategory itemCategory = db.ItemCategories.Find(id);
            if (itemCategory == null)
            {
                return HttpNotFound();
            }
            return View(itemCategory);
        }

        // POST: ItemCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ItemCategory itemCategory = db.ItemCategories.Find(id);
            db.ItemCategories.Remove(itemCategory);
            db.SaveChanges();
            TempData["display"] = "Your have successfully deleted an item category!";
            TempData["notificationtype"] = NotificationType.Info.ToString();
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
