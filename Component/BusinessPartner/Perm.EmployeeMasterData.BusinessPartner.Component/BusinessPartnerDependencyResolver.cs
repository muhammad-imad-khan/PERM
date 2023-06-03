using Perm.Core.DependencyResolver;
using Perm.Core.RequestManager.Processor;
using Perm.EmployeeMasterData.BusinessPartner.Component.Service;
using Perm.EmployeeMasterData.BusinessPartner.Data.Repository;
using Perm.EmployeeMasterData.BusinessPartner.Data.Repository.Abstraction;
using System.ComponentModel.Composition;

namespace Perm.EmployeeMasterData.BusinessPartner.Component
{
    [Export(typeof(IDependencyResolver))]
    public class BusinessPartnerDependencyResolver : IDependencyResolver
    {
        public void SetUp(IDependencyRegister dependencyRegister)
        {
            dependencyRegister.AddScoped<ServiceBase, AddBusinessPartnerService>();
            dependencyRegister.AddScoped<ServiceBase, UpdateBusinessPartnerService>();
            dependencyRegister.AddScoped<ServiceBase, DeleteBusinessPartnerService>();
            dependencyRegister.AddScoped<ServiceBase, GetBusinessPartnerService>();

            dependencyRegister.AddScoped<IBusinessPartnerRepository, BusinessPartnerRepository>();
            dependencyRegister.AddScoped<IBankDetailRepository, BankDetailRepository>();
            dependencyRegister.AddScoped<IEducationRepository, EducationRepository>();
            dependencyRegister.AddScoped<IEmployeeAddressRepository, EmployeeAddressRepository>();
            dependencyRegister.AddScoped<IEmployeeContactRepository, EmployeeContactRepository>();
            dependencyRegister.AddScoped<IEmployeeShiftRepository, EmployeeShiftRepository>();
            dependencyRegister.AddScoped<IHealthInsurranceRepository, HealthInsurranceRepository>();
            dependencyRegister.AddScoped<IHistoryInCompanyRepostory, HistoryInCompanyRepository>();
            dependencyRegister.AddScoped<ILeavingDetailRepository, LeavingDetailRepository>();
            dependencyRegister.AddScoped<ILevelRepository, LevelRepository>();
            dependencyRegister.AddScoped<IPersonalDetailRepository, PersonalDetailRepository>();



        }
    }
}