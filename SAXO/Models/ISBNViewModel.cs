using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAXO.Models
{
    public class ISBNViewModel
    {
        [Required]
        [DataType(DataType.MultilineText)]
        public String ISBN { get; set; }
    }
}