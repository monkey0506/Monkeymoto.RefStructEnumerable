# RefStructEnumerable
**Monkeymoto.RefStructEnumerable**

C# 13 class library which provides
[enumerable](https://learn.microsoft.com/en-us/dotnet/csharp/iterators)
extension methods for `ref struct` enumerators.

*This project is in early development, and largely exists as a
proof-of-concept.*

## How to use this project

The extension methods provided by this project depend on the
`IRefStructEnumerator<TEnumerator, T>` interface which it defines. This
interface is implemented by a `ref struct` enumerator for your user-defined
types.

You may choose to use the `IRefStructByRefEnumerator<TEnumerator, T>` which
will enable enumerating your values with by-`ref` parameters (allowing
in-place mutation during enumeration). This interface is derived from
`IRefStructEnumerator<TEnumerator, T>`.

Both interfaces take two generic type parameters. `TEnumerator` is the
enumerator type itself. `T` is the element type of values which will be
enumerated.

For example, you might have an inline array such as:

````C#
[InlineArray(8)]
public struct Buffer8<T>
{
    private T element0;

    public ref struct Enumerator(ref Buffer8<T> buffer) : IRefStructByRefEnumerator<Enumerator, T>
    {
        private Span<T>.Enumerator enumerator = GetSpanEnumerator(ref buffer);

        private static Span<T>.Enumerator GetSpanEnumerator(ref Buffer8<T> buffer)
        {
            return MemoryMarshal.CreateSpan(ref Unsafe.As<Buffer8<T>, T>(ref buffer), 8).GetEnumerator();
        }

        public ref T Current
        {
            get => ref enumerator.Current;
        }

        T IRefStructEnumerator<Enumerator, T>.Current
        {
            get => Current;
        }

        public Enumerator GetEnumerator() => this;

        public bool MoveNext() => enumerator.MoveNext();
    }

    public Enumerator GetEnumerator() => new(ref this);
}
````

The `Enumerator.GetEnumerator()` method is **required** by
`IRefStructEnumerator<TEnumerator, T>` to enable `foreach` enumeration of the
types introduced by this project (most of which are designed to be used
transparently).

The following extension methods are currently available for enumerators which
implement `IRefStructEnumerator<TEnumerator, T>`:

- `Any(T? _)`
- `Any(Func<T, bool> predicate, T? _)`
- `All(Func<T, bool> predicate, T? _)`
- `Select(SelectByRefSelector<TSource, TResult> selector)`
- `Select(Func<TSource, TResult> selector)`
- `Skip(int count, T? _)`
- `SkipByRef(int count, T? _)`
- `Take(int count, T? _)`
- `TakeByRef(int count, T? _)`
- `Where(WhereByRefPredicate<T> predicate)`
- `Where(Func<T, bool> predicate)`

Each of these methods mirrors the corresponding `System.Linq` method.

Each method which has a `T? _` parameter is used for generic type argument
deduction. The argument is not used by the method, and you may pass in any
appropriately typed value (recommended use is: `default(T)`).

The `SkipByRef` and `TakeByRef` methods preserve the
`IRefStructByRefEnumerator<TEnumerator, T>` interface for chained calls. The
`Skip` and `Take` methods drop the `-ByRef-` interface.

The `Select` and `Where` methods have overloads that take a custom delegate
that exposes the `.Current` element by `ref`. For `Where`, the predicate
argument is `ref readonly`. The `Select` method's selector takes its argument
by mutable `ref`, allowing you to mutate the current value while selecting a
new value. These by-`ref` overloads can only be chained with methods that
preserve the `IRefStructByRefEnumerator<TEnumerator, T>` interface. For
example, you cannot chain the by-`ref` `Select` after a call to `Take`, but you
can chain it after a call to `TakeByRef`.

### Example

Given the `Buffer8<T>` type above, you could then do:

````C#
var buffer = new Buffer8<int>();
// populate buffer
foreach
(
    int i in buffer.GetEnumerator()
        .Where(static (ref readonly int x) => x > 15 && x < 30) // call by-ref Where with explicit lambda argument
        .Select(static (ref int x) => // call by-ref Select with explicit lambda argument
        {
            x *= 3; // mutate values in-place
            return ref x; // return value by-ref
        })
        .Take(10, default(int)) // passing second argument assists with generic type parameter deduction
        .Skip(2, _: 0) // chaotic evil approach to passing second argument
)
{
    Console.WriteLine(i); // print each value
}
````
