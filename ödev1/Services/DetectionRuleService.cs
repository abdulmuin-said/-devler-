using KeyloggerTespitSistemi.Data;
using KeyloggerTespitSistemi.Models;
using KeyloggerTespitSistemi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace KeyloggerTespitSistemi.Services;

public class DetectionRuleService : IDetectionRuleService
{
    private readonly ApplicationDbContext _dbContext;

    public DetectionRuleService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<DetectionRule>> GetAllAsync()
    {
        return await _dbContext.DetectionRules.OrderBy(item => item.Id).ToListAsync();
    }

    public async Task<DetectionRule?> GetByIdAsync(int id)
    {
        return await _dbContext.DetectionRules.FindAsync(id);
    }

    public async Task SaveAsync(DetectionRuleFormViewModel model)
    {
        DetectionRule rule;

        if (model.Id.HasValue)
        {
            rule = await _dbContext.DetectionRules.FindAsync(model.Id.Value) ?? new DetectionRule();
        }
        else
        {
            rule = new DetectionRule();
            _dbContext.DetectionRules.Add(rule);
        }

        rule.Name = model.Name.Trim();
        rule.Description = model.Description.Trim();
        rule.RuleType = model.RuleType;
        rule.RiskPoint = model.RiskPoint;
        rule.IsActive = model.IsActive;

        await _dbContext.SaveChangesAsync();
    }

    public async Task ToggleAsync(int id)
    {
        var rule = await _dbContext.DetectionRules.FindAsync(id);
        if (rule is null)
        {
            return;
        }

        rule.IsActive = !rule.IsActive;
        await _dbContext.SaveChangesAsync();
    }
}
