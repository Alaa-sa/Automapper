using Automapper.Models;
using Automapper.DTOs;
using AutoMapper;
using System;
using System.ComponentModel;

namespace Automapper
{
	public class Program
	{
		private static Mapper _mapper;
		static void Main(string[] args)
		{
			RegisterMapperConfig();
			//MapContactToContactDTONoAutoMapper();
			//MapContactToContactDTO(); // a1
			//MapNullToContactDTO();
			//MapContactWithFullNameToContactDTO(); // a2
			//MapContactWithAddressToContactDTO(); // a3
			//MapContactWithAddressToContactFlatDTO();
			ExitApp();
		}

		private static void MapNullToContactDTO()
		{
			var dto = _mapper.Map<ContactDto>(null);
			PrintObject(dto);
		}

		private static void MapContactToContactDTONoAutoMapper()
		{
			var contact = new Contact { ContactId = 1, FirstName = "ALaa", LastName = "saghir", Age = 30 };

			// method 1
			var contactDTO = new ContactDto { Age = contact.Age, FullName = contact.FirstName + contact.LastName };

			// method 2
			//var contactDTO = new ContactDTO();
			//contactDTO.Age = contact.Age;
			//contactDTO.FullName = $"{contact.FirstName} {contact.LastName}";

			PrintObject(contactDTO);
		}

		private static void MapContactToContactDTO()
		{
			var contact = new Contact { ContactId = 1, FirstName = "ALaa", LastName = "saghir", Age = 30 };
			var dto = _mapper.Map<ContactDto>(contact); // no error
			PrintObject(dto);
		}

		private static void MapContactWithFullNameToContactDTO()
		{
			// inheritence
			var contact = new ContactWithFullName { ContactId = 1, FirstName = "ALaa", LastName = "saghir", Age = 20 };
			var dto = _mapper.Map<ContactDto>(contact); // no mapping error
			PrintObject(dto);
		}

		private static void MapContactWithAddressToContactDTO()
		{
			// no inheritence
			var address = new Address {HouseNumber = "Ab3C", PostalCode="CE44", StreetName="Bliss st." };
			var contact = new ContactWithAddress { ContactId = 1, FirstName = "ALaa", LastName = "saghir", Age = 10, Address =  address};
			var dto = _mapper.Map<ContactWithAddressDto>(contact); // error missing type map config if address not mapped
			PrintObject(dto);
		}

		private static void MapContactWithAddressToContactFlatDTO()
		{
			var address = new Address { HouseNumber = "Ab3C", PostalCode = "CE44", StreetName = "Bliss st." };
			var contact = new ContactWithAddress { ContactId = 1, FirstName = "ALaa", LastName = "saghir", Age = 10, Address = address };
			var dto = _mapper.Map<ContactWithAddressFlatDto>(contact); 
			PrintObject(dto);
		}
		private static void RegisterMapperConfig()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Contact, ContactDto>(); // a1
				cfg.CreateMap<ContactWithFullName, ContactDto>(); // a2, works without the mapping, above is enough

				cfg.CreateMap<ContactWithAddress, ContactWithAddressDto>(); // a3
				cfg.CreateMap<Address, AddressDto>()
					.ForMember(dest => dest.Code, org => org.MapFrom(d => d.PostalCode))
					.ForMember(dest => dest.HomeAddress, org => org.MapFrom(d => d.HouseNumber));

				cfg.CreateMap<ContactWithAddress, ContactWithAddressFlatDto>()
					.ForMember(dest => dest.Code, org => org.MapFrom(d => d.Address.PostalCode))
					.ForMember(dest => dest.StreetName, org => org.MapFrom(d => d.Address.StreetName))
					.ForMember(dest => dest.HomeAddress, org => org.MapFrom(d => d.Address.HouseNumber));
			});
			_mapper = new Mapper(config);
		}
		private static void ExitApp()
		{
			Console.WriteLine("Press any key to close the window...");
			Console.ReadKey();
		}
		private static void PrintObject(object obj)
		{
			if (obj is null) Console.WriteLine("null");
			foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
			{
				string name = descriptor.Name;
				object value = descriptor.GetValue(obj);
				if (value?.GetType().Name == "AddressDto")
				{
					Console.WriteLine("Address:");
					foreach (PropertyDescriptor dp in TypeDescriptor.GetProperties(value))
					{
						string name2 = dp.Name;
						object value2 = dp.GetValue(value);
						Console.WriteLine($"\t{name2}: {value2}");
					}
				}
				else
				{
					Console.WriteLine($"{name}: {value}");
				}
			}
		}
	}
}
/*
 * REFERENCES
 * https://docs.automapper.org/en/stable/Getting-started.html
 * https://code-maze.com/automapper-net-core/
 * https://dotnettutorials.net/lesson/automapper-in-c-sharp/
*/