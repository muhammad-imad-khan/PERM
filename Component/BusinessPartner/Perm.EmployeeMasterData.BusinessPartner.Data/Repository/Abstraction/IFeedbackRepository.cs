using Perm.DataAccessLayer.DataRepository.Core.Abstraction;
using Perm.Model.Employee;

namespace Perm.EmployeeMasterData.BusinessPartner.Data.Repository.Abstraction
{
    public interface IFeedbackRepository : IRepository<FeedbackModel> 
    {
        Task AddFeedback(FeedbackModel model);
    }
}
