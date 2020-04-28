using System;
using System.Collections;
using System.Collections.Generic;

namespace AppMo
{
    public class Set<T> : IEnumerable<T>
    {
        /// Коллекция хранимых данных.
        public List<T> _items = new List<T>();

        /// Количество элементов.
        public int Count => _items.Count;

        /// Добавить данные во множество.
        /// <param name="item"> Добавляемые данные. </param>
        public void Add(T item)
        {
            // Проверяем входные данные на пустоту.
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (!_items.Contains(item))
                _items.Add(item);
        }

        /// Удалить элемент из множества.
        /// <param name="item"> Удаляемый элемент данных. </param>
        public void Remove(T item)
        {
            // Проверяем входные данные на пустоту.
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (_items.Contains(item))
            {
                _items.Remove(item);
            }
        }

        /// Объединение множеств.
        /// <param name="set1"> Первое множество. </param>
        /// <param name="set2"> Второе множество. </param>
        /// <returns> Новое множество, содержащее все уникальные элементы полученных множеств. </returns>
        public static Set<T> Union(Set<T> set1, Set<T> set2)
        {
            // Проверяем входные данные на пустоту.
            if (set1 == null)
            {
                throw new ArgumentNullException(nameof(set1));
            }

            if (set2 == null)
            {
                throw new ArgumentNullException(nameof(set2));
            }

            // Результирующее множество.
            var resultSet = new Set<T>();

            // Элементы данных результирующего множества.
            var items = new List<T>();

            for (int i = 0; i < set1.Count; i++)
            {
                resultSet.Add(set1._items[i]);
            }
            for (int i = 0; i < set2.Count; i++)
            {
                if (!resultSet._items.Contains(set2._items[i]))
                {
                    resultSet.Add(set2._items[i]);
                }
            }


            // Возвращаем результирующее множество.
            return resultSet;
        }

        /// Пересечение множеств.
        /// <param name="set1"> Первое множество. </param>
        /// <param name="set2"> Второе множество. </param>
        /// <returns> Новое множество, содержащее совпадающие элементы данных из полученных множеств. </returns>
        public static Set<T> Intersection(Set<T> set1, Set<T> set2)
        {
            // Проверяем входные данные на пустоту.
            if (set1 == null)
            {
                throw new ArgumentNullException(nameof(set1));
            }

            if (set2 == null)
            {
                throw new ArgumentNullException(nameof(set2));
            }

            // Результирующее множество.
            var resultSet = new Set<T>();

            // Выбираем множество содержащее наименьшее количество элементов.
            if (set1.Count < set2.Count)
            {
                // Первое множество меньше.
                // Проверяем все элементы выбранного множества.
                foreach (var item in set1._items)
                {
                    if (set2._items.Contains(item))
                    {
                        resultSet.Add(item);
                    }
                }
            }
            else
            {
                // Второе множество меньше или множества равны.
                // Проверяем все элементы выбранного множества.
                foreach (var item in set2._items)
                {
                    if (set1._items.Contains(item))
                    {
                        resultSet.Add(item);
                    }
                }
            }

            // Возвращаем результирующее множество.
            return resultSet;
        }

        /// <summary>
        /// Разность множеств.
        /// </summary>
        /// <param name="set1"> Первое множество. </param>
        /// <param name="set2"> Второе множество. </param>
        /// <returns> Новое множество, содержащее не совпадающие элементы данных между полученными множествами. </returns>
        public static Set<T> Difference(Set<T> set1, Set<T> set2)
        {
            // Проверяем входные данные на пустоту.
            if (set1 == null)
            {
                throw new ArgumentNullException(nameof(set1));
            }

            if (set2 == null)
            {
                throw new ArgumentNullException(nameof(set2));
            }

            // Результирующее множество.
            var resultSet = new Set<T>();

            // Проходим по всем элементам первого множества.
            foreach (var item in set1._items)
            {
                if (!set2._items.Contains(item))
                {
                    resultSet.Add(item);
                }
            }

            // Проходим по всем элементам второго множества.
            foreach (var item in set2._items)
            {
                if (!set1._items.Contains(item))
                {
                    resultSet.Add(item);
                }
            }


            return resultSet;
        }

        /// <summary>
        /// Подмножество.
        /// </summary>
        /// <param name="set1"> Множество, проверяемое на вхождение в другое множество. </param>
        /// <param name="set2"> Множество в которое проверяется вхождение другого множества. </param>
        /// <returns> Является ли первое множество подмножеством второго. true - является, false - не является. </returns>
        public static bool Subset(Set<T> set1, Set<T> set2)
        {
            // Проверяем входные данные на пустоту.
            if (set1 == null)
            {
                throw new ArgumentNullException(nameof(set1));
            }

            if (set2 == null)
            {
                throw new ArgumentNullException(nameof(set2));
            }

            foreach (var item in set1._items)
            {
                if (!set2._items.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Вернуть перечислитель, выполняющий перебор всех элементов множества.
        /// </summary>
        /// <returns> Перечислитель, который можно использовать для итерации по коллекции. </returns>
        public IEnumerator<T> GetEnumerator()
        {
            // Используем перечислитель списка элементов данных множества.
            foreach (var item in this._items)
            {
                yield return item;
            }
        }

        /// <summary>
        /// Вернуть перечислитель, который осуществляет итерационный переход по множеству.
        /// </summary>
        /// <returns> Объект IEnumerator, который используется для прохода по коллекции. </returns>
        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

        public bool Contains(T item)
        {
            if (_items.Contains(item)) return true;
            else
                return false;
        }

        public void Write()
        {
            if (!(Count == 0))
            {
                foreach (var _item in _items)
                {
                    Console.WriteLine("" + _item + " ");
                }
            }
            else
            {
                Console.WriteLine("");
            }
        }
    }
}
