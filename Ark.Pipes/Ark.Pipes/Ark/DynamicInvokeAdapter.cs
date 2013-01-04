using System;
using System.Linq;
using System.Linq.Expressions;

namespace Ark {
    class DynamicInvokeAdapter<TDelegate> where TDelegate : class {
        static Func<Func<object[], object>, TDelegate> _factory;
        TDelegate _invokeHandler;

        /// <summary>
        /// This static method creates a static adapter factory for the <typeparamref name="TDelegate"/> delegate type.
        /// </summary>
        static DynamicInvokeAdapter() {
            if (!typeof(TDelegate).IsSubclassOf(typeof(Delegate))) {
                throw new InvalidOperationException("The TDelegate generic parameter of must be a delegate type.");
            }

            var delegateType = typeof(TDelegate);
            var delegateMethod = delegateType.GetMethod("Invoke");
            var delegateReturnType = delegateMethod.ReturnType;
            var delegateParameters = delegateMethod.GetParameters();

            var dynamicHandlerType = typeof(Func<object[], object>);
            var dynamicHandlerParameter = Expression.Parameter(dynamicHandlerType, "dynamicHandler");

            var parameterExpressions = delegateParameters.Select(p => Expression.Parameter(p.ParameterType, p.Name)).ToArray();
            var dynamicArgumentsExpressions = parameterExpressions.Select(p => Expression.Convert(p, typeof(object)));

            Expression invokeHandlerExpression = Expression.Invoke(
                dynamicHandlerParameter,
                Expression.NewArrayInit(typeof(object), dynamicArgumentsExpressions)
            );

            if (delegateReturnType != typeof(void)) {
                invokeHandlerExpression = Expression.Convert(invokeHandlerExpression, delegateReturnType);
            }

            var factoryExpression = Expression.Lambda<Func<Func<object[], object>, TDelegate>>(
                Expression.Lambda<TDelegate>(
                    invokeHandlerExpression,
                    parameterExpressions
                ),
                dynamicHandlerParameter
            );

            _factory = factoryExpression.Compile();
        }

        public DynamicInvokeAdapter(Func<object[], object> dynamicHandler) {
            _invokeHandler = _factory(dynamicHandler);
        }

        public TDelegate Invoke {
            get { return _invokeHandler; }
        }
    }
}
