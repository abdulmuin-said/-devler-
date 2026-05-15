namespace KeyloggerTespitSistemi.Services;

public interface IAuditLogService
{
    Task LogAsync(int? userId, string action, string entityName, int? entityId, string description);
}
