using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using SonagraCompany.Services;

namespace SonagraCompany.Controllers
{
    /// <summary>robots.txt and sitemap.xml for search engines.</summary>
    public class SeoController : Controller
    {
        private readonly IConfiguration _configuration;

        public SeoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string BaseUrl()
        {
            var configured = _configuration["Seo:BaseUrl"];
            return !string.IsNullOrWhiteSpace(configured)
                ? configured.TrimEnd('/')
                : $"{Request.Scheme}://{Request.Host}";
        }

        [HttpGet("robots.txt")]
        public IActionResult Robots()
        {
            var text = new StringBuilder()
                .AppendLine("User-agent: *")
                .AppendLine("Allow: /")
                .AppendLine("Disallow: /Home/Error")
                .AppendLine()
                .AppendLine($"Sitemap: {BaseUrl()}/sitemap.xml")
                .ToString();
            return Content(text, "text/plain", Encoding.UTF8);
        }

        [HttpGet("sitemap.xml")]
        public IActionResult Sitemap()
        {
            var baseUrl = BaseUrl();
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            var urlset = new XElement(ns + "urlset");

            foreach (var page in SeoCatalog.Pages)
            {
                urlset.Add(Entry(ns, baseUrl + (page.Key == "/" ? "/" : page.Key), null, page.Value.ChangeFrequency, page.Value.Priority));
            }

            foreach (var post in BlogCatalog.Posts)
            {
                urlset.Add(Entry(ns, $"{baseUrl}/blogs/{post.Slug}", post.Published.ToString("yyyy-MM-dd"), "monthly", 0.7));
            }

            var doc = new XDocument(new XDeclaration("1.0", "utf-8", null), urlset);
            return Content(doc.Declaration + Environment.NewLine + doc.Root, "application/xml", Encoding.UTF8);
        }

        private static XElement Entry(XNamespace ns, string loc, string? lastmod, string changefreq, double priority)
        {
            var url = new XElement(ns + "url", new XElement(ns + "loc", loc));
            if (lastmod is not null)
            {
                url.Add(new XElement(ns + "lastmod", lastmod));
            }

            url.Add(new XElement(ns + "changefreq", changefreq));
            url.Add(new XElement(ns + "priority", priority.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)));
            return url;
        }
    }
}
