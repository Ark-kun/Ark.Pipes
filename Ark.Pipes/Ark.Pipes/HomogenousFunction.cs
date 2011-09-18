using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Ark.Pipes {
    public class HomogenousFunction<T> : Provider<T> {
        int _arity;
        Func<T[], T> _function;
        ProviderArray<T> _args;

        public HomogenousFunction(Func<T[], T> function, int arity) {
            _function = function;
            _arity = arity;
            _args = new ProviderArray<T>(arity);
            _args.ElementChanged += OnElementChanged;
        }

        public HomogenousFunction(Func<T[], T> function, Provider<T>[] args) {
            _function = function;
            _arity = args.Length;

            _args = new ProviderArray<T>(args);
            _args.ElementChanged += OnElementChanged;
        }

        public override T GetValue() {
            return _function(_args.Values);
        }

        public ProviderArray<T> Arguments {
            get {
                return _args;
            }
        }

        public int Arity {
            get { return _arity; }
        }

        void OnElementChanged(int idx) {
            OnValueChanged();
        }
    }
}
