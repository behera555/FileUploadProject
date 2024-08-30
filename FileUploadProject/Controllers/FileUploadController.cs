using FileUploadProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Drawing;
using System.Web.Mvc;

namespace FileUploadProject.Controllers
{
    public class FileUploadController : Controller
    {
        private FileUploadDBEntities db = new FileUploadDBEntities();
        public ActionResult Index()
        {
            return View(db.FileUploads.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileUpload fileUpload = db.FileUploads.Find(id);
            if (fileUpload == null)
            {
                return HttpNotFound();
            }
            return View(fileUpload);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Description")] FileUpload fileUpload, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var uploadPath = Server.MapPath("~/UploadedFiles");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                file.SaveAs(filePath);

                fileUpload.FileName = fileName;
                fileUpload.FilePath = "/UploadedFiles/" + fileName;
                fileUpload.FileSize = file.ContentLength;
                fileUpload.ContentType = file.ContentType;
                fileUpload.UploadDate = DateTime.Now;
                fileUpload.UploadedBy = User.Identity.Name; 

                if (ModelState.IsValid)
                {
                    db.FileUploads.Add(fileUpload);
                    db.SaveChanges();
                    return RedirectToAction("Index"); 
                }
            }
            return View(fileUpload); 
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileUpload fileUpload = db.FileUploads.Find(id);
            if (fileUpload == null)
            {
                return HttpNotFound();
            }
            return View(fileUpload);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FileId,FileName,FilePath,FileSize,ContentType,UploadDate,UploadedBy,Description")] FileUpload fileUpload, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);

                    file.SaveAs(filePath);

                    fileUpload.FileName = fileName;
                    fileUpload.FilePath = "/UploadedFiles/" + fileName;
                    fileUpload.FileSize = file.ContentLength;
                    fileUpload.ContentType = file.ContentType;
                }

                fileUpload.UploadDate = DateTime.Now;
                fileUpload.UploadedBy = User.Identity.Name; 

                db.Entry(fileUpload).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fileUpload);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileUpload fileUpload = db.FileUploads.Find(id);
            if (fileUpload == null)
            {
                return HttpNotFound();
            }
            return View(fileUpload);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FileUpload fileUpload = db.FileUploads.Find(id);

            var filePath = Server.MapPath(fileUpload.FilePath);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            db.FileUploads.Remove(fileUpload);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}