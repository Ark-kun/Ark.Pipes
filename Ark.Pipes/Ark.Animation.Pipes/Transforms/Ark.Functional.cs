using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ark {
    public class Functional {

        public static IEnumerable<T> For<T>(T seed, Func<T, bool> condition, Func<T, T> transform) {
            while (condition(seed)) {
                yield return seed;
                seed = transform(seed);
            }
        }
    }
}
