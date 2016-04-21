using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMGF.PMGGameInstance; 

namespace GameInstance
{
    class Program
    {
        static void Main(string[] args)
        {
            PMGGenomeSet testset = new PMGGenomeSet();
            testset.TestPMGGenomeSet();

            PMGSingleGameInstance testInstance = new PMGSingleGameInstance();
            testInstance.DecodeGenome(testset);
            testInstance.DisplayActorTypeList();
            testInstance.DisplayGenomeSetErrors();
            

            Console.ReadKey();
        }
    }
}
