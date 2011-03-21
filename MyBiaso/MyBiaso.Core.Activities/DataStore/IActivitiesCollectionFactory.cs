using System.Collections.ObjectModel;
using MyBiaso.Core.Model;

namespace MyBiaso.Core.Activities.DataStore {

    public interface IActivitiesCollectionFactory {

        Collection<HomeVisit> CreateBindableActivitiesCollection();

    }
}
