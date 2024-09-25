using System;
using System.Runtime.CompilerServices;

namespace Monkeymoto.RefStructEnumerable
{
    public ref struct RefStructSkipEnumerator<TEnumerator, T>
    (
        TEnumerator enumerator,
        int count
    ) :
        IRefStructEnumerator<RefStructSkipEnumerator<TEnumerator, T>, T>
        where TEnumerator : struct, IRefStructEnumerator<TEnumerator, T>, allows ref struct
    {
        private TEnumerator enumerator = enumerator;
        private int count = Math.Max(count, 0);

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => enumerator.Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefStructSkipEnumerator<TEnumerator, T> GetEnumerator() => this;

        public bool MoveNext()
        {
            while (--count >= 0)
            {
                if (!enumerator.MoveNext())
                {
                    return false;
                }
            }
            return enumerator.MoveNext();
        }
    }
}
