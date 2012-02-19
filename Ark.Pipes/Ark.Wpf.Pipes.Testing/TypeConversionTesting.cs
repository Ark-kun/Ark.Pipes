using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Ark.Pipes.Wpf.Testing {
    [TypeConverter(typeof(TestClassConverter))]
    public class TestClass {
        public int IntProperty { get; set; }
        public string StringProperty { get; set; }
        public TestClass TestClassProperty { get; set; }
    }

    public class TestClassConverter : TypeConverter {

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
            return destinationType == typeof(string);
        }


        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value) {
            string str = value as string;
            if (str == null) {
                throw base.GetConvertFromException(value);
            }
            return ConvertFromString(str);
        }

        public new object ConvertFromString(string text) {
            if (text == null)
                return null;

            var parts = text.Split(',');
            int count = parts.Length;
            if (count > 2)
                throw base.GetConvertFromException(text);

            var res = new TestClass();
            if (count > 0) {
                res.IntProperty = int.Parse(parts[0]);
                if (count > 1) {
                    res.StringProperty = parts[1];
                }
            }
            return res;
        }


        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType) {
            if (destinationType == typeof(string)) {
                var obj = value as TestClass;
                if (obj != null) {
                    return obj.IntProperty.ToString() + "," + obj.StringProperty + (obj.TestClassProperty == null ? "" : "," + "(" + ConvertToString(obj.TestClassProperty) + ")");
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

    }

}
