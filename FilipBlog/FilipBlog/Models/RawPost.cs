using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilipBlog.Models
{
    public class RawPost
    {
        public int PostId { get; set; }

        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Content { get; set; }
        public string AuthorRefId { get; set; }
        public string ImageURLs { get; set; }
        public string VideoURLs { get; set; }

        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfModification { get; set; }
        public bool IsFlagged { get; set; }

        public CategoryIntermediate[] Categories { get; set; }

        public RawPost()
        {
           
        }
    }
}