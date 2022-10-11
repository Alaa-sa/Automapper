namespace Automapper.DTOs
{
	public class ContactDto
	{
		public string FullName { get; set; }
		public int Age { get; set; }
	}

	public class ContactWithAddressDto
	{
		public string FullName { get; set; }
		public int Age { get; set; }

		public AddressDto Address { get; set; }
	}

	public class ContactWithAddressFlatDto
	{
		public string FullName { get; set; }
		public int Age { get; set; }
		public string HomeAddress { get; set; }
		public string Code { get; set; }
		public string StreetName { get; set; }
	}
}
