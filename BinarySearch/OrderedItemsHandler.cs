using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearch
{
    public class NotOrderedItems : Exception
    {
        public IComparable[] items { get; set; }


        public NotOrderedItems(IComparable[] items)
        {
            this.items = items;
        }
        public NotOrderedItems(IComparable[] items, string message) : base(message) 
        {
            this.items = items;
        }
        public NotOrderedItems(IComparable[] items, string message, Exception innerException) : base(message, innerException)
        {
            this.items = items;
        }
    }
    public class OrderedItemsHandler
    {
        IComparable[] items;
        Func<IComparable, IComparable, bool> less;

        public OrderedItemsHandler(IComparable[] items)
        {
            this.items = items;
        }

        public bool IsOrdered(bool isAscending = true)
        {
            DefineLess(isAscending);
            for (int i = 1; i < items.Length; i++)
            {
                if (less(items[i], items[i - 1]))
                {
                    return false;
                }
            }

            return true;
        }

        void DefineLess(bool isAscending)
        {
            less = (a, b) => isAscending ? a.CompareTo(b) < 0 : b.CompareTo(a) < 0;
        }

        public void Sort(SortingMethod sortingMethod, bool isAscending = true)
        {
            DefineLess(isAscending);
            switch (sortingMethod)
            {
                case SortingMethod.Selection:
                    SelectionSort();
                    break;
                case SortingMethod.Bubble:
                    BubbleSort();
                    break;
                case SortingMethod.Insertion:
                    InsertionSort();
                    break;
                default:
                    break;
            }
        }

        private void Swap(int index1, int index2)
        {
            //puffer
            IComparable temp = items[index1];

            items[index1] = items[index2];
            items[index2] = temp;
        }


        //minimum cserés rendezés
        private void SelectionSort()
        {
            for (int i = 0; i < items.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = minIndex + 1; j < items.Length; j++)
                {
                    if (less(items[j], items[minIndex]))
                    {
                        minIndex = j;
                    }
                }
                Swap(i, minIndex);
            }
        }

        //buborékos rendezés
        private void BubbleSort()
        {
            int index = items.Length - 1;
            while (index < 0)
            {
                int lastSwap = 0;
                for (int j = 0; j < index; j++)
                {
                    if (less(items[j + 1], items[j]))
                    {
                        Swap(j, j + 1);
                        lastSwap = j;
                    }
                }
                index = lastSwap;
            }
        }

        //beszúrásos rendezés
        private void InsertionSort()
        {
            for (int i = 1; i < items.Length; i++)
            {
                int j = i - 1;
                IComparable temp = items[j];
                while (j >= 0 && less(temp, items[i]))
                {
                    items[j + 1] = items[i];
                    j--;
                }
                items[j + 1] = temp;
            }
        }

        //bináris keresés, iteratív

        public int BinarySearchIterative(IComparable searchedValue, bool isAscending = true)
        {
            if (!IsOrdered(isAscending)) throw new NotOrderedItems(items);
            DefineLess(isAscending);
            int leftIndex = 0;
            int rightIndex = items.Length - 1;
            int center = (leftIndex + rightIndex) / 2;

            while (leftIndex <= rightIndex && !(items[center].Equals(searchedValue)))
            {
                if (less(searchedValue, items[center]))
                {
                    rightIndex = center - 1;
                }
                else
                {
                    leftIndex = center + 1;
                }
                center = (leftIndex + rightIndex) / 2;
            }

            return leftIndex <= rightIndex ? center : -1;
        }

        //bináris keresés rekurzívan
        public int BinarySearchRecursive(int leftIndex, int rigthIndex, IComparable searchedValue, bool isAscending = true)
        {
            if (!IsOrdered(isAscending)) throw new NotOrderedItems(items);
            DefineLess(isAscending);

            //early exit
            if (leftIndex > rigthIndex)
            {
                return -1;
            }

            int center = (leftIndex + rigthIndex) / 2;

            //early exit
            if (items[center].Equals(searchedValue)) return center;

            if (less(searchedValue, items[center]))
            {
                return BinarySearchRecursive(leftIndex, center - 1, searchedValue, isAscending);
            }
            else
            {
                return BinarySearchRecursive(center + 1, rigthIndex, searchedValue, isAscending);
            }
        }
        public int LowerBound(IComparable searchedValue)
        {
            if(!IsOrdered()) throw new NotOrderedItems(items);
            int leftIndex = 0;
            int rightIndex = items.Length -1;
            int returnIndex = rightIndex + 1;

            while (leftIndex <= rightIndex)
            {
                int center = (leftIndex + rightIndex) / 2;

                //ha a két érték egyenlő akkor is csökkentjük a jobb indexet hiszen a legkisebb alsó határját keressük a keresett számnak
                if (items[center].CompareTo(searchedValue) >= 0)
                {
                    returnIndex = center;
                    rightIndex = center - 1;
                }
                else
                {
                    leftIndex = center + 1;
                }
            }

            return returnIndex;
        }

        public int HigherBound(IComparable searchedValue)
        {
            if(!IsOrdered()) throw new NotOrderedItems(items);

            int leftIndex = 0;
            int rightIndex = items.Length -1;
            int idx = items.Length;

            while (leftIndex <= rightIndex)
            {
                int center = (leftIndex + rightIndex) / 2;

                //itt nincs megengedve az egyenlőség hiszen a keresett szám legnagyobb határját keressük
                if (items[center].CompareTo(searchedValue) > 0)
                {
                    idx= center;
                    rightIndex = center - 1;
                }
                else
                {
                    leftIndex= center + 1;
                }
            }

            return idx;
        }

        public int CountSpecificNumber(IComparable searchedValue)
        {
            if (!IsOrdered()) throw new NotOrderedItems(items);

            int leftIndex = 0;
            int rightIndex = items.Length - 1;
            int db = 0;
            while (leftIndex <= rightIndex)
            {
                int center = (leftIndex + rightIndex) / 2;

                //itt nincs megengedve az egyenlőség hiszen a keresett szám legnagyobb határját keressük
                if (items[center].CompareTo(searchedValue) == 0)
                {
                    rightIndex = center - 1;
                    db++;
                }
                else
                {
                    leftIndex = center + 1;
                }
            }

            return db;
        }
        public int CountInASpecificRange(IComparable startRange, IComparable endRange)
        {
            return HigherBound(endRange) - LowerBound(startRange);
        }

        public IComparable[] FindAllItemInSpecificRange(IComparable startRange, IComparable endRange)
        {
            int lowValue = LowerBound(startRange);
            int highValue = HigherBound(endRange);

            IComparable[] result = new IComparable[highValue - lowValue];
            int j = 0;
            for (int i = lowValue; i < highValue; i++, j++)
            {
                result[j] = items[i];
            }

            return result;
        }
    }
}
