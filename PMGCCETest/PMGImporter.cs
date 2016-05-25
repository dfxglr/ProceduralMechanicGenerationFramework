using System;
using PMGF.PMGGameInstance;
using System.Linq;

namespace PMGF
{
	public class PMGImporter
	{
		public PMGGenomeSet GenomeSet;

		public PMGImporter ()
		{
		}

		public void ImportFromWebsite()
		{
			// Get serialized strings from website

			// Unserialize genome set
		}

		public PMGGenomeSet ImportFromFile(string filename)
		{
			PMGGenomeSet ret = new PMGGenomeSet();

			if (!System.IO.File.Exists (filename))
				return null;
			
			string[] lines = System.IO.File.ReadAllLines (filename);

			if (lines.Length != 4)
				return null;

			if (!ret.ImportSerializedGenomeSet (lines.ToList()))
				return null;

			return ret;
		}
	}
}

