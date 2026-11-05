using System.Text.Json;

namespace SonagraCompany.Services
{
    public record SeoEntry(string Title, string Description, string Keywords, string ChangeFrequency = "monthly", double Priority = 0.7, bool IsService = false, string? ServiceName = null);

    /// <summary>
    /// One place for every page's search-engine title, description and keywords.
    /// Keys are lower-case request paths without a trailing slash ("/" is the home page).
    /// </summary>
    public static class SeoCatalog
    {
        public const string Brand = "Sonagra & Company";

        public static readonly IReadOnlyDictionary<string, SeoEntry> Pages = new Dictionary<string, SeoEntry>
        {
            ["/"] = new("Company Secretary in Morbi, Gujarat",
                "Sonagra & Company – company secretary firm in Morbi, Gujarat: company registration, ROC filing, LLP compliance, SEBI, FEMA, trademark and corporate governance.",
                "company secretary in Morbi, CS firm in Morbi, company registration in Morbi, ROC filing Gujarat, LLP compliance, corporate governance, SEBI consultant, FEMA consultant, trademark registration Morbi, corporate advisory Gujarat",
                "weekly", 1.0),

            ["/home/about"] = new("About Us – Company Secretary Firm Morbi",
                "About Sonagra & Company, a multi-disciplinary company secretary and corporate advisory firm in Morbi, Gujarat built on trust and confidentiality.",
                "about Sonagra & Company, company secretary firm Morbi, corporate advisory firm Gujarat, secretarial compliance, professional ethics, confidentiality policy",
                "monthly", 0.8),

            ["/home/sectors"] = new("Sectors We Serve – Corporate Advisory",
                "Corporate governance, compliance and business advisory for growing businesses, startups, promoters and family businesses in Morbi and across Gujarat.",
                "corporate advisory for startups, compliance for family business, company secretary for growing business, corporate services Gujarat, business advisory Morbi",
                "monthly", 0.6),

            ["/home/careers"] = new("Careers – Company Secretary Jobs in Morbi",
                "Explore careers at Sonagra & Company in Morbi, Gujarat: company secretary, legal, taxation and corporate compliance roles for professionals and freshers.",
                "company secretary jobs Morbi, CS jobs Gujarat, legal jobs Morbi, corporate compliance careers, CS trainee Gujarat, careers Sonagra & Company",
                "monthly", 0.5),

            ["/home/team"] = new("Our Team – Company Secretaries & Experts",
                "Meet the company secretaries, chartered accountants and legal experts at Sonagra & Company who handle ROC, SEBI, FEMA and corporate governance matters.",
                "company secretary team Morbi, CS Morbi, chartered accountant Morbi, corporate law experts Gujarat, Sonagra & Company team",
                "monthly", 0.5),

            ["/blogs"] = new("Company Law, ROC & Compliance Blog",
                "Practical articles on company registration, ROC annual filings, LLP, SEBI, FEMA and corporate governance for founders and businesses in Gujarat.",
                "company law blog, ROC compliance guide, company registration guide Gujarat, LLP vs private limited, FEMA FDI compliance, corporate governance articles, company secretary blog",
                "weekly", 0.8),

            ["/home/contact"] = new("Contact Company Secretary in Morbi",
                "Contact Sonagra & Company, 326 Star Arcade, Sanala Road, Morbi 363641 for company registration, ROC filing and corporate advisory. Call +91 75670 96966.",
                "contact company secretary Morbi, company secretary near me Morbi, CS office Morbi, ROC filing consultant Morbi phone, corporate consultant Morbi address",
                "monthly", 0.9),

            ["/home/privacy"] = new("Privacy Policy",
                "Privacy policy of Sonagra & Company explaining how information shared through the website and contact forms is collected, used and protected.",
                "privacy policy, data protection, Sonagra & Company privacy",
                "yearly", 0.2),

            ["/our-services"] = new("Company Secretary & Corporate Services",
                "Sonagra & Company services: company registration, ROC and LLP compliance, SEBI and IPO, FEMA, trademark, mergers, NCLT, XBRL and governance in Gujarat.",
                "company secretary services, corporate law services Gujarat, ROC filing services, company registration services, SEBI IPO services, FEMA services, trademark services, NCLT services, XBRL filing, secretarial audit",
                "weekly", 0.9),

            ["/our-services/formation-of-business-entities"] = new("Company Registration in Morbi, Gujarat",
                "Private limited company, LLP, OPC and Section 8 registration in Morbi, Gujarat: incorporation, MOA/AOA drafting and foreign company registration in India.",
                "company registration in Morbi, private limited company registration Gujarat, LLP registration Morbi, OPC registration, Section 8 company, foreign company registration India, business entity formation, incorporation consultant",
                "monthly", 0.9, true, "Formation of Business Entities"),

            ["/our-services/company-law-llp-compliances"] = new("ROC Filing & LLP Compliance in Gujarat",
                "Company law and LLP compliance in Gujarat: secretarial audit report, MGT-8 certification, directors' report, statutory registers and meeting documentation.",
                "ROC filing Gujarat, annual ROC compliance, LLP compliance Gujarat, company law compliance Morbi, secretarial audit report, MGT-8 certification, statutory registers, conversion into LLP, CSR advisory",
                "monthly", 0.9, true, "Company Law & LLP Compliances"),

            ["/our-services/corporate-law-and-advisory"] = new("Corporate Law Advisory Services Gujarat",
                "Corporate law advisory for boards and promoters: governance, policies, risk, CSR and stakeholder communication from a company secretary firm in Gujarat.",
                "corporate law advisory Gujarat, corporate law consultant, board advisory, company law advice, CSR advisory, legal risk management, corporate legal services Morbi",
                "monthly", 0.8, true, "Corporate Law and Advisory"),

            ["/our-services/sebi-matters-and-ipo-services"] = new("SEBI Compliance & IPO Consultant Gujarat",
                "SEBI LODR, ICDR, SAST, insider trading and delisting regulations plus IPO, rights, preferential and QIP issue advice from company secretaries in Gujarat.",
                "SEBI compliance consultant, IPO consultant Gujarat, SEBI LODR compliance, SEBI ICDR, SEBI SAST takeover, insider trading regulations, rights issue, preferential issue, QIP, delisting, capital market advisory",
                "monthly", 0.9, true, "SEBI Matters and IPO Services"),

            ["/our-services/fema-matters"] = new("FEMA & FDI Compliance Consultant India",
                "FEMA and FDI advisory for inbound and outbound investment: reporting filings, ECB, overseas joint ventures, branch offices and AD bank liaison.",
                "FEMA consultant India, FDI compliance, inbound investment, outbound investment, overseas joint venture, external commercial borrowings, foreign company branch office, AD bank liaison, FEMA reporting",
                "monthly", 0.9, true, "FEMA Matters"),

            ["/our-services/intellectual-property-rights-trademark-tm"] = new("Trademark Registration in Morbi, Gujarat",
                "Trademark search, application filing, prosecution, registry hearings, assignment deeds and ownership transfer for brands in Morbi, Gujarat.",
                "trademark registration Morbi, trademark registration Gujarat, trademark search, trademark application filing, trademark assignment, brand registration India, intellectual property registration, logo registration",
                "monthly", 0.9, true, "Intellectual Property Rights – Trademark"),

            ["/our-services/certification-and-industrial-compliance-services"] = new("Certification & Industrial Compliance Services",
                "ISO, CE and BIS certification and import-export registration support (IEC, RCMC, APEDA) for manufacturers, traders and exporters in Gujarat.",
                "ISO certification, CE marking, BIS certification, IEC registration, RCMC, APEDA registration, import export code, industrial compliance Gujarat",
                "monthly", 0.8, true, "Certification & Industrial Compliance Services"),

            ["/our-services/rbi-and-nbfc-related-compliances"] = new("RBI & NBFC Compliance Consultant",
                "NBFC licence application, CERSAI reporting, CKYC uploads and quarterly and annual RBI return submission for non-banking financial companies.",
                "NBFC registration consultant, NBFC licence application, RBI compliance, NBFC compliance, RBI returns filing, CERSAI reporting, CKYC upload, RBI regulatory advisory",
                "monthly", 0.8, true, "RBI and NBFC Related Compliances"),

            ["/our-services/transactional-due-diligence-services"] = new("Due Diligence Services in Gujarat",
                "Transactional due diligence, compliance verification and secretarial certification for investments, M&A and capital-market transactions in Gujarat.",
                "due diligence services Gujarat, transactional due diligence, compliance due diligence, secretarial certification, M&A due diligence, DRHP due diligence, compliance verification",
                "monthly", 0.8, true, "Transactional Due Diligence Services"),

            ["/our-services/corporate-governance-services"] = new("Corporate Governance Services in Gujarat",
                "Corporate governance advisory in Gujarat: board and committee effectiveness, governance policies, secretarial compliance, compliance review and reporting.",
                "corporate governance services Gujarat, corporate governance advisory, board effectiveness, governance policies, secretarial compliance advisory, governance audit, independent directors compliance",
                "monthly", 0.9, true, "Corporate Governance Services"),

            ["/our-services/nclt-services"] = new("NCLT Services & Company Petitions",
                "NCLT petition drafting and filing for mergers, restoration of companies, oppression and mismanagement and other company law tribunal matters.",
                "NCLT services, NCLT petition filing, company petition NCLT, NCLT Ahmedabad, restoration of company NCLT, oppression and mismanagement, scheme of arrangement",
                "monthly", 0.8, true, "NCLT Services"),

            ["/our-services/agreements-petitions"] = new("Agreements, Petitions & Legal Drafting",
                "Drafting and vetting of corporate agreements and legal petitions for companies, promoters and investors in Gujarat.",
                "agreement drafting Gujarat, shareholders agreement, corporate agreements, legal petition drafting, agreement vetting, company agreements",
                "monthly", 0.7, true, "Agreements & Petitions"),

            ["/our-services/liquidation-under-insolvency-and-bankruptcy-code-2016"] = new("Company Liquidation under IBC 2016",
                "Liquidation support under the Insolvency and Bankruptcy Code 2016: liquidator advisory, documentation, filings, claims verification and asset distribution.",
                "liquidation under IBC 2016, liquidator support, company liquidation India, insolvency and bankruptcy code liquidation, claims verification, asset distribution",
                "monthly", 0.7, true, "Liquidation under Insolvency and Bankruptcy Code 2016"),

            ["/our-services/legal-metrology-act-2009"] = new("Legal Metrology Act 2009 Compliance",
                "Legal Metrology Act 2009 compliance for packaged commodities, manufacturer, packer and importer registration and labelling requirements.",
                "legal metrology compliance, packaged commodities rules, legal metrology registration, importer registration legal metrology, packer registration, labelling compliance India",
                "monthly", 0.6, true, "Legal Metrology Act, 2009"),

            ["/our-services/xbrl-filing"] = new("XBRL Filing Services for Companies",
                "XBRL data input, conversion of financial data, validation, pre-scrutiny, certification and filing for companies, done on time.",
                "XBRL filing services, XBRL conversion, XBRL filing MCA, financial statement XBRL, XBRL validation, MCA XBRL compliance",
                "monthly", 0.7, true, "XBRL Filing"),

            ["/our-services/other-services"] = new("MSME, DSC, PAN/TAN & LEI Registration",
                "MSME (Udyam) registration, ISIN liaison, PAN and TAN, LEI, share valuation and digital signature certificate renewal for companies and LLPs.",
                "MSME registration Morbi, Udyam registration, digital signature certificate, DSC renewal, LEI registration, PAN TAN application, ISIN liaison, share valuation",
                "monthly", 0.6, true, "Other Services")
        };

        public static string Normalise(string? path)
        {
            var p = (path ?? "/").Trim().ToLowerInvariant();
            if (p.Length > 1)
            {
                p = p.TrimEnd('/');
            }

            return p.Length == 0 ? "/" : p;
        }

        public static SeoEntry? For(string? path)
        {
            var p = Normalise(path);
            if (p == "/home" || p == "/home/index")
            {
                p = "/";
            }
            else if (p == "/home/blogs")
            {
                p = "/blogs";
            }
            else if (p == "/home/ourservices")
            {
                p = "/our-services";
            }

            return Pages.TryGetValue(p, out var entry) ? entry : null;
        }

        public static IEnumerable<KeyValuePair<string, SeoEntry>> Services =>
            Pages.Where(p => p.Value.IsService);
    }

    /// <summary>Builds the structured data (JSON-LD) that search engines read.</summary>
    public static class SeoJsonLd
    {
        private static readonly JsonSerializerOptions Options = new() { WriteIndented = false };

        public static string Business(string baseUrl, string logoUrl)
        {
            var data = new Dictionary<string, object?>
            {
                ["@context"] = "https://schema.org",
                ["@type"] = "ProfessionalService",
                ["@id"] = baseUrl + "/#business",
                ["name"] = SeoCatalog.Brand,
                ["description"] = "Company secretary and corporate advisory firm in Morbi, Gujarat offering company registration, ROC and LLP compliance, SEBI, FEMA, trademark, NCLT and corporate governance services.",
                ["url"] = baseUrl + "/",
                ["logo"] = logoUrl,
                ["image"] = logoUrl,
                ["telephone"] = "+91-75670-96966",
                ["email"] = "cs@sonagra.com",
                ["address"] = new Dictionary<string, object>
                {
                    ["@type"] = "PostalAddress",
                    ["streetAddress"] = "326, 3rd Floor, Star Arcade, Sanala Road",
                    ["addressLocality"] = "Morbi",
                    ["addressRegion"] = "Gujarat",
                    ["postalCode"] = "363641",
                    ["addressCountry"] = "IN"
                },
                ["areaServed"] = new object[]
                {
                    new Dictionary<string, object> { ["@type"] = "City", ["name"] = "Morbi" },
                    new Dictionary<string, object> { ["@type"] = "State", ["name"] = "Gujarat" },
                    new Dictionary<string, object> { ["@type"] = "Country", ["name"] = "India" }
                },
                ["knowsAbout"] = new[]
                {
                    "Company registration", "ROC compliance", "LLP compliance", "Corporate governance",
                    "SEBI compliance", "FEMA compliance", "Trademark registration", "Secretarial audit"
                },
                ["hasOfferCatalog"] = new Dictionary<string, object>
                {
                    ["@type"] = "OfferCatalog",
                    ["name"] = "Company secretary and corporate law services",
                    ["itemListElement"] = SeoCatalog.Services.Select(s => new Dictionary<string, object>
                    {
                        ["@type"] = "Offer",
                        ["itemOffered"] = new Dictionary<string, object>
                        {
                            ["@type"] = "Service",
                            ["name"] = s.Value.ServiceName ?? s.Value.Title,
                            ["url"] = baseUrl + s.Key
                        }
                    }).ToArray()
                }
            };
            return JsonSerializer.Serialize(data, Options);
        }

        public static string Article(string baseUrl, string url, string headline, string description, string imageUrl, string datePublished) =>
            JsonSerializer.Serialize(new Dictionary<string, object>
            {
                ["@context"] = "https://schema.org",
                ["@type"] = "Article",
                ["mainEntityOfPage"] = new Dictionary<string, object> { ["@type"] = "WebPage", ["@id"] = url },
                ["headline"] = headline,
                ["description"] = description,
                ["image"] = imageUrl,
                ["datePublished"] = datePublished,
                ["dateModified"] = datePublished,
                ["inLanguage"] = "en-IN",
                ["author"] = new Dictionary<string, object> { ["@type"] = "Organization", ["name"] = SeoCatalog.Brand, ["url"] = baseUrl + "/" },
                ["publisher"] = new Dictionary<string, object>
                {
                    ["@type"] = "Organization",
                    ["name"] = SeoCatalog.Brand,
                    ["logo"] = new Dictionary<string, object> { ["@type"] = "ImageObject", ["url"] = baseUrl + "/images/sonagra-logo.png" }
                }
            }, Options);

        public static string WebSite(string baseUrl) =>
            JsonSerializer.Serialize(new Dictionary<string, object>
            {
                ["@context"] = "https://schema.org",
                ["@type"] = "WebSite",
                ["name"] = SeoCatalog.Brand,
                ["url"] = baseUrl + "/",
                ["inLanguage"] = "en-IN"
            }, Options);

        public static string? Breadcrumbs(string baseUrl, string path, string pageName)
        {
            var p = SeoCatalog.Normalise(path);
            if (p == "/")
            {
                return null;
            }

            var items = new List<(string Name, string Url)> { ("Home", baseUrl + "/") };
            if (p.StartsWith("/our-services/"))
            {
                items.Add(("Our Services", baseUrl + "/our-services"));
            }
            else if (p.StartsWith("/blogs/"))
            {
                items.Add(("Blogs", baseUrl + "/blogs"));
            }

            items.Add((pageName, baseUrl + p));
            var data = new Dictionary<string, object>
            {
                ["@context"] = "https://schema.org",
                ["@type"] = "BreadcrumbList",
                ["itemListElement"] = items.Select((i, idx) => new Dictionary<string, object>
                {
                    ["@type"] = "ListItem",
                    ["position"] = idx + 1,
                    ["name"] = i.Name,
                    ["item"] = i.Url
                }).ToArray()
            };
            return JsonSerializer.Serialize(data, Options);
        }
    }
}
