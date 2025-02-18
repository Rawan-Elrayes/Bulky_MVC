﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
	public class Category
	{
        public int Id { get; set; }
		[Required]
		[DisplayName("Category Name")]
		[MaxLength(30)]
		public string Name { get; set; }
		[DisplayName("Display Order")]
		[Range(1,100,ErrorMessage ="1-100 Range")]
		public int DisplayOrder { get; set; }
    }
}
