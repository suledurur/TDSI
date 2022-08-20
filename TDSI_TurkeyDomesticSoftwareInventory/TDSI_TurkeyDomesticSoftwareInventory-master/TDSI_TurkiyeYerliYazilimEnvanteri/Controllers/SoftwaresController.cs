using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDSI_TurkiyeYerliYazilimEnvanteri.Models;

namespace TDSI_TurkiyeYerliYazilimEnvanteri.Controllers
{
    public class SoftwaresController : Controller
    {
        // GET: Softwares
        TDSIEntities db = new TDSIEntities();

        // Dinamik arama-sorgu için Software içine string tanımlayıp, PartialView ile çağırabiliriz
        //
        //[HttpPost]
        public ActionResult Software(int pg)
        {
            //4 elemanlı sayfa için formul  (pg-1) * 4

            TempData["Softwares"] = db.Softwares.OrderBy(x => x.softwareID).Skip((pg - 1) * 8).Take(8);
            TempData["Page"] = pg;
            return View();
        }

        public ActionResult SoftwareDetails(int id)
        {
            //
            //
            Software sftwr = db.Softwares.Where(x => x.softwareID == id).FirstOrDefault();
            //Account acc = db.Accounts.Where(x => x.accountID == sftwr.accountID).FirstOrDefault();
            TempData["Favorites"] = db.FavoriteSoftwares.Where(x => x.favsoftwareID == id).ToList<FavoriteSoftware>();
            TempData["SoftwareDetail"] = sftwr;
            TempData["SoftwareCompany"] = db.Companies.Where(x => x.accountID == sftwr.accountID).FirstOrDefault();
            TempData["CompanySoftwares"] = db.Softwares.Where(x => x.accountID == sftwr.accountID).ToList();


            // Comment bölümü eklenince çalıştırılacak
            //
            // TempData["Comments"] = db.Comments.Where(x => x.softwareID == id).ToList();
            return View();
        }


        public ActionResult AddToFavorites(int id)
        {
            if (Session["OnlineKullanici"] == null)
            {
                return RedirectToAction("Login", "Login");
            }


            FavoriteSoftware fav = db.FavoriteSoftwares.Where(x => x.favsoftwareID == id && x.accountID == TemporaryUserData.OnlineUserID).FirstOrDefault();
            FavoriteSoftware fav1 = db.FavoriteSoftwares.OrderByDescending(x => x.fsID).FirstOrDefault();
            int favID;
            if (fav1 == null)
            {
                favID = 0;

            }
            else
            {
                favID = fav1.fsID + 1;
            }


            if (fav == null || fav.favsoftwareID != id)
            {
                fav = new FavoriteSoftware();
                fav.accountID = TemporaryUserData.OnlineUserID;
                fav.favsoftwareID = id;
                fav.fsID = favID;

                db.FavoriteSoftwares.Add(fav);

                //ToDo: Takip edildikten sonra Takip edilen kişiye bildirim gönderme olabilir
            }
            else
            {
                db.FavoriteSoftwares.Remove(fav);
            }

            db.SaveChanges();

            return RedirectToAction("SoftwareDetails", "Softwares", new { id = id });
        }
    }
}