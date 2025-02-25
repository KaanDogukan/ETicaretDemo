using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ETicaretDemo.Models;
using System.Data.SqlClient;
using System.Data;

namespace ETicaretDemo.Controllers;

public class HomeController : Controller
{
    SqlConnection connection = new SqlConnection("Server=(localdb)\\MSSQLLocalDb; Database=ETicaretDemoDb; Integrated Security=true");

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
        SqlDataAdapter adapter = new SqlDataAdapter("select * from Urunler", connection);
        DataTable dataTable = new DataTable();
        dataTable.Clear();
        adapter.Fill(dataTable);

        List<Urunler> urunlerim = new List<Urunler>();

        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            Urunler urunler = new Urunler();
            urunler.Id = Convert.ToInt32(dataTable.Rows[i]["Id"].ToString());
            urunler.UrunAdi = dataTable.Rows[i]["UrunAdi"].ToString();
            urunler.Resim = dataTable.Rows[i]["Resim"].ToString();
            urunler.BirimFiyati = Convert.ToDecimal(dataTable.Rows[i]["BirimFiyati"].ToString());
            urunlerim.Add(urunler);
        }
        return View(urunlerim);
    }

}
