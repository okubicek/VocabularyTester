using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Common.Cqrs;
using VocabularyPracticeWeb.Domain.Users;

namespace VocabularyPracticeWeb.Bootstrap.Installers
{
	public class WebInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Classes
				.FromAssemblyContaining<ApplicationUserQuery>()
				.BasedOn(typeof(IQuery<,>))
				.OrBasedOn(typeof(ICommand<,>))
				.WithService
				.Base()
				.LifestyleTransient()
			);
		}
	}
}
