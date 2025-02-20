using Autofac;
using CloudComputingProvider.Infrastructure.Persistence;
using System.Reflection;

namespace CloudComputingProvider.DI
{
    public class RegisterAutofacAssemblies : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory
            // for you.
            List<Assembly> AssembliesForDI = new List<Assembly>();
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            foreach (string dll in Directory.GetFiles(path, "*.dll"))
            {
                if (dll.Contains("Persistence", StringComparison.OrdinalIgnoreCase) == true)
                    continue;

                if (dll.Contains("Services", StringComparison.OrdinalIgnoreCase) == true
                    || dll.Contains("BusinessLogic", StringComparison.OrdinalIgnoreCase) == true
                    || dll.Contains("Infrastructure", StringComparison.OrdinalIgnoreCase) == true
                    )
                {
                    AssembliesForDI.Add(Assembly.LoadFile(dll));
                }
            }

            foreach (var assembly in AssembliesForDI)
            {
                builder.RegisterAssemblyTypes(assembly)
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
            }

            builder.RegisterType<SeedData>().InstancePerLifetimeScope();
        }
    }
}
