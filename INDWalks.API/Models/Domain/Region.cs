using System;
namespace INDWalks.API.Models.Domain
{
	public class Region
	{
		public Region()
		{
		}

		public Guid Id { get; set; }

		public String Code { get; set; }

		public String Name { get; set; }

		public string? RegionImageURL { get; set; }
	}
}

