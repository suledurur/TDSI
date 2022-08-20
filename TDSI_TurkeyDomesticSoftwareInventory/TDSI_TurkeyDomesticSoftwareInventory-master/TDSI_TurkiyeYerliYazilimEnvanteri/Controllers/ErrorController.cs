using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Security.Policy;

namespace TDSI_TurkiyeYerliYazilimEnvanteri.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Router(byte err_code)
        {
            //hash algorithm
            string str = Convert.ToString(err_code);
            string hashed_url; 
            
            using (var md5Hash = MD5.Create())
            {
                var src_bytes = ASCIIEncoding.ASCII.GetBytes(str);
                var hash_bytes = md5Hash.ComputeHash(src_bytes);
                var hash= BitConverter.ToString(hash_bytes).Replace("-", string.Empty);
                hashed_url = hash.ToLower();
            }
            

            // Error türleri bulunup, View oluşturulacak
            switch (err_code)
            {
                // Default Error
                case 100:
                    return RedirectToAction(hashed_url, "Error");
                // Page Couldn't Load
                case 101:
                    return RedirectToAction("a" + hashed_url, "Error");
                // Time Out Error
                case 102:
                    return RedirectToAction(hashed_url, "Error");                    
                // 
                case 103:
                    return RedirectToAction("a" + hashed_url, "Error");
                // 
                case 104:
                    return RedirectToAction(hashed_url, "Error");
                //
                case 105:
                    return RedirectToAction("a" + hashed_url, "Error");
                //
                case 106:
                    return RedirectToAction(hashed_url, "Error");
                //
                case 107:
                    return RedirectToAction(hashed_url, "Error");
                //
                case 108:
                    return RedirectToAction(hashed_url, "Error");
                //
                case 109:
                    return RedirectToAction("a" + hashed_url, "Error");
                //
                case 110:
                    return RedirectToAction("a" + hashed_url, "Error");
                // Default Error (100)
                default:
                    return RedirectToAction(hashed_url, "Error");
            }            
        }

        public ActionResult f899139df5e1059396431415e770c6dd()//100
        {
            return View();
        }
        public ActionResult a38b3eff8baf56627478ec76a704e9b52()//101 giriş error
        {
            return View();
        }
        public ActionResult ec8956637a99787bd197eacd77acce5e()//102 site bakım error
        {
            return View();
        }
        public ActionResult a6974ce5ac660610b44d9b9fed0ff9548()//103
        {
            return View();
        }
        public ActionResult c9e1074f5b3f9fc8ea15d152add07294()//104
        {
            return View();
        }
        public ActionResult a65b9eea6e1cc6bb9f0cd2a47751a186f()//105 Arama çubuğu error
        {
            return View();
        }
        public ActionResult f0935e4cd5920aa6c7c996a5ee53a70f()//106
        {
            return View();
        }
        public ActionResult a97da629b098b75c294dffdc3e463904()//107
        {
            return View();
        }
        public ActionResult a3c65c2974270fd093ee8a9bf8ae7d0b()//108
        {
            return View();
        }
        public ActionResult a2723d092b63885e0d7c260cc007e8b9d()//109
        {
            return View();
        }
        public ActionResult a5f93f983524def3dca464469d2cf9f3e()//110
        {
            return View();
        }
    }
}