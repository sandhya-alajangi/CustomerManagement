using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Domain
{
	public class Customer
	{
		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		[Key]
		public Guid Id { get; set; }
		/// <summary>
		/// Gets or sets the first name
		/// </summary>
		[MaxLength(64)]
		public string FirstName { get; set; }

		/// <summary>
		/// Gets or sets the last name
		/// </summary>
		[MaxLength(64)]
		public string LastName { get; set; }

		/// <summary>
		/// Gets or sets Emails for the customer
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		/// Gets or sets PhoneNumber for the customer
		/// </summary>
		public string PhoneNumber { get; set; }
	}
}
