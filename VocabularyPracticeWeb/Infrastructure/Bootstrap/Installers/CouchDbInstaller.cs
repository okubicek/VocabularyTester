using ArmChair;
using ArmChair.Http;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ComponentActivator;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.Extensions.Configuration;
using VocabularyPracticeWeb.Infrastructure.CouchDb;
using VocabularyPracticeWeb.Infrastructure.CouchDb.Setup;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace VocabularyPracticeWeb.Infrastructure.Bootstrap.Installers
{
	//https://bitbucket.org/dboneslabs/arm-chair/src/master/samples/todo/Todo.Service/Infrastructure/Modules/DataAccessModule.cs

	public class CouchDbInstaller : IWindsorInstaller
	{
		private IConfiguration _configuration;

		private string DbName = "wizarddata";

		public CouchDbInstaller(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Component
				.For(typeof(IBlobRepository<>))
				.ImplementedBy(typeof(CouchDbRepository<>))
				.LifestyleTransient());

			container.Register(Component
					.For<Connection>()
					.DependsOn(Dependency.OnValue("baseUrl", _configuration.GetConnectionString("CouchDbConnection")))
					.OnCreate(x => new CouchDbSetup().EnsureDatabaseExists(x, DbName))
					.LifestyleSingleton());

			container.Register(Component
				.For<Database>()
				.DependsOn(Dependency.OnValue("databaseName", DbName))
				.OnCreate(x => new CouchDbSetup().SetupDatabase(x))
				.LifestyleSingleton());

			container.Register(Component
				.For<ISession>()
				.Activator<CouchDbSessionActivator>()
				.LifeStyle.Transient);
		}

		public class CouchDbSessionActivator : DefaultComponentActivator
		{
			public CouchDbSessionActivator(ComponentModel model, IKernelInternal kernel, ComponentInstanceDelegate onCreation, ComponentInstanceDelegate onDestruction) 
				: base(model, kernel, onCreation, onDestruction)
			{
			}

			protected override object InternalCreate(CreationContext context)
			{
				return Kernel.Resolve<Database>().CreateSession();
			}
		}
	}
}
