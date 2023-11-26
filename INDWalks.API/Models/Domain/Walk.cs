using System;
namespace INDWalks.API.Models.Domain
{
	public class Walk
	{
		public Walk()
		{
		}

		public Guid ID { get; set; }

		public String Name { get; set; }

		public String Description { get; set; }

		public double LengthInKM { get; set; }

		public String? WalkImageURL { get; set; }

		public Guid DifficultyID { get; set; }

		public Guid RegionID { get; set; }


		//Navigation Property
		public Difficulty Difficulty { get; set; }

		public Region Region { get; set; }
		 
	}
}

