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

            PMGSingleGameInstance testGameInstance = new PMGSingleGameInstance();

            //decodes the entire set
            //testGameInstance.ParsedSet.DecodeGenomeSet(testset);
            testGameInstance.BuildInstance(testset);
        
            //jsut for checking that genomes got correctly decoded
            testGameInstance.ParsedSet.DisplayActorTypeList();
            testGameInstance.ParsedSet.DisplayActorTypePossplit3List();

            //genome error report
            testGameInstance.ParsedSet.GenomeSetErrorReport();
            

            Console.ReadKey();
        }
    }
}
