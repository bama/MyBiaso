﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyBiaso.Core.Activities.Views;

namespace MyBiaso.Plugin.Activities {

    public class ActivitiesViewFactory:IActivitiesViewFactory {
        
        public IActivitiesListView CreateListView() {
            return new Window.ActivitiesListFrame();
        }

        public IHomeVisitDataView CreateHomeVisitDataView() {
            return new Window.ActivityDataWindow();
        }
    }
}
