using JetBrains.Application.Settings;
using JetBrains.Application.Settings.WellKnownRootKeys;

namespace ReSharperPlugin.XamlStyler.dotUltimate
{
    [SettingsKey(
        typeof(EnvironmentSettings),
//        typeof(CodeEditingSettings),
        "Settings for XamlStyler.dotUltimate")]
    public class SampleSettings
    {
        [SettingsEntry(DefaultValue: "<default>", Description: "Sample Description")]
        public string SampleText;
    }
}