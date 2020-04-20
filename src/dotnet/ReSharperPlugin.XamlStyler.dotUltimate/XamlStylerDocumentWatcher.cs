using System.Linq;
using JetBrains.Annotations;
using JetBrains.Application.Settings;
using JetBrains.DocumentModel.Impl;
using JetBrains.DocumentModel.Storage;
using JetBrains.Lifetimes;
using JetBrains.ProjectModel;
using JetBrains.ProjectModel.DataContext;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Files;
using JetBrains.ReSharper.Psi.Xaml;
using Xavalon.XamlStyler.Core;

namespace ReSharperPlugin.XamlStyler.dotUltimate
{
    [SolutionComponent]
    public class XamlStylerDocumentWatcher
    {
        [NotNull] private readonly Lifetime _lifetime;
        [NotNull] private readonly ISolution _solution;
        [NotNull] private readonly IDocumentStorageHelpers _storageHelper;

        public XamlStylerDocumentWatcher(
            [NotNull] Lifetime lifetime,
            [NotNull] ISolution solution,
            [NotNull] IDocumentStorageHelpers storageHelper)
        {
            _lifetime = lifetime;
            _solution = solution;
            _storageHelper = storageHelper;
            _storageHelper.BeforeDocumentSaved.Advise(_lifetime, BeforeDocumentSavedHandler);
        }

        private void BeforeDocumentSavedHandler(BeforeDocumentSavedEventArgs args)
        {
            if (args?.Document == null) return;
            
            // Determine data, if not a XAML file then we do not care about the change
            var psiSourceFile = args.Document.GetPsiSourceFile(_solution);
            if (psiSourceFile == null || !psiSourceFile.GetLanguages().Any(it => PsiLanguageTypeExtensions.IsLanguage(it, XamlLanguage.Instance))) return;
            
            // Fetch settings
            var settings = _solution.GetSettingsStore().SettingsStore.BindToContextLive(_lifetime, ContextRange.Smart(_solution.ToDataContext()));
            var stylerOptions = StylerOptionsFactory.FromSettings(
                settings,
                _solution, 
                psiSourceFile?.GetProject(),
                psiSourceFile as IPsiSourceFileWithLocation);

            // Bail out early if needed
            if (stylerOptions.SuppressProcessing || !stylerOptions.FormatOnSave) return;
            
            // Perform styling
            var styler = new StylerService(stylerOptions);
            args.Document.SetText(
                styler.StyleDocument(args.Document.GetText()).Replace("\r\n", "\n"));
        }
    }
}