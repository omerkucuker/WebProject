using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class PersonelimController : Controller
    {
        Context c = new Context();

        [Authorize]
        public IActionResult Index()
        {
            var degerler = c.Personels.Include(x=>x.Birim).ToList();//bunu da tam anlamadım
            return View(degerler);
        }

        [HttpGet]
        public IActionResult YeniPersonel()
        {
            List<SelectListItem> degerler = (from x in c.Birims.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.BirimAd,
                                                 Value = x.BirimID.ToString()
                                             }
                                              ).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public IActionResult YeniPersonel(Personel p)
        {
            var per = c.Birims.Where(x => x.BirimID == p.Birim.BirimID).FirstOrDefault(); //burayı tam anlamadım
            //Veri tabanındaki Birim tablosunda BirimId si parametre olarak gelen p nin birimindeki birim idsi olanları getir.
            p.Birim = per;
            c.Personels.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult PersonelSil(int id)
        {
            var idbul = c.Personels.Find(id);
            c.Personels.Remove(idbul);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult PersonelGetir(int id)
        {
            var dep = c.Personels.Find(id);
            return View("PersonelGetir", dep);
        }

        public IActionResult PersonelGuncelle(Personel p)
        {
            var per = c.Personels.Find(p.PersonelID);
            per.Ad = p.Ad;
            per.Soyad = p.Soyad;
            // dep.depId = d.depId;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
