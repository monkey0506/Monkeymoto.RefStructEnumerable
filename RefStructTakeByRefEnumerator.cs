using System.Runtime.CompilerServices;

namespace Monkeymoto.RefStructEnumerable
{
    public ref struct RefStructTakeByRefEnumerator<T, TEnumerator>
    (
        TEnumerator enumerator,
        int count
    ) :
        IRefStructByRefEnumerator<T, RefStructTakeByRefEnumerator<T, TEnumerator>>
        where TEnumerator : struct, IRefStructByRefEnumerator<T, TEnumerator>, allows ref struct
    {
        private TEnumerator enumerator = enumerator;
        private int count = count;

        public ref T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref enumerator.Current;
        }

        T IRefStructEnumerator<T, RefStructTakeByRefEnumerator<T, TEnumerator>>.Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RefStructTakeByRefEnumerator<T, TEnumerator> GetEnumerator() => this;

        public bool MoveNext()
        {
            if (count >= 1)
            {
                --count;
                return enumerator.MoveNext();
            }
            return false;
        }
    }
}
