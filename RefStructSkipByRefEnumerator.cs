using System.Runtime.CompilerServices;

namespace Monkeymoto.RefStructEnumerable
{
    public ref struct RefStructSkipByRefEnumerator<T, TEnumerator>
    (
        TEnumerator enumerator,
        int count
    ) :
        IRefStructByRefEnumerator<T, RefStructSkipByRefEnumerator<T, TEnumerator>>
        where TEnumerator : struct, IRefStructByRefEnumerator<T, TEnumerator>, allows ref struct
    {
        private TEnumerator enumerator = enumerator;
        private int count = count;

        public ref T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref enumerator.Current;
        }

        T IRefStructEnumerator<T, RefStructSkipByRefEnumerator<T, TEnumerator>>.Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefStructSkipByRefEnumerator<T, TEnumerator> GetEnumerator() => this;

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
