using System;
using JetBrains.Annotations;
using JetBrains.Application.Progress;
using JetBrains.Application.Settings;
using JetBrains.DocumentModel.Impl;
using JetBrains.ProjectModel;
using JetBrains.ProjectModel.DataContext;
using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Feature.Services.Xaml.Bulbs;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Files;
using JetBrains.ReSharper.Psi.Xaml;
using JetBrains.TextControl;
using JetBrains.Util;
using Xavalon.XamlStyler.Core;

namespace ReSharperPlugin.XamlStyler.dotUltimate
{
    [ContextAction(
        Name = "XamlStyler.Reformat",
        Description = "Formats the document(s) using XAML Styler.",
        Group = CommonContextActions.GroupID,
        Disabled = false,
        Priority = 100)]
    public class XamlStylerReformatContextAction : ContextActionBase
    {
        [NotNull] private readonly XamlContextActionDataProvider _dataProvider;

        public XamlStylerReformatContextAction([NotNull] XamlContextActionDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
 
        public override string Text => "Format with XAML Styler";
        
        public override bool IsAvailable(IUserDataHolder cache) => _dataProvider.Document != null;

        protected override Action<ITextControl> ExecutePsiTransaction(
            [NotNull] ISolution solution, 
            [NotNull] IProgressIndicator progress)
        {
            // Fetch settings
            var lifetime = solution.GetLifetime();
            var settings = solution.GetSettingsStore().SettingsStore.BindToContextLive(lifetime, ContextRange.Smart(solution.ToDataContext()));
            var stylerOptions = StylerOptionsFactory.FromSettings(
                settings,
                solution, 
                _dataProvider.Project,
                _dataProvider.SourceFile as IPsiSourceFileWithLocation);

            // Bail out early if needed
            if (stylerOptions.SuppressProcessing) return null;
            
            // Perform styling
            var styler = new StylerService(stylerOptions);

            foreach (var prjItem in _dataProvider.Document.GetPsiSourceFiles(solution))
            {
                foreach (var file in prjItem.GetPsiFiles<XamlLanguage>())
                {
                    var sourceFile = file.GetSourceFile();
                    if (sourceFile?.Document != null)
                    {
                        var oldText = sourceFile.Document.GetText();
                        var newText = styler.StyleDocument(oldText).Replace("\r\n", "\n");
                        file.ReParse(new TreeTextRange(new TreeOffset(0), new TreeOffset(oldText.Length)), newText);
                    }
                }
            }

            return null;
        }
    }
}