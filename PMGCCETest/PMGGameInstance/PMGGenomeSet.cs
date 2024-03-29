﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMGF.PMGGenerator;
using GeneticSharp.Domain.Chromosomes;

namespace PMGF
{
    namespace PMGGameInstance
    { 
        public class PMGGenomeSet
        {
            //actors
            public List<int> actorGenome = new List<int>();
            public List<int> actorPositionsGenome = new List<int>();
            //events
            public List<List<int>> eventGenome = new List<List<int>>();
            //methods
            public List<List<int>> methodGenome = new List<List<int>>();

            public PMGGenomeSet()
            {
			}
			public PMGGenomeSet(List<IChromosome> ChromosomeSet)
			{
				ChromosomeSetToGenomeSet (ChromosomeSet);
			}
			public PMGGenomeSet(PMGGenomeSet GenomeSet)
			{
				Clear ();
				actorGenome = new List<int> (GenomeSet.actorGenome);
				actorPositionsGenome = new List<int> (GenomeSet.actorPositionsGenome);

				foreach (List<int> eSet in GenomeSet.eventGenome)
					eventGenome.Add (eSet);

				foreach (List<int> mSet in GenomeSet.methodGenome)
					methodGenome.Add (mSet);
			}

			public void ChromosomeSetToGenomeSet(List<IChromosome> ChromosomeSet)
			{				
				Clear ();


				foreach (Gene g in ChromosomeSet[0].GetGenes())
				{
					actorGenome.Add ((int)g.Value);
				}
				foreach (Gene g in ChromosomeSet[1].GetGenes())
				{
					actorPositionsGenome.Add ((int)g.Value);
				}
				foreach (Gene g in ChromosomeSet[2].GetGenes())
				{
					eventGenome.Add (g.Value as List<int>);
				}
				foreach (Gene g in ChromosomeSet[3].GetGenes())
				{
					methodGenome.Add (g.Value as List<int>);
				}

			}

			public bool ImportSerializedGenomeSet(List<string> SerializedSet)
			{
				Clear ();

				if (SerializedSet.Count != 4)
					return false;

				actorGenome = SerializedSet [0].Split (';').Select(int.Parse).ToList ();
				actorPositionsGenome = SerializedSet [1].Split (';').Select(int.Parse).ToList ();

				foreach (string s in SerializedSet[2].Split(';')) {
					eventGenome.Add(new List<int>(s.Split(',').ToList().Select(int.Parse).ToList()));
				}

				foreach (string s in SerializedSet[3].Split(';')) {
					methodGenome.Add(new List<int>(s.Split(',').ToList().Select(int.Parse).ToList()));
				}

				return true;
			}

			public List<string> ExportSerializedGenomeSet()
			{
				List<string> resultSet = new List<string> ();

				// a;b;c;d
				resultSet.Add(string.Join(";", actorGenome.ToArray()));

				resultSet.Add (string.Join (";", actorPositionsGenome.ToArray ()));

				// a,b;a,b;a,b
				List<string> eventGenes = new List<string>();

				foreach (List<int> g in eventGenome)
					eventGenes.Add (string.Join (",", g.ToArray ()));
				
				resultSet.Add (string.Join (";", eventGenes.ToArray ()));

				List<string> methodGenes = new List<string>();

				foreach (List<int> g in methodGenome)
					methodGenes.Add (string.Join (",", g.ToArray ()));

				resultSet.Add (string.Join (";", methodGenes.ToArray ()));


				return resultSet;
			}

			protected void Clear()
			{
				//actors
				actorGenome = new List<int>();
				actorPositionsGenome = new List<int>();
				//events
				eventGenome = new List<List<int>>();
				//methods
				methodGenome = new List<List<int>>();
			}
        }
    }
}
