using System;
using System.Collections.Generic;
using AiFrame.InterfaceLib.MVP;
using MyBiaso.Core.Setting.Controller;
using MyBiaso.Core.Setting.DataStore;
using MyBiaso.Core.Setting.Views;

namespace MyBiaso.Core.Setting.ViewModel {

    /// <summary>
    /// ViewModel für die Einstellungen.
    /// </summary>
    public class SettingsViewModel:ViewModelBase, IDataSourceProvider<IDictionary<string, Model.Setting>> {
        
        private IDictionary<string, Model.Setting> settings = new Dictionary<string, Model.Setting>();
        private ISettingsView view;

        public SettingsViewModel(ISettingsView view) {
            this.view = view;
            LoadSettings();
            view.BindToViewModel(this);
        }
        
        public void SetDataSource(IDictionary<string, Model.Setting> dataSource) {
            settings = dataSource;
        }

        public IDictionary<string, Model.Setting> GetDataSource() {
            return settings;
        }

        private void LoadSettings() {
            settings.Clear();

            var readSettings = DaoFactory.Instance.SettingStore.FindAll();

            foreach (var setting in readSettings) {
                settings.Add(setting.Key, setting);   
            }
        }

        public void UserWantsToEdit() {
            try {
                view.OpenEditMode();
            } catch (Exception e) {
                view.DisplayError(String.Format("Es ist der folgende Fehler aufgetreten: {0}", e.Message));
            }
        }

        public void UserWantsToSave() {
            try {
                // Editierung abbrechen
                view.CloseEditMode();
                // durchlaufen und alle speichern
                foreach (var setting in settings.Values) {
                    DaoFactory.Instance.SettingStore.SaveOrUpdate(setting);
                }
            } catch (Exception e) {
                view.DisplayError(String.Format("Es ist der folgende Fehler aufgetreten: {0}", e.Message));
            }
        }

        public void UserWantsToCancelEdit() {
            try {
                view.CloseEditMode();
                LoadSettings();
                view.RefreshView();
            } catch (Exception e) {
                view.DisplayError(String.Format("Es ist der folgende Fehler aufgetreten: {0}", e.Message));
            }
        }
    }
}
