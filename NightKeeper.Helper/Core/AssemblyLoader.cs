using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;

namespace NightKeeper.Helper.Core
{
    public class AssemblyLoader : AssemblyLoadContext
    {
        protected override Assembly Load(AssemblyName assemblyName)
        {
            var deps = DependencyContext.Default;
            var res = deps.CompileLibraries.Where(d => d.Name.Contains(assemblyName.Name)).ToList();
            var name = res.FirstOrDefault()?.Name;
            if (name != null && !"runtime.native.System.Net.Http".Equals(name))
            {
                try
                {
                    return Assembly.Load(new AssemblyName(name));
                }
                catch
                {
                    return Assembly.Load(assemblyName);
                }
            }

            return Assembly.Load(assemblyName);
        }
    }
}