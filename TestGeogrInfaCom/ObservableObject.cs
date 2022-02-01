using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGeogrInfaCom
{
    public class ObservableObject : INotifyPropertyChanged, INotifyCollectionChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            var e = new PropertyChangedEventArgs(propertyName);
            handler?.Invoke(this, e);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        protected virtual void OnCollectionChanged()
        {
            NotifyCollectionChangedEventHandler handler = this.CollectionChanged;
            var e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            handler?.Invoke(this, e);
        }
    }
}
