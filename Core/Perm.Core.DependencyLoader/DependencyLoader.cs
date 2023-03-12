using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Perm.Core.RequestManager.Processor;

namespace Perm.Core.DependencyResolver
{
    public static class DependencyLoader
    {
        public static void LoadDependencies(this IServiceCollection serviceCollection)
        {
            DirectoryCatalog dirCat = new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory);
            ImportDefinition importDef = BuildImportDefinition();
            try
            {
                using (AggregateCatalog aggregateCatalog = new AggregateCatalog())
                {
                    aggregateCatalog.Catalogs.Add(dirCat);

                    using (CompositionContainer compositionContainer = new CompositionContainer(aggregateCatalog))
                    {
                        IEnumerable<Export> exports = compositionContainer.GetExports(importDef);


                        IEnumerable<IDependencyResolver> modules = exports.Select(export => export.Value as IDependencyResolver).Where(m => m != null);
                        //  IEnumerable<IDependencyResolver> modules = exports.Select(export => export.Value as IDependencyResolver).Where(m => m != null);

                        DependencyRegister registerComponent = new DependencyRegister(serviceCollection);
                        foreach (IDependencyResolver module in modules)
                        {
                            module.SetUp(registerComponent);
                        }
                    }
                }
            }
            catch (ReflectionTypeLoadException typeLoadException)
            {
                StringBuilder builder = new StringBuilder();
                foreach (Exception loaderException in typeLoadException.LoaderExceptions)
                {
                    if (loaderException != null) builder.AppendFormat("{0}\n", loaderException.Message);
                }

                throw new TypeLoadException(builder + Environment.NewLine +
                                            string.Join(Environment.NewLine, typeLoadException.Types.Where(c => c is not null).Select(c => c.Name + "=>" + c.FullName)) + Environment.NewLine
                    , typeLoadException);
            }
        }

        private static ImportDefinition BuildImportDefinition()
        {
            return new ImportDefinition(
                def => true, typeof(IDependencyResolver).FullName, ImportCardinality.ZeroOrMore, false, false);
        }

        public static List<ServiceBase> RegisteredServices { get; set; }
    }
}