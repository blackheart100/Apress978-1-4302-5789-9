using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace QueryEntityDataModel
{
    public class FunCollection
    {
        public void async()
        {
            var asyncTask = EF6AsyncDemo();
            foreach (var c in BusyChars())
            {
                if (asyncTask.IsCompleted) { break; }
                Console.Write(c);
                Console.CursorLeft = 0;
                Thread.Sleep(100);
            }
            Console.WriteLine("\nPress <enter> to continue...");
            Console.ReadLine();
        }
        private static IEnumerable<char> BusyChars()
        {
            while (true)
            {
                yield return '\\';
                yield return '|';
                yield return '/';
                yield return '-';
            }
        }
        private static async Task EF6AsyncDemo()
        {
            await Cleanup();
            await LoadData();
            await RunForEachAsyncExample();
            await RunToListAsyncExampe();
            await RunSingleOrDefaultAsyncExampe();
        }
    }
}
