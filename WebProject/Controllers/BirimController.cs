using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class BirimController : Controller
    {
        Context c = new Context();
        public IActionResult Index()
        {
            var degerler = c.Birims.ToList();
            return View(degerler);
        }
        [HttpGet]
        public IActionResult YeniBirim()
        {
            return View();
        }

        [HttpPost]
        public IActionResult YeniBirim(Birim b)
        {
            c.Birims.Add(b);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult BirimSil(int id)
        {
            var idbul = c.Birims.Find(id);
            c.Birims.Remove(idbul);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult BirimGetir(int id)
        {
            var dep = c.Birims.Find(id);
            return View("BirimGetir", dep);
        }

        public IActionResult BirimGuncelle(Birim b)
        {
            var dep = c.Birims.Find(b.BirimID);
            dep.BirimAd = b.BirimAd;
            // dep.depId = d.depId;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult BirimDetay(int id)
        {
            var degerler = c.Personels.Where(x => x.BirimID == id).ToList();
            //x öyle bir degerki int id den gelen degeri al ve personellerde birimid si bu olanları liste şeklinde getir
            var birmad = c.Birims.Where(x=>x.BirimID==id).Select(y=>y.BirimAd).FirstOrDefault(); //firstordefault bir deger döndürüyor
            ViewBag.birmd = birmad;
            return View(degerler);
        }
    }
}
