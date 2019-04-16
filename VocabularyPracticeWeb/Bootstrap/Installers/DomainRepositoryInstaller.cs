using Castle.Windsor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VocabularyPracticeEFCoreRepository;

namespace VocabularyPracticeWeb.Bootstrap.Installers
{
    public class DomainRepositoryInstaller
    {
		public void Install(IServiceCollection services, IConfiguration configuration, IWindsorContainer container)
		{
			services.AddDbContext<VocabularyPracticeDbContext>(
					options => options.UseSqlServer(
							configuration.GetConnectionString("ApplicationConnection"),
							x => x.MigrationsAssembly("VocabularyPracticeEFCoreRepository"))
			);
		}
    }
}
