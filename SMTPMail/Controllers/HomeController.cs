using System.Diagnostics;
using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;
using SMTPMail.Models;
using SMTPMail.Utils;

namespace SMTPMail.Controllers;

public class HomeController : Controller
{
    private readonly IMailSender _mailSender;
    public HomeController(IMailSender mailSender)
    {
        _mailSender = mailSender;
    }

    public async Task<IActionResult> Index([FromServices] IFluentEmail mailer)
    {



        IEnumerable<string> sendToEmails = new List<string>
            {
                "coommark@gmail.com",
                "coommark@yahoo.com"
            };
        _mailSender.SendSendgridBulk(sendToEmails);

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
}

