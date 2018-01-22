using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryEntityDataModel
{
    class Program
    {
        //FunCollection里面的Public方法都是用来执行的
        static void Main(string[] args)
        {
            FunCollection.fetchObjectByNativeSQL();
        }
    }
}