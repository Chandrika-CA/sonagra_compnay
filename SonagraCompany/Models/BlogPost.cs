namespace SonagraCompany.Models
{
    public record RelatedService(string Name, string Path);

    public record BlogPost(
        string Slug,
        string Title,
        string Description,
        string Keywords,
        string Category,
        DateOnly Published,
        int ReadMinutes,
        string ImageFile,
        string ContentHtml,
        IReadOnlyList<RelatedService> RelatedServices);
}
