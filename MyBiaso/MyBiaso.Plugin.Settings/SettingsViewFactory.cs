using System;
using MyBiaso.Core.Setting.Views;
using MyBiaso.Plugin.Settings.Window;

namespace MyBiaso.Plugin.Settings {
    public class SettingsViewFactory : ISettingsViewFactory {
        public ISettingsView CreateSettingsView() {
            return new SettingsFrame();
        }
    }
}