using SonagraCompany.Models;

namespace SonagraCompany.Services
{
    /// <summary>
    /// Blog articles. To publish a new article add one more BlogPost to the list below.
    /// The text is general information for readers, not legal advice.
    /// </summary>
    public static class BlogCatalog
    {
        private static readonly DateOnly Published = new(2026, 11, 1);

        public static readonly IReadOnlyList<BlogPost> Posts = new List<BlogPost>
        {
            new("private-limited-company-registration-in-gujarat",
                "Private Limited Company Registration in Gujarat: Step-by-Step Guide",
                "How to register a private limited company in Gujarat: eligibility, documents, SPICe+ filing, MOA and AOA, and what to do after incorporation.",
                "private limited company registration in Gujarat, company registration in Morbi, how to register a company, documents for company registration, SPICe+, MOA AOA, company incorporation India",
                "Company Registration", Published, 5, "Private Limited Company Registration in Gujarat.png",
                """
                <p>A private limited company is the most popular structure for founders who want limited liability, a separate legal identity and an easy path to raising investment. This guide explains how company registration works for businesses in Morbi and the rest of Gujarat.</p>

                <h2>Who can start a private limited company?</h2>
                <ul>
                  <li>At least <strong>two directors</strong> and <strong>two shareholders</strong> (the same persons can be both).</li>
                  <li>At least one director who has stayed in India for <strong>182 days or more</strong> in the previous calendar year.</li>
                  <li>A registered office address in India.</li>
                </ul>

                <h2>Documents usually required</h2>
                <ul>
                  <li>PAN and Aadhaar of every director and shareholder, with a recent photograph.</li>
                  <li>Address proof of directors (such as a bank statement or utility bill).</li>
                  <li>Registered office proof: a rent agreement or ownership deed, a recent utility bill and a no-objection letter from the owner.</li>
                  <li>Digital Signature Certificate (DSC) for the proposed directors.</li>
                </ul>

                <h2>Step-by-step process</h2>
                <ol>
                  <li><strong>Obtain DSCs</strong> for the proposed directors.</li>
                  <li><strong>Choose and apply for the company name</strong> so that it is unique and follows the naming rules.</li>
                  <li><strong>File the incorporation application (SPICe+)</strong> on the MCA portal with the Memorandum of Association (MOA) and Articles of Association (AOA).</li>
                  <li><strong>Receive the Certificate of Incorporation</strong> with the company's CIN. PAN and TAN are issued along with it.</li>
                  <li><strong>Complete post-incorporation steps</strong>: open the current account, hold the first board meeting, appoint the first auditor within 30 days and file the declaration for commencement of business within 180 days.</li>
                </ol>

                <h2>How long does it take?</h2>
                <p>The time depends on document readiness and the processing time of the Registrar. With complete and correct documents, incorporation is typically completed in a few working days.</p>

                <h2>Why take professional help?</h2>
                <p>The objects clause, share capital, director details and registered office decisions affect your compliance and fundraising later. A company secretary can choose the right structure, draft the MOA and AOA correctly and make sure nothing is missed after incorporation.</p>
                <p><em>This article is general information and not legal advice. Requirements can change, so confirm the latest rules before you file.</em></p>
                """,
                new RelatedService[]
                {
                    new("Company Registration in Morbi, Gujarat", "/our-services/formation-of-business-entities/"),
                    new("ROC Filing & LLP Compliance in Gujarat", "/our-services/company-law-llp-compliances/"),
                    new("Trademark Registration in Morbi, Gujarat", "/our-services/intellectual-property-rights-trademark-tm/")
                }),

            new("annual-roc-compliance-checklist-for-private-limited-companies",
                "Annual ROC Compliance Checklist for Private Limited Companies",
                "A practical checklist of yearly ROC and Companies Act compliances: board meetings, AGM, financial statements, annual return, DIR-3 KYC and more.",
                "annual ROC compliance, ROC filing checklist, private limited company compliance, MGT-7 AOC-4, AGM compliance, DIR-3 KYC, company annual filing Gujarat, ROC compliance Morbi",
                "ROC Compliance", Published, 5, "Annual ROC Compliance Checklist for Private Limited Companies.png",
                """
                <p>Registering a company is only the beginning. Every company in India has to meet yearly compliance requirements under the Companies Act, 2013. Missing them leads to additional fees, penalties and, in serious cases, disqualification of directors. Here is a practical checklist.</p>

                <h2>Meetings and records</h2>
                <ul>
                  <li><strong>Board meetings:</strong> a private company generally needs to hold at least four board meetings in a year with a gap of not more than 120 days between two meetings (some small companies have relaxations).</li>
                  <li><strong>Annual General Meeting (AGM):</strong> to be held within six months from the end of the financial year, that is by 30 September for a company with a 31 March year-end.</li>
                  <li><strong>Minutes and statutory registers:</strong> keep the minutes, register of members, directors and charges properly updated.</li>
                </ul>

                <h2>Filings with the Registrar of Companies</h2>
                <ul>
                  <li><strong>Financial statements (Form AOC-4):</strong> filed after the AGM within the prescribed time.</li>
                  <li><strong>Annual return (Form MGT-7 or MGT-7A):</strong> filed after the AGM within the prescribed time.</li>
                  <li><strong>Director KYC (DIR-3 KYC):</strong> every director holding a DIN has to complete this each year.</li>
                  <li><strong>Auditor appointment (ADT-1)</strong> and the <strong>return of deposits (DPT-3)</strong> where applicable.</li>
                  <li><strong>MSME Form 1</strong> for companies that have outstanding dues to micro and small enterprises beyond the permitted period.</li>
                </ul>

                <h2>Other points to keep in mind</h2>
                <ul>
                  <li>Income-tax and GST returns are separate from ROC compliance but should be consistent with the financial statements.</li>
                  <li>A secretarial audit report applies to certain companies such as listed and larger companies.</li>
                  <li>Due dates, exemptions and fees are revised from time to time, so check the current position or ask your company secretary.</li>
                </ul>

                <h2>How a company secretary helps</h2>
                <p>A company secretary prepares a compliance calendar, drafts notices and minutes, files the forms on time and flags risks early. This saves you from late fees and keeps your company's record clean for investors, banks and regulators.</p>
                <p><em>This article is general information and not legal advice.</em></p>
                """,
                new RelatedService[]
                {
                    new("ROC Filing & LLP Compliance in Gujarat", "/our-services/company-law-llp-compliances/"),
                    new("Corporate Law Advisory Services Gujarat", "/our-services/corporate-law-and-advisory/"),
                    new("XBRL Filing Services for Companies", "/our-services/xbrl-filing/")
                }),

            new("llp-vs-private-limited-company-which-is-better",
                "LLP vs Private Limited Company: Which Is Better for Your Business?",
                "Compare an LLP and a private limited company on liability, compliance, funding, audit and ownership to choose the right structure for your business.",
                "LLP vs private limited company, LLP registration Gujarat, which is better LLP or pvt ltd, difference between LLP and company, business structure India, LLP compliance",
                "Company Registration", Published, 4, "Which Is Better for Your Business.png",
                """
                <p>Choosing between a Limited Liability Partnership (LLP) and a private limited company is one of the first decisions for a new business. Both give limited liability, but they differ in compliance, funding and flexibility.</p>

                <h2>Quick comparison</h2>
                <ul>
                  <li><strong>Liability:</strong> limited to the capital contributed (LLP) or the unpaid share capital (company).</li>
                  <li><strong>Compliance:</strong> an LLP generally has lighter yearly compliance. A company has more filings, board meetings and an AGM.</li>
                  <li><strong>Audit:</strong> a private company needs a statutory audit every year. An LLP needs an audit only when its turnover or contribution crosses the limits set by law.</li>
                  <li><strong>Funding:</strong> venture capital and private equity investors usually prefer a company because they can invest through shares. An LLP has no shares.</li>
                  <li><strong>Ownership change:</strong> shares of a company can be transferred and issued more easily. Changes in an LLP follow the LLP agreement and need filings.</li>
                  <li><strong>Foreign investment:</strong> both can receive it where the sector and the route allow, but a company is the simpler choice for most foreign investors.</li>
                  <li><strong>Taxation:</strong> the tax treatment of the two is different, so compare it with your tax adviser before deciding.</li>
                </ul>

                <h2>Which one should you choose?</h2>
                <p>An LLP suits professional firms, small family businesses and service businesses that want low compliance cost and do not plan to raise equity funding. A private limited company suits startups and growing businesses that want to raise investment, issue employee stock options or scale across regions.</p>

                <h2>Can you convert later?</h2>
                <p>Yes. An LLP can be converted into a company and a private company can be converted into an LLP, subject to conditions and filings. Planning the right structure at the start can still save time and cost.</p>
                <p><em>This article is general information and not legal advice.</em></p>
                """,
                new RelatedService[]
                {
                    new("Company Registration in Morbi, Gujarat", "/our-services/formation-of-business-entities/"),
                    new("ROC Filing & LLP Compliance in Gujarat", "/our-services/company-law-llp-compliances/"),
                    new("Agreements, Petitions & Legal Drafting", "/our-services/agreements-petitions/")
                }),

            new("what-does-a-company-secretary-do",
                "What Does a Company Secretary Do and When Does Your Business Need One?",
                "Understand the role of a company secretary: compliance, board meetings, ROC filings, governance and SEBI rules, and when you should appoint one.",
                "what does a company secretary do, company secretary role, CS in India, company secretary services, when to hire a company secretary, practising company secretary Gujarat",
                "Company Secretary", Published, 4, "What Does a Company Secretary Do and When Does Your Business Need One.png",
                """
                <p>A company secretary (CS) is a professional who makes sure a company follows the corporate laws that apply to it. For many founders the CS is the person who keeps the company compliant, the board organised and the records clean.</p>

                <h2>What a company secretary does</h2>
                <ul>
                  <li>Advises the board on the Companies Act, SEBI rules and other corporate laws.</li>
                  <li>Organises board and general meetings, drafts notices, agendas and minutes.</li>
                  <li>Files forms and returns with the Registrar of Companies and other authorities.</li>
                  <li>Maintains statutory registers and records.</li>
                  <li>Handles corporate governance, policies and disclosures.</li>
                  <li>Carries out secretarial audit and compliance certification.</li>
                  <li>Supports incorporation, share issues, mergers, restructuring and listing.</li>
                </ul>

                <h2>When is a company secretary compulsory?</h2>
                <p>Under the Companies Act, 2013 listed companies and public companies with a paid-up share capital of ₹10 crore or more must appoint a whole-time company secretary. Other companies are not required to, but most growing businesses benefit from professional help.</p>

                <h2>In-house or practising company secretary?</h2>
                <p>A small or medium business often does not need a full-time employee. A practising company secretary or a CS firm can handle compliance on a retainer or for each project, which is more cost effective and gives access to a team with broader experience.</p>

                <h2>Signs that you need one</h2>
                <ul>
                  <li>You are unsure about the yearly ROC filings and due dates.</li>
                  <li>You are planning to raise funds, add partners or investors, or list your company.</li>
                  <li>You have received a notice from the Registrar or a regulator.</li>
                  <li>You operate in several states or have foreign shareholders.</li>
                </ul>
                <p><em>This article is general information and not legal advice.</em></p>
                """,
                new RelatedService[]
                {
                    new("Corporate Governance Services in Gujarat", "/our-services/corporate-governance-services/"),
                    new("ROC Filing & LLP Compliance in Gujarat", "/our-services/company-law-llp-compliances/"),
                    new("SEBI Compliance & IPO Consultant Gujarat", "/our-services/sebi-matters-and-ipo-services/")
                }),

            new("corporate-governance-for-growing-companies",
                "Corporate Governance for Growing Companies: Why It Matters",
                "Why corporate governance matters for growing and listed companies: board structure, committees, policies, disclosures and regular compliance reviews.",
                "corporate governance for growing companies, corporate governance in India, board of directors, governance policies, whistleblower policy, related party transactions, governance compliance Gujarat",
                "Corporate Governance", Published, 4, "Corporate Governance for Growing Companies.png",
                """
                <p>Corporate governance is the system by which a company is directed and controlled. Good governance builds trust with investors, banks, employees and regulators and helps a business grow without avoidable legal or reputational problems.</p>

                <h2>Key elements of good governance</h2>
                <ul>
                  <li><strong>An effective board:</strong> a suitable mix of executive, non-executive and independent directors with clearly defined responsibilities.</li>
                  <li><strong>Committees:</strong> audit, nomination and remuneration, stakeholders relationship, risk management and CSR committees where they apply.</li>
                  <li><strong>Written policies:</strong> a code of conduct, whistleblower policy, related party transaction policy and, for listed entities, an insider trading policy.</li>
                  <li><strong>Proper meetings and records:</strong> notices, agendas, minutes and resolutions prepared and kept correctly.</li>
                  <li><strong>Transparent disclosures:</strong> timely reporting to shareholders, the Registrar and, where listed, the stock exchanges.</li>
                </ul>

                <h2>Why growing companies should care</h2>
                <p>As ownership widens and operations become more complex, informal decision making stops working. Investors ask for governance and compliance records during due diligence, and gaps can delay funding, IPOs or mergers.</p>

                <h2>Regular compliance reviews</h2>
                <p>A periodic review by a company secretary can find gaps before they become penalties. It covers statutory registers, board processes, filings and disclosures, and gives practical recommendations to fix them.</p>
                <p><em>This article is general information and not legal advice.</em></p>
                """,
                new RelatedService[]
                {
                    new("Corporate Governance Services in Gujarat", "/our-services/corporate-governance-services/"),
                    new("Corporate Law Advisory Services Gujarat", "/our-services/corporate-law-and-advisory/"),
                    new("Due Diligence Services in Gujarat", "/our-services/transactional-due-diligence-services/")
                }),

            new("fema-and-fdi-compliance-for-foreign-investment-in-india",
                "FEMA and FDI Compliance for Foreign Investment in India",
                "A simple guide to FEMA and FDI compliance: investment routes, reporting to RBI through the authorised dealer bank, annual returns and common mistakes.",
                "FEMA compliance, FDI compliance India, foreign investment in India, FEMA consultant, RBI reporting, annual FLA return, overseas investment, foreign company in India",
                "FEMA & FDI", Published, 4, "FEMA and FDI Compliance for Foreign Investment in India.png",
                """
                <p>The Foreign Exchange Management Act, 1999 (FEMA) regulates foreign exchange transactions and foreign investment in India. Indian companies that receive foreign investment, or invest abroad, have to follow FEMA rules and report certain transactions to the Reserve Bank of India (RBI).</p>

                <h2>Foreign direct investment (FDI) basics</h2>
                <ul>
                  <li>FDI is allowed under the <strong>automatic route</strong> or the <strong>government route</strong>, depending on the sector.</li>
                  <li>Some sectors have investment caps and conditions.</li>
                  <li>Pricing, the type of instruments issued and the use of funds have to follow FEMA rules.</li>
                </ul>

                <h2>Reporting and filings</h2>
                <ul>
                  <li>Receipt of foreign investment and the allotment or transfer of shares to non-residents have to be reported within the prescribed time through the authorised dealer bank.</li>
                  <li>Companies with foreign investment also submit an annual return on foreign liabilities and assets.</li>
                  <li>External commercial borrowings and overseas investments have their own conditions and reporting.</li>
                </ul>

                <h2>Common mistakes</h2>
                <ul>
                  <li>Late reporting of investment received or shares issued.</li>
                  <li>Issuing shares at a price that does not meet the pricing guidelines.</li>
                  <li>Ignoring sectoral conditions or the need for prior approval.</li>
                </ul>
                <p>Late reporting can be regularised through the compounding process, but it takes time and cost. Careful planning at the start avoids this.</p>

                <h2>Foreign companies entering India</h2>
                <p>A foreign business can set up a subsidiary, joint venture, branch office or liaison office, each with different permissions and compliance. A company secretary experienced in FEMA can recommend the right structure and manage the filings.</p>
                <p><em>This article is general information and not legal advice.</em></p>
                """,
                new RelatedService[]
                {
                    new("FEMA & FDI Compliance Consultant India", "/our-services/fema-matters/"),
                    new("Company Registration in Morbi, Gujarat", "/our-services/formation-of-business-entities/"),
                    new("RBI & NBFC Compliance Consultant", "/our-services/rbi-and-nbfc-related-compliances/")
                })
        };

        public static BlogPost? Find(string? slug) =>
            Posts.FirstOrDefault(p => string.Equals(p.Slug, slug, StringComparison.OrdinalIgnoreCase));
    }
}
