using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiWeb_master.Data;
using ApiWeb_master.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {


        private readonly ILogger<BlogController> _logger;
        private readonly BloggingContext _context;

        public BlogController(BloggingContext context, ILogger<BlogController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Blog>> Gets()
        {
            return await _context.Blogs.ToListAsync();
        }
        [HttpGet("one")]
        public Blog Get()
        {
            Console.WriteLine("Querying for a blog");
            var blog = _context.Blogs
                .FirstOrDefault();

            return blog;
        }

        [HttpGet("count")]
        public int Count()
        {
            Console.WriteLine("Counting blogs");
            var Count = _context.Blogs.Count();


            return Count;
        }
        [HttpPost]
        public Blog Create(Blog newBlog)
        {
            var createBlog = new Blog { Url = newBlog.Url };
            // Create
            Console.WriteLine("Inserting a new blog");
            _context.Add(createBlog);
            _context.SaveChanges();


            return createBlog;
        }
        [HttpDelete]
        public Blog Delete(Blog newBlog)
        {
            var deleteBlog = new Blog { BlogId = newBlog.BlogId };
            // Delete
            Console.WriteLine("Inserting a new blog");
            _context.Remove(deleteBlog);
            _context.SaveChanges();
            return deleteBlog;
        }
        // [HttpPut]
        // public Blog Put(Blog newBlog)
        // {
        //      var deleteBlog = new Blog { Url=newBlog.Url };
        //     // Delete
        //     Console.WriteLine("Inserting a new blog");
        //     _context.Remove(deleteBlog);
        //     _context.SaveChanges();
        //     return deleteBlog;
        // }

        [HttpPut]
        public Blog Update(Blog newBlog)
        {
            var blog = _context.Blogs
                .SingleOrDefault(b => b.BlogId == newBlog.BlogId);

            blog.Url = newBlog.Url;
            // blog.Posts.Add(
            //         new Post
            //         {

            //             BlogId =  newBlog.BlogId,
            //             Title = "Hello World",
            //             Content = "I wrote an app using EF Core!"

            //         });
            var createPost = new Post
            {
                BlogId = newBlog.BlogId,
                Title = "Hello World",
                Content = "I wrote an app using EF Core!"
            };
            Console.WriteLine("updating blog");
            _context.Update(blog);
            _context.SaveChanges();
            return blog;
        }

        //METHOD PUT CON ENVIO DE ID EN LA URL
        private bool TodoItemExists(int id) =>
     _context.Blogs.Any(e => e.BlogId == id);

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(int id, Blog Blog)
        {
            if (id != Blog.BlogId)
            {
                return BadRequest();
            }

            var todoItem = await _context.Blogs.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Url = Blog.Url;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
