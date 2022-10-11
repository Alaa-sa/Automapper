namespace Automapper.Models
{
	public class Contact
	{
		public int ContactId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }
	}

	public class ContactWithFullName: Contact
	{
		public string FullName { get; set; }
		//public string GetFullName() => $"{FirstName} {LastName}";
	}

	public class ContactWithAddress
	{
		public int ContactId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }
		public Address Address { get; set; }
	}
}
