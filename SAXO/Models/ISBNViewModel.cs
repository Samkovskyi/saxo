using System;
using System.ComponentModel.DataAnnotations;

namespace SAXO.Models
{
    public class ISBNViewModel
    {
        [Required]
        [RegularExpression("^([^\r\n]([0-9]{9}|[0-9]{12})(\r?\n|$))+$", ErrorMessage = "Invalid ISBN Format")]
        [DataType(DataType.MultilineText)]
        public String ISBN { get; set; }
    }
}