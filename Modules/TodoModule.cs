using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Raven.Client;
using Nancy.ModelBinding;
using NancyRavenTodoMvc.Models;

namespace NancyRavenTodoMvc.Modules
{
	public class TodoModule : NancyModule
	{
		private IDocumentStore _documentStore;
		private IDocumentSession _documentSession;

		public TodoModule(IDocumentStore documentStore, IDocumentSession documentSession) : base("todo")
		{
			_documentStore = documentStore;
			_documentSession = documentSession;

			Get["/"] = _ =>
			{
				return Response.AsJson<List<Todo>>(_documentSession.Query<Todo>().ToList());
			};

			Post["/"] = _ =>
			{
				var todo = this.Bind<Todo>();
				_documentSession.Store(todo);
				return Response.AsJson<Todo>(todo, HttpStatusCode.Created);
			};

			Put["/"] = _ =>
			{
				var todo = this.Bind<Todo>();

				_documentSession.Store(todo);

				return Response.AsJson<Todo>(todo);
			};

			Delete["/"] = _ =>
			{
				var todo = this.Bind<Todo>();

				todo = _documentSession.Load<Todo>(todo.Id);
				_documentSession.Delete<Todo>(todo);

				return Response.AsJson<Todo>(todo);
			};
		}
	}
}
