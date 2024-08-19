using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Records_Master.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string RegNumber { get; set; }
        public string Status { get; set; }

        public char Gender {  get; set; }

        public static readonly List<SelectListItem> RegionOptions = new List<SelectListItem>
        {
            new SelectListItem{Value="Central", Text="Central" },
            new SelectListItem{Value="Northern", Text="Northern" },
            new SelectListItem{Value="Eastern", Text="Eastern" },
            new SelectListItem{Value="Western", Text="Western" },
            new SelectListItem{Value="West Nile", Text="West Nile" }
        };

        public static readonly List<SelectListItem> StatusOptions = new List<SelectListItem>
        { 
            new SelectListItem{Value="In Patient", Text="In Patient"},
            new SelectListItem{Value="Out Patient", Text="Out Patient"},
            new SelectListItem{Value="Arrival", Text="Arrival"},
            new SelectListItem{Value="Discharged", Text="Discharged"}
        };
        public static readonly List<SelectListItem> GenderOptions = new List<SelectListItem>
        {
            new SelectListItem{Value="M", Text="Male" },
            new SelectListItem{Value="F", Text="Female" }
        };

    }
}
