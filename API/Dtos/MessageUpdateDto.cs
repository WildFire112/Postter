using System;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class MessageUpdateDto
    {
        [Required]
        [MaxLength(250)]
        public string Text { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}