using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DiscordRPC;

namespace TheLeftExit.Presence {
    public class PresenceClient : IDisposable {
        private const string ApplicationId = "904038099275898941";
        private DiscordRpcClient client;

        public PresenceClient() {
            client = new DiscordRpcClient(ApplicationId);
            client.SetPresence(new RichPresence {
                Assets = new Assets {
                    LargeImageKey = "visual-studio",
                    LargeImageText = "Visual Studio"
                }
            });
        }

        public bool SafeInitialize() => client.IsInitialized || client.Initialize();
        public void SafeDeinitialize() {
            if (client.IsInitialized)
                client.Deinitialize();
        }

        public void Update(string solutionName, string fileName) {
            if (!SafeInitialize())
                return;

            PresenceSettings settings = PresenceSettings.Default;
            if (settings.Enabled) {
                if (!SafeInitialize())
                    return;
                RichPresence presence = new RichPresence() {
                    Assets = new Assets {
                        LargeImageKey = "visual-studio",
                        LargeImageText = "Visual Studio"
                    }
                };
                if (PresenceSettings.Default.SecretMode) {
                    presence.Details = settings.SecretModeMessage;
                } else {
                    if (settings.ShowSolution)
                        presence.Details = string.Format(settings.SolutionFormat, solutionName);
                    if (settings.ShowFile)
                        presence.Details = string.Format(settings.FileFormat, fileName);
                    if (settings.ShowTimestamp)
                        presence.Timestamps.Start = DateTime.Now;
                }
                client.SetPresence(presence);
            } else
                SafeDeinitialize();
        }

        public void Dispose() {
            client.Dispose();
        }
    }
}
