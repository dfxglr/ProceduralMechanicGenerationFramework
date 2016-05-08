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
            testGameInstance.BuildInstance(testset,true);



            /*/test find one actor's event's method's value function
            if (debug)
            {
                Console.WriteLine("Total number of created actor : " + CreatedActors.Count);
                Console.WriteLine("first actor's first event's first method's first executelist's first step: " + CreatedActors[0].Events[0]._method._steps[0]._functions[0].Type);

            }

            /*///jsut for checking that genomes got correctly decoded
            //testGameInstance.ParsedSet.DisplayActorTypeList();
            //testGameInstance.ParsedSet.DisplayActorTypePossplit3List();

            //genome error report
            //testGameInstance.ParsedSet.GenomeSetErrorReport();


            Console.ReadKey();
        }
    }
}
