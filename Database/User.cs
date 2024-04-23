using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginAndRegistration.Database
{
	//This class uses as a model or a template to store the user information that will be associated with the database users table
	public class User
	{
		public int UserId { get; set; }
		public string? UserEmail { get; set; }
		public string? Password { get; set; }	
		public string? Address1 { get; set; }
		public string? Address2 { get; set; }
		public string? City { get; set; }
		public string? State {get; set; }
		public string? PostalCode { get; set; }

	}
}
