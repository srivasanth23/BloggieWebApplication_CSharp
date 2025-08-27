using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Bloggie.Web.Repositoris
{
    public class TagRepo : ITagRepositary // Go on Interface and clikc Cntrl + . (all the functions will come)
    {
        // Dependency injection of DbContext
        private readonly BloggieDbContext _context;

        public TagRepo(BloggieDbContext context)
        {
            _context = context;
        }


        public async Task<Tag> AddTagAsync(Tag tag)
        {
            await _context.Tags.AddAsync(tag); // Adding a copy to Database
            await _context.SaveChangesAsync(); // Saveing changes to database
            return tag;
        }

        public async Task<Tag?> DeleteTagAsync(Guid id)
        {
            var existingTags = await _context.Tags.FindAsync(id);

            if (existingTags != null)
            {
                _context.Tags.Remove(existingTags);
                await _context.SaveChangesAsync();
                return existingTags;
            }
            return null;

        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            // we need to use dbContext to read data as we done post method using dbContext to add the tags to it
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag?> GetByIdAsync(Guid id)
        {
            return await _context.Tags.FirstOrDefaultAsync(x => x.Id == id);
            
        }

        public async Task<Tag?> UpdateTagAsync(Tag tag)
        {
            // 1st method (may return null too)
            //var tag = _context.Tags.Find(id);

            // 2nd method 
            var existingTag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == tag.Id); // we can also use SingleorDefault

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                //save the chnages
                await _context.SaveChangesAsync();
                return existingTag;
            }

            return null;
        }
    }
}
