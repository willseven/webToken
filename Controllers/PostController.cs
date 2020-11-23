using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiWeb_master.Data;
using ApiWeb_master.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {


        private readonly ILogger<PostController> _logger;
        private readonly BloggingContext _context;

        public PostController(BloggingContext context, ILogger<PostController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public Post Get()
        {
            Console.WriteLine("Querying for a post");
            var post = _context.Posts
                .FirstOrDefault();

            return post;
        }

        [HttpGet("count")]
        public int Count()
        {
            Console.WriteLine("Counting posts");
            var Count = _context.Posts.Count();


            return Count;
        }
        [HttpPost]
        public Post Create(Post newPost)
        {
            var createPost = new Post { Title = newPost.Title, Content = newPost.Content, BlogId = newPost.BlogId };
            // Create
            Console.WriteLine("Inserting a new Post");
            _context.Add(createPost);
            _context.SaveChanges();


            return createPost;
        }
        

    }
}
