using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CarTeLL

{
    [Serializable]
    internal class GenericStore<T> : IEnumerable<T>
        where T : class
    {
        T[] array = new T[0];


        public void Add(T entity)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = entity;
        }


        public void RemoveAt(int index)
        {
            if (index < 0 || index >= array.Length)
                return;


            for (int i = index; i < array.Length - 1; i++)
            {
                array[i] = array[i + 1];
            }

            Array.Resize(ref array, array.Length - 1);
        }

        public void Remove(T item)
        {
            int index = Array.IndexOf(array, item);

            RemoveAt(index);
        }

        public bool Exists(Func<T, bool> predicate)
        {
            bool hasEntity = array.Any(predicate);
            return hasEntity;
        }

        public T Find(Func<T, bool> predicate)
        {
            T current = array.FirstOrDefault(predicate);
            return current;
        }

        public T[] FindAll(Func<T, bool> predicate)
        {
            T[] current = array.Where(predicate).ToArray();
            return current;
        }


        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= array.Length)
                {
                    throw new IndexOutOfRangeException();
                }

                return array[index];
            }
        }


        public int Count
        {
            get
            {
                return array.Length;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in array)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            //var unboxing = (IEnumerable)this;
            //var unboxing = this as IEnumerable;

            //if (unboxing != null)
            //{

            //}

            return GetEnumerator();
        }
    }
}
