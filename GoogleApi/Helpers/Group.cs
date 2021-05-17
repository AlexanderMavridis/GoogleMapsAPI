using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleApi.Helpers
{
    public class Group<K, T>
    {
        public K Key;
        public IEnumerable<T> Values;
    }
}
