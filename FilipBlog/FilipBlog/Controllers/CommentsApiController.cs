using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FilipBlog.Models;
using Microsoft.AspNet.Identity;
namespace FilipBlog.Controllers
{
    public class CommentsApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CommentsApi
        public IQueryable<Comment> GetComments()
        {
            return db.Comments;
        }

        // GET: api/CommentsApi/5
        [ResponseType(typeof(Comment))]
        public IHttpActionResult GetComment(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // PUT: api/CommentsApi/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PutComment(int id, Comment comment)
        {
            Comment oldComment = db.Comments.Find(id);
            oldComment.DateOfModification = DateTime.Now;
            oldComment.Content = comment.Content;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != oldComment.CommentId)
            {
                return BadRequest();
            }

            db.Entry(oldComment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CommentsApi
        [ResponseType(typeof(Comment))]
        public IHttpActionResult PostComment(Comment comment)
        {
            comment.CommenterRefId = User.Identity.GetUserId();
            comment.DateOfCreation = DateTime.Now;
            comment.DateOfModification = DateTime.Now;
            comment.Commenter = db.Users.Find(comment.CommenterRefId);

            
            comment.Post = db.Posts.Find(comment.Post_PostId);
            comment.Commenter.PostsCommentedOn.Add(comment.Post);



            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(comment.ParentComment_CommentId !=0)
            {
                db.Comments.Find(comment.ParentComment_CommentId ).Replies.Add(comment);
               // comment.ParentComment = db.Comments.Find(comment.ParentComment_CommentId );
            }
            else db.Comments.Add(comment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = comment.CommentId }, comment);
        }
        
        







        // DELETE: api/CommentsApi/5
        [ResponseType(typeof(Comment))]
        public IHttpActionResult DeleteComment(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }

            if (comment.ParentComment_CommentId  ==0)
            {
                //db.Users.Find(comment.CommenterRefId).CommentsCommentedOn.Remove(comment.ParentComment);
                comment.Replies.ToList().ForEach(c => db.Entry(c).State = EntityState.Deleted);

               // db.Comments.Find(comment.ParentComment_CommentId ).Replies.Remove(comment);
            }
            db.Entry(comment).State = EntityState.Deleted;
            db.Comments.Remove(comment);
            db.SaveChanges();

            return Ok(comment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentExists(int id)
        {
            return db.Comments.Count(e => e.CommentId == id) > 0;
        }
    }
}