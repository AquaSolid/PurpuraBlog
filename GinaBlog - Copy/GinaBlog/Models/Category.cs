using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GinaBlog.Models
{
    public class Category
    {
        public String Name { get; set; }
        [Key]
        public int CategoryId { get; set; }

        public Category()
        {

        }
    }
}