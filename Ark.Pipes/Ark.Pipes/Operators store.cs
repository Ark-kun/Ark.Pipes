using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Ark.Pipes.Operators {
    public class ProviderOperators {
        Unary _Unary = new Unary();
        Conditional _Conditional = new Conditional();
        Arithmetic _Arithmetic = new Arithmetic();
        Bitwise _Bitwise = new Bitwise();
        //logical
        //conversion!

        internal ProviderOperators() { }

        public Unary Unary { get { return _Unary; } }
        public Conditional Conditional { get { return _Conditional; } }
        public Arithmetic Arithmetic { get { return _Arithmetic; } }
        public Bitwise Bitwise { get { return _Bitwise; } }
    }


    public class Unary {
        Dictionary<Tuple<Type, Type>, object> _UnaryNegationStore;
        Dictionary<Tuple<Type, Type>, object> _UnaryPlusStore;
        Dictionary<Tuple<Type, Type>, object> _IncrementStore;
        Dictionary<Tuple<Type, Type>, object> _DecrementStore;
        Dictionary<Tuple<Type, Type>, object> _ComplementStore;
        UnaryOperatorEntry.SameTypes _UnaryNegationEntry;
        UnaryOperatorEntry.SameTypes _UnaryPlusEntry;
        UnaryOperatorEntry.SameTypes _IncrementEntry;
        UnaryOperatorEntry.SameTypes _DecrementEntry;
        UnaryOperatorEntry.SameTypes _ComplementEntry;

        internal Unary() {
            _UnaryNegationStore = new Dictionary<Tuple<Type, Type>, object>();
            _UnaryPlusStore = new Dictionary<Tuple<Type, Type>, object>();
            _IncrementStore = new Dictionary<Tuple<Type, Type>, object>();
            _DecrementStore = new Dictionary<Tuple<Type, Type>, object>();
            _ComplementStore = new Dictionary<Tuple<Type, Type>, object>();

            _UnaryNegationEntry = new UnaryOperatorEntry.SameTypes(_UnaryNegationStore);
            _UnaryPlusEntry = new UnaryOperatorEntry.SameTypes(_UnaryPlusStore);
            _IncrementEntry = new UnaryOperatorEntry.SameTypes(_IncrementStore);
            _DecrementEntry = new UnaryOperatorEntry.SameTypes(_DecrementStore);
            _ComplementEntry = new UnaryOperatorEntry.SameTypes(_ComplementStore);
        }

        public UnaryOperatorEntry.SameTypes UnaryNegation { get { return _UnaryNegationEntry; } }
        public UnaryOperatorEntry.SameTypes UnaryPlus { get { return _UnaryPlusEntry; } }
        public UnaryOperatorEntry.SameTypes Increment { get { return _IncrementEntry; } }
        public UnaryOperatorEntry.SameTypes Decrement { get { return _DecrementEntry; } }
        public UnaryOperatorEntry.SameTypes Complement { get { return _ComplementEntry; } }
    }


    public class Conditional {
        Dictionary<Tuple<Type, Type, Type>, object> _EqualityStore;
        Dictionary<Tuple<Type, Type, Type>, object> _InequalityStore;
        Dictionary<Tuple<Type, Type, Type>, object> _GreaterThanStore;
        Dictionary<Tuple<Type, Type, Type>, object> _GreaterThanOrEqualStore;
        Dictionary<Tuple<Type, Type, Type>, object> _LessThanStore;
        Dictionary<Tuple<Type, Type, Type>, object> _LessThanOrEqualStore;
        BinaryOperatorEntry.SameTypes_ConstrainedResult<bool> _EqualityEntry;
        BinaryOperatorEntry.SameTypes_ConstrainedResult<bool> _InequalityEntry;
        BinaryOperatorEntry.SameTypes_ConstrainedResult<bool> _GreaterThanEntry;
        BinaryOperatorEntry.SameTypes_ConstrainedResult<bool> _GreaterThanOrEqualEntry;
        BinaryOperatorEntry.SameTypes_ConstrainedResult<bool> _LessThanEntry;
        BinaryOperatorEntry.SameTypes_ConstrainedResult<bool> _LessThanOrEqualEntry;

        internal Conditional() {
            _EqualityStore = new Dictionary<Tuple<Type, Type, Type>, object>();
            _InequalityStore = new Dictionary<Tuple<Type, Type, Type>, object>();
            _GreaterThanStore = new Dictionary<Tuple<Type, Type, Type>, object>();
            _GreaterThanOrEqualStore = new Dictionary<Tuple<Type, Type, Type>, object>();
            _LessThanStore = new Dictionary<Tuple<Type, Type, Type>, object>();
            _LessThanOrEqualStore = new Dictionary<Tuple<Type, Type, Type>, object>();
            _EqualityEntry = new BinaryOperatorEntry.SameTypes_ConstrainedResult<bool>(_EqualityStore);
            _InequalityEntry = new BinaryOperatorEntry.SameTypes_ConstrainedResult<bool>(_InequalityStore);
            _GreaterThanEntry = new BinaryOperatorEntry.SameTypes_ConstrainedResult<bool>(_GreaterThanStore);
            _GreaterThanOrEqualEntry = new BinaryOperatorEntry.SameTypes_ConstrainedResult<bool>(_GreaterThanOrEqualStore);
            _LessThanEntry = new BinaryOperatorEntry.SameTypes_ConstrainedResult<bool>(_LessThanStore);
            _LessThanOrEqualEntry = new BinaryOperatorEntry.SameTypes_ConstrainedResult<bool>(_LessThanOrEqualStore);
        }

        public BinaryOperatorEntry.SameTypes_ConstrainedResult<bool> Equality { get { return _EqualityEntry; } }
        public BinaryOperatorEntry.SameTypes_ConstrainedResult<bool> Inequality { get { return _InequalityEntry; } }
        public BinaryOperatorEntry.SameTypes_ConstrainedResult<bool> GreaterThan { get { return _GreaterThanEntry; } }
        public BinaryOperatorEntry.SameTypes_ConstrainedResult<bool> GreaterThanOrEqual { get { return _GreaterThanOrEqualEntry; } }
        public BinaryOperatorEntry.SameTypes_ConstrainedResult<bool> LessThan { get { return _LessThanEntry; } }
        public BinaryOperatorEntry.SameTypes_ConstrainedResult<bool> LessThanOrEqual { get { return _LessThanOrEqualEntry; } }
    }

    public class Arithmetic {
        Dictionary<Tuple<Type, Type, Type>, object> _AdditionStore;
        Dictionary<Tuple<Type, Type, Type>, object> _SubtractionStore;
        Dictionary<Tuple<Type, Type, Type>, object> _MultiplicationtionStore;
        Dictionary<Tuple<Type, Type, Type>, object> _DivisionStore;
        Dictionary<Tuple<Type, Type, Type>, object> _ModulusStore;
        BinaryOperatorEntry.AnyTypes _AdditionEntry;
        BinaryOperatorEntry.AnyTypes _SubtractionEntry;
        BinaryOperatorEntry.AnyTypes _MultiplicationEntry;
        BinaryOperatorEntry.AnyTypes _DivisionEntry;
        BinaryOperatorEntry.AnyTypes _ModulusEntry;

        internal Arithmetic() {
            _AdditionStore = new Dictionary<Tuple<Type, Type, Type>, object>();
            _SubtractionStore = new Dictionary<Tuple<Type, Type, Type>, object>();
            _MultiplicationtionStore = new Dictionary<Tuple<Type, Type, Type>, object>();
            _DivisionStore = new Dictionary<Tuple<Type, Type, Type>, object>();
            _ModulusStore = new Dictionary<Tuple<Type, Type, Type>, object>();
            _AdditionEntry = new BinaryOperatorEntry.AnyTypes(_AdditionStore);
            _SubtractionEntry = new BinaryOperatorEntry.AnyTypes(_SubtractionStore);
            _MultiplicationEntry = new BinaryOperatorEntry.AnyTypes(_MultiplicationtionStore);
            _DivisionEntry = new BinaryOperatorEntry.AnyTypes(_DivisionStore);
            _ModulusEntry = new BinaryOperatorEntry.AnyTypes(_ModulusStore);
        }

        public BinaryOperatorEntry.AnyTypes Addition { get { return _AdditionEntry; } }
        public BinaryOperatorEntry.AnyTypes Subtraction { get { return _SubtractionEntry; } }
        public BinaryOperatorEntry.AnyTypes Multiply { get { return _MultiplicationEntry; } }
        public BinaryOperatorEntry.AnyTypes Division { get { return _DivisionEntry; } }
        public BinaryOperatorEntry.AnyTypes Modulus { get { return _ModulusEntry; } }
    }

    public class Bitwise {
        Dictionary<Tuple<Type, Type, Type>, object> _BitwiseAndStore;
        Dictionary<Tuple<Type, Type, Type>, object> _BitwiseOrStore;
        Dictionary<Tuple<Type, Type, Type>, object> _ExclusiveOrStore;
        Dictionary<Tuple<Type, Type>, object> _OnesComplementStore;
        Dictionary<Tuple<Type, Type, Type>, object> _LeftShiftStore;
        Dictionary<Tuple<Type, Type, Type>, object> _RightShiftStore;
        BinaryOperatorEntry.SameTypes _BitwiseAndEntry;
        BinaryOperatorEntry.SameTypes _BitwiseOrEntry;
        BinaryOperatorEntry.SameTypes _ExclusiveOrEntry;
        UnaryOperatorEntry.SameTypes _OnesComplementEntry;
        BinaryOperatorEntry.SameTypes_ConstrainedOperand2<int> _LeftShiftEntry;
        BinaryOperatorEntry.SameTypes_ConstrainedOperand2<int> _RightShiftEntry;

        internal Bitwise() {
            _BitwiseAndStore = new Dictionary<Tuple<Type, Type, Type>, object>();
            _BitwiseOrStore = new Dictionary<Tuple<Type, Type, Type>, object>();
            _ExclusiveOrStore = new Dictionary<Tuple<Type, Type, Type>, object>();
            _OnesComplementStore = new Dictionary<Tuple<Type, Type>, object>();
            _LeftShiftStore = new Dictionary<Tuple<Type, Type, Type>, object>();
            _RightShiftStore = new Dictionary<Tuple<Type, Type, Type>, object>();
            _BitwiseAndEntry = new BinaryOperatorEntry.SameTypes(_BitwiseAndStore);
            _BitwiseOrEntry = new BinaryOperatorEntry.SameTypes(_BitwiseOrStore);
            _ExclusiveOrEntry = new BinaryOperatorEntry.SameTypes(_ExclusiveOrStore);
            _OnesComplementEntry = new UnaryOperatorEntry.SameTypes(_OnesComplementStore);
            _LeftShiftEntry = new BinaryOperatorEntry.SameTypes_ConstrainedOperand2<int>(_LeftShiftStore);
            _RightShiftEntry = new BinaryOperatorEntry.SameTypes_ConstrainedOperand2<int>(_RightShiftStore);
        }

        public BinaryOperatorEntry.SameTypes BitwiseAnd { get { return _BitwiseAndEntry; } }
        public BinaryOperatorEntry.SameTypes BitwiseOr { get { return _BitwiseOrEntry; } }
        public BinaryOperatorEntry.SameTypes ExclusiveOr { get { return _ExclusiveOrEntry; } }
        public UnaryOperatorEntry.SameTypes OnesComplement { get { return _OnesComplementEntry; } }
        public BinaryOperatorEntry.SameTypes_ConstrainedOperand2<int> LeftShift { get { return _LeftShiftEntry; } }
        public BinaryOperatorEntry.SameTypes_ConstrainedOperand2<int> RightShift { get { return _RightShiftEntry; } }
    }

    class Nullary<TResult> {
    }

    public static class UnaryOperatorEntry {
        public class SameTypes {
            Dictionary<Tuple<Type, Type>, object> _entries;

            internal SameTypes(Dictionary<Tuple<Type, Type>, object> entries) {
                _entries = entries;
            }

            public void SetHandler<T>(Func<T, T> handler) {
                var entry = UnaryOperatorEntry.GetEntry<T, T>(_entries);
                entry.SetHandler(handler);
            }

            public void SetProviderFactory<T>(Func<Provider<T>, Provider<T>> providerFactory) {
                var entry = UnaryOperatorEntry.GetEntry<T, T>(_entries);
                entry.SetProviderFactory(providerFactory);
            }

            public Provider<T> GetProvider<T>(Provider<T> operand) {
                var entry = UnaryOperatorEntry.GetEntry<T, T>(_entries);
                return entry.GetProvider(operand);
            }
        }

        public class AnyTypes {
            Dictionary<Tuple<Type, Type>, object> _entries;

            internal AnyTypes(Dictionary<Tuple<Type, Type>, object> entries) {
                _entries = entries;
            }

            public void SetHandler<TOperand, TResult>(Func<TOperand, TResult> handler) {
                var entry = UnaryOperatorEntry.GetEntry<TOperand, TResult>(_entries);
                entry.SetHandler(handler);
            }

            public void SetProviderFactory<TOperand, TResult>(Func<Provider<TOperand>, Provider<TResult>> providerFactory) {
                var entry = UnaryOperatorEntry.GetEntry<TOperand, TResult>(_entries);
                entry.SetProviderFactory(providerFactory);
            }

            public Provider<TResult> GetProvider<TOperand, TResult>(Provider<TOperand> operand) {
                var entry = UnaryOperatorEntry.GetEntry<TOperand, TResult>(_entries);
                return entry.GetProvider(operand);
            }
        }

        internal static UnaryOperatorEntry<TOperand, TResult> GetEntry<TOperand, TResult>(Dictionary<Tuple<Type, Type>, object> entries) {
            var key = Tuple.Create(typeof(TOperand), typeof(TResult));
            UnaryOperatorEntry<TOperand, TResult> entry;
            object entryObject;
            if (entries.TryGetValue(key, out entryObject)) {
                entry = (UnaryOperatorEntry<TOperand, TResult>)entryObject;
            } else {
                entry = new UnaryOperatorEntry<TOperand, TResult>();
                entries[key] = entry;
            }
            return entry;
        }
    }

    class UnaryOperatorEntry<TOperand, TResult> {
        Func<TOperand, TResult> _handler;
        Func<Provider<TOperand>, Provider<TResult>> _providerFactory;

        public void SetHandler(Func<TOperand, TResult> handler) {
            _handler = handler;
        }

        public void SetProviderFactory(Func<Provider<TOperand>, Provider<TResult>> providerFactory) {
            _providerFactory = providerFactory;
        }

        public Provider<TResult> GetProvider(Provider<TOperand> operand) {
            if (_providerFactory != null) {
                return _providerFactory(operand);
            }
            if (_handler != null) {
                return Provider.Create(_handler, operand);
            }
            throw new InvalidOperationException(string.Format("No handler was registered for this operation for types {0}->{1}.", typeof(TOperand), typeof(TResult)));
        }
    }

    public class BinaryOperatorEntry {
        public class SameTypes {
            Dictionary<Tuple<Type, Type, Type>, object> _entries;

            internal SameTypes(Dictionary<Tuple<Type, Type, Type>, object> entries) {
                _entries = entries;
            }

            public void SetHandler<T>(Func<T, T, T> handler) {
                var entry = BinaryOperatorEntry.GetEntry<T, T, T>(_entries);
                entry.SetHandler(handler);
            }

            public void SetProviderFactory<T>(Func<Provider<T>, Provider<T>, Provider<T>> providerFactory) {
                var entry = BinaryOperatorEntry.GetEntry<T, T, T>(_entries);
                entry.SetProviderFactory(providerFactory);
            }

            public void SetProviderFactory<T>(Func<Provider<T>, T, Provider<T>> providerFactoryRightConst) {
                var entry = BinaryOperatorEntry.GetEntry<T, T, T>(_entries);
                entry.SetProviderFactory(providerFactoryRightConst);
            }

            public void SetProviderFactory<T>(Func<T, Provider<T>, Provider<T>> providerFactoryLeftConst) {
                var entry = BinaryOperatorEntry.GetEntry<T, T, T>(_entries);
                entry.SetProviderFactory(providerFactoryLeftConst);
            }

            public Provider<T> GetProvider<T>(Provider<T> operands1, Provider<T> operands2) {
                var entry = BinaryOperatorEntry.GetEntry<T, T, T>(_entries);
                return entry.GetProvider(operands1, operands2);
            }

            public Provider<T> GetProvider<T>(Provider<T> operands1, T operand2) {
                var entry = BinaryOperatorEntry.GetEntry<T, T, T>(_entries);
                return entry.GetProvider(operands1, operand2);
            }

            public Provider<T> GetProvider<T>(T operand1, Provider<T> operands2) {
                var entry = BinaryOperatorEntry.GetEntry<T, T, T>(_entries);
                return entry.GetProvider(operand1, operands2);
            }
        }

        public class SameTypes_ConstrainedOperand2<TOperand2> {
            Dictionary<Tuple<Type, Type, Type>, object> _entries;

            internal SameTypes_ConstrainedOperand2(Dictionary<Tuple<Type, Type, Type>, object> entries) {
                _entries = entries;
            }

            public void SetHandler<T>(Func<T, TOperand2, T> handler) {
                var entry = BinaryOperatorEntry.GetEntry<T, TOperand2, T>(_entries);
                entry.SetHandler(handler);
            }

            public void SetProviderFactory<T>(Func<Provider<T>, Provider<TOperand2>, Provider<T>> providerFactory) {
                var entry = BinaryOperatorEntry.GetEntry<T, TOperand2, T>(_entries);
                entry.SetProviderFactory(providerFactory);
            }

            public void SetProviderFactory<T>(Func<Provider<T>, TOperand2, Provider<T>> providerFactoryRightConst) {
                var entry = BinaryOperatorEntry.GetEntry<T, TOperand2, T>(_entries);
                entry.SetProviderFactory(providerFactoryRightConst);
            }

            public void SetProviderFactory<T>(Func<T, Provider<TOperand2>, Provider<T>> providerFactoryLeftConst) {
                var entry = BinaryOperatorEntry.GetEntry<T, TOperand2, T>(_entries);
                entry.SetProviderFactory(providerFactoryLeftConst);
            }

            public Provider<T> GetProvider<T>(Provider<T> operands1, Provider<TOperand2> operands2) {
                var entry = BinaryOperatorEntry.GetEntry<T, TOperand2, T>(_entries);
                return entry.GetProvider(operands1, operands2);
            }

            public Provider<T> GetProvider<T>(Provider<T> operands1, TOperand2 operand2) {
                var entry = BinaryOperatorEntry.GetEntry<T, TOperand2, T>(_entries);
                return entry.GetProvider(operands1, operand2);
            }

            public Provider<T> GetProvider<T>(T operand1, Provider<TOperand2> operands2) {
                var entry = BinaryOperatorEntry.GetEntry<T, TOperand2, T>(_entries);
                return entry.GetProvider(operand1, operands2);
            }
        }

        public class SameTypes_ConstrainedResult<TResult> {
            Dictionary<Tuple<Type, Type, Type>, object> _entries;

            internal SameTypes_ConstrainedResult(Dictionary<Tuple<Type, Type, Type>, object> entries) {
                _entries = entries;
            }

            public void SetHandler<TOperand>(Func<TOperand, TOperand, TResult> handler) {
                var entry = BinaryOperatorEntry.GetEntry<TOperand, TOperand, TResult>(_entries);
                entry.SetHandler(handler);
            }

            public void SetProviderFactory<TOperand>(Func<Provider<TOperand>, Provider<TOperand>, Provider<TResult>> providerFactory) {
                var entry = BinaryOperatorEntry.GetEntry<TOperand, TOperand, TResult>(_entries);
                entry.SetProviderFactory(providerFactory);
            }

            public void SetProviderFactory<TOperand>(Func<Provider<TOperand>, TOperand, Provider<TResult>> providerFactoryRightConst) {
                var entry = BinaryOperatorEntry.GetEntry<TOperand, TOperand, TResult>(_entries);
                entry.SetProviderFactory(providerFactoryRightConst);
            }

            public void SetProviderFactory<TOperand>(Func<TOperand, Provider<TOperand>, Provider<TResult>> providerFactoryLeftConst) {
                var entry = BinaryOperatorEntry.GetEntry<TOperand, TOperand, TResult>(_entries);
                entry.SetProviderFactory(providerFactoryLeftConst);
            }

            public Provider<TResult> GetProvider<TOperand>(Provider<TOperand> operands1, Provider<TOperand> operands2) {
                var entry = BinaryOperatorEntry.GetEntry<TOperand, TOperand, TResult>(_entries);
                return entry.GetProvider(operands1, operands2);
            }

            public Provider<TResult> GetProvider<TOperand>(Provider<TOperand> operands1, TOperand operand2) {
                var entry = BinaryOperatorEntry.GetEntry<TOperand, TOperand, TResult>(_entries);
                return entry.GetProvider(operands1, operand2);
            }

            public Provider<TResult> GetProvider<TOperand>(TOperand operand1, Provider<TOperand> operands2) {
                var entry = BinaryOperatorEntry.GetEntry<TOperand, TOperand, TResult>(_entries);
                return entry.GetProvider(operand1, operands2);
            }
        }

        public class AnyTypes {
            Dictionary<Tuple<Type, Type, Type>, object> _entries;

            internal AnyTypes(Dictionary<Tuple<Type, Type, Type>, object> entries) {
                _entries = entries;
            }

            public void SetHandler<TOperand1, TOperand2, TResult>(Func<TOperand1, TOperand2, TResult> handler) {
                var entry = BinaryOperatorEntry.GetEntry<TOperand1, TOperand2, TResult>(_entries);
                entry.SetHandler(handler);
            }

            public void SetProviderFactory<TOperand1, TOperand2, TResult>(Func<Provider<TOperand1>, Provider<TOperand2>, Provider<TResult>> providerFactory) {
                var entry = BinaryOperatorEntry.GetEntry<TOperand1, TOperand2, TResult>(_entries);
                entry.SetProviderFactory(providerFactory);
            }

            public void SetProviderFactory<TOperand1, TOperand2, TResult>(Func<Provider<TOperand1>, TOperand2, Provider<TResult>> providerFactoryRightConst) {
                var entry = BinaryOperatorEntry.GetEntry<TOperand1, TOperand2, TResult>(_entries);
                entry.SetProviderFactory(providerFactoryRightConst);
            }

            public void SetProviderFactory<TOperand1, TOperand2, TResult>(Func<TOperand1, Provider<TOperand2>, Provider<TResult>> providerFactoryLeftConst) {
                var entry = BinaryOperatorEntry.GetEntry<TOperand1, TOperand2, TResult>(_entries);
                entry.SetProviderFactory(providerFactoryLeftConst);
            }

            public Provider<T> GetProvider<T>(Provider<T> operands1, Provider<T> operands2) {
                return GetProvider<T, T, T>(operands1, operands2);
            }

            public Provider<T> GetProvider<T>(Provider<T> operands1, T operand2) {
                return GetProvider<T, T, T>(operands1, operand2);
            }

            public Provider<T> GetProvider<T>(T operand1, Provider<T> operands2) {
                return GetProvider<T, T, T>(operand1, operands2);
            }

            public Provider<TResult> GetProvider<TOperand1, TOperand2, TResult>(Provider<TOperand1> operands1, Provider<TOperand2> operands2) {
                var entry = BinaryOperatorEntry.GetEntry<TOperand1, TOperand2, TResult>(_entries);
                return entry.GetProvider(operands1, operands2);
            }

            public Provider<TResult> GetProvider<TOperand1, TOperand2, TResult>(Provider<TOperand1> operands1, TOperand2 operand2) {
                var entry = BinaryOperatorEntry.GetEntry<TOperand1, TOperand2, TResult>(_entries);
                return entry.GetProvider(operands1, operand2);
            }

            public Provider<TResult> GetProvider<TOperand1, TOperand2, TResult>(TOperand1 operand1, Provider<TOperand2> operands2) {
                var entry = BinaryOperatorEntry.GetEntry<TOperand1, TOperand2, TResult>(_entries);
                return entry.GetProvider(operand1, operands2);
            }
        }


        internal static BinaryOperatorEntry<TOperand1, TOperand2, TResult> GetEntry<TOperand1, TOperand2, TResult>(Dictionary<Tuple<Type, Type, Type>, object> entries) {
            var key = Tuple.Create(typeof(TOperand1), typeof(TOperand2), typeof(TResult));
            BinaryOperatorEntry<TOperand1, TOperand2, TResult> entry;
            object entryObject;
            if (entries.TryGetValue(key, out entryObject)) {
                entry = (BinaryOperatorEntry<TOperand1, TOperand2, TResult>)entryObject;
            } else {
                entry = new BinaryOperatorEntry<TOperand1, TOperand2, TResult>();
                entries[key] = entry;
            }
            return entry;
        }
    }


    class BinaryOperatorEntry<TOperand1, TOperand2, TResult> {
        Func<TOperand1, TOperand2, TResult> _handler;
        Func<Provider<TOperand1>, Provider<TOperand2>, Provider<TResult>> _providerFactory;
        Func<TOperand1, Provider<TOperand2>, Provider<TResult>> _providerFactoryLeftConst;
        Func<Provider<TOperand1>, TOperand2, Provider<TResult>> _providerFactoryRightConst;

        public void SetHandler(Func<TOperand1, TOperand2, TResult> handler) {
            _handler = handler;
        }

        public void SetProviderFactory(Func<Provider<TOperand1>, Provider<TOperand2>, Provider<TResult>> providerFactory) {
            _providerFactory = providerFactory;
        }

        public void SetProviderFactory(Func<Provider<TOperand1>, TOperand2, Provider<TResult>> providerFactoryRightConst) {
            _providerFactoryRightConst = providerFactoryRightConst;
        }

        public void SetProviderFactory(Func<TOperand1, Provider<TOperand2>, Provider<TResult>> providerFactoryLeftConst) {
            _providerFactoryLeftConst = providerFactoryLeftConst;
        }

        public Provider<TResult> GetProvider(Provider<TOperand1> operands1, Provider<TOperand2> operands2) {
            if (_providerFactory != null) {
                return _providerFactory(operands1, operands2);
            }
            if (_handler != null) {
                return Provider.Create(_handler, operands1, operands2);
            }
            throw new InvalidOperationException(string.Format("No handler was registered for this operation for types ({0}, {1}) -> {2}.", typeof(TOperand1).Name, typeof(TOperand2).Name, typeof(TResult).Name));
        }

        public Provider<TResult> GetProvider(Provider<TOperand1> operands1, TOperand2 operand2) {
            if (_providerFactoryRightConst != null) {
                return _providerFactoryRightConst(operands1, operand2);
            }
            return GetProvider(operands1, Provider.Create(operand2));
        }

        public Provider<TResult> GetProvider(TOperand1 operand1, Provider<TOperand2> operands2) {
            if (_providerFactoryLeftConst != null) {
                return _providerFactoryLeftConst(operand1, operands2);
            }
            return GetProvider(Provider.Create(operand1), operands2);
        }
    }
}
