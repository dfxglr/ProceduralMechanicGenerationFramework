using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;
using System.Collections.Generic;


namespace PMGF
{
    namespace PMGGenerator
    {


        public class PMGMethodGenome : ChromosomeBase
        {

			private int _maxGeneVal;

			public PMGMethodGenome(int len, int maxVal) : base(len)
            {
				_maxGeneVal = maxVal;
                CreateGenes();
            }

            public override Gene GenerateGene(int geneIndex)
			{

                // Create a gene
				List<int> _gene = new List<int>();

                float rand = RandomizationProvider.Current.GetFloat();

                float probSum = 0;

                // Use probabilities in PMGGenomeConfigurations to add some
                // key to the "top" of the genome, i.e. first item
                if( rand < (probSum += PMGGenomeConfigurations.MethodProb_Key))
                    _gene.Add((int) MethodGenomeEnums.MethodKey);
				else if( rand < (probSum += PMGGenomeConfigurations.MethodProb_Timestep))
                    _gene.Add((int) MethodGenomeEnums.TimeStep);
				else if( rand < (probSum += PMGGenomeConfigurations.MethodProb_ValFunc))
                    _gene.Add((int) MethodGenomeEnums.ValueFunc);
				else if( rand < (probSum += PMGGenomeConfigurations.MethodProb_ChgFunc))
                    _gene.Add((int) MethodGenomeEnums.ChangeFunc);
                else
					_gene.Add((int) MethodGenomeEnums.UtilFunc);


				_gene.Add (RandomizationProvider.Current.GetInt (0, _maxGeneVal));

				return new Gene(_gene);

            }

            public override IChromosome CreateNew()
            {
				return new PMGMethodGenome(
							RandomizationProvider.Current.GetInt(2,PMGGenomeConfigurations.MethodMaxInitLen)
							, _maxGeneVal);
            }
        }


    }

}
