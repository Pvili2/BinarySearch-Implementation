using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearch
{
    public class PhoneBookItem : IComparable
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public int CompareTo(object? obj)
        {
            if (obj is null) throw new ArgumentNullException(nameof(obj));
            if (obj is not PhoneBookItem || obj is string) throw new ArgumentException(nameof(obj));

            string? otherString = obj is PhoneBookItem otherPhoneBookItem
                ? otherPhoneBookItem.Name
                : obj as string;

            return Name.CompareTo(otherString);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) throw new ArgumentNullException(nameof(obj));
            if (obj is not PhoneBookItem || obj is string) throw new ArgumentException(nameof(obj));

            PhoneBookItem? other = obj as PhoneBookItem;

            return Name == other?.Name && PhoneNumber == other.PhoneNumber;
        }
    }
}
