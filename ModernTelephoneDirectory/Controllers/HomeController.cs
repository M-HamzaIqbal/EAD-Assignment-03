using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModernTelephoneDirectory.Models;

namespace ModernTelephoneDirectory.Controllers
{
    public class HomeController : Controller
    {

        DataClasses1DataContext dc = new DataClasses1DataContext();

        public ActionResult Index()
        {
            IQueryable<string> cities_list = dc.contacts.Select(p => p.City).Distinct();
            string selectedCity = Request["SelectedCity"];
            var s = dc.contacts.ToList();
            if (selectedCity != "all" && selectedCity != null)
            {
               s = dc.contacts.Where(sd => sd.City == selectedCity).ToList();
            }

            ViewBag.cities = cities_list.ToList();
            return View(s);
        }

        public ActionResult AddRecord()
        {
            return View();
        }
        public ActionResult Add()
        {
            string Name = Request["name"];
            string Phone = Request["phone"];
            string Email = Request["email"];
            string City = Request["city"];

            contact p = new contact();
            p.Name = Name;
            p.Phone = Phone;
            p.City = City;
            p.Email = Email;

            dc.contacts.InsertOnSubmit(p);
            dc.SubmitChanges();

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public ActionResult EditContact(int id)
        {

            return View(dc.contacts.First(x => x.Id == id));
        }
        public ActionResult Edit(int id)
        {
            var s = dc.contacts.First(x => x.Id == id);
            s.Name = Request["name"];
            s.Phone = Request["phone"];
            s.Email = Request["email"];
            s.City = Request["city"];
            dc.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteContact(int id)
        {
            var s = dc.contacts.First(x => x.Id == id);
            dc.contacts.DeleteOnSubmit(s);
            dc.SubmitChanges();
            return RedirectToAction("Index");
        }

       
    }
}