using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logvera.API.Contracts.Logs;
using Logvera.API.Domain;
using Logvera.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Logvera.API.Application.Logs
{
    public class LogService : ILogService
    {

        private readonly LogveraDbContext _db;
        public LogService(LogveraDbContext db)
        {
            _db = db;
        }
        public async Task IngestAsync(Guid apiId, LogIngestRequest request)
        {
            var log = new LogEntry
            {
                Id = Guid.NewGuid(),
                ApiId = apiId,
                Endpoint = request.Endpoint,
                Method = request.Method,
                StatusCode = request.StatusCode,
                DurationMs = request.DurationMs,
                Timestamp = DateTime.UtcNow
            };

            _db.LogEntries.Add(log);
            await _db.SaveChangesAsync();
        }

        public async Task<(List<LogResponse>, int)> QueryAsync(
    Guid userId,
    LogQueryRequest query)
        {
            var baseQuery =
                from log in _db.LogEntries
                join api in _db.Apis on log.ApiId equals api.Id
                where api.UserId == userId
                select log;

            if (query.ApiId.HasValue)
                baseQuery = baseQuery.Where(l => l.ApiId == query.ApiId);

            if (!string.IsNullOrWhiteSpace(query.Method))
            {
                baseQuery = baseQuery.Where(l =>
                    l.Method.ToLower() == query.Method.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(query.Endpoint))
            {
                baseQuery = baseQuery.Where(l =>
                    l.Endpoint.Contains(query.Endpoint));
            }

            if (query.StatusCode.HasValue)
                baseQuery = baseQuery.Where(l => l.StatusCode == query.StatusCode);

            if (query.From.HasValue)
                baseQuery = baseQuery.Where(l => l.Timestamp >= query.From);

            if (query.To.HasValue)
                baseQuery = baseQuery.Where(l => l.Timestamp <= query.To);

            var totalCount = await baseQuery.CountAsync();

            var logs = await baseQuery
                .OrderByDescending(l => l.Timestamp)
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(l => new LogResponse
                {
                    Id = l.Id,
                    ApiId = l.ApiId,
                    Endpoint = l.Endpoint,
                    Method = l.Method,
                    StatusCode = l.StatusCode,
                    DurationMs = l.DurationMs,
                    Timestamp = l.Timestamp
                })
                .ToListAsync();

            return (logs, totalCount);
        }


    }
}