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

        public void Update(string newStatus, string newDetails) {
            if (!SafeInitialize())
                return;
            client.UpdateState(newStatus);
            client.UpdateDetails(newDetails);
            client.UpdateStartTime();
        }

        public void Dispose() {
            client.Dispose();
        }
    }
}
