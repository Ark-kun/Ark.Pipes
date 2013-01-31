using System;

namespace Ark {
    interface IInvokable<TDelegate> where TDelegate : class {
        TDelegate TryGetInvoker();
        TDelegate Invoker { get; }
    }

    interface IDynamicInvokable {
        Func<object[], object> TryGetDynamicInvoker();
        object DynamicInvoke(object[] args);
    }
}
