using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Logvera.API.Contracts.Apis;
using Logvera.API.Domain;
using Logvera.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Logvera.API.Application.Apis
{
    public class ApiService : IApiService
    {
        private readonly LogveraDbContext _db;
        public ApiService(LogveraDbContext db)
        {
            _db = db;
        }
        public async Task<ApiResponse> CreateApiAsync(Guid userId, CreateApiRequest request)
        {
            var api = new Api
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Name = request.Name,
                BaseUrl = request.BaseUrl,
                ApiKey = GenerateApiKey(),
                CreatedAt = DateTime.UtcNow
            };

            _db.Apis.Add(api);
            await _db.SaveChangesAsync();

            return new ApiResponse
            {
                Id = api.Id,
                Name = api.Name,
                BaseUrl = api.BaseUrl,
                ApiKey = api.ApiKey
            };
        }

        public async Task<List<ApiResponse>> GetUserApisAsync(Guid userId)
        {
            return await _db.Apis
                .Where(a => a.UserId == userId)
                .Select(a => new ApiResponse
                {
                    Id = a.Id,
                    Name = a.Name,
                    BaseUrl = a.BaseUrl,
                    ApiKey = a.ApiKey
                })
                .ToListAsync();
        }

        private static string GenerateApiKey()
        {
            return Convert.ToHexString(
                RandomNumberGenerator.GetBytes(32)
            );
        }

    }
}