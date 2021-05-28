using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckAnagram
{
    class Program
    {
        static void Main()
        {
            var dictSample = new Dictionary<string, string>
            {
                {"car", "race"},
                {"nod", "done"},
                {"bag", "grab"},

                {"carol", "race"}, // string1 is longer than string2
                {"caro", "race"},  // 'o' is not found in string2
                {"ccar", "race"},  // string1 has more duplicate char than string2
                {"bags", "grabthosebag"},
                {"espn", "grabthosepensplease"},
            };

            var result = string.Empty;
            foreach (var sample in dictSample)
            {
                result = IsAnagram(sample.Key, sample.Value) ? "yes" : "no";
                
                Console.Write($"{sample.Key} : {sample.Value}\n");
                Console.Write($"Is Anagram? {result} \n\n");
            }

            Console.Read();
        }

        private static bool IsAnagram(string str1, string str2)
        {
            if (str1.Length > str2.Length)
                return false;

            var lstUniqueChar1 = str1.ToLower().ToList().GroupBy(x => x).ToList();
            var lstUniqueChar2 = str2.ToLower().ToList().GroupBy(y => y).ToList();

            //check any char in str1 not found in str2
            if (lstUniqueChar1.Select(x => x.Key).Any(x => !lstUniqueChar2.Select(y => y.Key).Contains(x)))
                return false;

            //check whether number of unique char in str1 is more than those in str2
            var diffCharCount = from x in lstUniqueChar1
                                join y in lstUniqueChar2 on x.Key equals y.Key
                                where x.Count() > y.Count()
                                select x;

            if (diffCharCount.Any())
                return false;
           
            //details check
            var lstChars1 = str1.ToLower().ToList();
            var lstChars2 = str2.ToLower().ToList();
            var firstCharFound = str2.First(y => lstChars1.Contains(y));
            var intIndexFirstFound = str2.IndexOf(firstCharFound);
            var lstMatchChars1 = new List<char>();

            for (var intCount = intIndexFirstFound; intCount < str2.Length; intCount++)
            {
                var nextChar2 = lstChars2[intCount];
                if (lstChars1.Contains(nextChar2))
                {
                    lstMatchChars1.Add(nextChar2);

                    var matchCharCount = lstMatchChars1.FindAll(x => x == nextChar2).Count;
                    if (matchCharCount <= lstChars1.FindAll(x => x == nextChar2).Count)
                    {
                        if (lstMatchChars1.Count == str1.Length)
                            break;
                    }
                    else
                        lstMatchChars1.RemoveAt(lstMatchChars1.IndexOf(nextChar2));
                }
                else
                {
                    if (intCount + str1.Length >= str2.Length)
                        return false;

                    lstMatchChars1.Clear();
                }
            }

            return true;
        }
    }
}

