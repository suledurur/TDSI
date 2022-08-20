using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDSI_TurkiyeYerliYazilimEnvanteri.Models;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace TDSI_TurkiyeYerliYazilimEnvanteri.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        TDSIEntities db = new TDSIEntities();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection frm)
        {
            string loginName = frm["loginName"];
            string password = frm["password"];
            string hashedId = ComputeSha256Hash(loginName);
            string hashedPW = ComputeSha256Hash(frm["password"]);

            //Test işlemleri bittikten sonra Linq'daki x.loginPassword == pasword kısmı kaldırılacak.
            //
            Account account = db.Accounts.Where(x => x.loginName == loginName && (x.loginPassword == hashedPW + hashedId || x.loginPassword == password)).FirstOrDefault();

            if (account != null)
            {
                Session["OnlineKullanici"] = account.loginName;
                TemporaryUserData.OnlineUserID = account.accountID;
                account.accountLastSeen = DateTime.Now;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(FormCollection frm)
        {
            Account account1 = db.Accounts.OrderByDescending(x => x.accountID).FirstOrDefault();
            Company company1 = db.Companies.OrderByDescending(x => x.companyID).FirstOrDefault();
            MailActivation ma1 = db.MailActivations.OrderByDescending(x => x.maID).FirstOrDefault();
            int accountID = account1.accountID + 1;
            int companyID = company1.companyID + 1;
            int maID = ma1.maID + 1;
            string check = frm["individualLogin"];
            if (check == "1")
            {
                string accountName = frm["accountName"];
                string loginName = frm["loginName"];
                Account account = db.Accounts.Where(x => x.accountName == accountName || x.loginName == loginName).FirstOrDefault();
                if (account == null)
                {
                    //Hesabı oluşturma
                    account = new Account();
                    account.accountID = accountID;
                    account.loginName = loginName;
                    account.accountName = accountName;
                    account.accountMail = frm["email"];
                    account.accountTelNum = frm["tel"];
                    string hashedId = ComputeSha256Hash(loginName);
                    string hashedPW = ComputeSha256Hash(frm["password"]);
                    account.loginPassword = hashedPW + hashedId;
                    account.roleID = 3;
                    account.accountDescription = "";
                    account.accountCreated = DateTime.Now;
                    account.accountLastSeen = DateTime.Now;
                    account.isActive = true;

                    db.Accounts.Add(account);
                    db.SaveChanges();

                    string actCode = activationCode();
                    sendcode(frm["email"], accountName, actCode);

                    //Mail Onay Tablosu
                    MailActivation mailAct = new MailActivation();
                    mailAct.maID = maID;
                    mailAct.accountID = accountID;
                    mailAct.isVerified = false;
                    mailAct.validationDate = DateTime.Now.AddDays(2);
                    mailAct.activationCode = actCode;


                    db.MailActivations.Add(mailAct);
                    db.SaveChanges();

                    Session["OnlineKullanici"] = account.accountName;
                    TemporaryUserData.OnlineUserID = account.accountID;
                    return RedirectToAction("Activation", "Login");
                }
                else
                {
                    // Bu isimde bir kullanıcı bulunmaktadır.
                }
            }
            else if (frm["companyLogin"] == "1")
            {
                string loginNameComp = frm["loginNameComp"];
                string accountNameComp = frm["accountNameComp"];
                Account account = db.Accounts.Where(x => x.accountName == accountNameComp || x.loginName == loginNameComp).FirstOrDefault();
                Company company;
                if (account == null)
                {
                    //Aktivasyona gönder
                    account = new Account();
                    account.loginName = loginNameComp;
                    account.accountID = accountID;
                    account.accountName = accountNameComp;
                    account.accountMail = frm["emailComp"];
                    account.accountTelNum = "";
                    string hashedId = ComputeSha256Hash(loginNameComp);
                    string hashedPW = ComputeSha256Hash(frm["passwordComp"]);
                    account.loginPassword = hashedPW + hashedId;
                    account.roleID = 5;
                    account.accountDescription = "";
                    account.accountCreated = DateTime.Now;
                    account.accountLastSeen = DateTime.Now;
                    account.isActive = true;
                    db.Accounts.Add(account);
                    db.SaveChanges();

                    string actCode = activationCode();
                    sendcode(frm["emailComp"], accountNameComp, actCode);

                    company = new Company();
                    company.companyAddress = "";
                    company.companyWebSiteLink = "";
                    company.companyTelNum = frm["telComp"];
                    company.isStationExist = false;
                    company.taxNumber = frm["taxNum"];
                    company.accountID = account.accountID;
                    company.companyID = companyID;

                    db.Companies.Add(company);
                    db.SaveChanges();

                    MailActivation mailAct = new MailActivation();
                    mailAct.maID = maID;
                    mailAct.accountID = accountID;
                    mailAct.isVerified = false;
                    mailAct.validationDate = DateTime.Now.AddDays(2);
                    mailAct.activationCode = actCode;

                    db.MailActivations.Add(mailAct);
                    db.SaveChanges();

                    Session["OnlineKullanici"] = account.accountName;
                    TemporaryUserData.OnlineUserID = account.accountID;
                    return RedirectToAction("Activation", "Login");
                }
                else
                {
                    // Bu isimde bir kullanıcı bulunmaktadır.
                }
            }
            else
            {
                Console.WriteLine("Error");
            }



            return View();
        }

        public ActionResult Logout()
        {
            Session["OnlineKullanici"] = null;
            TemporaryUserData.OnlineUserID = 0;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Activation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Activation(FormCollection frm)
        {
            //Account acc = db.Accounts.Where(x => x.accountID == TemporaryUserData.OnlineUserID).FirstOrDefault();
            MailActivation ma = db.MailActivations.Where(x => x.accountID == TemporaryUserData.OnlineUserID).FirstOrDefault();

            if (ma.isVerified == false)
            {
                //DateTime validateTime = ma.validationDate;
                if (DateTimeOffset.Compare(ma.validationDate, DateTime.Now) >= 0)
                {
                    if (ma.activationCode == frm["activationCode"])
                    {
                        //aktif et

                        ma.isVerified = true;
                        ma.verifiedDate = DateTime.Now;

                        db.SaveChanges();

                        //Onaylandı ekranına gönder
                        return RedirectToAction("EmailVerified", "Login");
                    }
                    else
                    {
                        //kod yanlış
                        //Tekrar dene ekranı
                        return RedirectToAction("RetryActivation", "Login");
                    }
                }
                else
                {
                    // tarih geçmiş
                    // Eski kod, yeni mail gönder ekranı
                    return RedirectToAction("InvalidDate", "Login");

                }
            }
            else
            {
                // zaten aktif
                return RedirectToAction("Index", "Home");
            }


            //return RedirectToAction("Index","Home");
        }

        public ActionResult EmailVerified()
        {
            return View();
        }

        public ActionResult RetryActivation()
        {
            return View();
        }

        public ActionResult InvalidDate()
        {
            return View();
        }

        private string activationCode()
        {
            Random rnd = new Random();
            int intCode = rnd.Next(100000, 999999);
            string code = Convert.ToString(intCode);
            return code;
        }
        private void sendcode(string userMail, string userName, string activationCode)
        {
            var fromAddress = new MailAddress("no.reply.deneme@gmail.com", "Team TDSI");
            var toAddress = new MailAddress(userMail, "To Name");
            const string fromPassword = "deneme.123";
            const string subject = "Activation Code to Verify Email Address";
            string body = "Dear " + userName + ", Your Activation Code is: " + activationCode + "\n\n\nThanks & Regards\nTeam TDSI";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }


        }
        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }

        }

    }
}


