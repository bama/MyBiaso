using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiFrame.InterfaceLib.MVP;
using MyBiaso.Core.Setting.ViewModel;

namespace MyBiaso.Core.Setting.Views {
    
    public interface ISettingsView:IWindow {
        void BindToViewModel(SettingsViewModel viewModel);
        void OpenEditMode();
        void DisplayError(string format);
        void CloseEditMode();
    }
}
