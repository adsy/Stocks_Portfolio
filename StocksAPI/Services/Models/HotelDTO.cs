using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Models
{
    public class HotelDTO:CreateHotelDTO
    {
        public int Id { get; set; }
        //public CountryDTO Country { get; set; }
    }

    public class CreateHotelDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Hotel name is too long")]
        public string Name { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required]
        [Range(1,5)]
        public double Rating { get; set; }
        [Required]
        [StringLength(maximumLength:250,ErrorMessage = "Address is too long")]
        public string Address { get; set; }
    }

    public class UpdateHotelDTO:CreateHotelDTO
    {

    }
}
