using System;
using System.ComponentModel;
using System.Windows;

namespace Ark.Pipes.Wpf {
    //Provider that reads from  DP
    public class ProviderAdapter<T> : Provider<T> {
        DependencyObject _obj;
        DependencyProperty _dp;

        public ProviderAdapter(DependencyObject obj, DependencyProperty dp) {
            if (dp.PropertyType != typeof(T)) {
                throw new ArgumentException(string.Format("Dependency property type {0} doesn't match provider type {1}", dp.PropertyType, typeof(T)), "dp");
            }

            _obj = obj;
            _dp = dp;

            var npc = _obj as INotifyPropertyChanged;
            if (npc != null) {
                npc.PropertyChanged += (s, e) => { if (e.PropertyName == dp.Name) OnValueChanged(); };
            }
        }

        public override T GetValue() {
            return (T)_obj.GetValue(_dp);
        }
    }
}
