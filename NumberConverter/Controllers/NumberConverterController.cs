using Microsoft.AspNetCore.Mvc;
using NumberConverter.Services;

namespace NumberConverter.Controllers
{
    public class NumberConverterController : Controller
    {
        private readonly INumberToWordConverterService _numberToWordConverterService;

        public NumberConverterController(INumberToWordConverterService numberToWordConverterService)
        {
            _numberToWordConverterService = numberToWordConverterService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConvertNumberToWord(string input)
        {
            ViewBag.Input = input;
            ViewBag.ConvertedNumber = _numberToWordConverterService.ConvertNumberToWords(input);
            return View("Index");
        }
    }
}
