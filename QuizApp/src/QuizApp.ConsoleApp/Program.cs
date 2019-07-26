using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QuizApp.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ListRepository<string> listRepository = new ListRepository<string>();

            string[] strArr = { "Hello", "World", "Lorem", "Ipsum", "No", "Time", "Monday" };

            // insert sync and async
            Console.WriteLine("insert sync and async");
            listRepository.Insert(strArr[0]);
            await listRepository.InsertAsync(strArr[1]);
            Show(listRepository.List);

            // delete one element
            Console.WriteLine("delete one element");
            listRepository.Delete(strArr[0]);
            Show(listRepository.List);

            // delete non existent element
            Console.WriteLine("delete non existent element");
            listRepository.Delete(strArr[4]);
            Show(listRepository.List);

            // delete all elements, add new ones, delete elements by some predicate
            Console.WriteLine("delete all elements, add new ones, delete elements by some predicate");
            listRepository.Delete(null as Expression<Func<string, bool>>);
            listRepository.Insert(strArr[0]);
            listRepository.Insert(strArr[1]);
            listRepository.Insert(strArr[2]);
            listRepository.Insert(strArr[3]);
            listRepository.Insert(strArr[4]);
            Show(listRepository.List);
            listRepository.Delete(str => str.Contains("o"));
            Show(listRepository.List);

            // get by id sync and async
            Console.WriteLine("get by id sync and async");
            listRepository.Delete(null as Expression<Func<string, bool>>);
            listRepository.Insert(strArr[0]);
            listRepository.Insert(strArr[1]);
            listRepository.Insert(strArr[2]);
            Show(listRepository.List);
            Console.WriteLine(listRepository.GetById(1));
            Console.WriteLine(await listRepository.GetByIdAsync(2));

            // count all elements and by predicate
            Console.WriteLine($"{Environment.NewLine}count all elements and by predicate");
            listRepository.Delete(null as Expression<Func<string, bool>>);
            listRepository.Insert(strArr[0]);
            listRepository.Insert(strArr[1]);
            listRepository.Insert(strArr[2]);
            Show(listRepository.List);
            Console.WriteLine(listRepository.Count());
            Console.WriteLine(listRepository.Count(str => str.Contains("e")));

            // get all elements and by predicate, sort all elements, filter elements and then sort them
            Console.WriteLine($"{Environment.NewLine}get all elements and by predicate, sort all elements, filter elements and then sort them");
            listRepository.Delete(null as Expression<Func<string, bool>>);
            listRepository.Insert(strArr[0]);
            listRepository.Insert(strArr[1]);
            listRepository.Insert(strArr[2]);
            Show(listRepository.List);
            Show(listRepository.Get());
            Show(listRepository.Get(filter: str => str.Contains("e")));
            Show(listRepository.Get(orderBy: str => str.OrderBy(s => s)));
            Show(listRepository.Get(filter: str => str.Contains("e"), orderBy: str => str.OrderByDescending(s => s)));

            Console.ReadLine();
        }

        static void Show(IEnumerable<string> list)
        {
            foreach (string str in list) 
            {
                Console.WriteLine(str);
            }
            Console.WriteLine();
        }
    }
}
