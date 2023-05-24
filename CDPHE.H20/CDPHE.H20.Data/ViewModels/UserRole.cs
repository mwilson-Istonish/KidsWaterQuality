using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPHE.H20.Data.ViewModels
{
    public class UserRole
    {
        // Unique identifier for the user and role
        [JsonProperty("id")]
        public int Id { get; set; }

        // First name of the user
        [JsonProperty("firstName")]
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "First name should be between 1 and 50 characters")]
        public string FirstName { get; set; }

        // Last name of the user
        [JsonProperty("lastName")]
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Last name should be between 1 and 50 characters")]
        public string LastName { get; set; }

        // Email address of the user
        [JsonProperty("email")]
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string Email { get; set; }

        // PHone number of the user
        [JsonProperty("phone")]
        [Required]
        [EmailAddress(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        // Role assigned to the user
        [JsonProperty("role")]
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Role should be between 1 and 50 characters")]
        public string Role { get; set; }

        [JsonProperty("wqcid")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "WQCID should be between 1 and 10 characters")]
        public string WQCID { get; set; }
    }
}
