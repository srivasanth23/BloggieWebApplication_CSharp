using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModel;
using Bloggie.Web.Repositoris;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        // This is shifted to Repo's so now here we call here the Interfce

        //// Dependency injection of DbContext
        //private readonly BloggieDbContext _context;
        //public AdminTagsController(BloggieDbContext context) // Cntrl + .
        //{
        //    _context = context;
        //}


        private readonly ITagRepositary _tagsRepositary;
        public AdminTagsController(ITagRepositary iTagRepo)
        {
            _tagsRepositary = iTagRepo;
        }


        // ------------------
        // Get Action method for ADD()
        // ------------------
        [HttpGet]
        public IActionResult Add()
        {
            return View();
            // cntrl + .
        }

        // Explanation of types of data submission in two different ways
        // ----------------------------------------------
        //// 1st method (Form Submission)
        //[HttpPost]
        //[ActionName("Add")] // We gave function Name as SubmitTag but in real the Action name is "Add"
        //public IActionResult SubmitTag()
        //{
        //    // In Add.cshtml we added "name" tabs to bothe the input fields, so they will come and stored here.
        //    // (We are calling them here in controller)
        //    // In .cshtml we use "name"
        //    var name = Request.Form["name"];
        //    var displayName = Request.Form["displayName"];
        //    return View("Add");
        //}


        //// 2nd method (Data Binding) 
        //// Here what we are doing is creating ViewModel and denoting there and calling them here. 
        //// In .cshtml we use "asp-for"
        //[HttpPost]
        //public IActionResult SubmitTag(AddingTagRequest addingTagRequest)
        //{
        //    var name = addingTagRequest.Name;
        //    var displayName = addingTagRequest.DisplayName;
        //    return View("Add");
        //}


        // ----------------------
        // Post method using Add() (Create Operation)
        // ----------------------

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddingTagRequest request) // here we are using AddingTagRequest because its a model reference
        {
            // Mapping AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = request.Name,
                DisplayName = request.DisplayName,
            };

            await _tagsRepositary.AddTagAsync(tag); // Now changes like adding and saving is done in repo file we are just calling the function

            return RedirectToAction("List"); // redirect to List page
        }





        // ------------------------------
        // Get to display all the Tags (Read operation)
        // ------------------------------

        [HttpGet]
        [ActionName("List")]  // Explicity assigning action name
        public async Task<IActionResult> List()
        {
            var tags = await _tagsRepositary.GetAllAsync();
            return View(tags);
        }





        // ----------------------------------
        // Edit the tags (Get the Udate Operation page) 
        // ----------------------------------

        // This is only the Get method for Edit (which helps in displaying)
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) // id parameter spell should match with the asp-route-{---} bracketed spell
        {

            var tag = await _tagsRepositary.GetByIdAsync(id);
            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };

                return View(editTagRequest);

            }

            return View(null);
        }




        // ----------------------------------
        // Edit the tags (Update Operation) 
        // ----------------------------------

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {

            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

            var updatedTag = await _tagsRepositary.UpdateTagAsync(tag);

            if (updatedTag != null)
            {
                // Show success notification
            }
            else
            {

            }

            //show failure notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }




        // ---------------------------
        // Delete Tag 
        // ---------------------------
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await _tagsRepositary.DeleteTagAsync(editTagRequest.Id);

            if (deletedTag != null)
            {
                // success notification
                return RedirectToAction("List");
            }

            // Show an error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
    }
}
