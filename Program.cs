using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using Nancy.TinyIoc;
using NancyRavenTodoMvc.Helpers;
using Raven.Client;
using Raven.Client.Document;

namespace NancyRavenTodoMvc
{
	class Program
	{
		static void Main(string[] args)
		{
			var nancyHost = new Nancy.Hosting.Self.NancyHost(new Uri("http://localhost:8081"), new TodoMvcBootstrap());
			nancyHost.Start();

			Console.ReadLine();
			nancyHost.Stop();
		}
	}
}
