Ark.Pipes
====

`Ark.Pipes` is a work-in-progress .Net library for creating procedural bindings between different values. It is similar to `DependencyProperty` in `WPF` (more like `DependencyValue`), but is independent from any UI system, lightweight, non-intrusive and easier to use. `Ark.Pipes` can be used for animation, game development and UI (on any platform).
The main part is the simple `Provider<T>` class that represents a value of type `T` that can be read. It has the `Value` property through which you can read this value. But what value does it return? There are endless possibilities.

The goal is to make it as simple to establish links between values as it is to assign expressions to variables. This calls for some magic.

**Creating provider form a constant**
```csharp
Provider<int> p1 = 1;        //Creating provider using implicit conversion
var p2 = Provider.Create(2); //Creating a provider using factory
```

**Getting the value from the provider**
```csharp
int i1 = p1;       //Getting value using implicit conversion
int i2 = p2.Value; //Getting value through the Value property
```

**Creating provider form a argument-less lambda**
```csharp
//Creating provider from a lambda, returning the current time
var currentTime = Provider.Create(() => DateTime.Now);
//Let's test it:
DateTime startTime = currentTime;
Thread.Sleep(1000);
DateTime endTime = currentTime;
Console.WriteLine(endTime - startTime); //You should see "00:00:01"
```

**Creating provider based on another provider**
```csharp
//The secondsElapsed provider depends on the currentTime provider
var secondsElapsed = Provider.Create(time => (time - startTime).TotalSeconds, currentTime); 

//Let's create a point, moving in a circle
var xs = Provider.Create(Math.Cos, secondsElapsed);
var ys = Provider.Create(Math.Sin, secondsElapsed);

//point moves in a circle as time passes
var point = Provider.Create((x, y) => new Vector2(x, y), xs, ys); 

//Let's measure the angle
var angle = Provider.Create(Math.Atan2, ys, xs); //it changes over time too
```

**Doing arithmetics with providers**
```csharp
var p3 = xs * 2 + ys * 3; //pc is now a provider whose value is the combination of xs and ys
```

**Provider properties**

`Property<T>` is a "provider of providers". You can use it to switch underlying providers of a property without disconnecting the providers that are bound to that property.
```csharp
//A simple class with a public property
class MyClass {
    Property<int> _foo = new Property<int>();
    
    public Provider<int> Foo {
        get { return _foo; }           //returning the property
        set { _foo.Provider = value; } //replacing the provider
    }
}

var myObj = new MyClass();
var foo10 = myObj.Foo * 10; //foo10 == myObj.Foo == 0 * 10 == 0
//See how the foo10 provider stays bound to the myObj.Foo even though the underlying providers change
myObj.Foo = p1; //foo10 is now 10
myObj.Foo = p2; //foo10 is now 20
myObj.Foo = Provider.Create(x => (int)x, secondsElapsed); //foo10 is now 10x the number of seconds passed
```
