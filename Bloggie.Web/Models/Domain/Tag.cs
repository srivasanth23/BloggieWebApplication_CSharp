namespace Bloggie.Web.Models.Domain
{
    // Tag table Schema
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public ICollection<BlogPost> BlogPost { get; set; }
    }
}
