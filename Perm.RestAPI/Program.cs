using Perm.Common.APIHelper;

namespace Perm.RestAPI
{
    /// <summary>
    ///     Entry class of API
    /// </summary>
    public class Program
    {
        /// <summary>
        ///     Entry method of API
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            ApiStartup
                .InitializeMainMethod(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .Build()
                .Run();
        }
    }
}