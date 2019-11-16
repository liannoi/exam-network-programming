using System.Collections.Concurrent;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Exam.BL.Helpers
{
    public class Bindable : INotifyPropertyChanged
    {
        private readonly ConcurrentDictionary<string, object> properties = new ConcurrentDictionary<string, object>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool CallPropertyChangeEvent { get; set; } = true;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected T Get<T>(T defValue = default, [CallerMemberName] string name = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                return defValue;
            }

            if (properties.TryGetValue(name, out object value))
            {
                return (T)value;
            }

            properties.AddOrUpdate(name, defValue, (s, o) => defValue);
            return defValue;
        }

        protected bool Set(object value, [CallerMemberName] string name = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            bool isExists = properties.TryGetValue(name, out object getValue);
            if (isExists && Equals(value, getValue))
            {
                return false;
            }

            properties.AddOrUpdate(name, value, (s, o) => value);

            if (CallPropertyChangeEvent)
            {
                OnPropertyChanged(name);
            }

            return true;
        }
    }
}
