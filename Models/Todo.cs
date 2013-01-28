using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NancyRavenTodoMvc.Models
{
	public class Todo
	{
		public string Id { get; set; }
		public string title { get; set; }
		public bool completed { get; set; }
	}
}
