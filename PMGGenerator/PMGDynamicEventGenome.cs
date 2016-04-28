using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;


namespace PMGF
{
    namespace PMGGenerator
    {


        public class PMGDynamicEventGenome : ChromosomeBase
        {
            public PMGDynamicEventGenome() : base(10)
            {
                CreateGenes();
            }

            public override Gene GenerateGene(int geneIndex)
            {
                // Create a gene
                List<int> _gene;

                float rand = RandomizationProvider.Current.GetFloat();

                float probSum = 0f;

                if( rand < (probSum += EventProb_Key))
                    _gene.Add((int)EventGenomeEnum.EventKey);
                else if (rand < (probSum += EventProb_UtilFunc))
                    _gene.Add((int)EventGenomeEnum.UtilFunc);
                else if( rand < (probSum += EventProb_ValFunc))
                    _gene.Add((int)EventGenomeEnum.ValueFunc);
                else
                    _gene.Add((int)EventGenomeEnum.CondFunc);


                //_gene.Add (    random to max possible 
                //                  lol. RANDOM TO THE MAX

                return new Gene(_gene);
            }

            public override IChromosome CreateNew()
            {
                return new PMGDynamicEventGenome();
            }
        }


    }

}
