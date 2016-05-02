using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;
using System.Collections.Generic;


namespace PMGF
{
    namespace PMGGenerator
    {


        public class PMGDynamicEventGenome : ChromosomeBase
        {
			private int _maxGeneVal;

			public PMGDynamicEventGenome(int len, int maxVal) : base(len)
            {
				_maxGeneVal = maxVal;
                CreateGenes();
            }

            public override Gene GenerateGene(int geneIndex)
			{
                // Create a gene
				List<int> _gene = new List<int>();

                float rand = RandomizationProvider.Current.GetFloat();

                float probSum = 0f;

				if( rand < (probSum += PMGGenomeConfigurations.EventProb_Key))
                    _gene.Add((int)EventGenomeEnum.EventKey);
				else if (rand < (probSum += PMGGenomeConfigurations.EventProb_UtilFunc))
                    _gene.Add((int)EventGenomeEnum.UtilFunc);
				else if( rand < (probSum += PMGGenomeConfigurations.EventProb_ValFunc))
                    _gene.Add((int)EventGenomeEnum.ValueFunc);
                else
                    _gene.Add((int)EventGenomeEnum.CondFunc);


				_gene.Add ( RandomizationProvider.Current.GetInt(0,_maxGeneVal));

                return new Gene(_gene);
            }

            public override IChromosome CreateNew()
            {
				// Create with random length
				return new PMGDynamicEventGenome(
							RandomizationProvider.Current.GetInt(2, PMGGenomeConfigurations.EventMaxInitLen)
							, _maxGeneVal);
            }
        }


    }

}
