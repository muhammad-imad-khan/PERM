namespace Perm.Core.DependencyResolver
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDependencyResolver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependencyRegister"></param>
        void SetUp(IDependencyRegister dependencyRegister);
    }
}