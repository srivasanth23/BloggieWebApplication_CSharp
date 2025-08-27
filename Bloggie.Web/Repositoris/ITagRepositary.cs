using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositoris
{
    public interface ITagRepositary
    {
        // as we are using asyncronous programming we need to use Task as wrapper
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag?> GetByIdAsync(Guid id);
        Task<Tag> AddTagAsync(Tag tag);
        Task<Tag?> UpdateTagAsync(Tag tag);
        Task<Tag?> DeleteTagAsync(Guid id);
    }
}
