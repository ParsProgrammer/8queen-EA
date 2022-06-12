using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{

    class Individual
    {
        #region properties
        public List<int> Chromosome { get; set; }
        public int Fitness { get; set; }
        #endregion
        #region methods
        public void SetChromosome()
        {
            //randomly
            Chromosome  = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
            new Random().Shuffle(Chromosome);
         
        }
        public void SetFitness()
        {
            Fitness = 0;
            for (int i = 0; i < Chromosome.Count; i++)
                for (int j = 0; j < Chromosome.Count; j++)
                {
                    if (i!=j && Math.Abs(i - j) == Math.Abs(Chromosome[i] - Chromosome[j]))
                        Fitness ++;
                }
        }
        #endregion
    }

    class EvolutionaryAlgorithm
    {
        #region properties
        private int Size;
        public List<Individual> Population;
        public List<Individual> SelectedParents;
        public List<Individual> OffSprings;
        
        #endregion
        #region methods
        public EvolutionaryAlgorithm(int _size)
        {
            Size = _size;
        }
        public void Initialization()
        {
            Population = new List<Individual>();
            for (int i = 0; i < Size; i++)
            {
                var ind = new Individual();
                ind.SetChromosome();
                ind.SetFitness();
                Population.Add(ind);
            }
        }
        public void ParentSelection(double rate)
        {
            var rng = new Random();
            rng.Shuffle(Population);
            SelectedParents = Population.Take((int)(Size * rate)).ToList();

        }
        public void SurvivorSelection()
        {
            Population = Population.Concat(OffSprings).OrderBy(p => p.Fitness).Take(100).ToList();

        }
        public void Mutation(double MutationRate)
        {
            for(int i=0;i<OffSprings.Count;i++)
            {
                if(MutationRate<new Random().NextDouble())
                {
                    int FirstIndex = new Random().Next(0, 7);
                    int SecoundIndex = new Random().Next(0, 7);
                    while (FirstIndex == SecoundIndex)
                    {
                        SecoundIndex = new Random().Next(0, 7);
                    }

                    var temp = OffSprings[i].Chromosome[FirstIndex];
                    OffSprings[i].Chromosome[FirstIndex] = OffSprings[i].Chromosome[SecoundIndex];
                    OffSprings[i].Chromosome[SecoundIndex] = temp;
                    OffSprings[i].SetFitness();
                }
            }
        }
        public void Recombination()
        {
            OffSprings = new List<Individual>();
            for (int i=0; i+1 < SelectedParents.Count ;i=i+2)
            {
                OffSprings.AddRange(CrossOver(SelectedParents[i], SelectedParents[i + 1]));
            }
        }
        public List<Individual> CrossOver(Individual A,Individual B)
        {
            Random rnd = new Random();
            Individual C = new Individual();
            Individual D = new Individual();
            int CrossOverPoint = rnd.Next(1, 7);
            C.Chromosome = A.Chromosome.Take(CrossOverPoint).ToList();
            D.Chromosome = B.Chromosome.Take(CrossOverPoint).ToList();
            for (int i= CrossOverPoint; i<8 ;i++)
            {             
                if (! C.Chromosome.Contains(B.Chromosome[i]))
                {
                    C.Chromosome.Add(B.Chromosome[i]);
                }
                if (!D.Chromosome.Contains(A.Chromosome[i]))
                {
                    D.Chromosome.Add(A.Chromosome[i]);
                }
            }
            for (int i = 0; i < CrossOverPoint; i++)
            {
                if (!C.Chromosome.Contains(B.Chromosome[i]))
                {
                    C.Chromosome.Add(B.Chromosome[i]);
                }
                if (!D.Chromosome.Contains(A.Chromosome[i]))
                {
                    D.Chromosome.Add(A.Chromosome[i]);
                }
            }      
            C.SetFitness();  
            D.SetFitness();
            return new List<Individual>() {C,D };        
        }
        public void PrintMyPopulation()
        {
            for (int i = 0; i < Size; i++)
            {
                  Console.WriteLine(" Individual("+i+") : "+
                                    Population[i].Chromosome[0]+" "+
                                    Population[i].Chromosome[1] + " "+
                                    Population[i].Chromosome[2] + " "+
                                    Population[i].Chromosome[3] + " "+
                                    Population[i].Chromosome[4] + " "+
                                    Population[i].Chromosome[5] + " "+
                                    Population[i].Chromosome[6] + " "+
                                    Population[i].Chromosome[7] + 
                                    "      fitness=" + Population[i].Fitness);
            }
        }
        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initialization==> ");
            EvolutionaryAlgorithm MyEA = new EvolutionaryAlgorithm(100);      
            MyEA.Initialization();
            MyEA.PrintMyPopulation();
            int i = 1;
            for (; !MyEA.Population.Any(p=>p.Fitness==0);i++)
            {
                Console.WriteLine("Iteration==> "+i);

                MyEA.ParentSelection(0.4);
                MyEA.Recombination();
                MyEA.Mutation(0.8);
                MyEA.SurvivorSelection();
                MyEA.PrintMyPopulation();
            }
            Console.WriteLine("\n\n***************************** Algorithm Succeeded ******************************\n\n  Solutions :");
            MyEA.Population.Where(p => p.Fitness == 0).ToList()
                .ForEach(o =>Console.WriteLine("\t\tchromosome="+
                        o.Chromosome[0] + " " +
                        o.Chromosome[1] + " " +
                        o.Chromosome[2] + " " +
                        o.Chromosome[3] + " " +
                        o.Chromosome[4] + " " +
                        o.Chromosome[5] + " " +
                        o.Chromosome[6] + " " +
                        o.Chromosome[7] +
                    "\tfitness=" + o.Fitness+"\tIteration="+i
                    ));
            Console.WriteLine("\n\n******************************************************************************"); Console.ReadLine();
        }
    }

    static class RandomExtensions
    {
        public static void Shuffle<T>(this Random rng, List<T> array)
        {
            int n = array.Count;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

    }
}