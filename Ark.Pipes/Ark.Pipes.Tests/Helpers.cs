using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ark.Pipes.Tests {
    static class StaticClass {
        public static int StaticField = 0;
        public static void StaticMethod() {
            StaticField++;
        }
    }

    class NormalClass {
        public int ClassField = 0;
        public void ClassMethod() {
            ClassField++;
        }
    }

    struct NormalStruct {
        public int StructField;
        public void StructMethod() {
            StructField++;
        }
    }
}
