using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FilipBlog.Models
{
    [NotMapped]
    public class RawPost:Post
    {
      
        public string RawImageURLs { get; set; }
        public string RawVideoURLs { get; set; }

        public CategoryIntermediate[] RawCategories { get; set; }

        public RawPost():base()
        {
           
        }
    }
}