using Microsoft.Extensions.Localization;

namespace DDDProject.Infrastructure.Localization;

public class SharedLocalizer
{
    private readonly IStringLocalizer<SharedResource> _localizer;
        
    public SharedLocalizer(IStringLocalizer<SharedResource> localizer)
    {
        _localizer = localizer;
    }
        
    public LocalizedString this[string key] => _localizer[key];
        
    public LocalizedString this[string key, params object[] arguments] => 
        _localizer[key, arguments];
}