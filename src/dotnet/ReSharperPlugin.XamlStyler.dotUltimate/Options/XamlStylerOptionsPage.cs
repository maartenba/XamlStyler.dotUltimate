using System;
using System.Linq.Expressions;
using JetBrains.Annotations;
using JetBrains.Application.Settings;
using JetBrains.Application.UI.Controls.FileSystem;
using JetBrains.Application.UI.Options;
using JetBrains.Application.UI.Options.OptionPages;
using JetBrains.Application.UI.Options.Options.ThemedIcons;
using JetBrains.Application.UI.Options.OptionsDialog;
using JetBrains.Application.UI.Options.OptionsDialog.SimpleOptions;
using JetBrains.DataFlow;
using JetBrains.IDE.UI.Extensions;
using JetBrains.IDE.UI.Options;
using JetBrains.Lifetimes;
using JetBrains.ReSharper.Feature.Services.Daemon.OptionPages;
using JetBrains.ReSharper.Feature.Services.InlayHints;
using JetBrains.ReSharper.Feature.Services.ParameterNameHints;
using JetBrains.Rider.Model.UIAutomation;
using JetBrains.Util;

namespace ReSharperPlugin.XamlStyler.dotUltimate.Options
{
    [OptionsPage(Id, PageTitle, typeof(OptionsThemedIcons.EnvironmentGeneral), ParentId = ToolsPage.PID)]
    public class XamlStylerOptionsPage : BeSimpleOptionsPage
    {
        private const string Id = nameof(XamlStylerOptionsPage);
        private const string PageTitle = "XAML Styler";

        private readonly Lifetime _lifetime;

        public XamlStylerOptionsPage(
            Lifetime lifetime,
            OptionsPageContext optionsPageContext,
            OptionsSettingsSmartContext optionsSettingsSmartContext,
            [NotNull] ICommonFileDialogs commonFileDialogs)
            : base(lifetime, optionsPageContext, optionsSettingsSmartContext)
        {
            _lifetime = lifetime;

            // Indentation
            AddHeader("Indentation");
            var indentSizeOption = AddSpinner((XamlStylerSettings x) => x.IndentSize, "Indent size");
            using (Indent())
            {
                AddBoolOption((XamlStylerSettings x) => x.UseIdeIndentSize, "Use IDE value");
            }
            
            AddBinding(indentSizeOption, BindingStyle.IsEnabledProperty,
                (XamlStylerSettings x) => x.UseIdeIndentSize,
                x => (bool)x);
            
            var indentWithTabsOption = AddBoolOption((XamlStylerSettings x) => x.IndentWithTabs, "Indent with tabs");
            using (Indent())
            {
                AddBoolOption((XamlStylerSettings x) => x.UseIdeIndentWithTabs, "Use IDE value");
            }
            
            AddBinding(indentWithTabsOption, BindingStyle.IsEnabledProperty,
                (XamlStylerSettings x) => x.UseIdeIndentWithTabs,
                x => (bool)x);
            
            // Attribute formatting
            AddHeader("Attribute formatting");
            AddSpinner((XamlStylerSettings x) => x.AttributesTolerance, "Attribute tolerance");
            AddBoolOption((XamlStylerSettings x) => x.KeepFirstAttributeOnSameLine, "Keep first attribute on same line");
            AddSpinner((XamlStylerSettings x) => x.MaxAttributeCharactersPerLine, "Max attribute characters per line");
            AddSpinner((XamlStylerSettings x) => x.MaxAttributesPerLine, "Max attributes per line");
            AddTextBox((XamlStylerSettings x) => x.NoNewLineElements, "Newline exemption elements");
            AddBoolOption((XamlStylerSettings x) => x.PutAttributeOrderRuleGroupsOnSeparateLines, "Separate by groups");
            AddSpinner((XamlStylerSettings x) => x.AttributeIndentation, "Attribute indentation");
            AddComboEnum((XamlStylerSettings x) => x.AttributeIndentationStyle, "Attribute indentation style");
            AddBoolOption((XamlStylerSettings x) => x.RemoveDesignTimeReferences, "Remove design-time references");
            
            // Attribute Reordering
            AddHeader("Attribute reordering");
            AddBoolOption((XamlStylerSettings x) => x.EnableAttributeReordering, "Enable attribute reordering");

            // [SettingsEntry(
            //     DefaultValue: new[]
            //     {
            //         "x:Class",
            //         "xmlns, xmlns:x",
            //         "xmlns:*",
            //         "x:Key, Key, x:Name, Name, x:Uid, Uid, Title",
            //         "Grid.Row, Grid.RowSpan, Grid.Column, Grid.ColumnSpan, Canvas.Left, Canvas.Top, Canvas.Right, Canvas.Bottom",
            //         "Width, Height, MinWidth, MinHeight, MaxWidth, MaxHeight",
            //         "Margin, Padding, HorizontalAlignment, VerticalAlignment, HorizontalContentAlignment, VerticalContentAlignment, Panel.ZIndex",
            //         "*:*, *",
            //         "PageSource, PageIndex, Offset, Color, TargetName, Property, Value, StartPoint, EndPoint",
            //         "mc:Ignorable, d:IsDataSource, d:LayoutOverrides, d:IsStaticText",
            //         //Storyboards, fixes #30
            //         "Storyboard.*, From, To, Duration",
            //     }, 
            //     Description: "Defines attribute ordering rule groups. Each string element is one group. Use ',' as a delimiter between attributes. 'DOS' wildcards are allowed. XAML Styler will order attributes in groups from top to bottom, and within groups left to right.")]
            // public string[] AttributeOrderingRuleGroups { get; set; }

            var firstLineAttributesOption = AddTextBox((XamlStylerSettings x) => x.FirstLineAttributes, "First-line attributes");
            var orderAttributesByNameOption = AddBoolOption((XamlStylerSettings x) => x.OrderAttributesByName, "Order attributes by name");
            
            AddBinding(firstLineAttributesOption, BindingStyle.IsEnabledProperty,
                (XamlStylerSettings x) => x.EnableAttributeReordering,
                x => (bool)x);
            
            AddBinding(orderAttributesByNameOption, BindingStyle.IsEnabledProperty,
                (XamlStylerSettings x) => x.EnableAttributeReordering,
                x => (bool)x);
            
            // Element formatting
            AddHeader("Element formatting");
            AddBoolOption((XamlStylerSettings x) => x.PutEndingBracketOnNewLine, "Put ending brackets on new line");
            AddBoolOption((XamlStylerSettings x) => x.RemoveEndingTagOfEmptyElement, "Remove ending tag of empty element");
            AddBoolOption((XamlStylerSettings x) => x.SpaceBeforeClosingSlash, "Space before ending slash in self-closing elements");
            AddComboEnum((XamlStylerSettings x) => x.RootElementLineBreakRule, "Root element line breaks between attributes");
            
            // Element reordering
            AddHeader("Element reordering");
            AddComboEnum((XamlStylerSettings x) => x.ReorderVSM, "Reorder visual state manager");
            AddBoolOption((XamlStylerSettings x) => x.ReorderGridChildren, "Reorder grid panel children");
            AddBoolOption((XamlStylerSettings x) => x.ReorderCanvasChildren, "Reorder canvas panel children");
            AddComboEnum((XamlStylerSettings x) => x.ReorderSetters, "Reorder setters");

            // Markup extension
            AddHeader("Markup extension");
            AddBoolOption((XamlStylerSettings x) => x.FormatMarkupExtension, "Enable markup extension formatting");
            var noNewLineMarkupExtensionsOption = AddTextBox((XamlStylerSettings x) => x.NoNewLineMarkupExtensions, "Keep markup extensions of these types on one line");
            
            AddBinding(noNewLineMarkupExtensionsOption, BindingStyle.IsEnabledProperty,
                (XamlStylerSettings x) => x.FormatMarkupExtension,
                x => (bool)x);
            
            // Thickness formatting
            AddHeader("Thickness formatting");
            AddComboEnum((XamlStylerSettings x) => x.ThicknessStyle, "Thickness separator");
            AddTextBox((XamlStylerSettings x) => x.ThicknessAttributes, "Thickness attributes");

            // Misc
            AddHeader("Misc");
            AddBoolOption((XamlStylerSettings x) => x.FormatOnSave, "Format XAML on save");
            AddBoolOption((XamlStylerSettings x) => x.SaveAndCloseOnFormat, "Automatically save and close documents opened by XAML Styler");
            AddSpinner((XamlStylerSettings x) => x.CommentSpaces, "Comment padding");
            
            // Configuration
            AddHeader("Configuration");
            AddText("External configuration file:");
            var configPath = new Property<FileSystemPath>(lifetime, "XamlStylerOptionsPage::configPath");
            OptionsSettingsSmartContext.SetBinding(lifetime, (XamlStylerSettings k) => k.ConfigPath, configPath);
            AddFileChooserOption(configPath, "External configuration file", FileSystemPath.Empty, null, commonFileDialogs, null, false, "", null, null, null, null);
            AddBoolOption((XamlStylerSettings x) => x.SearchToDriveRoot, "Search to drive root");
            AddBoolOption((XamlStylerSettings x) => x.ResetToDefault, "Reset to default");
            AddBoolOption((XamlStylerSettings x) => x.SuppressProcessing, "Suppress processing");
        }

        private BeTextBox AddTextBox<TKeyClass>(Expression<Func<TKeyClass, string>> lambdaExpression,
            string description)
        {
            var property = new Property<string>(description);
            OptionsSettingsSmartContext.SetBinding(_lifetime, lambdaExpression, property);
            var control = property.GetBeTextBox(_lifetime);
            AddControl(control.WithDescription(description, _lifetime, size: BeSizingType.Fit));
            return control;
        }
        
        private BeSpinner AddSpinner<TKeyClass>(Expression<Func<TKeyClass, int>> lambdaExpression,
            string description, int min = int.MinValue, int max = int.MaxValue)
        {
            var property = new Property<int>(description);
            OptionsSettingsSmartContext.SetBinding(_lifetime, lambdaExpression, property);
            var control = property.GetBeSpinner(_lifetime, min, max);
            AddControl(control.WithDescription(description, _lifetime, size: BeSizingType.Fit));
            return control;
        }
    }
}