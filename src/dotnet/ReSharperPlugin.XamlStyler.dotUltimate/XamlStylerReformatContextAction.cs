using System;
using JetBrains.Annotations;
using JetBrains.Application.Progress;
using JetBrains.DocumentModel.Impl;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Feature.Services.Xaml.Bulbs;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Files;
using JetBrains.ReSharper.Psi.Xaml;
using JetBrains.TextControl;
using JetBrains.Util;
using Xavalon.XamlStyler.Core;
using Xavalon.XamlStyler.Core.Options;

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

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            var styler = new StylerService(new StylerOptions());
            
            foreach (var prjItem in _dataProvider.Document.GetPsiSourceFiles(solution))
            {
                foreach (var file in prjItem.GetPsiFiles<XamlLanguage>())
                {
                    var sourceFile = file.GetSourceFile();
                    if (sourceFile?.Document != null)
                    {
                        sourceFile.Document.SetText(
                            styler.StyleDocument(sourceFile.Document.GetText()).Replace("\r\n", "\n"));
                    }
                }
            }
            
            return null;
        }
    }
}