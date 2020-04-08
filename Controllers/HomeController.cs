using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TalkToStudentsBack.Models;
using TalkToStudentsBack.Services;
using System.Text;

namespace TalkToStudentsBack.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGithubService _ghService;

        public HomeController(ILogger<HomeController> logger, IGithubService ghService)
        {
            _logger = logger;
            _ghService = ghService;
        }


        [HttpGet("{*path}")]
        public async Task<IActionResult> Index(string path)
        {            
            var file = await _ghService.GetFileFromGithub("lunet-io", "markdig", string.IsNullOrEmpty(path) ? "readme.md" : path);
            
            if (file==null)
            {
                return NotFound();
            }
            if (file.IsMd)
            {
                var converted = new MdConverted { MdHtml = (await _ghService.RepoMdToHtml(file)) };
                return View(converted);
            }
            return File(await _ghService.RepoFileStream(file), "application/octet-stream", file.Name);
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
}
