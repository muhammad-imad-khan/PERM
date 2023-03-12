using Perm.Core.RequestManager.Processor;

namespace Perm.Core.ComponentFactoryManager
{
    public interface IComponentFactory
    {
        ServiceBase GetComponent();
        ServiceBase GetComponent(string url);

        /// <summary>
        /// Check if this request is authenticated or not
        /// </summary>
        /// <param name="serviceBase"></param>
        void IsAuthenticate(ServiceBase serviceBase);

        void IsAuthorized(long userID, string apiPath, string httpHeader);
    }
}