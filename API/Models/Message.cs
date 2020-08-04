using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Text { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}