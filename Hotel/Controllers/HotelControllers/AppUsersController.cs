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
    public class AppUsersController : Controller
    {
        private AppUserDataContext db = new AppUserDataContext();

        // GET: AppUsers
        public ActionResult Index()
        {
            var appUsers = db.AppUsers.Include(a => a.Role);
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "Name");
            return View(appUsers.ToList());
        }

        // GET: AppUsers/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.AppUsers.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // GET: AppUsers/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "Name");
            return View();
        }

        // POST: AppUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppUserId,Firstname,Lastname,Email,Mobile,Password,RoleId")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                    var loggedinuser = Session["hotelloggedinuser"] as AppUser;
                    appUser.DateCreated = DateTime.Now;
                    appUser.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    appUser.LastModifiedBy = loggedinuser.AppUserId;
                    appUser.CreatedBy = loggedinuser.AppUserId;
                }
                else
                {
                    TempData["login"] = "Your session has expired, Login again!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                }
                    db.AppUsers.Add(appUser);
                    db.SaveChanges();
                TempData["display"] = "Your have successfully added a new user!";
                TempData["notificationtype"] = NotificationType.Info.ToString();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "Name", appUser.RoleId);
            return View(appUser);
        }

        // GET: AppUsers/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.AppUsers.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "Name", appUser.RoleId);
            return View(appUser);
        }

        // POST: AppUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppUserId,Firstname,Lastname,Email,Mobile,Password,RoleId,CreatedBy,DateCreated")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                var loggedinuser = Session["hotelloggedinuser"] as AppUser;
                appUser.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    appUser.LastModifiedBy = loggedinuser.AppUserId;
                }
                else
                {
                    TempData["login"] = "Your session has expired, Login again!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                }
                db.Entry(appUser).State = EntityState.Modified;
                db.SaveChanges();
                TempData["display"] = "Your have successfully modified a user!";
                TempData["notificationtype"] = NotificationType.Info.ToString();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "Name", appUser.RoleId);
            return View(appUser);
        }

        // GET: AppUsers/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.AppUsers.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // POST: AppUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AppUser appUser = db.AppUsers.Find(id);
            db.AppUsers.Remove(appUser);
            db.SaveChanges();
            TempData["display"] = "Your have successfully deleted the user!";
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
