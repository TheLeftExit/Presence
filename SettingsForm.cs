using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheLeftExit.Presence {
    public class SettingsForm : Form {
        public SettingsForm() {
            Size = new System.Drawing.Size(300, 300);

            PropertyGrid grid = new PropertyGrid();
            grid.SelectedObject = new Settings(PresenceSettings.Default);
            grid.Dock = DockStyle.Fill;
            grid.Parent = this;
        }

        private class Settings {
            private PresenceSettings _settings;
            public Settings(PresenceSettings settings) {
                _settings = settings;
            }

            [Browsable(true)]
            [Category("General")]
            [DisplayName("Enabled")]
            [Description("Enable or disable Discord Rich Presence.")]
            public bool Enabled {
                get => _settings.Enabled;
                set => _settings.Enabled = value;
            }

            [Browsable(true)]
            [Category("Options")]
            [DisplayName("Secret Mode")]
            [Description("Hide all activity, instead displaying a static message.")]
            public bool SecretMode {
                get => _settings.SecretMode;
                set => _settings.SecretMode = value;
            }

            [Browsable(true)]
            [Category("Options")]
            [DisplayName("Show solution")]
            [Description("Display current solution name in status.")]
            public bool ShowSolution {
                get => _settings.ShowSolution;
                set => _settings.ShowSolution = value;
            }

            [Browsable(true)]
            [Category("Options")]
            [DisplayName("Show filename")]
            [Description("Display current file in status.")]
            public bool ShowFile {
                get => _settings.ShowFile;
                set => _settings.ShowFile = value;
            }

            [Browsable(true)]
            [Category("Options")]
            [DisplayName("Show time")]
            [Description("Display time the current document has been open for in status.")]
            public bool ShowTimestamp {
                get => _settings.ShowTimestamp;
                set => _settings.ShowTimestamp = value;
            }

            [Browsable(true)]
            [Category("Customization")]
            [DisplayName("Secret Mode message")]
            [Description("Static message displayed while Secret Mode is active.")]
            public string SecretModeMessage {
                get => _settings.SecretModeMessage;
                set => _settings.SecretModeMessage = value;
            }

            [Browsable(true)]
            [Category("Customization")]
            [DisplayName("Solution format")]
            [Description("Custom formatting rule for solution status.")]
            public string SolutionFormat {
                get => _settings.SolutionFormat;
                set => _settings.SolutionFormat = value;
            }

            [Browsable(true)]
            [Category("Customization")]
            [DisplayName("File format")]
            [Description("Custom formatting rule for file status")]
            public string FileFormat {
                get => _settings.FileFormat;
                set => _settings.FileFormat = value;
            }

        }
    }

    public enum TimestampMode {
        None = 0,
        OnLaunch = 1,
        OnFileChanged = 2
    }
}
