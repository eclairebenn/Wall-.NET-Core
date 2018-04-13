using System;
using System.ComponentModel.DataAnnotations;

namespace the_wall.Models
{
    public class Comment : BaseEntity
    {
        [Required]
        [MinLength(2)]
        public string content {get; set;}

        [Required]
        public int user_id {get;set;}

        [Required]
        public int message_id {get;set;}

    }
}