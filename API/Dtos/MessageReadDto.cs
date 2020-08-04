using System;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class MessageReadDto
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