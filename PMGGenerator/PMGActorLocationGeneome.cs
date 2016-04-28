using GeneticSharp.Domain.Chromosomes;


namespace PMGF
{
    namespace PMGGenerator
    {


        public class PMGActorLocationGeneome : ChromosomeBase
        {
            public PMGActorLocationGeneome() : base(10)
            {
                CreateGenes();
            }

            public override Gene GenerateGene(int geneIndex)
            {
                // Create a gene
                //  random between 0 and max coordr, mby num actors
            }

            public override IChromosome CreateNew()
            {
                return new PMGActorLocationGeneome();
            }
        }


    }

}
