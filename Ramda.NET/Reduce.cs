using System;
using System.Collections.Generic;

namespace Ramda.NET
{
    public static partial class Util
    {
        public static Func<U, Func<IEnumerable<T>, U>> Reduce<T, U>(Func<U, T, U> accumulator)
            => seed => xs => Reduce(accumulator, seed, xs);

        public static Func<IEnumerable<T>, U> Reduce<T, U>(Func<U, T, U> accumulator, U seed)
            => xs => Reduce(accumulator, seed, xs);

        public static U Reduce<T, U>(Func<U, T, U> accumulator, U memo, IEnumerable<T> xs) 
        {
            if (xs == null || memo == null || accumulator == null) throw new ArgumentNullException();

            var iterator = xs.GetEnumerator();
            if (!iterator.MoveNext())
            {
                return memo;
            }
            U output = accumulator(memo, iterator.Current);

            while (iterator.MoveNext())
            {
                output = accumulator(output, iterator.Current);
            }
            return output;
        }
    }
}
