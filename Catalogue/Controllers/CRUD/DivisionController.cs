﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Catalogue.Models.Tables;
using Catalogue.Models;
using System.Net;
using PagedList;

namespace Catalogue.Controllers.CRUD
{
    public class DivisionController : Controller
    {
        CatalogueContext db = new CatalogueContext();

        public ActionResult AjaxPositionList(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return PartialView(db.Divisions.OrderBy(i => i.DivisionName).ToPagedList(pageNumber, pageSize));
        }
        // GET: Position
        [Authorize(Roles = "admin")]
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(db.Divisions.OrderBy(i => i.DivisionName).ToPagedList(pageNumber, pageSize));
        }

        // GET: Position/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Division division = db.Divisions.Find(id);
            if (division == null)
                return HttpNotFound();
            return View(division);
        }

        // GET: Position/Create
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Position/Create
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Create(Division collection)
        {
            try
            {
                // TODO: Add insert logic here
                db.Divisions.Add(collection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Position/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Division division = db.Divisions.Find(id);
            if (division == null)
                return HttpNotFound();
            return View(division);
        }

        // POST: Position/Edit/5
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id, Division collection)
        {
            try
            {
                db.Entry(collection).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Position/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();
            Division division = db.Divisions.Find(id);
            if (division != null)
                return PartialView("Delete", division);
            return View("Index");
        }

        // POST: Position/Delete/5
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ActionName("Delete")]
        public ActionResult Delete(int? id, Division collection)
        {
            Division division = new Division();
            try
            {
                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                division = db.Divisions.Find(id);
                if (division == null)
                    return HttpNotFound();
                db.Divisions.Remove(division);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
