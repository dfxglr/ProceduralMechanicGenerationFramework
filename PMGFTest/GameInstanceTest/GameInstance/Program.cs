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

            //decodes the entire set
            testInstance.ParsedSet.DecodeGenomeSet(testset);
            
            //jsut for checking that genomes got correctly decoded
            testInstance.ParsedSet.DisplayActorTypeList();
            testInstance.ParsedSet.DisplayActorTypePossplit3List();

            //genome error report
            testInstance.ParsedSet.GenomeSetErrorReport();
            

            Console.ReadKey();
        }
    }
}
