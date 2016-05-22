using System;
using System.IO;
using System.Collections.Generic;
using GeneticSharp.Domain.Chromosomes;
using PMGF.PMGGameInstance;

namespace PMGF
{
	public class PMGExporter
	{
		public PMGExporter ()
		{
		}

		public void ExportSetToFile(List<IChromosome> ChromosomeSet, string filename)
		{
			ExportSetToFile (new PMGGenomeSet (ChromosomeSet), filename);
		}

		public void ExportSetToFile(PMGGenomeSet GenomeSet, string filename)
		{
			using (StreamWriter sw = new StreamWriter (filename, false)) {
				foreach (string s in GenomeSet.ExportSerializedGenomeSet())
					sw.WriteLine (s);
			}

		}

//		public void ExportSetToDB(PMGGenomeSet GenomeSet)
//		{
//		}
	}
}

