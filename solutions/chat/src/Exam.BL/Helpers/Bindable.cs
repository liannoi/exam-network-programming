﻿// Copyright 2020 Maksym Liannoi
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
