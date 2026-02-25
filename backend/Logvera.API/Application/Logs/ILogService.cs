using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logvera.API.Contracts.Logs;

namespace Logvera.API.Application.Logs
{
    public interface ILogService
    {
        Task IngestAsync(Guid apiId, LogIngestRequest request);
        Task<(List<LogResponse> Logs, int TotalCount)> QueryAsync(
        Guid userId,
        LogQueryRequest query
    );
    }
}