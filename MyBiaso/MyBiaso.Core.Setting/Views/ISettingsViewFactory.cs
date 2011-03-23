using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiFrame.InterfaceLib.MVP;

namespace MyBiaso.Core.Setting.Views {
    public interface ISettingsViewFactory {

        ISettingsView CreateSettingsView();
    }
}
