using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FakeDat
{
    internal static class Output
    {
        private static IList<string> data;

        static Output()
        {
            data = new List<string>();

            using (var reader = new StreamReader("list"))
            {
                while (!reader.EndOfStream)
                {
                    data.Add(reader.ReadLine());
                }
            }
        }

        public static void Generate(ParamResolver.Result param)
        {
            if (param == null)
            {
                printDatas();
            }
            else
            {
                searchAndPrint(param);
            }
        }

        private static void printDatas()
        {
            foreach (var line in data)
            {
                Console.WriteLine(line);
            }
        }

        private static void searchAndPrint(ParamResolver.Result param)
        {
            Regex reg = new Regex(string.Format(@"^{0}\s+\d+$", param.Name));
            int matchedCount = 0;
            foreach(var line in data)
            {
                if (reg.IsMatch(line))
                {
                    if (matchedCount == param.Index)
                    {
                        Console.WriteLine("{0}{1}", param.Name, param.Index == 0 ? null : param.Index.ToString());
                        return;
                    }
                    matchedCount++;
                }
            }
            Console.WriteLine("File No Found with name \"{0}\" and index {1}", param.Name, param.Index);
        }
    }
}
