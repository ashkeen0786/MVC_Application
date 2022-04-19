using Application.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Application.Controllers
{
  
    public class HomeController : Controller
    {
      public ActionResult first()
        {
            return View();
        }


        [HttpGet]

       
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
     
        public ActionResult Index(usertable obj)

        {
            applicationEntities2 obj2 = new applicationEntities2();

            var log = obj2.usertables.Where(m => m.name.ToLower() == obj.name.ToLower()).FirstOrDefault();
            if (log == null)
            {
                TempData["wrong"] = "username not found";
                return View();
            }
            else
            {
                if(log.name==obj.name && log.password==obj.password){
                    FormsAuthentication.SetAuthCookie(log.name,false);
                    Session["name"] = log.name;
                   
                    return RedirectToAction("dashboard");

                }
                else
                {
                    TempData["invalid"] = "wrong password plz try again";
                    return View();
                }
            }
           

         
        }
        public ActionResult logout()
        {
            FormsAuthentication.SignOut();


            return RedirectToAction("index");
        }
        [Authorize]
        public ActionResult dashboard()
        {
            return View();
        }


        [Authorize]
        public ActionResult table()
        {
            applicationEntities2 dbobj = new applicationEntities2();
            var tab = dbobj.applicationtables.ToList();
            return View(tab);
        }
        [HttpGet]
        [Authorize]
        public ActionResult forms()
        {
            

            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult forms(applicationtable mdobj)
        {
            applicationEntities2 dbobj = new applicationEntities2();
            applicationtable tbobj = new applicationtable();
        
            tbobj.id = mdobj.id;
            tbobj.name = mdobj.name;
            tbobj.email = mdobj.email;
            tbobj.mobile = mdobj.mobile;
            tbobj.address = mdobj.address;
            if (mdobj.id==0) {
                dbobj.applicationtables.Add(tbobj);
                dbobj.SaveChanges();

            }
            else
            {
                dbobj.Entry(tbobj).State = System.Data.Entity.EntityState.Modified;
                dbobj.SaveChanges();

            }
           
            return RedirectToAction("table");




        }

        [Authorize]
        public ActionResult delete(int id)
        {
            applicationEntities2 dbobj = new applicationEntities2();
            var del = dbobj.applicationtables.Where(m => m.id == id).First();
            dbobj.applicationtables.Remove(del);
            dbobj.SaveChanges();
            return RedirectToAction("table");
        }
        [Authorize]
        public ActionResult edit(int id)
        {
            applicationEntities2 dbobj = new applicationEntities2();
            var ed = dbobj.applicationtables.Where(m => m.id == id).First();
            applicationtable tbobj = new applicationtable();
            tbobj.id = ed.id;
            tbobj.name = ed.name;
            tbobj.email = ed.email;
            tbobj.mobile = ed.mobile;
            tbobj.address = ed.address;

            return View("forms",tbobj);
        }

        public ActionResult sign(usertable clobj)

        {
            applicationEntities2 dbobj = new applicationEntities2();
            usertable tbobj = new usertable();
          
            tbobj.name = clobj.name;
            tbobj.eamil = clobj.eamil;
            tbobj.mobile = clobj.mobile;
            tbobj.password = clobj.password;
         
            
                dbobj.usertables.Add(tbobj);
                dbobj.SaveChanges();
            
           
            return View();
        }

    }
}