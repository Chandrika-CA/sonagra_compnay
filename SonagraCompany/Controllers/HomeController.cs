using Microsoft.AspNetCore.Mvc;
using SonagraCompany.Models;
using System.Diagnostics;

namespace SonagraCompany.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Title"] = "About Sonagra & Company";
            ViewData["Description"] = "Learn about Sonagra & Company, a corporate governance, compliance and business advisory firm based in Morbi, Gujarat.";
            return View();
        }

        public IActionResult Sectors()
        {
            ViewData["Title"] = "Sectors We Serve | Sonagra & Company";
            ViewData["Description"] = "Explore the sectors supported by Sonagra & Company through practical corporate governance, compliance and business advisory services.";
            return View();
        }

        public IActionResult Careers()
        {
            ViewData["Title"] = "Careers | Sonagra & Company";
            ViewData["Description"] = "Explore career opportunities with Sonagra & Company in corporate secretarial, legal, tax and design roles.";
            return View();
        }

        [Route("our-services/formation-of-business-entities")]
        public IActionResult FormationOfBusinessEntities()
        {
            ViewData["Title"] = "Formation of Business Entities | Sonagra & Company";
            ViewData["Description"] = "Business entity formation and company registration support from Sonagra & Company for startups, promoters and growing businesses.";
            return View();
        }

        [Route("our-services/company-law-llp-compliances")]
        public IActionResult CompanyLawLlpCompliances()
        {
            ViewData["Title"] = "Company Law & LLP Compliances | Sonagra & Company";
            ViewData["Description"] = "Company law, LLP compliance, board governance, statutory records, audits and regulatory support from Sonagra & Company.";
            return View();
        }

        [Route("our-services/corporate-law-and-advisory")]
        public IActionResult CorporateLawAndAdvisory()
        {
            ViewData["Title"] = "Corporate Law and Advisory | Sonagra & Company";
            ViewData["Description"] = "Corporate law and advisory support from Sonagra & Company for businesses, promoters and management teams.";
            return View();
        }

        [Route("our-services/sebi-matters-and-ipo-services")]
        public IActionResult SebiMattersAndIpoServices()
        {
            ViewData["Title"] = "SEBI Matters and IPO Services | Sonagra & Company";
            ViewData["Description"] = "SEBI, IPO, capital markets and securities compliance support from Sonagra & Company.";
            return View();
        }

        [Route("our-services/fema-matters")]
        public IActionResult FemaMatters()
        {
            ViewData["Title"] = "FEMA Matters | Sonagra & Company";
            ViewData["Description"] = "FEMA, FDI, foreign investment, overseas entity and foreign exchange compliance support from Sonagra & Company.";
            return View();
        }

        [Route("our-services/intellectual-property-rights-trademark-tm")]
        public IActionResult IntellectualPropertyRightsTrademark()
        {
            ViewData["Title"] = "Intellectual Property Rights - Trademark (TM) | Sonagra & Company";
            ViewData["Description"] = "Trademark search, registration, prosecution, assignment and ownership transfer support from Sonagra & Company.";
            return View();
        }

        [Route("our-services/amalgamation-merger-de-merger-takeover")]
        public IActionResult AmalgamationMergerDeMergerTakeover()
        {
            ViewData["Title"] = "Amalgamation, Merger, De-merger & Takeover | Sonagra & Company";
            ViewData["Description"] = "Amalgamation, merger, de-merger, takeover, restructuring and corporate transaction support from Sonagra & Company.";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ValidateCareerApplication([FromForm] CareerApplicationRequest application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Please enter a valid 10-digit phone number and complete the required fields." });
            }

            return Ok(new { message = "Phone number validation passed. Application saving and notifications will be added after client confirmation." });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Title"] = "Contact Sonagra & Company in Morbi";
            ViewData["Description"] = "Contact Sonagra & Company in Morbi, Gujarat for company law compliance, ROC filing, governance and corporate advisory support.";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
