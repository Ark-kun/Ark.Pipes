using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ark.Pipes.Testing {
    public static class HelperProperties {
        static AttachedProperty<string> _name = new AttachedProperty<string>();

        public static AttachedProperty<string> Name {
            get { return _name; }
        }
    }
}
