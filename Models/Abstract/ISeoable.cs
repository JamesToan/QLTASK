namespace coreWeb.Models.Abstract
{
    public interface ISeoable
    {
        string SeoTitle { get; set; }

        string SeoDescription { get; set; }

        string SeoKeywords { get; set; }
    }
}
