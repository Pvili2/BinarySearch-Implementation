using System;
using NUnit.Framework;
using BinarySearch;

namespace BInarySearchTest
{
    [TestFixture]
    public class BinarySearchTest
    {
        
        PhoneBookItem[] ascendingArray;
        PhoneBookItem[] descendingArray;
        PhoneBookItem[] notOrdered;

        [SetUp]
        public void Init()
        {
            ascendingArray = new PhoneBookItem[]
            {
                new PhoneBookItem{Name = "Kis Anna", PhoneNumber = "123456" },
                new PhoneBookItem{Name = "Kis Anna", PhoneNumber = "123456" },
                new PhoneBookItem{Name = "Nagy Hanna", PhoneNumber = "345678" },
                new PhoneBookItem{Name = "Nagy Viktória", PhoneNumber = "345679" },
                new PhoneBookItem{Name = "Szép Éva", PhoneNumber = "456789" },
            };

            descendingArray = new PhoneBookItem[]
            {
                new PhoneBookItem{Name = "Szép Éva", PhoneNumber = "456789" },
                new PhoneBookItem{Name = "Nagy Viktória", PhoneNumber = "345679" },
                new PhoneBookItem{Name = "Nagy Hanna", PhoneNumber = "345678" },
                new PhoneBookItem{Name = "Kis Anna", PhoneNumber = "123457" },
                new PhoneBookItem{Name = "Kis Anna", PhoneNumber = "123456" },
            };

            notOrdered = new PhoneBookItem[]
            {
                new PhoneBookItem{Name = "Kis Anna", PhoneNumber = "123457" },
                new PhoneBookItem{Name = "Nagy Viktória", PhoneNumber = "345679" },
                new PhoneBookItem{Name = "Szép Éva", PhoneNumber = "456789" },
                new PhoneBookItem{Name = "Kis Anna", PhoneNumber = "123456" },
                new PhoneBookItem{Name = "Nagy Hanna", PhoneNumber = "345678" },
            };
        }

        [Test]
        public void IsOrderedTest()
        {
            Assert.That(new OrderedItemsHandler(ascendingArray).IsOrdered(), Is.True);
            Assert.That(new OrderedItemsHandler(ascendingArray).IsOrdered(false), Is.False);

            Assert.That(new OrderedItemsHandler(descendingArray).IsOrdered(), Is.False);
            Assert.That(new OrderedItemsHandler(descendingArray).IsOrdered(false), Is.True);

            Assert.That(new OrderedItemsHandler(notOrdered).IsOrdered(), Is.False);
            Assert.That(new OrderedItemsHandler(notOrdered).IsOrdered(false), Is.False);
        }

        [Test]
        public void AscendingSortTest()
        {
            foreach (SortingMethod sort in Enum.GetValues(typeof(SortingMethod)))
            {
                OrderedItemsHandler asc = new(ascendingArray);

                asc.Sort(sort);

                Assert.That(asc.IsOrdered(), Is.True);
                Assert.That(asc.IsOrdered(false), Is.False);

                OrderedItemsHandler desc = new(descendingArray);

                desc.Sort(sort);

                Assert.That(desc.IsOrdered(), Is.True);
                Assert.That(desc.IsOrdered(false), Is.False);

                OrderedItemsHandler notOrd = new(notOrdered);

                notOrd.Sort(sort);

                Assert.That(notOrd.IsOrdered(), Is.True);
                Assert.That(notOrd.IsOrdered(false), Is.False);
            }
        }

        [Test]
        public void BinarySearchItemNotOrderedTest()
        {
            PhoneBookItem item = new PhoneBookItem();
            Assert.Throws<NotOrderedItems>(() => new OrderedItemsHandler(ascendingArray).BinarySearchIterative(item, false));

            Assert.Throws<NotOrderedItems>(() => new OrderedItemsHandler(descendingArray).BinarySearchIterative(item));

            Assert.Throws<NotOrderedItems>(() => new OrderedItemsHandler(notOrdered).BinarySearchIterative(item));

            Assert.Throws<NotOrderedItems>(() => new OrderedItemsHandler(notOrdered).BinarySearchIterative(item, false));
        }

        [TestCase("Padla Vilmos", "1234567818", -1)]
        [TestCase("Szép Éva", "456789", 4)]
        public void BinarySearchItemCorrectTest(string name, string phoneNumber, int expected)
        {
            PhoneBookItem item = new PhoneBookItem { Name = name, PhoneNumber = phoneNumber };

            Assert.That(new OrderedItemsHandler(ascendingArray).BinarySearchIterative(item), Is.EqualTo(expected));

            Assert.That(new OrderedItemsHandler(ascendingArray).BinarySearchRecursive(0,4,item), Is.EqualTo(expected));
            
        }

        [TestCase("Padla Vilmos", "1234567818", -1)]
        [TestCase("Szép Éva", "456789", 0)]
        public void BinarySearchItemCorrectTestDesc(string name, string phoneNumber, int expected)
        {
            PhoneBookItem item = new PhoneBookItem { Name = name, PhoneNumber = phoneNumber };

            Assert.That(new OrderedItemsHandler(descendingArray).BinarySearchIterative(item, false), Is.EqualTo(expected));

            Assert.That(new OrderedItemsHandler(descendingArray).BinarySearchRecursive(0, 4, item, false), Is.EqualTo(expected));
        }
        [TestCase("Padla Vilmos", "1234567818", 4)]
        [TestCase("Szép Éva", "456789", 4)]
        [TestCase("Albert Albert", "345212", 0)]
        [TestCase("Vass Vanda", "9999999", 5)]
        public void LowerBoundTest(string name, string phoneNumber, int expected)
        {
            PhoneBookItem item = new PhoneBookItem { Name = name, PhoneNumber = phoneNumber };

            Assert.That(new OrderedItemsHandler(ascendingArray).LowerBound(item), Is.EqualTo(expected));

        }

        [TestCase("Kis Anna", "123456", 2)]
        public void HigherBoundTest(string name, string phoneNumber, int expected)
        {
            PhoneBookItem item = new PhoneBookItem { Name = name, PhoneNumber = phoneNumber };

            Assert.That(new OrderedItemsHandler(ascendingArray).HigherBound(item), Is.EqualTo(expected));
        }
        [TestCase("Kis Anna", "123456", 2)]
        public void CountSpecificNumberTest(string name, string phoneNumber, int expected)
        {
            PhoneBookItem item = new PhoneBookItem { Name = name, PhoneNumber = phoneNumber };

            Assert.That(new OrderedItemsHandler(ascendingArray).HigherBound(item), Is.EqualTo(expected));
        }
    }
}
