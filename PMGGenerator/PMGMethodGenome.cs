using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;


namespace PMGF
{
    namespace PMGGenerator
    {


        public class PMGMethodGenome : ChromosomeBase
        {
            public PMGMethodGenome() : base(10)
            {
                CreateGenes();
            }

            public override Gene GenerateGene(int geneIndex)
            {
                // Create a gene
                List<int> _gene;

                float rand = RandomizationProvider.Current.GetFloat();

                float probSum = 0;

                // Use probabilities in PMGGenomeConfigurations to add some
                // key to the "top" of the genome, i.e. first item
                if( rand < (probSum += MethodProb_Key))
                    _gene.Add((int) MethodGenomeEnums.MethodKey);
                else if( rand < (probSum += MethodProb_Timestep))
                    _gene.Add((int) MethodGenomeEnums.TimeStep);
                else if( rand < (probSum += MethodProb_ValFunc))
                    _gene.Add((int) MethodGenomeEnums.ValueFunc);
                else if( rand < (probSum += MethodProb.ChgFunc))
                    _gene.Add((int) MethodGenomeEnums.ChangeFunc);
                else
                    _gene.Add((in) MethodGenomeEnums.UtilFunc));


                //_gene.Add(    random up to max( num/anyFunc , num

            }

            public override IChromosome CreateNew()
            {
                return new PMGMethodGenome();
            }
        }


    }

}
