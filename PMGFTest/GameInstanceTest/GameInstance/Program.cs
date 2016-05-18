using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMGF.PMGGameInstance;
using System.Windows.Input;
using PMGF.PMGCore;



namespace GameInstance
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            PMGGenomeSet testset = new PMGGenomeSet();
            testset.TestPMGGenomeSet();
            PMGGenomeParse Parsedtestset = new PMGGenomeParse();
            Parsedtestset.DecodeGenomeSet(testset);
            PMGSingleGameInstance testGameInstance = new PMGSingleGameInstance();

            //decodes the entire set
            //testGameInstance.ParsedSet.DecodeGenomeSet(testset);
            testGameInstance.SetInternalParsedSet(Parsedtestset);
            testGameInstance.BuildInstance(true);



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

            
            bool programRunning = true;
            bool programPaused = false;


            //pretend update function
            for (int timestep = 0; timestep < 2; timestep++)
            {
                Console.WriteLine("-- T = {0} --", timestep);
                testGameInstance.UpdateActors();
  
            }
               // Console.WriteLine("X: "+e.position[0]+"Y: "+e.position[1]);
            
            /*/pretedn update function
            while (programRunning)
            {
                
                if (Keyboard.IsKeyDown(Key.Q))
                {
                    programRunning = false;
                }
                if (Keyboard.IsKeyDown(Key.P))
                {
                    programPaused = true;
                }
                if (!programPaused)
                {
                    //Console.WriteLine("-- T = {0} --", i);
                    testGameInstance.UpdateActors();


                    i++;
                }


                             
            }//*/
            Console.ReadKey();
        }
    }
}
