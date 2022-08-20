using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using TDSI_TurkiyeYerliYazilimEnvanteri.Models;
using System.Linq.Expressions;
using System.Web.Helpers;

namespace TDSI_TurkiyeYerliYazilimEnvanteri.Controllers
{
    public class UserController : Controller
    {
        TDSIEntities db = new TDSIEntities();

        // GET: User
        public ActionResult UserProfile(int id)
        {
            if (Session["OnlineKullanici"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else if (TemporaryUserData.OnlineUserID == id)
            {
                TempData["Account"] = db.Accounts.Where(x => x.accountID == id).FirstOrDefault();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult CompanyProfile(int id, int? notification)
        {
            if (Session["OnlineKullanici"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else if (TemporaryUserData.OnlineUserID == id)
            {

                Company company = db.Companies.Where(x => x.accountID == id).FirstOrDefault();
                TempData["Company"] = company;
                List<FavoriteSoftware> favoriteList = db.FavoriteSoftwares.Where(x => x.accountID == TemporaryUserData.OnlineUserID).ToList();
                List<Follow> followingList = db.Follows.Where(x => x.accountID == TemporaryUserData.OnlineUserID).ToList();
                List<Company> flwComp = new List<Company>();
                //List<Account> flwAccount = new List<Account>();
                foreach (var flw in followingList)
                {
                    flwComp.Add(db.Companies.Where(x => x.companyID == flw.followingID).FirstOrDefault());
                }

                TempData["Follows"] = flwComp;
                TempData["Favorites"] = favoriteList;
                TempData["Branches"] = db.Stations.Where(x => x.companyID == company.companyID).ToList();

                TempData["CompanySectors"] = db.CompanySectors.Where(x => x.companyID == company.companyID).ToList();

                if (notification != null)
                {
                    InstantNotification instantNotification = db.InstantNotifications.Where(x => x.notificationID == notification).FirstOrDefault();
                    TempData["InstantNotification"] = instantNotification.notificationText;
                }

                //switch (notification)
                //{
                //    case null:
                //        break;
                //    case 1:
                //        ViewBag.Message = "SoftwareAddingSucceeded";
                //        break;
                //    case 2:
                //        ViewBag.Message = "SoftwareAddingFailure";
                //        break;
                //    case 3:
                //        ViewBag.Message = "SoftwareEditingSucceeded";
                //        break;
                //    case 4:
                //        ViewBag.Message = "SoftwareEditingFailure";
                //        break;
                //    case 5:
                //        ViewBag.Message = "BranchAddingSucceeded";
                //        break;
                //    case 6:
                //        ViewBag.Message = "BranchAddingFailure";
                //        break;
                //    case 7:
                //        ViewBag.Message = "BranchEditingSucceeded";
                //        break;
                //    case 8:
                //        ViewBag.Message = "BranchEditingFailure";
                //        break;
                //    case 9:
                //        ViewBag.Message = "AccountInformationsChangingSucceeded";
                //        break;
                //    case 10:
                //        ViewBag.Message = "AccountInformationsChangingFailure";
                //        break;
                //    case 11:
                //        ViewBag.Message = "PasswordChangingSucceeded";
                //        break;
                //    case 12:
                //        ViewBag.Message = "PasswordChangingFailure";
                //        break;
                //    case 13:
                //        ViewBag.Message = "DeleteFromFavoritesSucceeded";
                //        break;
                //    case 14:
                //        ViewBag.Message = "DeleteFromFavoritesFailure";
                //        break;
                //    case 15:
                //        ViewBag.Message = "DeleteFromFollowsSucceeded";
                //        break;
                //    case 16:
                //        ViewBag.Message = "DeleteFromFollowsFailure";
                //        break;

                //    default:
                //        break;
                //}

                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DeleteFromFollows(int id)
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

            return RedirectToAction("CompanyProfile", "User", new { id = TemporaryUserData.OnlineUserID });
        }

        public ActionResult DeleteFromFavorites(int id)
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

            return RedirectToAction("CompanyProfile", "User", new { id = TemporaryUserData.OnlineUserID });
        }


        [HttpPost]
        public ActionResult AddSoftwaresToCompany(int id, FormCollection frm, HttpPostedFile image)
        {
            if (Session["OnlineKullanici"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            //Güvenlik koşulu oluştur
            else if (TemporaryUserData.OnlineUserID == id)
            {
                /// ToDo:
                ///
                /// Kullanıcıyı Oturum düşmesi, sayfanın kapanması, internetin kesilmesi, yanlış bilgi girilmesi durumunda kaybedeceği giriş bilgileri için uyar
                /// 
                ///
                string addingSoftwareName = frm["s_name"];
                try
                {
                    Software softwareNameCheck = db.Softwares.Where(x => x.softwareName == addingSoftwareName).FirstOrDefault();

                    if (softwareNameCheck != null)
                    {
                        return RedirectToAction("CompanyProfile", "User", new { id = TemporaryUserData.OnlineUserID, notification = 2 });
                    }
                    else
                    {


                        Software s = db.Softwares.OrderByDescending(x => x.softwareID).FirstOrDefault();
                        SoftwareWorkPlatform softWP = db.SoftwareWorkPlatforms.OrderByDescending(x => x.swpID).FirstOrDefault();
                        SoftwareIsFree sif = db.SoftwareIsFrees.OrderByDescending(x => x.sifID).FirstOrDefault();

                        int sID, swpID, sifID;
                        if (s == null)
                        { sID = 1; }
                        else
                        { sID = s.softwareID + 1; }

                        if (softWP == null)
                        { swpID = 1; }
                        else
                        { swpID = softWP.swpID + 1; }

                        if (sif == null)
                        { sifID = 1; }
                        else
                        { sifID = sif.sifID + 1; }

                        Software software = new Software();
                        SoftwareWorkPlatform softwareWorkPlatform = new SoftwareWorkPlatform();
                        SoftwareIsFree softwareIsFree = new SoftwareIsFree();

                        if (image != null)
                        {
                            software.softwareImg = new byte[image.ContentLength];
                            image.InputStream.Read(software.softwareImg, 0, image.ContentLength);
                        }
                        else
                        {
                            //hazır yap
                        }

                        //Software Area
                        software.accountID = TemporaryUserData.OnlineUserID;
                        software.softwareID = sID;
                        software.softwareName = frm["s_name"];
                        software.description = frm["s_description"];
                        software.softwareVersion = frm["s_version"];
                        software.softwareCreatedDate = DateTime.Now;
                        software.softwareImg = null;

                        if (software.softwareName == "" || software.description == "" || software.softwareVersion == "")
                        {
                            return RedirectToAction("CompanyProfile", "User", new { id = TemporaryUserData.OnlineUserID, notification = 2 });
                        }

                        //SoftwareWorkPlatform Area
                        #region checboxStrings
                        string chk_android = frm["chk_android"];
                        string chk_desktopApp = frm["chk_desktopApp"];
                        string chk_windows = frm["chk_windows"];
                        string chk_pardus = frm["chk_pardus"];
                        string chk_linux = frm["chk_linux"];
                        string chk_ios = frm["chk_ios"];
                        string chk_unix = frm["chk_unix"];
                        string chk_macOS = frm["chk_macOS"];
                        string chk_webApp = frm["chk_webApp"];
                        string chk_other = frm["chk_other"];

                        string chk_demo = frm["chk_demo"];
                        string chk_fullVersion = frm["chk_fullVersion"];
                        string chk_beta = frm["chk_beta"];
                        string chk_free = frm["chk_free"];

                        #endregion

                        softwareWorkPlatform.softwareID = sID;
                        softwareWorkPlatform.isAndroid = Convert.ToBoolean(chk_android);
                        softwareWorkPlatform.isDesktopApp = Convert.ToBoolean(chk_desktopApp);
                        softwareWorkPlatform.isWindows = Convert.ToBoolean(chk_windows);
                        softwareWorkPlatform.isPardus = Convert.ToBoolean(chk_pardus);
                        softwareWorkPlatform.isLinux = Convert.ToBoolean(chk_linux);
                        softwareWorkPlatform.isIOS = Convert.ToBoolean(chk_ios);
                        softwareWorkPlatform.isUnix = Convert.ToBoolean(chk_unix);
                        softwareWorkPlatform.isMacOS = Convert.ToBoolean(chk_macOS);
                        softwareWorkPlatform.isWebApp = Convert.ToBoolean(chk_webApp);
                        softwareWorkPlatform.isOthers = Convert.ToBoolean(chk_other);
                        softwareWorkPlatform.swpID = swpID;

                        //SoftwareIsFree Area
                        softwareIsFree.softwareID = sID;
                        softwareIsFree.isDemo = Convert.ToBoolean(chk_demo);
                        softwareIsFree.isFullVersion = Convert.ToBoolean(chk_fullVersion);
                        softwareIsFree.isBeta = Convert.ToBoolean(chk_beta);
                        softwareIsFree.isFree = Convert.ToBoolean(chk_free);
                        softwareIsFree.sifID = sifID;


                        db.Softwares.Add(software);
                        db.SoftwareWorkPlatforms.Add(softwareWorkPlatform);
                        db.SoftwareIsFrees.Add(softwareIsFree);
                        db.SaveChanges();
                    }
                    return RedirectToAction("CompanyProfile", "User", new { id = TemporaryUserData.OnlineUserID, notification = 1 });

                }
                catch (Exception)
                {
                    return RedirectToAction("CompanyProfile", "User", new { id = TemporaryUserData.OnlineUserID, notification = 2 });
                    throw;
                }


            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult UpdateSoftwareCompany(int sID)
        {
            Software software = db.Softwares.Where(x => x.softwareID == sID).FirstOrDefault();
            Account account = db.Accounts.Where(x => x.accountID == TemporaryUserData.OnlineUserID).FirstOrDefault();

            if (software == null)
            {
                return RedirectToAction("CompanyProfile", "User", new { id = TemporaryUserData.OnlineUserID, notification = 4 });
            }
            else
            {
                if (TemporaryUserData.OnlineUserID == software.accountID)
                {
                    List<bool> chk_platformList = new List<bool>();
                    List<bool> chk_isFreeList = new List<bool>();
                    SoftwareIsFree sif = db.SoftwareIsFrees.Where(x => x.softwareID == software.softwareID).FirstOrDefault();
                    SoftwareWorkPlatform swp = db.SoftwareWorkPlatforms.Where(x => x.softwareID == software.softwareID).FirstOrDefault();


                    #region isFreeList
                    if (sif == null)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            chk_isFreeList.Add(false);
                        }
                    }
                    else
                    {
                        chk_isFreeList.Add(Convert.ToBoolean(sif.isDemo));
                        chk_isFreeList.Add(Convert.ToBoolean(sif.isFullVersion));
                        chk_isFreeList.Add(Convert.ToBoolean(sif.isBeta));
                        chk_isFreeList.Add(Convert.ToBoolean(sif.isFree));
                    }
                    #endregion

                    #region platformList
                    if (swp == null)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            chk_platformList.Add(false);
                        }
                    }
                    else
                    {
                        chk_platformList.Add(Convert.ToBoolean(swp.isWindows));
                        chk_platformList.Add(Convert.ToBoolean(swp.isLinux));
                        chk_platformList.Add(Convert.ToBoolean(swp.isUnix));
                        chk_platformList.Add(Convert.ToBoolean(swp.isPardus));
                        chk_platformList.Add(Convert.ToBoolean(swp.isAndroid));
                        chk_platformList.Add(Convert.ToBoolean(swp.isIOS));
                        chk_platformList.Add(Convert.ToBoolean(swp.isDesktopApp));
                        chk_platformList.Add(Convert.ToBoolean(swp.isWebApp));
                        chk_platformList.Add(Convert.ToBoolean(swp.isOthers));
                        chk_platformList.Add(Convert.ToBoolean(swp.isMacOS));
                    }

                    #endregion

                    TempData["UpdateSoftware"] = software;
                    TempData["chk_platformList"] = chk_platformList;
                    TempData["chk_isFreeList"] = chk_isFreeList;

                    return View();
                }
                else
                {
                    return RedirectToAction("CompanyProfile", "User", new { id = TemporaryUserData.OnlineUserID, notification = 4 });
                }
            }

        }

        [HttpPost]
        public ActionResult UpdateSoftwareCompany(int sID, FormCollection frm)
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetSoftwares()
        {
            var softwares = db.Softwares.Select(x => new
            {
                x.softwareName
            }).ToList();
            return Json(softwares, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult AddBranchToCompany(FormCollection frm)
        {
            /// ToDo:
            /// 
            /// 
            /// Güvenlik koşulu oluştur
            /// Aynı isimde şube var mı kontrol et
            /// formdan gelen verileri çekip db'e at
            /// Yanlış formatta veri girilirse tekrar deneme ekranı göster
            /// Doğru formatta şube oluşturulursa başarıyla eklendi ekranı göster
            ///
            ///
            return RedirectToAction("CompanyProfile", "User");
        }

        public ActionResult AdminProfile(int id)
        {
            if (Session["OnlineKullanici"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else if (TemporaryUserData.OnlineUserID == id)
            {
                TempData["Admin"] = db.Accounts.Where(x => x.accountID == id && x.roleID <= 2).FirstOrDefault();

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}