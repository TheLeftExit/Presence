using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace TheLeftExit.Presence {
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid("023aa146-836a-4cf3-9951-3f46c464aa10")]
    [ProvideAutoLoad(UIContextGuids80.SolutionExists, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideAutoLoad(UIContextGuids80.NoSolution, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    public sealed class PresencePackage : AsyncPackage {
        private PresenceClient client;
        private DTE ide;

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress) {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await base.InitializeAsync(cancellationToken, progress);
            await SettingsCommand.InitializeAsync(this);

            client = new PresenceClient();
            WindowActivated();

            ide = GetGlobalService(typeof(SDTE)) as DTE;

            ide.Events.WindowEvents.WindowActivated += WindowActivated;
            
        }

        private void WindowActivated(Window oldWindow = null, Window newWindow = null) {
            ThreadHelper.ThrowIfNotOnUIThread();
            string solutionName = ide.Solution.FileName;
            if (solutionName is null or "")
                client.Update(null, null);
            else
                client.Update(
                    $"Developing {Path.GetFileNameWithoutExtension(ide.Solution.FileName)}",
                    ide.ActiveDocument?.Name
                );
        }

        protected override void Dispose(bool disposing) {
            client.Dispose();
            base.Dispose(disposing);
        }
    }
}
