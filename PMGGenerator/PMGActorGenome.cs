using GeneticSharp.Domain.Randomizations;
using GeneticSharp.Domain.Chromosomes;


namespace PMGF
{
    namespace PMGGenerator
    {


        public class PMGActorGenome : ChromosomeBase
        {
            public PMGActorGenome() : base(10)
            {
                CreateGenes();
            }

            public override Gene GenerateGene(int geneIndex)
            {
                // Create a gene
                float rand = RandomizationProvider.Current.GetFloat();

                if( rand < ActorProb_Key )
                {
                    return new Gene((int) -1);
                }
                else
                {

                    //return new Gene((int)
                    //   between 0 and max( numDynEvents, numMethods )
                    //      -- favors the one which has the most instancs,
                    //          which kinda is fair when you think about it.
                }
            }

            public override IChromosome CreateNew()
            {
                return new PMGActorGenome();
            }
        }


    }

}
