using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UN_SingleMenuManager
{
    public sealed class SingleMenuManager
    {
        //-------------------------------------------------// singleton thread safe with no lock
        private static readonly SingleMenuManager instance = new SingleMenuManager();
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static SingleMenuManager()
        {
        }
        private SingleMenuManager()
        {
        }
        public static SingleMenuManager Instance
        {
            get
            {
                return instance;
            }
        }
        //-------------------------------------------------//

        public void test()
        {
            Console.WriteLine("fagit");
        }
    }
}

