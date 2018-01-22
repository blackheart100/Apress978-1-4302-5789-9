using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;

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
        public static void InsertStatement()
        {
            using (var context = new AssociateContainer())
            {
                context.Database.ExecuteSqlCommand("delete from Chapter3.Payments");
            }
            // insert two rows of data
            using (var context = new AssociateContainer())
            {
                // note how using the following syntax with parameter place holders of @p0 and @p1
                // automatically create the ADO.NET SqlParameters object for you
                var sql = @"insert into Chapter3.Payments(Amount, Vendor) values (@p0, @p1)";
                var rowCount = context.Database.ExecuteSqlCommand(sql, 99.97M, "Ace Plumbing");
                rowCount += context.Database.ExecuteSqlCommand(sql, 43.83M, "Joe's Trash Service");
                Console.WriteLine("{0} rows inserted", rowCount);
                // retrieve and materialize data using (var context = new EFRecipesEntities())
                Console.WriteLine("Payments");
                Console.WriteLine("========");
                foreach (var payment in context.Payments)
                {
                    Console.WriteLine("Paid {0} to {1}", payment.Amount.ToString(),
                                        payment.Vendor);
                }
            }
        }
        public static void fetchObjectByNativeSQL()
        {
            using (var context = new AssociateContainer())
            {
                // delete previous test data
                context.Database.ExecuteSqlCommand("delete from Chapter3.Students");

                // insert student data
                context.Students.Add(new Student
                {
                    FirstName = "Robert",

                    LastName = "Smith",
                    Degree = "Masters"
                });

                context.Students.Add(new Student
                {
                    FirstName = "Julia",
                    LastName = "Kerns",
                    Degree = "Masters"
                });

                context.Students.Add(new Student
                {
                    FirstName = "Nancy",

                    LastName = "Stiles",
                    Degree = "Doctorate"
                });

                context.SaveChanges();
            }

            using (var context = new AssociateContainer())
            {
                string sql = "select * from Chapter3.Student where Degree = @Major";
                var parameters = new DbParameter[] {
        new SqlParameter {ParameterName = "Major", Value = "Masters"}};
                var students = context.Students.SqlQuery(sql, parameters);
                Console.WriteLine("Students...");
                foreach (var student in students)
                {
                    Console.WriteLine("{0} {1} is working on a {2} degree",
                                student.FirstName, student.LastName, student.Degree);
                }
            }
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
        private static async Task Cleanup()
        {
            using (var context = new AssociateContainer())
            {
                // delete previous test data
                // execute raw sql statement asynchronoulsy
                Console.WriteLine("Cleaning Up Previous Test Data");
                Console.WriteLine("=========\n");

                //await context.Database.ExecuteSqlCommandAsync("delete from chapter3.AssociateSalary");
                //await context.Database.ExecuteSqlCommandAsync("delete from chapter3.Associate");
                await Task.Delay(5000);
            }
        }

        private static async Task LoadData()
        {
            using (var context = new AssociateContainer())
            {
                // add new test data
                Console.WriteLine("Adding Test Data");
                Console.WriteLine("=========\n");

                var assoc1 = new Associate { Name = "Janis Roberts" };
                var assoc2 = new Associate { Name = "Kevin Hodges" };
                var assoc3 = new Associate { Name = "Bill Jordan" };
                var salary1 = new AssociateSalary
                {
                    Salary = 39500M,
                    SalaryDate = DateTime.Parse("8/4/09")
                };
                var salary2 = new AssociateSalary
                {
                    Salary = 41900M,
                    SalaryDate = DateTime.Parse("2/5/10")
                };
                var salary3 = new AssociateSalary
                {
                    Salary = 33500M,
                    SalaryDate = DateTime.Parse("10/08/09")
                };
                assoc1.AssociateSalaries.Add(salary1);
                assoc2.AssociateSalaries.Add(salary2);
                assoc3.AssociateSalaries.Add(salary3);
                context.Associates.Add(assoc1);
                context.Associates.Add(assoc2);
                context.Associates.Add(assoc3);

                // update datastore asynchronoulsy
                //await context.SaveChangesAsync();
                await Task.Delay(5000);
            }
        }
        private static async Task RunForEachAsyncExample()
        {
            using (var context = new AssociateContainer())
            {
                Console.WriteLine("Async ForEach Call");
                Console.WriteLine("=========");

                // leverage ForEachAsync
                //await context.Associates.Include(x => x.AssociateSalaries).ForEachAsync(x =>
                //{
                //    Console.WriteLine("Here are the salaries for Associate {0}:", x.Name);

                //    foreach (var salary in x.AssociateSalaries)
                //    {
                //        Console.WriteLine("\t{0}", salary.Salary);
                //    }
                //});
                await Task.Delay(5000);
            }
        }

        private static async Task RunToListAsyncExampe()
        {
            using (var context = new AssociateContainer())
            {
                Console.WriteLine("\n\nAsync ToList Call");
                Console.WriteLine("=========");

                // leverage ToListAsync
                //var associates = await context.Associates.Include(x => x.AssociateSalaries).OrderBy(x =>x.Name).ToListAsync();

                //foreach (var associate in associates)
                //{
                //    Console.WriteLine("Here are the salaries for Associate {0}:", associate.Name);
                //    foreach (var salaryInfo in associate.AssociateSalaries)
                //    {
                //        Console.WriteLine("\t{0}", salaryInfo.Salary);
                //    }
                //}
                await Task.Delay(5000);
            }
        }

        private static async Task RunSingleOrDefaultAsyncExampe()
        {
            using (var context = new AssociateContainer())
            {
                Console.WriteLine("\n\nAsync SingleOrDefault Call");
                Console.WriteLine("=========");


                //var associate = await context.Associates.Include(x => x.AssociateSalaries).OrderBy(x => x.Name).FirstOrDefaultAsync(y => y.Name == "Kevin Hodges");

                //Console.WriteLine("Here are the salaries for Associate {0}:", associate.Name);
                //foreach (var salaryInfo in associate.AssociateSalaries)
                //{
                //    Console.WriteLine("\t{0}", salaryInfo.Salary);
                //}
                await Task.Delay(5000);
            }
        }
    }
}



