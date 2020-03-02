using MDEMGTT.ArchitectureCRA;
using MDEMGTT.Fragments;
using MDEMGTT.Problems;
using MOEA.AlgorithmModels;
using MOEA.ComponentModels;
using MOEA.ComponentModels.SolutionModels;
using Newtonsoft.Json;
using NMF.Models;
using NMF.Models.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MDEMGTT
{
    class Program
    {        
        public static void Main(string[] args)
        {
            
            #region variables
            int nPopulation = 100;            
            //string fragmentsDefinitionPath = "Fragments.txt";
            //Dictionary<int, IFragmentElemet> VectorTemplate = new Dictionary<int, IFragmentElemet>(); //string shows fragmentElement and int shows its location in population
            //Dictionary<int, Dictionary<int, IFragmentElemet>> ModelFragmentVector = new Dictionary<int, Dictionary<int, IFragmentElemet>>(); //string shows fragmentElement and int shows its location in population
            Dictionary<Model, int> ModelFragmentVector = new Dictionary<Model, int>();
            //List<int[]> population = new List<int[]>(); //each bool shows if a fragment is selected or not
            List<Dictionary<Model, int>> population = new List<Dictionary<Model, int>>();
            System.Random rnd = new System.Random();
            #endregion

            #region work with model
            var repository = new ModelRepository();
            //var model = repository.Resolve("ClassModel.xmi");

            //var model_a = repository.Resolve("a.xmi");
            //var classModel_a = model_a.RootElements[0] as ClassModel;

            var model_b = repository.Resolve("b.xmi");
            var classModel_b = model_b.RootElements[0] as ClassModel;

            //var model_c = repository.Resolve("c.xmi");
            //var classModel_c = model_c.RootElements[0] as ClassModel;

            //var model_d = repository.Resolve("d.xmi");
            //var classModel_d = model_d.RootElements[0] as ClassModel;

            //var model_e = repository.Resolve("e.xmi");
            //var classModel_e = model_e.RootElements[0] as ClassModel;

            //step 1: Read All Fragments
            DirectoryInfo d = new DirectoryInfo("Inputs");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.xmi");
            foreach (FileInfo file in Files)
            {
                //step2:Make solution space based on loaded Fragments                
                try
                {
                    var fragmentModel = repository.Resolve(file.FullName);
                    var fragmentClassModel = fragmentModel.RootElements[0] as ClassModel;  // for trace purpose only                
                    ModelFragmentVector.Add(fragmentModel, 0); //temp
                }
                catch (Exception ex)
                { }                
            }
            //step2:Make solution space based on loaded Fragments            
            for (int i = 0; i < nPopulation; i++)
            {
                //int[] solution = new int[ModelFragmentVector.Count];
                //for (int j = 0; j < ModelFragmentVector.Count; j++)
                //    solution[j] = rnd.NextDouble() >= 0.5 ? 1 : 0;
                //population.Add(solution);

                //clone as ModelFragmentVector
                Dictionary<Model, int> solution = new Dictionary<Model, int>();
                foreach (KeyValuePair<Model, int> entry in ModelFragmentVector)                
                    solution.Add(entry.Key, rnd.NextDouble() >= 0.5 ? 1 : 0);
                //add to population
                population.Add(solution);
            }
            //step3:  write objectives
            //step4: run the algorithm
            #endregion work with model

            #region work with meta-model

            #region create vector template

            //Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "MDEMGTT.ArchitectureCRA");
            //for (int i = 0; i < typelist.Length; i++)
            //    if (!typelist[i].GetTypeInfo().IsInterface)
            //        VectorTemplate.Add(typelist[i].Name, false);

            #endregion make solution space from template

            #endregion work with meta-model

            Console.WriteLine("Model Fragments Extracted!");

            #region algrorithm play

            //NSGAII<ContinuousVector> algorithm = new NSGAII<ContinuousVector>(new CRAProblem(population, ModelFragmentVector));
            Algorithms.NSGAII algorithm = new Algorithms.NSGAII(new CRAProblem(population, ModelFragmentVector));

            algorithm.PopulationSize = 100;

            algorithm.Initialize();

            while (!algorithm.IsTerminated)
            {
                algorithm.Evolve();
                Console.WriteLine("Current Generation: {0}", algorithm.CurrentGeneration);
                Console.WriteLine("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            }

            
            ContinuousVector finalSolution = algorithm.GlobalBestSolution;
            NondominatedPopulation<ContinuousVector> paretoFront = algorithm.NondominatedArchive;
            Console.WriteLine(finalSolution.CrowdingDistance);
            Console.WriteLine(paretoFront.Count);
            Console.ReadKey();
            #endregion algorithm play
        }

        private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return
              assembly.GetTypes()
                      .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .ToArray();
        }
    }
}
