using JetBrains.Application.Settings;
using Xavalon.XamlStyler.Core.Options;

namespace ReSharperPlugin.XamlStyler.dotUltimate
{
    public static class StylerOptionsFactory
    {
        public static IStylerOptions FromSettings(IContextBoundSettingsStoreLive settings)
        {
            var stylerOptions = new StylerOptions();
            
            stylerOptions.IndentSize = settings.GetValue((XamlStylerSettings s) => s.IndentSize);
            //stylerOptions.UseVisualStudioIndentSize = settings.GetValue((XamlStylerSettings s) => s.UseIdeIndentSize);
            stylerOptions.IndentWithTabs = settings.GetValue((XamlStylerSettings s) => s.IndentWithTabs);
            //stylerOptions.UseVisualStudioIndentWithTabs = settings.GetValue((XamlStylerSettings s) => s.UseIdeIndentWithTabs);
            stylerOptions.AttributesTolerance = settings.GetValue((XamlStylerSettings s) => s.AttributesTolerance);
            stylerOptions.KeepFirstAttributeOnSameLine = settings.GetValue((XamlStylerSettings s) => s.KeepFirstAttributeOnSameLine);
            stylerOptions.MaxAttributeCharactersPerLine = settings.GetValue((XamlStylerSettings s) => s.MaxAttributeCharactersPerLine);
            stylerOptions.MaxAttributesPerLine = settings.GetValue((XamlStylerSettings s) => s.MaxAttributesPerLine);
            stylerOptions.NoNewLineElements = settings.GetValue((XamlStylerSettings s) => s.NoNewLineElements);
            stylerOptions.PutAttributeOrderRuleGroupsOnSeparateLines = settings.GetValue((XamlStylerSettings s) => s.PutAttributeOrderRuleGroupsOnSeparateLines);
            stylerOptions.AttributeIndentation = settings.GetValue((XamlStylerSettings s) => s.AttributeIndentation);
            stylerOptions.AttributeIndentationStyle = settings.GetValue((XamlStylerSettings s) => s.AttributeIndentationStyle);
            stylerOptions.RemoveDesignTimeReferences = settings.GetValue((XamlStylerSettings s) => s.RemoveDesignTimeReferences);
            stylerOptions.EnableAttributeReordering = settings.GetValue((XamlStylerSettings s) => s.EnableAttributeReordering);
            stylerOptions.AttributeOrderingRuleGroups = settings.GetValue((XamlStylerSettings s) => s.AttributeOrderingRuleGroups);
            stylerOptions.FirstLineAttributes = settings.GetValue((XamlStylerSettings s) => s.FirstLineAttributes);
            stylerOptions.OrderAttributesByName = settings.GetValue((XamlStylerSettings s) => s.OrderAttributesByName);
            stylerOptions.PutEndingBracketOnNewLine = settings.GetValue((XamlStylerSettings s) => s.PutEndingBracketOnNewLine);
            stylerOptions.RemoveEndingTagOfEmptyElement = settings.GetValue((XamlStylerSettings s) => s.RemoveEndingTagOfEmptyElement);
            stylerOptions.SpaceBeforeClosingSlash = settings.GetValue((XamlStylerSettings s) => s.SpaceBeforeClosingSlash);
            stylerOptions.RootElementLineBreakRule = settings.GetValue((XamlStylerSettings s) => s.RootElementLineBreakRule);
            stylerOptions.ReorderVSM = settings.GetValue((XamlStylerSettings s) => s.ReorderVSM);
            stylerOptions.ReorderGridChildren = settings.GetValue((XamlStylerSettings s) => s.ReorderGridChildren);
            stylerOptions.ReorderCanvasChildren = settings.GetValue((XamlStylerSettings s) => s.ReorderCanvasChildren);
            stylerOptions.ReorderSetters = settings.GetValue((XamlStylerSettings s) => s.ReorderSetters);
            stylerOptions.FormatMarkupExtension = settings.GetValue((XamlStylerSettings s) => s.FormatMarkupExtension);
            stylerOptions.NoNewLineMarkupExtensions = settings.GetValue((XamlStylerSettings s) => s.NoNewLineMarkupExtensions);
            stylerOptions.ThicknessStyle = settings.GetValue((XamlStylerSettings s) => s.ThicknessStyle);
            stylerOptions.ThicknessAttributes = settings.GetValue((XamlStylerSettings s) => s.ThicknessAttributes);
            stylerOptions.FormatOnSave = settings.GetValue((XamlStylerSettings s) => s.FormatOnSave);
            stylerOptions.SaveAndCloseOnFormat = settings.GetValue((XamlStylerSettings s) => s.SaveAndCloseOnFormat);
            stylerOptions.CommentSpaces = settings.GetValue((XamlStylerSettings s) => s.CommentSpaces);
            stylerOptions.ConfigPath = settings.GetValue((XamlStylerSettings s) => s.ConfigPath).FullPath;
            stylerOptions.SearchToDriveRoot = settings.GetValue((XamlStylerSettings s) => s.SearchToDriveRoot);
            stylerOptions.ResetToDefault = settings.GetValue((XamlStylerSettings s) => s.ResetToDefault);
            stylerOptions.SuppressProcessing = settings.GetValue((XamlStylerSettings s) => s.SuppressProcessing);

            return stylerOptions;
        }
    }
}