﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestApp.Web.Models;

namespace TestApp.Web.Controllers
{
  /// <summary>
  /// 
  /// </summary>
  public class HomeController : Controller
  {
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
      return View();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IActionResult About()
    {
      ViewData["Message"] = "Your application description page.";

      return View();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IActionResult Contact()
    {
      ViewData["Message"] = "Your contact page.";

      return View();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
