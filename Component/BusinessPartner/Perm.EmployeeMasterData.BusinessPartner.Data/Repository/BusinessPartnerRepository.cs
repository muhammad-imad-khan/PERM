using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.DataAccessLayer.DataRepository.Core;
using Perm.EmployeeMasterData.BusinessPartner.Data.Repository.Abstraction;
using Perm.Model.Abstraction;
using Perm.Model.EmployeeMasterData;

namespace Perm.EmployeeMasterData.BusinessPartner.Data.Repository
{
    public class BusinessPartnerRepository : Repository<BusinessPartnerModel>, IBusinessPartnerRepository
    {
        private readonly IEmployeeAddressRepository _employeeAddressRepository;
        private readonly IEmployeeContactRepository _employeeContactRepository;
        private readonly IEmployeeShiftRepository _employeeShiftRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IBankDetailRepository _bankDetailRepository;
        private readonly IPersonalDetailRepository _personalDetailRepository;
        private readonly IHistoryInCompanyRepostory _historyInCompanyRepostory;
        private readonly ILeavingDetailRepository _leavingDetailRepository;
        private readonly ILevelRepository _levelRepository;
        private readonly IHealthInsurranceRepository _healthInsurranceRepository;

        public BusinessPartnerRepository(
            PermDataContext dataContext,
            IHttpContextAccessor httpContextAccessor,
            IEmployeeAddressRepository employeeAddressRepository,
            IEmployeeContactRepository employeeContactRepository,
            IEmployeeShiftRepository employeeShiftRepository,
            IEducationRepository educationRepository,
            IBankDetailRepository bankDetailRepository,
            IPersonalDetailRepository personalDetailRepository,
            IHistoryInCompanyRepostory historyInCompanyRepostory,
            ILeavingDetailRepository leavingDetailRepository,
            ILevelRepository levelRepository,
            IHealthInsurranceRepository healthInsurranceRepository) : base(dataContext, httpContextAccessor)
        {
            _employeeAddressRepository = employeeAddressRepository;
            _employeeContactRepository = employeeContactRepository;
            _employeeShiftRepository = employeeShiftRepository;
            _educationRepository = educationRepository;
            _bankDetailRepository = bankDetailRepository;
            _personalDetailRepository = personalDetailRepository;
            _historyInCompanyRepostory = historyInCompanyRepostory;
            _leavingDetailRepository = leavingDetailRepository;
            _levelRepository = levelRepository;
            _healthInsurranceRepository = healthInsurranceRepository;
        }

        protected override IIncludableQueryable<BusinessPartnerModel, object> IncludeForeignKeys(IQueryable<BusinessPartnerModel> entities)
        {
            entities = entities.Include(s => s.Department);
            entities = entities.Include(s => s.ParamGender);

            return entities.Include(s => s.ParamLevel);
        }

        public async Task AddBusinessPartner(BusinessPartnerModel businessPartnerModel)
        {
            await AddAsync(businessPartnerModel);

            MapAuditFields(businessPartnerModel.PersonalDetail, businessPartnerModel);
            MapAuditFields(businessPartnerModel.Address, businessPartnerModel);
            MapAuditFields(businessPartnerModel.Contact, businessPartnerModel);
            MapAuditFields(businessPartnerModel.Education, businessPartnerModel);
            MapAuditFields(businessPartnerModel.BankDetail, businessPartnerModel);
            MapAuditFields(businessPartnerModel.Shift, businessPartnerModel);
            MapAuditFields(businessPartnerModel.HealthInsurance, businessPartnerModel);
            MapAuditFields(businessPartnerModel.HistoryInCompany, businessPartnerModel);
            MapAuditFields(businessPartnerModel.Level, businessPartnerModel);
            MapAuditFields(businessPartnerModel.LeavingDetail, businessPartnerModel);

            await Context.CommitChangesAsync();
        }

        public async Task UpdateBusinessPartner(BusinessPartnerModel businessPartnerModel)
        {
            Update(businessPartnerModel);

            await AddDeleteAddress(businessPartnerModel);
            await AddDeleteEducation(businessPartnerModel);
            await AddDeleteShift(businessPartnerModel);
            await AddDeleteContact(businessPartnerModel);
            await AddDeleteEmployeeBankDetail(businessPartnerModel);
            await AddDeleteHistoryInCompany(businessPartnerModel);

            await Context.CommitChangesAsync();
        }

        public async Task DeleteBusinessPartner(long businessPartnerID)
        {
            IQueryable<EmployeeAddressModel> dbEmployeeAddress = _employeeAddressRepository.Query(s => s.BusinessPartnerID == businessPartnerID);
            IQueryable<EmployeeContactModel> dbEmployeeContact = _employeeContactRepository.Query(s => s.BusinessPartnerID == businessPartnerID);
            IQueryable<EmployeeBankDetailModel> dbEmployeeBankDetail = _bankDetailRepository.Query(s => s.BusinessPartnerID == businessPartnerID);
            IQueryable<EducationModel> dbEducation = _educationRepository.Query(s => s.BusinessPartnerID == businessPartnerID);
            IQueryable<HistoryInCompanyModel> dbHistoryInCompany = _historyInCompanyRepostory.Query(s => s.BusinessPartnerID == businessPartnerID);
            IQueryable<HealthInsurranceModel> dbHealthInsurrance = _healthInsurranceRepository.Query(s => s.BusinessPartnerID == businessPartnerID);
            IQueryable<EmployeeShiftModel> dbShift = _employeeShiftRepository.Query(s => s.BusinessPartnerID == businessPartnerID);
            IQueryable<LeavingDetailModel> dbLeavingDetail = _leavingDetailRepository.Query(s => s.BusinessPartnerID == businessPartnerID);
            IQueryable<LevelModel> dbLevel = _levelRepository.Query(s => s.BusinessPartnerID == businessPartnerID);
            IQueryable<PersonalDetailModel> dbPersonalDetail = _personalDetailRepository.Query(s => s.BusinessPartnerID == businessPartnerID);

            _employeeAddressRepository.DeleteRange(dbEmployeeAddress);
            _employeeContactRepository.DeleteRange(dbEmployeeContact);
            _bankDetailRepository.DeleteRange(dbEmployeeBankDetail);
            _educationRepository.DeleteRange(dbEducation);
            _historyInCompanyRepostory.DeleteRange(dbHistoryInCompany);
            _healthInsurranceRepository.DeleteRange(dbHealthInsurrance);
            _employeeShiftRepository.DeleteRange(dbShift);
            _leavingDetailRepository.DeleteRange(dbLeavingDetail);
            _levelRepository.DeleteRange(dbLevel);
            _personalDetailRepository.DeleteRange(dbPersonalDetail);

            Delete(new BusinessPartnerModel { BusinessPartnerID = businessPartnerID });

            await Context.CommitChangesAsync();
        }

        #region Private Methods for 

        #region one to many

        //employeeAddress
        private async Task AddDeleteAddress(BusinessPartnerModel businessPartnerModel)
        {
            if (businessPartnerModel.Address is null) return;

            List<EmployeeAddressModel> deleteEmployeeAddress = new List<EmployeeAddressModel>();

            List<EmployeeAddressModel> dbEmployeeAddress = _employeeAddressRepository.Query(s => s.BusinessPartnerID == businessPartnerModel.BusinessPartnerID).ToList();

            if (dbEmployeeAddress != null && businessPartnerModel.Address != null)
            {
                foreach (EmployeeAddressModel dbItem in dbEmployeeAddress)
                {
                    if (!businessPartnerModel.Address.Exists(s => s.BusinessPartnerID == dbItem.BusinessPartnerID))
                    {
                        deleteEmployeeAddress.Add(new EmployeeAddressModel { EmployeeAddressID = dbItem.EmployeeAddressID });
                    }
                }
            }

            _employeeAddressRepository.DeleteRange(deleteEmployeeAddress);

            if (businessPartnerModel.Address != null)
            {
                foreach (EmployeeAddressModel? reqItem in businessPartnerModel.Address)
                {
                    if (dbEmployeeAddress != null && !dbEmployeeAddress.Exists(s => s.EmployeeAddressID == reqItem.EmployeeAddressID))
                    {
                        await _employeeAddressRepository.AddAsync(reqItem);
                    }
                }
            }
        }

        //BankDetails
        private async Task AddDeleteEmployeeBankDetail(BusinessPartnerModel businessPartnerModel)
        {
            if (businessPartnerModel.BankDetail is null) return;

            List<EmployeeBankDetailModel> deleteEmployeeBankDetail = new List<EmployeeBankDetailModel>();

            List<EmployeeBankDetailModel> dbEmployeeBankDetail = _bankDetailRepository.Query(s => s.BusinessPartnerID == businessPartnerModel.BusinessPartnerID).ToList();

            if (dbEmployeeBankDetail != null && businessPartnerModel.BankDetail != null)
            {
                foreach (EmployeeBankDetailModel dbItem in dbEmployeeBankDetail)
                {
                    if (!businessPartnerModel.BankDetail.Exists(s => s.BusinessPartnerID == dbItem.BusinessPartnerID))
                    {
                        deleteEmployeeBankDetail.Add(new EmployeeBankDetailModel { EmployeeBankDetailID = dbItem.EmployeeBankDetailID });
                    }
                }
            }

            _bankDetailRepository.DeleteRange(deleteEmployeeBankDetail);

            if (businessPartnerModel.BankDetail != null)
            {
                foreach (EmployeeBankDetailModel? reqItem in businessPartnerModel.BankDetail)
                {
                    if (dbEmployeeBankDetail != null && !dbEmployeeBankDetail.Exists(s => s.EmployeeBankDetailID == reqItem.EmployeeBankDetailID))
                    {
                        await _bankDetailRepository.AddAsync(reqItem);
                    }
                }
            }
        }

        //education
        private async Task AddDeleteEducation(BusinessPartnerModel businessPartnerModel)
        {
            if (businessPartnerModel.Education is null) return;

            List<EducationModel> deleteEmployeeEducation = new List<EducationModel>();

            List<EducationModel> dbEmployeeEducation = _educationRepository.Query(s => s.BusinessPartnerID == businessPartnerModel.BusinessPartnerID).ToList();

            if (dbEmployeeEducation != null && businessPartnerModel.Education != null)
            {
                foreach (EducationModel dbItem in dbEmployeeEducation)
                {
                    if (!businessPartnerModel.Education.Exists(s => s.BusinessPartnerID == dbItem.BusinessPartnerID))
                    {
                        deleteEmployeeEducation.Add(new EducationModel { EducationID = dbItem.EducationID });
                    }
                }
            }

            _educationRepository.DeleteRange(deleteEmployeeEducation);

            if (businessPartnerModel.Education != null)
            {
                foreach (EducationModel? reqItem in businessPartnerModel.Education)
                {
                    if (dbEmployeeEducation != null && !dbEmployeeEducation.Exists(s => s.EducationID == reqItem.EducationID))
                    {
                        await _educationRepository.AddAsync(reqItem);
                    }
                }
            }
        }

        //Shift
        private async Task AddDeleteShift(BusinessPartnerModel businessPartnerModel)
        {
            if (businessPartnerModel.Shift is null) return;

            List<EmployeeShiftModel> deleteEmployeeShift = new List<EmployeeShiftModel>();

            List<EmployeeShiftModel> dbEmployeeShift = _employeeShiftRepository.Query(s => s.BusinessPartnerID == businessPartnerModel.BusinessPartnerID).ToList();

            if (dbEmployeeShift != null && businessPartnerModel.Shift != null)
            {
                foreach (EmployeeShiftModel dbItem in dbEmployeeShift)
                {
                    if (!businessPartnerModel.Address.Exists(s => s.BusinessPartnerID == dbItem.BusinessPartnerID))
                    {
                        deleteEmployeeShift.Add(new EmployeeShiftModel { EmployeeShiftID = dbItem.EmployeeShiftID });
                    }
                }
            }

            _employeeShiftRepository.DeleteRange(deleteEmployeeShift);

            if (businessPartnerModel.Address != null)
            {
                foreach (EmployeeShiftModel? reqItem in businessPartnerModel.Shift)
                {
                    if (dbEmployeeShift != null && !dbEmployeeShift.Exists(s => s.EmployeeShiftID == reqItem.EmployeeShiftID))
                    {
                        await _employeeShiftRepository.AddAsync(reqItem);
                    }
                }
            }
        }

        //Contact

        private async Task AddDeleteContact(BusinessPartnerModel businessPartnerModel)
        {
            if (businessPartnerModel.Contact is null) return;

            List<EmployeeContactModel> deleteEmployeeContact = new List<EmployeeContactModel>();

            List<EmployeeContactModel> dbEmployeeContact = _employeeContactRepository.Query(s => s.BusinessPartnerID == businessPartnerModel.BusinessPartnerID).ToList();

            if (dbEmployeeContact != null && businessPartnerModel.Contact != null)
            {
                foreach (EmployeeContactModel dbItem in dbEmployeeContact)
                {
                    if (!businessPartnerModel.Address.Exists(s => s.BusinessPartnerID == dbItem.BusinessPartnerID))
                    {
                        deleteEmployeeContact.Add(new EmployeeContactModel { ContactID = dbItem.ContactID });
                    }
                }
            }

            _employeeContactRepository.DeleteRange(deleteEmployeeContact);

            if (businessPartnerModel.Address != null)
            {
                foreach (EmployeeContactModel? reqItem in businessPartnerModel.Contact)
                {
                    if (dbEmployeeContact != null && !dbEmployeeContact.Exists(s => s.ContactID == reqItem.ContactID))
                    {
                        await _employeeContactRepository.AddAsync(reqItem);
                    }
                }
            }
        }

        //History In Company
        private async Task AddDeleteHistoryInCompany(BusinessPartnerModel businessPartnerModel)
        {
            if (businessPartnerModel.Shift is null) return;

            List<HistoryInCompanyModel> deleteEmployeeHistoryInCompany = new List<HistoryInCompanyModel>();

            List<HistoryInCompanyModel> dbEmployeeHistoryInCompany = _historyInCompanyRepostory.Query(s => s.BusinessPartnerID == businessPartnerModel.BusinessPartnerID).ToList();

            if (dbEmployeeHistoryInCompany != null && businessPartnerModel.HistoryInCompany != null)
            {
                foreach (HistoryInCompanyModel dbItem in dbEmployeeHistoryInCompany)
                {
                    if (!businessPartnerModel.HistoryInCompany.Exists(s => s.BusinessPartnerID == dbItem.BusinessPartnerID))
                    {
                        deleteEmployeeHistoryInCompany.Add(new HistoryInCompanyModel { HistoryInCompanyID = dbItem.HistoryInCompanyID });
                    }
                }
            }

            _historyInCompanyRepostory.DeleteRange(deleteEmployeeHistoryInCompany);

            if (businessPartnerModel.HistoryInCompany != null)
            {
                foreach (HistoryInCompanyModel? reqItem in businessPartnerModel.HistoryInCompany)
                {
                    if (dbEmployeeHistoryInCompany != null && !dbEmployeeHistoryInCompany.Exists(s => s.HistoryInCompanyID == reqItem.HistoryInCompanyID))
                    {
                        await _historyInCompanyRepostory.AddAsync(reqItem);
                    }
                }
            }
        }

        #endregion

        private void MapAuditFields<T>(T model, BusinessPartnerModel businessPartnerModel) where T : ModelBase
        {
            if (model != null)
            {
                {
                    model.CreatedOn = businessPartnerModel.CreatedOn;
                    model.CreatedBy = businessPartnerModel.CreatedBy;
                    model.IsDeleted = businessPartnerModel.IsDeleted;
                }
            }
        }

        private void MapAuditFields<T>(List<T> model, BusinessPartnerModel businessPartnerModel) where T : ModelBase
        {
            if (model != null)
            {
                foreach (T item in model)
                {
                    item.CreatedOn = businessPartnerModel.CreatedOn;
                    item.CreatedBy = businessPartnerModel.CreatedBy;
                    item.IsDeleted = businessPartnerModel.IsDeleted;
                }
            }
        }

        #endregion
    }
}
