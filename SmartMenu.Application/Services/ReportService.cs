using Microsoft.EntityFrameworkCore;
using SmartMenu.Infrastructure;
using SmartMenu.Infrastructure.Data;

namespace SmartMenu.Application.Services
{
    public interface IReportService
    {
        Task<object> GetSystemOverviewAsync();
        Task<object> GetDeviceStatusReportAsync();
        Task<object> GetScheduleReportAsync();
        Task<object> GetUserActionsReportAsync();
    }

    public class ReportService : IReportService
    {
        private readonly SmartMenuDbContext _context;
        public ReportService(SmartMenuDbContext context)
        {
            _context = context;
        }

        // Tổng quan hệ thống
        public async Task<object> GetSystemOverviewAsync()
        {
            var data = new
            {
                TotalUsers = await _context.Users.CountAsync(),
                TotalDevices = await _context.Devices.CountAsync(),
                ActiveDevices = await _context.Devices.CountAsync(d => d.Status == "online"),
                TotalPlaylists = await _context.Playlists.CountAsync(),
                TotalTemplates = await _context.MenuTemplates.CountAsync(),
                TotalProducts = await _context.Products.CountAsync(),
                TotalCategories = await _context.Categories.CountAsync(),
                TotalSchedules = await _context.Schedules.CountAsync(),
                RecentLogs = await _context.AuditLogs
                    .OrderByDescending(l => l.OccurredAt)
                    .Take(5)
                    .Select(l => new
                    {
                        l.AuditId,
                        l.Action,
                        l.EntityType,
                        l.Status,
                        l.OccurredAt
                    }).ToListAsync()
            };
            return data;
        }

        // Báo cáo thiết bị
        public async Task<object> GetDeviceStatusReportAsync()
        {
            var report = await _context.Devices
                .GroupBy(d => d.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            return report;
        }

        // Báo cáo lịch phát
        public async Task<object> GetScheduleReportAsync()
        {
            var report = await _context.Schedules
                .Include(s => s.Playlist)
                .GroupBy(s => s.Playlist.Name)
                .Select(g => new { Playlist = g.Key, Count = g.Count() })
                .ToListAsync();

            return report;
        }

        // Báo cáo hành động (AuditLogs)
        public async Task<object> GetUserActionsReportAsync()
        {
            var report = await _context.AuditLogs
                .GroupBy(a => a.Action)
                .Select(g => new
                {
                    Action = g.Key,
                    Count = g.Count(),
                    LastOccurred = g.Max(x => x.OccurredAt)
                })
                .OrderByDescending(x => x.Count)
                .Take(10)
                .ToListAsync();

            return report;
        }
    }
}
