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

        List<int> amount = _context.Transactions.Select(p =>p.Amount).ToList();
        data.Add(amount);
        List<String> label =_context.Categories.Select(p => p.Title).ToList();
        data.Add(label);
        List<String> typeOfExpense = _context.Categories.Select(p => p.Type).ToList();
        data.Add(typeOfExpense);

        return data;
    }
        
}
