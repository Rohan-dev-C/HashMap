using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace HashMap
{
    public class Hashmap<TKey, TValue> : IDictionary<TKey, TValue>
    {
        // https://sharplab.io/#v2:C4LgTgrgdgNAJiA1AHwAICYCMBYAUHgSymAFMwAzAQwGMSACASQAUwjhKAjAGxLwG88dIXVQAWOizYAKAJQBuYXgC+eQsTJVajAOqt23EgB4AKgD5+g4cboBZAJ4A1Slwj0+AcxLA5K3MJHiugSkUtYAbs6u8sqquGwaNPQMAGLQJqZ0IIySxJw8MDp6eUZmdBb4uL54GHQAwgD29TxgDMSZjKlQhmzmuAJ+wqgAzHRsdBFccpZCw3WNza3AUmMTXHYydAC8GRNb485rUwMz4sysuQYAdDlLMtN0/f7+qJgAnFIT0cd0vv73s2N7E4XG5PMAHqgAOz7SZKJRlb5iQrBYrdYimS5BEIrSIkO7fR5PGZvD64+QI/y/RSVWKzGq1B4UwbodD3QlElJpHp0AAO52Adj2UBIAHc5k0yIspOgAKxfIlCe5CAD0yqEfLYdmu/NkRwVvP5WqxJCkGuIWqBZIU918SiAA
        // https://sharplab.io/#v2:C4LgTgrgdgNAJiA1AHwAICYCMBYAUHjAAgEEoBLAWwEMAbPAbz0OcIAcwyA3K4AU0LJRghKgHNeAbkJMWqAMwChJcYXrjg9VAHYR4iQF9mMlidNmAzrw1kAZgAox/ADyFuNCLwCU9R4QC8rrQeBsZmYSa8NJb0wAAWYAD2AO6EULwpAJJQbmRwAPKsvGA8ZAlQAKIAHgDGvKzApVB2ngb6hqGomAAMhAByVBSSJh0KpJS0doLCjjCEnT1QA16h9CyhJnFk5gB0xCoBjlLrLJs7/YP+qUsSofojcwAshAAKHELNK8fMnQCcdnteI64cK/OznQHSYEsO64GF4RaDcysKi1QjVMAATwYeFCHXQhAAwp8oWEMOgvsxGCTwiRyNQaIRErEqFBLmkUmN6XZurMAERMlm8loUkwCqC7faEAC0mCBNNFCWZ4teU2acvCMJMhjWsP0QA=
        public LinkedList<KeyValuePair<TKey, TValue>>[] list;
        public int Count => count;

        private int count;

        Comparer comparer = Comparer.Default;

        public Hashmap(int capacity = 10)
        {
            this.list = new LinkedList<KeyValuePair<TKey, TValue>>[capacity];
            count = 0;
        }

        public TValue this[TKey key]
        {
            get
            {
                var temp = GetPair(key);
                if (temp != null) return temp.Value.Value;
                throw new Exception("not real");
            }
            set
            {
                var temp = GetPair(key);

                if (temp != null) list[GetIndex(key)].Remove(temp.Value);

                Add(key, value);
            }
        }




        public int GetIndex(TKey item)
        {
            return Math.Abs(item.GetHashCode()) % list.Length;
        }

        private LinkedListNode<KeyValuePair<TKey, TValue>> GetPair(TKey key)
        {
            var index = GetIndex(key);

            if (list[index] == null) return null;


            for (var temp = list[index].First; temp != null; temp = temp.Next)
            {
                if (key.Equals(temp.Value.Key)) return temp;
            }
            return null;
        }

        public void Add(TKey key, TValue value)
        {
            Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            int index = GetIndex(item.Key);
            if (list[index] == null)
            {
                list[index] = new LinkedList<KeyValuePair<TKey, TValue>>();
            }
            if (list.Length < count)
            {
                ReHash();
            }
            if (ContainsKey(item.Key))
            {
                throw new Exception("duplicate");
            }

            if (list[index] == null)
            {
                list[index] = new LinkedList<KeyValuePair<TKey, TValue>>();
                list[index].AddFirst(item);
            }
            else
            {
                list[index].AddFirst(item);
            }
            count++;
        }

        public void ReHash()
        {
            LinkedList<KeyValuePair<TKey, TValue>>[] temp = new LinkedList<KeyValuePair<TKey, TValue>>[count * 2];

            foreach (var linkedlist in list)
            {
                foreach (var pair in linkedlist)
                {
                    int index = GetIndex(pair.Key);
                    if (temp[index] == null)
                    {
                        temp[index] = new LinkedList<KeyValuePair<TKey, TValue>>();
                        temp[index].AddFirst(pair);
                    }
                    else
                    {
                        temp[index].AddFirst(pair);
                    }
                }
            }
        }

        public void Clear()
        {
            for (int i = 0; i < list.Length; i++)
            {
                list[i].Clear();
            }

        }
        public bool ContainsKey(TKey key)
        {
            foreach (var linkedlist in list)
            {
                foreach (var pair in linkedlist)
                {
                    if (pair.Key.Equals(key))
                    {
                        return true;
                    }

                }
            }
            return false;
        }


        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (Remove(item.Key))
            {
                return true;
            }
            return false;
        }
        public bool Remove(TKey key)
        {
            if (!ContainsKey(key))
            {
                return false;
            }

            foreach (var linkedlist in list)
            {
                foreach (var pair in linkedlist)
                {
                    if (pair.Key.Equals(key))
                    {
                        linkedlist.Remove(pair);
                        return true;
                    }

                }
            }
            return false;

        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }


        public ICollection<TKey> Keys
        {
            get
            {
                List<TKey> epiclist = new List<TKey>();
                for (int i = 0; i < list.Length; i++)
                {
                    foreach (var pair in list[i])
                    {
                        epiclist.Add(pair.Key);
                    }
                }
                return epiclist;
            }
        }

        public ICollection<TValue> Values
        {
         get
            {
                List<TValue> epiclist = new List<TValue>();
                for (int i = 0; i<list.Length; i++)
                {
                    foreach (var pair in list[i])
                    {
                        epiclist.Add(pair.Value);
                    }
                }
                return epiclist;
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
           var pair = GetPair(key); 
            
            if (pair != null)
            {
                value = pair.Value.Value;
                return true;
            }
            value = default;
            return false;
        }

    
        public bool IsReadOnly => throw new NotImplementedException();
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < list.Length; i++)
            {
                foreach (var pair in list[i])
                {
                    yield return pair;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        { 
            return GetEnumerator(); 
        }

    }
}
