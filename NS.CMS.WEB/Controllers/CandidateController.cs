using PagedList;
using NS.CMS.Model;
using PagedList.Mvc;
using NS.CMS.Buisness;
using Microsoft.AspNetCore.Mvc;

namespace NS.CMS.WEB.Controllers;

public class CandidateController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICandidateBuisness _ICandidateBuisness;
    private readonly IWebHostEnvironment _hostEnvironment;

    public CandidateController(ILogger<HomeController> logger, ICandidateBuisness iCandidateBuisness, IWebHostEnvironment hostEnvironment)
    {
        _logger = logger;
        _ICandidateBuisness = iCandidateBuisness;
        this._hostEnvironment = hostEnvironment;
    }
   
    // Index
    public IActionResult Index(string sort, string searchData)
    {
      if (searchData != null)
      {
        var resSearch = _ICandidateBuisness.GetAllCandidates();
        var resSearching = resSearch.Where(stu => stu.Name.ToUpper().Contains(searchData.ToUpper()));
        if (sort == "Id")
        {
            //var resSort = _ICandidateBuisness.GetAllCandidates();
            var resSorting = resSearching.OrderBy(s => s.Id);
            return View(resSorting.ToList());
        }
        if (sort == "Name")
        {
            //var resSort = _ICandidateBuisness.GetAllCandidates();
            var resSorting = resSearching.OrderBy(s => s.Name);
            Console.WriteLine(resSorting.ToList()[0].Name);
            return View(resSorting.ToList());
        }
        return View(resSearching.ToList());
      }

      //For Sorting
      if (sort == "Id")
      {   
        var resSort = _ICandidateBuisness.GetAllCandidates();
        var resSorting = resSort.OrderBy(s => s.Id);
        return View(resSorting.ToList());
      }
      if (sort == "Name")
      { 
        var resSort = _ICandidateBuisness.GetAllCandidates();
        var resSorting = resSort.OrderBy(s => s.Name);
        Console.WriteLine(resSorting.ToList()[0].Name);
        return View(resSorting.ToList());
      }
      var res = _ICandidateBuisness.GetAllCandidates();
      return View(res);
    }

    // Details Of A Candidate
    public IActionResult Details(int id)
    {
      var res = _ICandidateBuisness.GetCandidateById(id);
      return View(res);
    }

    ///////// Add A New Candidate ///////////////
    [HttpGet]
    public IActionResult Create() {
      return View();
    }

    [HttpPost]
    public IActionResult Create(CandidateModel candidateModel)
    {
      var wwwrootPath = _hostEnvironment.WebRootPath;
      var res = _ICandidateBuisness.AddCandidate(candidateModel,wwwrootPath);
      var result = _ICandidateBuisness.GetAllCandidates();
      return RedirectToAction("Index");
    }
    ///////////////////////////////////////////////// 

    //////// Edit A Candidate ///////
    public IActionResult Edit(int id)
    {
      return View(_ICandidateBuisness.GetCandidateById(id));
    }

    [HttpPost]
    public IActionResult Edit(CandidateModel candidateModel)
    {
      var wwwrootPath = _hostEnvironment.WebRootPath;
      var res = _ICandidateBuisness.Edit(candidateModel,wwwrootPath);
      var result = _ICandidateBuisness.GetAllCandidates();
      return RedirectToAction("Index");
    } 
    ////////////////////////////////////


    ///// Delete A Candidate //////
    public IActionResult Delete(int id,CandidateModel candidateModel)
    {
      var res = _ICandidateBuisness.Delete(id, candidateModel);
      var result = _ICandidateBuisness.GetAllCandidates();
      return RedirectToAction("Index");
    }
    ///////////////////////////////////

    // Download Resume
    public IActionResult DownloadResume(string filePath) 
    {
      string path = Path.Combine(_hostEnvironment.WebRootPath, "resume/", filePath);
      byte[] fileBytes = System.IO.File.ReadAllBytes(path);
      string fileName = "myfile.pdf";
      return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
    }
    
}
