using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;

namespace Ark.Wpf { //.Pipes.Wpf {
    //public class TwoWayDependencyPropertyAdapter<T> : DependencyObject, INotifyPropertyChanged, IIn<T>, IOut<T> {
    //    static string _propertyName = "Value";
    //    public event Action Changed;
    //    public event PropertyChangedEventHandler PropertyChanged;

    //    static TwoWayDependencyPropertyAdapter() {
    //        ValueProperty = DependencyProperty.Register(_propertyName, typeof(T), typeof(DependencyPropertyAdapter<T>));
    //    }

    //    public static readonly DependencyProperty ValueProperty;

    //    public T GetValue() {
    //        return (T)GetValue(ValueProperty);
    //    }

    //    public void SetValue(T value) {
    //        base.SetValue(ValueProperty, value);
    //        OnChanged();
    //        ValueSource = ValueSourceType.Pipes;
    //    }

    //    public T Value {
    //        get {
    //            return GetValue();
    //        }
    //        set {
    //            SetValue(value);
    //        }
    //    }

    //    protected void OnChanged() {
    //        if (Changed != null) {
    //            Changed();
    //        }
    //    }

    //    protected void OnPropertyChanged() {
    //        if (PropertyChanged != null) {
    //            PropertyChanged(this, new PropertyChangedEventArgs(_propertyName));
    //        }
    //    }

    //    public ValueSourceType ValueSource { get; private set; }

    //    public enum ValueSourceType {
    //        Value,
    //        Pipes,
    //        DependencyProperty
    //    }
    //}
}
