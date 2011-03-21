using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiFrame.Base.Core;
using AiFrame.Base.Core.Data.Access;
using MyBiaso.Core.DataStore;

namespace MyBiaso.Core {
    public class CoreInitialization {
        
        public static void Initialize() {
        

            ConfigurationProxy.ApplicationName = "MyBiaso";
            ConfigurationProxy.CompanyName = "Sebastian Martens";
            /** You can configure a smtp server for sending error mails, too:
            ConfigurationProxy.ProblemDialogSmtpServer = "smtp.myserver.com";
             */

            ConfigurationProxy.SessionManager = NHibernateSessionManager.Instance;
            DatabaseConnection.Instance.LoadFromConfigFile = true;
            DatabaseConnection.Instance.GetSession();
        }
    }
}
