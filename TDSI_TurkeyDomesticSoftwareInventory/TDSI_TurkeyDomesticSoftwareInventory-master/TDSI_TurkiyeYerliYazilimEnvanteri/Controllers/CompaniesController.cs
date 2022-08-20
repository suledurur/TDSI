using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDSI_TurkiyeYerliYazilimEnvanteri.Models;

namespace TDSI_TurkiyeYerliYazilimEnvanteri.Controllers
{

    public class CompaniesController : Controller
    {
        TDSIEntities db = new TDSIEntities();
        // GET: Companies
        
        public ActionResult Company(int pg)
        {
            //Şirket listesi ile Hesap listesini Join işlemi
            List<Company> compList = db.Companies.OrderBy(x => x.companyID).Skip((pg - 1) * 8).Take(8).ToList(); 
            List<Account> accList = db.Accounts.ToList();
            TempData["Companies"] = compList.Join(
                accList,
                comp => comp.accountID,
                acc => acc.accountID,
                (comp, acc) => comp);
            TempData["Page"] = pg;

            //List<CompanyStatistic> cs = db.CompanyStatistics.Where(x => x.accountID == ).ToList<CompanyStatistic>();

            return View();
        }

        public ActionResult CompanyDetails(int id)
        {
            Company cmp = db.Companies.Where(x => x.companyID == id).FirstOrDefault();
            int accID = cmp.accountID;
            TempData["CompanyDetail"] = cmp;
            TempData["Follows"] = db.Follows.Where(x => x.followingID == id).ToList<Follow>();
            TempData["CompanySoftwares"] = db.Softwares.Where(x => x.accountID == accID).ToList();
            TempData["CompanySectors"] = db.CompanySectors.Where(x => x.companyID == id).ToList();
            return View();

        }

        public ActionResult AddToFollows(int id)
        {
            if (Session["OnlineKullanici"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            //Follow flw = db.Follows.Where(x => x.accountID == TemporaryUserData.OnlineUserID).FirstOrDefault();
            Follow flw = db.Follows.Where(x => x.followingID == id && x.accountID == TemporaryUserData.OnlineUserID).FirstOrDefault();
            Follow flw1 = db.Follows.OrderByDescending(x => x.fID).FirstOrDefault();
            int flwID = flw1.fID + 1;
            if (flw == null || flw.followingID != id)
            {
                flw = new Follow();
                flw.accountID = TemporaryUserData.OnlineUserID;
                flw.followingID = id;
                flw.fID = flwID;

                db.Follows.Add(flw);

                //ToDo: Takip edildikten sonra Takip edilen kişiye bildirim gönderme olabilir
            }
            else
            {
                db.Follows.Remove(flw);
            }

            db.SaveChanges();

            return RedirectToAction("CompanyDetails", "Companies", new { id = id });
        }
    }
}