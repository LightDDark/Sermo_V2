using System;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Data;

namespace Sermo_WAPI_Trial2
{
	public class Ratings
	{
		[Key]

		public int Id { get; set; }
		public string? Author { get; set; }
		public string? Content { get; set; }

		public List<Ratings> getRatings()
		{
			using (var db = new WebApplication1Context())
			{
				var RateList = db.Ratings.ToList();
				return RateList;

			}
		}

	}
}

