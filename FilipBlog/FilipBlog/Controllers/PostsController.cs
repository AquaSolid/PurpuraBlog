using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FilipBlog.Models;
using Microsoft.AspNet.Identity;

namespace FilipBlog.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.Author).Include(p => p.ImageURLs);
            return View(posts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
			ViewBag.CommenterRefId = new SelectList(db.Users, "Id", "FirstName");
			ViewBag.Post_PostId = new SelectList(db.Posts, "PostId", "Title");

			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts
				.Include(p => p.Comments)
				.Include(p => p.Author)
				.Single(p => p.PostId == id);


            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //// GET: Posts/Create
        //public ActionResult Create()
        //{
        //    ViewBag.AuthorRefId = new SelectList(db.Users, "Id", "FirstName");
        //    ViewBag.Message = User.Identity.GetUserId();            
        //    return View();
        //}

        // GET: Posts/Create
        public ActionResult Create()
        {
            ViewBag.Message = User.Identity.GetUserId();
            RawPost rawPost = new RawPost();

            rawPost.RawCategories = db.Categories
                .Select(c => new CategoryIntermediate
                {
                    CategoryName = c.Name,
                    IsSelected = false
                })
                .ToArray();


            return View(rawPost);
        }



        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Post,RawImageURLs,RawCategories")] RawPost rawPost)
        {

            

            rawPost.RawVideoURLs = rawPost.RawImageURLs;
            rawPost.Post.Author = db.Users.Find(rawPost.Post.AuthorRefId);

            if (ModelState.IsValid)
            {

                db.Posts.Add(rawPost.Post);
                db.Users.Find(rawPost.Post.AuthorRefId).PostsAuthored.Add(rawPost.Post);
                db.SaveChanges();

                var latestId = db.Posts.Where(x => x.Content == rawPost.Post.Content && x.Title == rawPost.Post.Title).Max(x => x.PostId);
             
                rawPost.updatePost(latestId, db.Categories.ToList() );
                db.ImageLinks.AddRange(rawPost.Post.ImageURLs);
                db.VideoLinks.AddRange(rawPost.Post.VideoURLs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }




            return View(rawPost);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            RawPost rawPost = new RawPost(post, db.Categories.ToList());

           


            return View(rawPost);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Post,RawImageURLs,RawCategories")] RawPost rawPost)
        {
            

            rawPost.updatePost(rawPost.Post.PostId, db.Categories.ToList());
            if (ModelState.IsValid)
            {   
              /*  foreach (ImageLink image in rawPost.Post.ImageURLs)
                {   if (db.ImageLinks.ToList().Contains(image))
                        db.Entry(image).State = EntityState.Modified;
                    else
                        db.ImageLinks.Add(image);
                }
*/
                db.Entry(rawPost.Post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        
            return View(rawPost);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

		


		public ActionResult LikePost(int postId)
        {
            Post post = db.Posts.Find(postId);
            ApplicationUser liker = db.Users.Find(User.Identity.GetUserId());

            if (post.Dislikers.Contains(liker))
                post.Dislikers.Remove(liker);

            if (liker.PostsDisliked.Contains(post))
                liker.PostsDisliked.Remove(post);

            if (!post.Likers.Contains(liker))
                post.Likers.Add(liker);

            if (!liker.PostsLiked.Contains(post))
                liker.PostsLiked.Add(post);

            db.SaveChanges();

            return PartialView("_LikeDislikeCount", post);
        }


        public ActionResult DislikePost(int postId)
        {
            Post post = db.Posts.Find(postId);
            ApplicationUser disliker = db.Users.Find(User.Identity.GetUserId());

            if (post.Likers.Contains(disliker))
                post.Likers.Remove(disliker);

            if (disliker.PostsLiked.Contains(post))
                disliker.PostsLiked.Remove(post);

            if (!post.Dislikers.Contains(disliker))
                post.Dislikers.Add(disliker);

            if (!disliker.PostsDisliked.Contains(post))
                disliker.PostsDisliked.Add(post);

            db.SaveChanges();

            return PartialView("_LikeDislikeCount", post);
        }


    }
}
