using Perm.Common.APIModel;
using Perm.Config.ApplicationParamDetail.Data.Repository;
using Perm.Core.RequestManager.Processor;
using Perm.DataAccessLayer.DataRepository.Core.Model;
using Perm.Model.Config;

namespace Perm.Config.ApplicationParamDetail.Component.Service;

public class GetApplicationParamDetailService : ServiceBase
{
    public override string URL => "/api/ApplicationParam";

    public override HttpMethod HttpMethod => HttpMethod.Get;

    private readonly IApplicationParamDetailRepository _applicationParamDetailRepository;

    public GetApplicationParamDetailService(IApplicationParamDetailRepository applicationParamDetailRepository)
    {
        _applicationParamDetailRepository = applicationParamDetailRepository;
    }

    protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
    {
        PaginationList<ApplicationParamDetailModel> paginationListAsync = await _applicationParamDetailRepository.GetAllWithPagination(true).ToPaginationListAsync();

        return new ResponseModel<T>
        {
            Data = (T)(object)paginationListAsync
        };
    }
}