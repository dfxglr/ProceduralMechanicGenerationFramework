using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;


namespace PMGF
{
    namespace PMGGenerator
    {


        public class PMGActorLocationGenome : ChromosomeBase
        {
			private int _maxGeneVal;

			public PMGActorLocationGenome(int len, int maxVal) : base(len)
            {
				_maxGeneVal = maxVal;
                CreateGenes();
            }

            public override Gene GenerateGene(int geneIndex)
            {
				return new Gene( RandomizationProvider.Current.GetInt(0,_maxGeneVal));
            }

            public override IChromosome CreateNew()
            {
				return new PMGActorLocationGenome(
							RandomizationProvider.Current.GetInt(2, PMGGenomeConfigurations.ActorLocMaxInitLen)
							, _maxGeneVal);
            }
        }


    }

}
