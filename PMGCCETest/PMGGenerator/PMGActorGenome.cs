using GeneticSharp.Domain.Randomizations;
using GeneticSharp.Domain.Chromosomes;
using PMGF.PMGGenerator;


namespace PMGF
{
    namespace PMGGenerator
    {


        public class PMGActorGenome : ChromosomeBase
        {
			private int _maxGeneVal;

			public PMGActorGenome(int len, int maxVal) : base(len)
            {
				_maxGeneVal = maxVal;
                CreateGenes();
            }

            public override Gene GenerateGene(int geneIndex)
            {
                // Create a gene
                float rand = RandomizationProvider.Current.GetFloat();


				if( rand < PMGGenomeConfigurations.ActorProb_Key )
                {
                    return new Gene((int) GenomeKeys.ActorKey);
                }
                else
                {
					return new Gene(RandomizationProvider.Current.GetInt(0,_maxGeneVal));
                }
            }

            public override IChromosome CreateNew()
            {
				return new PMGActorGenome(
							RandomizationProvider.Current.GetInt(2, PMGGenomeConfigurations.ActorMaxInitLen)
							, _maxGeneVal);
            }
        }


    }

}
