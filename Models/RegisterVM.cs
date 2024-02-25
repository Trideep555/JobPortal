using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DREAMCatcher.Models
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Please Enter First Name")]
        public string? First_Name { get; set; }
		[Required(ErrorMessage = "Please Enter Last Name")]
		public string? Last_Name { get; set; }
		[Required(ErrorMessage = "Please Enter Email")]
		public string? Email { get; set; }
		[Required(ErrorMessage = "Please Enter Password")]
		public string? Password { get; set; }
		[Required(ErrorMessage = "Please Enter Confirm Password")]
		[Compare("Password")]
        public string? Confirm_Password { get; set; }
		[Required(ErrorMessage = "Please Enter Date of Birth")]
		public string? DOB { get; set; }
		[Required(ErrorMessage = "Please Enter Address")]
		public string? Address { get; set; }
		[Required(ErrorMessage = "Please Enter Country")]

		public string? Country { get; set; }
		[Required(ErrorMessage = "Please Enter Phone")]
		public string? Phone { get; set; }
		[Required(ErrorMessage = "Please Enter State")]
		public string? State { get; set; }
		[Required(ErrorMessage = "Please Enter Skills")]
		public string? Skills { get; set; }
        [AllowNull]
        public string Git { get; set; }
        [AllowNull]
		public string Link { get; set; }
        [AllowNull]
        public IFormFile? Image { get; set; }
		[Required(ErrorMessage = "Please Select Resume")]
		public IFormFile? Resume { get; set; }
		[Required(ErrorMessage = "Please Enter School Name")]
		public string? s_schoolname { get; set; }
		[Required(ErrorMessage = "Please Enter Stream Name")]
		public string? s_streamname { get; set; }
		[Required(ErrorMessage = "Please Enter Start Date")]
		public string? s_startdate { get; set; }
		[Required(ErrorMessage = "Please Enter End Date")]
		public string? s_enddate { get; set; }
		[Required(ErrorMessage = "Please Enter Marks")]
		public double s_marks { get; set; }
		[Required(ErrorMessage = "Please Enter School Name")]
		public string? ss_schoolname { get; set; }
		[Required(ErrorMessage = "Please Enter Stream Name")]
		public string? ss_streamname { get; set; }
		[Required(ErrorMessage = "Please Enter Start Date")]
		public string? ss_startdate { get; set; }
		[Required(ErrorMessage = "Please Enter End Date")]
		public string? ss_enddate { get; set; }
		[Required(ErrorMessage = "Please Enter Marks")]
		public double ss_marks { get; set; }
		[Required(ErrorMessage = "Please Enter College Name")]
		public string? c_schoolname { get; set; }
		[Required(ErrorMessage = "Please Enter Stream Name")]
		public string? c_streamname { get; set; }
		[Required(ErrorMessage = "Please Enter Start Date")]
		public string? c_startdate { get; set; }
		[Required(ErrorMessage = "Please Enter End Date")]
		public string? c_enddate { get; set; }
		[Required(ErrorMessage = "Please Enter College Marks")]
		public double c_marks { get; set; }

    }
}
