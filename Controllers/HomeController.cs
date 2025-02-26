using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ETicaretDemo.Models;
using System.Data.SqlClient;
using System.Data;
using ETicaretDemo.Dto;

namespace ETicaretDemo.Controllers;

public class HomeController : Controller
{
    //SqlConnection connection = new SqlConnection("Server=(localdb)\\MSSQLLocalDb; Database=ETicaretDemoDb; Integrated Security=true");

    Context con = new Context();


    // wwww.eticaret.com/api/Home/Index
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(Login login)
    {
        if (login.Email == "kaandgkn@gmail.com" && login.Password == "1")
        {
            return RedirectToAction("Home", "Home");
        }
        TempData["hata"] = "Hatalı giriş yaptınız";
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Home()
    {
        //SqlDataAdapter adapter = new SqlDataAdapter("select * from Urunler", connection);
        //DataTable dataTable = new DataTable();
        //dataTable.Clear();
        //adapter.Fill(dataTable);

        //List<Urun> urunlerim = new List<Urun>();

        //for (int i = 0; i < dataTable.Rows.Count; i++)
        //{
        //    Urun urunler = new Urun();
        //    urunler.Id = Convert.ToInt32(dataTable.Rows[i]["Id"].ToString());
        //    urunler.UrunAdi = dataTable.Rows[i]["UrunAdi"].ToString();
        //    urunler.Resim = dataTable.Rows[i]["Resim"].ToString();
        //    urunler.BirimFiyati = Convert.ToDecimal(dataTable.Rows[i]["BirimFiyati"].ToString());
        //    urunlerim.Add(urunler);
        //}


        List<Urun> urunlerim = new List<Urun>();
        urunlerim = con.Urunler.ToList();

        List<Sepet> sepetim = new List<Sepet>();
        sepetim = con.Sepetim.ToList();

        HomeDto list = new HomeDto();
        list.Urunlerim = urunlerim;
        list.Sepetim = sepetim;

        return View(list);
    }

    [HttpPost]
    public IActionResult SepeteEkle(int id, int adet)
    {
        Urun urun = con.Urunler.Where(x => x.Id == id).FirstOrDefault();

        Sepet sepet = new Sepet();
        sepet.UrunAdi = urun.UrunAdi;
        sepet.Adet = adet;
        sepet.BirimFiyati = urun.BirimFiyati;
        sepet.ToplamTutar = sepet.Adet * sepet.BirimFiyati;

        con.Sepetim.Add(sepet);
        con.SaveChanges();

        return RedirectToAction("Home", "Home");
    }

    [HttpPost]
    public IActionResult SepetUrunuSil(int id)
    {
        Sepet sepet = con.Sepetim.Where(x => x.Id == id).FirstOrDefault();
        con.Sepetim.Remove(sepet);
        con.SaveChanges();
        return RedirectToAction("Home", "Home");
    }

    [HttpPost]
    public IActionResult OdemeYap()
    {
        var result = con.Sepetim.ToList();
        foreach (var item in result)
        {
            con.Sepetim.Remove(item);
            con.SaveChanges();
        }
        TempData["odeme"] = "Odeme Başarı ile yapıldı";
        return RedirectToAction("Home", "Home");
    }


}
