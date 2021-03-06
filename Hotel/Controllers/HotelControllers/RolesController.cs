﻿using System;
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
    public class RolesController : Controller
    {
        private RoleDataContext db = new RoleDataContext();

        // GET: Roles
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // GET: Roles/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoleId,Name,ManageItems,ManageItemCategory,ManageUsers,AccessItemLog,AccessGeneralLog")] Role role)
        {
            if (ModelState.IsValid)
            {
                var loggedinuser = Session["hotelloggedinuser"] as AppUser;
                role.DateCreated = DateTime.Now;
                role.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    role.LastModifiedBy = loggedinuser.AppUserId;
                    role.CreatedBy = loggedinuser.AppUserId;
                }
                else
                {
                    TempData["login"] = "Your session has expired, Login again!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                }
                db.Roles.Add(role);
                db.SaveChanges();
                TempData["display"] = "Your have successfully added a role!";
                TempData["notificationtype"] = NotificationType.Success.ToString();
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoleId,Name,ManageItems,ManageItemCategory,ManageUsers,AccessItemLog,AccessGeneralLog,CreatedBy,DateCreated")] Role role)
        {
            if (ModelState.IsValid)
            {
                var loggedinuser = Session["hotelloggedinuser"] as AppUser;
                role.DateLastModified = DateTime.Now;
                if (loggedinuser != null)
                {
                    role.LastModifiedBy = loggedinuser.AppUserId;
                }
                else
                {
                    TempData["login"] = "Your session has expired, Login again!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                }
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                db.SaveChanges();
                TempData["display"] = "Your have successfully modified the role!";
                TempData["notificationtype"] = NotificationType.Success.ToString();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Role role = db.Roles.Find(id);
            db.Roles.Remove(role);
            db.SaveChanges();
            TempData["display"] = "Your have successfully deleted the role!";
            TempData["notificationtype"] = NotificationType.Error.ToString();
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
