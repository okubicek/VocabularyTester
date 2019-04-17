using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VocabularyPracticeEFCoreRepository;

namespace VocabularyPracticeWeb.Bootstrap.Installers
{
    public class DomainRepositoryInstaller : IWindsorInstaller
    {
		private IServiceCollection _services;

		private IConfiguration _configuration;

		public DomainRepositoryInstaller(IServiceCollection services, IConfiguration configuration)
		{
			_services = services;
			_configuration = configuration;
		}		

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			_services.AddDbContext<VocabularyPracticeDbContext>(
					options => options.UseSqlServer(
							_configuration.GetConnectionString("ApplicationConnection"),
							x => x.MigrationsAssembly("VocabularyPracticeEFCoreRepository"))
			);
		}
	}
}
