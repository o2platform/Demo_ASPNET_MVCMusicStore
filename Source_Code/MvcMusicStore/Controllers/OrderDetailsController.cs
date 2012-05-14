using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{ 
    public class OrderDetailsController : Controller
    {
        private MusicStoreEntities db = new MusicStoreEntities();

        //
        // GET: /OrderDetailController/

        public ViewResult Index()
        {
            var orderdetails = db.OrderDetails.Include(o => o.Album).Include(o => o.Order);
            return View(orderdetails.ToList());
        }

        //
        // GET: /OrderDetailController/Details/5

        public ViewResult Details(int id)
        {
            OrderDetail orderdetail = db.OrderDetails.Find(id);
            return View(orderdetail);
        }

        //
        // GET: /OrderDetailController/Create

        public ActionResult Create()
        {
            ViewBag.AlbumId = new SelectList(db.Albums, "AlbumId", "Title");
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Username");
            return View();
        } 

        //
        // POST: /OrderDetailController/Create

        [HttpPost]
        public ActionResult Create(OrderDetail orderdetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderdetail);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.AlbumId = new SelectList(db.Albums, "AlbumId", "Title", orderdetail.AlbumId);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Username", orderdetail.OrderId);
            return View(orderdetail);
        }
        
        //
        // GET: /OrderDetailController/Edit/5
 
        public ActionResult Edit(int id)
        {
            OrderDetail orderdetail = db.OrderDetails.Find(id);
            ViewBag.AlbumId = new SelectList(db.Albums, "AlbumId", "Title", orderdetail.AlbumId);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Username", orderdetail.OrderId);
            return View(orderdetail);
        }

        //
        // POST: /OrderDetailController/Edit/5

        [HttpPost]
        public ActionResult Edit(OrderDetail orderdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlbumId = new SelectList(db.Albums, "AlbumId", "Title", orderdetail.AlbumId);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Username", orderdetail.OrderId);
            return View(orderdetail);
        }

        //
        // GET: /OrderDetailController/Delete/5
 
        public ActionResult Delete(int id)
        {
            OrderDetail orderdetail = db.OrderDetails.Find(id);
            return View(orderdetail);
        }

        //
        // POST: /OrderDetailController/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            OrderDetail orderdetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderdetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}