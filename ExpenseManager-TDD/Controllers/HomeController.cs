using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ExpenseManager_TDD.Models;
using Microsoft.VisualBasic;

namespace ExpenseManager_TDD.Controllers;

public class HomeController : Controller
{

    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult DonutGraph()
    {
        return View();
    }

    [HttpPost]
    public List<object> GetExpenseData()
    {
        List<object> data = new List<object>();

        List<int> labels = _context.Transactions.Select(p =>p.Amount).ToList();
        data.Add(labels);
        List<DateTime> date = _context.Transactions.Select(p => p.Date).ToList();
        data.Add(date);
        return data;
    }
        
}
