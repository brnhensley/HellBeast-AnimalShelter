using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;
using System;

namespace AnimalShelter.Controllers
{
  public class HellBeastController : Controller
  {

    [HttpGet("/beasts")]
    public ActionResult Index()
    {
      List<HellBeast> allHellBeasts = HellBeast.GetAll();
      return View(allHellBeasts);
    }

    [HttpGet("/beasts/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/beasts")]
    public ActionResult Create(string name, string origin, string location, int id)
    {
      HellBeast newHellBeast = new HellBeast(name, origin, location, id);
      newHellBeast.Save();
      return RedirectToAction("Index");
    }

    [HttpGet("/beasts/{id}")]
    public ActionResult Show(int id)
    {
      HellBeast hellbeast = HellBeast.Find(id);
      return View(hellbeast);
    }

    // [HttpPost("/beasts/{id}/delete")]
    // public ActionResult Destroy(int id)
    // {
    //   HellBeast.RemoveHellBeast(id);
    //   return RedirectToAction("Index");
    // }
  }
}
