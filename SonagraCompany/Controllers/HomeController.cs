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

        public IActionResult Team()
        {
            ViewData["Title"] = "Our Team | Sonagra & Company";
            ViewData["Description"] = "Meet the professionals of Sonagra & Company – Company Secretaries, Chartered Accountants and legal experts supporting corporate governance and compliance.";
            return View();
        }

        public IActionResult Blogs()
        {
            ViewData["Title"] = "Blogs | Sonagra & Company";
            ViewData["Description"] = "Articles and updates from Sonagra & Company on corporate governance, company law, SEBI, FEMA and compliance.";
            return View();
        }

        [Route("our-services")]
        public IActionResult OurServices()
        {
            ViewData["Title"] = "Our Services | Sonagra & Company";
            ViewData["Description"] = "Explore the corporate, company law, SEBI, FEMA, governance and compliance services offered by Sonagra & Company.";
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

        [Route("our-services/rbi-and-nbfc-related-compliances")]
        public IActionResult RbiAndNbfcRelatedCompliances()
        {
            ViewData["Title"] = "RBI and NBFC Related Compliances | Sonagra & Company";
            ViewData["Description"] = "RBI and NBFC compliance support, regulatory filings and advisory services from Sonagra & Company.";
            return View();
        }

        [Route("our-services/transactional-due-diligence-services")]
        public IActionResult TransactionalDueDiligenceServices()
        {
            ViewData["Title"] = "Transactional Due Diligence Services | Sonagra & Company";
            ViewData["Description"] = "Transactional due diligence, compliance verification and secretarial certification support from Sonagra & Company.";
            return View();
        }

        [Route("our-services/corporate-governance-services")]
        public IActionResult CorporateGovernanceServices()
        {
            ViewData["Title"] = "Corporate Governance Services | Sonagra & Company";
            ViewData["Description"] = "Corporate governance advisory, board and committee support, policy framework development and compliance review services from Sonagra & Company.";
            return View();
        }

        [Route("our-services/nclt-services")]
        public IActionResult NcltServices()
        {
            ViewData["Title"] = "NCLT Services | Sonagra & Company";
            ViewData["Description"] = "NCLT petition filing and company law tribunal support for restructuring, restoration, oppression and mismanagement matters.";
            return View();
        }

        [Route("our-services/agreements-petitions")]
        public IActionResult AgreementsPetitions()
        {
            ViewData["Title"] = "Agreements and Petitions | Sonagra & Company";
            ViewData["Description"] = "Agreements and petitions advisory content based on corporate law and advisory service requirements.";
            return View();
        }

        [Route("our-services/liquidation-under-insolvency-and-bankruptcy-code-2016")]
        public IActionResult LiquidationUnderInsolvencyAndBankruptcyCode2016()
        {
            ViewData["Title"] = "Liquidation Under Insolvency and Bankruptcy Code 2016 | Sonagra & Company";
            ViewData["Description"] = "Liquidation process advisory support under IBC 2016 including compliance, documentation, filings and stakeholder coordination.";
            return View();
        }

        [Route("our-services/legal-metrology-act-2009")]
        public IActionResult LegalMetrologyAct2009()
        {
            ViewData["Title"] = "Legal Metrology Act, 2009 | Sonagra & Company";
            ViewData["Description"] = "Legal Metrology Act compliance, registration, documentation, reporting and regulatory liaison support.";
            return View();
        }

        [Route("our-services/xbrl-filing")]
        public IActionResult XbrlFiling()
        {
            ViewData["Title"] = "XBRL Filing | Sonagra & Company";
            ViewData["Description"] = "XBRL filing support including data input, conversion, validation, pre-scrutiny, certification and filing.";
            return View();
        }

        [Route("our-services/other-services")]
        public IActionResult OtherServices()
        {
            ViewData["Title"] = "Other Services | Sonagra & Company";
            ViewData["Description"] = "Other corporate support services including MSME registration, ISIN liaison, PAN/TAN, LEI, valuation and DSC renewals.";
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
