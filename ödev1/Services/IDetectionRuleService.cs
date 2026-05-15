using KeyloggerTespitSistemi.Models;
using KeyloggerTespitSistemi.ViewModels;

namespace KeyloggerTespitSistemi.Services;

public interface IDetectionRuleService
{
    Task<List<DetectionRule>> GetAllAsync();
    Task<DetectionRule?> GetByIdAsync(int id);
    Task SaveAsync(DetectionRuleFormViewModel model);
    Task ToggleAsync(int id);
}
