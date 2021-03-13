using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Services.Models;

namespace Services.Models
{
    // User will never see Data class, DB will never see DTO classes
    // Transfer data from DTO, to Data classes, into database

    public class CreateCountryDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Country name is too long")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 3, ErrorMessage = "Short Country name is too long")]
        public string ShortName { get; set; }
    }

    public class UpdateCountryDTO : CreateCountryDTO
    {
    }

    public class CountryDTO : UpdateCountryDTO
    {
        public int Id { get; set; }
        public IList<HotelDTO> Hotels { get; set; }
    }

}
