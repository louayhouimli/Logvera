using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logvera.API.Contracts.Apis;

namespace Logvera.API.Application.Apis
{
    public interface IApiService
    {
        Task<ApiResponse> CreateApiAsync(Guid userId, CreateApiRequest request);
        Task<List<ApiResponse>> GetUserApisAsync(Guid userId);
    }
}