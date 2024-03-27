using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your contact number.")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Please select a date and time.")]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "Please enter the number of guests.")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of guests must be at least 1.")]
        public int Guests { get; set; }
    }
}
