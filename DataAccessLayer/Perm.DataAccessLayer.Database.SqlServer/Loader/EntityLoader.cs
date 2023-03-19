using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ImportDefinition = System.ComponentModel.Composition.Primitives.ImportDefinition;

namespace Perm.DataAccessLayer.Database.SqlServer.Loader;
public static class EntityLoader
{
    public static void LoadEntities(this ModelBuilder modelBuilder)
    {
        DirectoryCatalog dirCat = new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory);
        ImportDefinition importDef = BuildImportDefinition();
        try
        {
            using (AggregateCatalog aggregateCatalog = new AggregateCatalog())
            {
                aggregateCatalog.Catalogs.Add(dirCat);

                using (CompositionContainer componsitionContainer = new CompositionContainer(aggregateCatalog))
                {
                    IEnumerable<Export> exports = componsitionContainer.GetExports(importDef);

                    IEnumerable<IEntityResolver> components = exports.Select(export => export.Value as IEntityResolver).Where(m => m != null);

                    foreach (IEntityResolver component in components)
                        component.SetUp(modelBuilder);
                }
            }
        }
        catch (ReflectionTypeLoadException typeLoadException)
        {
            StringBuilder builder = new StringBuilder();
            if (typeLoadException.LoaderExceptions != null)
                foreach (Exception loaderException in typeLoadException.LoaderExceptions)
                    if (loaderException != null) builder.AppendFormat("{0}\n", loaderException.Message);

            throw new TypeLoadException(builder.ToString(), typeLoadException);
        }
    }

    private static ImportDefinition BuildImportDefinition() => new ImportDefinition(def => true, typeof(IEntityResolver).FullName, ImportCardinality.ZeroOrMore, false, false);
}