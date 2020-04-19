using System;
using System.Linq.Expressions;
using JetBrains.Application.Settings;
using JetBrains.Application.UI.Options;
using JetBrains.Application.UI.Options.Options.ThemedIcons;
using JetBrains.Application.UI.Options.OptionsDialog;
using JetBrains.DataFlow;
using JetBrains.IDE.UI.Extensions;
using JetBrains.IDE.UI.Options;
using JetBrains.Lifetimes;
using JetBrains.ReSharper.Feature.Services.Daemon.OptionPages;
using JetBrains.Rider.Model.UIAutomation;

namespace ReSharperPlugin.XamlStyler.dotUltimate.Options
{
    [OptionsPage(Id, PageTitle, typeof(OptionsThemedIcons.EnvironmentGeneral), ParentId = "Tools"
    )]
    public class XamlStylerOptionsPage : BeSimpleOptionsPage
    {
        private const string Id = nameof(XamlStylerOptionsPage);
        private const string PageTitle = "XAML Styler";

        private readonly Lifetime _lifetime;

        public XamlStylerOptionsPage(
            Lifetime lifetime,
            OptionsPageContext optionsPageContext,
            OptionsSettingsSmartContext optionsSettingsSmartContext)
            : base(lifetime, optionsPageContext, optionsSettingsSmartContext)
        {
            _lifetime = lifetime;

            AddHeader("Sample header");
            AddTextBox((XamlStylerSettings x) => x.SampleText, "Description");
        }

        private BeTextBox AddTextBox<TKeyClass>(Expression<Func<TKeyClass, string>> lambdaExpression,
            string description)
        {
            var property = new Property<string>(description);
            OptionsSettingsSmartContext.SetBinding(_lifetime, lambdaExpression, property);
            var control = property.GetBeTextBox(_lifetime);
            AddControl(control.WithDescription(description, _lifetime));
            return control;
        }
    }
}