using BankApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers
{
    public class BankController : Controller
    {
        private Account account = new Account
        {
            accountNumber = 1001,
            accountHolderName = "Example Name",
            currentBalance = 5000
        };

        [Route("/")]
        public IActionResult Index()
        {
            return Content("Welcome to the Best Bank", "text/plain");
        }

        [Route("/account-details")]
        public IActionResult AccountDetails()
        {
            return Json(account);
        }


        [Route("/account-statement")]
        public IActionResult AccountStatement()
        {
            return File("/Sample.pdf", "application/pdf");
        }

        [Route("/get-current-balance/{accountNumber:int?}")]
        public IActionResult GetCurrentBalance()
        {
            if(!Request.RouteValues.ContainsKey("accountNumber"))
            {
                return NotFound("Account Number should be supplied");
            }
            if (Convert.ToInt32(Request.RouteValues["accountNumber"]) != account.accountNumber)
            {
                return BadRequest("Account Number should be 1001");            
            }
            return Content($"{account.currentBalance}", "text/plain");

        }
    }
}
