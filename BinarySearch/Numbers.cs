using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearch
{
    internal class Numbers : IComparable
    {
        int number;

        public Numbers(int number)
        {
            this.Number = number;
        }

        public int Number { get => number; set => number = value; }

        public int CompareTo(object? obj)
        {
            Numbers? other = obj as Numbers;
            return Number.CompareTo(other?.Number);
        }
    }
}
