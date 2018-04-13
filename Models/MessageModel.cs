using System;
using System.ComponentModel.DataAnnotations;

namespace the_wall.Models
{
    public class Message : BaseEntity
    {
        [Required]
        [MinLength(2)]
        public string content {get; set;}

        [Required]
        public int user_id {get;set;}

    }
}