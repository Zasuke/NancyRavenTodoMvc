using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.TinyIoc;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;

namespace NancyRavenTodoMvc.Helpers
{
	public class TodoMvcBootstrap : Nancy.DefaultNancyBootstrapper
	{
		protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
		{
			base.ConfigureRequestContainer(container, context);

			var documentStore = new DocumentStore() { Url = "http://localhost:8080" };
			documentStore.Initialize();
			documentStore.DatabaseCommands.EnsureDatabaseExists("TodosMvc");
			

			container.Register<IDocumentStore>(documentStore);
			container.Register<IDocumentSession>(documentStore.OpenSession("TodosMvc"));
		}

		protected override void RequestStartup(TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines, NancyContext context)
		{
			base.RequestStartup(container, pipelines, context);

			pipelines.AfterRequest.AddItemToEndOfPipeline(ctx =>
			{
				var documentStore = container.Resolve<IDocumentStore>();
				var documentSession = container.Resolve<IDocumentSession>();
				if (ctx.Response.StatusCode != HttpStatusCode.InternalServerError)
				{
					documentSession.SaveChanges();
				}
				documentSession.Dispose();
				documentStore.Dispose();
			});
		}
	}
}
