using System;
using System.ComponentModel.DataAnnotations;

namespace Sermo_WAPI_Trial2
{
	public class Ratings
	{
		[Key]

		public int Id { get; set; }
		public string? Author { get; set; }
		public string? Content { get; set; }

	}
}

