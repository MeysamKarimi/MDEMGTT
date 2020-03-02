                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDEMGTT.Problems;
using NMF.Models;

namespace MDEMGTT
{
    public class NSGAII
    {
        public Model MetaModel { get; set; }
        private Dictionary<Model, int> modelFragmentVector;
        private List<Dictionary<Model, int>> population;
        int iteration;
        int objectivesCount;
        CRAProblem problem;
        public Dictionary<Dictionary<Model, int>, List<double>> FinalePareto;

        public NSGAII(List<Dictionary<Model, int>> population, Dictionary<Model, int> modelFragmentVector, int iteration)
        {
            this.population = population;
            this.modelFragmentVector = modelFragmentVector;
            this.iteration = iteration;
            problem = new CRAProblem(population, modelFragmentVector);
            objectivesCount = problem.GetObjectiveCount(); //badan bekhon az mas'alat
        }

        public List<Dictionary<Model, int>> Invoke()
        {
            for (int i = 0; i < iteration; i++)
            {
                //0- CreateOfSpring(); Q(T)
                List<Dictionary<Model, int>>  PtQT = CreateOfSpring(); //Q(T)
                //1- NonDomiatedSorting();
                //2- P(T) + Q(T) => P(T + 1)                
                //4- Pick N and set as new population P(T+1)
                population = NonDomiatedSorting(PtQT, false);                
            }

            return NonDomiatedSorting(population, true);
        }
        private List<Dictionary<Model, int>> CreateOfSpring()
        {
            List<Dictionary<Model, int>> PtQT = new List<Dictionary<Model, int>>();
            for (int i = 0; i < population.Count; i++)
            {
                PtQT.Add(population[i]);
            }
            int counter = population.Count - 1;
            for (int i = 0; i < population.Count / 2 - 1; i++)
            {
                int random1 = new Random().Next(0, counter);
                int random2 = new Random().Next(0, counter);

                while (random1 == random2)
                    random2 = new Random().Next(0, counter);
                
                var modelElemetVector1 = (population[random1] as Dictionary<Model, int>);
                var modelElemetVector2 = (population[random2] as Dictionary<Model, int>);

                Dictionary<Model, int> crossdOverModel = CorssOver(modelElemetVector1, modelElemetVector2);
                Dictionary<Model, int> mutantModel = Mutation(crossdOverModel);

                PtQT.Add(mutantModel);
                counter -= 2;
            }

            return PtQT;
        }

        private Dictionary<Model, int> Mutation(Dictionary<Model, int> crossdOverModel)
        {
            int mutantPoint = new Random().Next(0, crossdOverModel.Count - 1);
            Dictionary<Model, int> mutantModel = new Dictionary<Model, int>();
            mutantModel.Add(crossdOverModel.ElementAt(mutantPoint).Key, (crossdOverModel.ElementAt(mutantPoint).Value == 0) ? 1 : 0);            
            return crossdOverModel;
        }

        private Dictionary<Model, int> CorssOver(Dictionary<Model, int> modelElemetVector1, Dictionary<Model, int> modelElemetVector2)
        {
            int crossOverPoint = new Random().Next(0, modelElemetVector1.Count - 1);
            Dictionary<Model, int> CorssOver = new Dictionary<Model, int>();

            for (int i = 0; i < crossOverPoint; i++)            
                CorssOver.Add(modelElemetVector1.ElementAt(i).Key, modelElemetVector1.ElementAt(i).Value);            

            for (int i = crossOverPoint; i <= modelElemetVector1.Count - 1; i++)
                CorssOver.Add(modelElemetVector2.ElementAt(i).Key, modelElemetVector2.ElementAt(i).Value);

            return CorssOver;
        }

        private List<Dictionary<Model, int>> NonDomiatedSorting(List<Dictionary<Model, int>> PtQT, bool returnParetoFront)
        {
            //0- CalculateObjectives
            Dictionary<Dictionary<Model, int>, List<double>> objectivesValues = new Dictionary<Dictionary<Model, int>, List<double>>();
            // Key: currentModel. ke har ozv ye list az maghadire mokhtalege objectives ast. List[0].MAx_Coverage va List[0].Minimum            
            for (int rank = 0; rank < PtQT.Count; rank++)
            {
                var modelElemetVector = (PtQT[rank] as Dictionary<Model, int>);
                List<double> objectivesValuesOfCurrentIndividual = CalulateObjectives(modelElemetVector, PtQT, rank); //a List  objevive values: 1-MAx_Coverage 2-Minimum. 
                objectivesValues.Add(modelElemetVector, objectivesValuesOfCurrentIndividual);
            }
            //1- Sort by CD

            Dictionary<Dictionary<Model, int>, List<double>> cdValues = new Dictionary<Dictionary<Model, int>, List<double>>();
            Dictionary<Dictionary<Model, int>, double> finalCdValues = new Dictionary<Dictionary<Model, int>, double>();
            for (int j = 0; j < objectivesCount; j++)
            {
                //1-1 Sort by each objective (j)            
                var orderedItemsByJthObjective = from pair in objectivesValues
                                                 orderby pair.Value[j] ascending
                                                 select pair;

                //1-2 Calculate d(i) -> i=individual                 
                // d(ij) = (|f(i+1)(j) - f(i-1)(j)|) / (|f(1)j - f(n)j|)

                if (!cdValues.ContainsKey(orderedItemsByJthObjective.ElementAt(0).Key))
                    cdValues.Add(orderedItemsByJthObjective.ElementAt(0).Key, new List<double>());
                cdValues[orderedItemsByJthObjective.ElementAt(0).Key].Add(orderedItemsByJthObjective.ElementAt(0).Value[j]);
                if (!cdValues.ContainsKey(orderedItemsByJthObjective.ElementAt(orderedItemsByJthObjective.Count() - 1).Key))
                    cdValues.Add(orderedItemsByJthObjective.ElementAt(orderedItemsByJthObjective.Count() - 1).Key, new List<double>());
                cdValues[orderedItemsByJthObjective.ElementAt(orderedItemsByJthObjective.Count() - 1).Key].Add(orderedItemsByJthObjective.ElementAt(orderedItemsByJthObjective.Count() - 1).Value[j]);

                for (int i = 1; i < orderedItemsByJthObjective.Count() - 1; i++)
                {
                    double dij = Math.Abs(orderedItemsByJthObjective.ElementAt(i + 1).Value[j] - orderedItemsByJthObjective.ElementAt(i - 1).Value[j]) /
                        Math.Abs(orderedItemsByJthObjective.ElementAt(0).Value[j] - orderedItemsByJthObjective.ElementAt(orderedItemsByJthObjective.Count() - 1).Value[j]);

                    if (!cdValues.ContainsKey(orderedItemsByJthObjective.ElementAt(i).Key))
                        cdValues.Add(orderedItemsByJthObjective.ElementAt(i).Key, new List<double>());

                    cdValues[orderedItemsByJthObjective.ElementAt(i).Key].Add(double.IsNaN(dij) ? 0 : dij);
                }
            }
            // d(i) = d(i1) + d(i2). j=1,2            
            foreach (KeyValuePair<Dictionary<Model, int>, List<double>> item in cdValues)
            {
                double di = 0;
                for (int j = 0; j < objectivesCount; j++)
                {
                    di += (double.IsNaN(item.Value[j]) ? 0 : item.Value[j]);
                }
                if (!finalCdValues.ContainsKey(item.Key))
                    finalCdValues.Add(item.Key, di);
            }
            //2- Calculate and Sort by rank
            Dictionary<Dictionary<Model, int>, int> nonDominatedNQ = new Dictionary<Dictionary<Model, int>, int>();
            foreach (KeyValuePair<Dictionary<Model, int>, List<double>> currentModel in objectivesValues)
            {
                var otherIndividualExceptCurretModel = objectivesValues.Where(x => x.Key != currentModel.Key);
                if (!nonDominatedNQ.Keys.Contains(currentModel.Key))
                {
                    nonDominatedNQ.Add(currentModel.Key, 0);
                    foreach (var otherModel in otherIndividualExceptCurretModel)
                    {
                        bool nqShouldbePlused = false;
                        //for (int j = 0; j < objectivesCount; j++)
                        {
                            if ((currentModel.Value[0] > otherModel.Value[0]) && (currentModel.Value[1] > otherModel.Value[1]))
                                nqShouldbePlused = true;
                        }
                        if (nqShouldbePlused)
                            nonDominatedNQ[currentModel.Key]++;
                    }
                }
            }

            var orderedItemsByRank = from pair in nonDominatedNQ
                                     orderby pair.Value ascending
                                     select pair;
            //3- pick the best by rank and CD
            int threshold = population.Count();
            int nq = 0;
            List<Dictionary<Model, int>> nextPopulation = new List<Dictionary<Model, int>>();

            if (returnParetoFront)
            {
                var finalRankedModels = orderedItemsByRank.Where(x => x.Value == nq);
                while (finalRankedModels == null)
                {
                    nq++;
                    finalRankedModels = orderedItemsByRank.Where(x => x.Value == nq);
                }

                nextPopulation.AddRange(finalRankedModels.ToDictionary(x => x.Key, x => x.Value).Keys);
                FinalePareto = new Dictionary<Dictionary<Model, int>, List<double>>();
                foreach (var item in nextPopulation)
                {
                    if (!FinalePareto.ContainsKey(item))
                    {
                        var temp = cdValues.Where(x => x.Key == item);                        
                        FinalePareto.Add(item, temp.FirstOrDefault().Value);
                    }
                       // = cdValues;
                }                
                return nextPopulation;
            }

            while (threshold > 0 && nq <= population.Count())
            {
                var currentRankedModels = orderedItemsByRank.Where(x => x.Value == nq);

                if (currentRankedModels == null || currentRankedModels.Count() == 0)
                {
                    nq++;
                    continue;
                }
                if (threshold > currentRankedModels.Count())
                {
                    //foreach (var item in currentRankedModels.ToDictionary(x => x.Key, x => x.Value).Keys)                    
                    //nextPopulation.Add(item);
                    nextPopulation.AddRange(currentRankedModels.ToDictionary(x => x.Key, x => x.Value).Keys);
                    threshold -= currentRankedModels.Count();
                }
                else
                {
                    Dictionary<Dictionary<Model, int>, int> cr = currentRankedModels.ToDictionary(x => x.Key, x => x.Value);
                    var xx =
                        finalCdValues.Where(x => cr.ContainsKey(x.Key)).OrderBy(x => x.Value).Take(threshold);
                    threshold = 0;
                    nextPopulation.AddRange(xx.ToDictionary(x => x.Key, x => x.Value).Keys);
                }
                nq++;
            }
            if (nextPopulation.Count < population.Count())
            {
                Random rnd = new Random();
                while(population.Count() != nextPopulation.Count)
                {
                    int randomRank = rnd.Next(population.Count());
                    if (!nextPopulation.Contains(population[randomRank]))
                    {
                        nextPopulation.Add(population[randomRank]);
                    }
                }
            }
            return nextPopulation;
        }

        private List<double> CalulateObjectives(Dictionary<Model, int> modelElemetVector, List<Dictionary<Model, int>> PtQt, int modelRank)
        {
            List<double> objectiveValues = new List<double>();
            //var problem = new CRAProblem(population, modelFragmentVector);
            //int objectivesCount = problem.GetObjectiveCount(); //badan bekhon az mas'alat
            for (int objectiveRank = 0; objectiveRank < objectivesCount; objectiveRank++)            
               objectiveValues.Add(problem.CalcObjective(modelElemetVector, objectiveRank, PtQt, modelRank));            
            return objectiveValues;
        }
        
        private double MaximizeMMCoverage(int rank)
        {
            var modelElemetVector = (population[rank] as Dictionary<Model, int>);
            List<NMF.Models.Meta.Type> myFinalModelTypes = new List<NMF.Models.Meta.Type>();
            foreach (var fragment in modelElemetVector.Keys)
            {
                if (modelElemetVector[fragment] == 0) // fragment is not selected
                    continue;
                var fragmentClassModel = fragment.RootElements[0] as NMF.Models.Meta.Namespace;  // for trace purpose only                
                foreach (var item in fragmentClassModel.Types)
                {
                    //if (!myFinalModelTypes.Contains(item))
                    if (!myFinalModelTypes.Any(x => x.Name == item.Name))
                        myFinalModelTypes.Add((NMF.Models.Meta.Type)item);
                }
            }
            var repository = new NMF.Models.Repository.ModelRepository();
            var metaModel = repository.Resolve("BibTeX.nmf");
            //foreach (var reference in ((NMF.Models.Meta.Namespace)metaModel.RootElements[0]).ReferencedElements)                
            List<NMF.Models.Meta.Type> tempModelTypesTobeRemoved = new List<NMF.Models.Meta.Type>();
            foreach (NMF.Models.Meta.Type type in myFinalModelTypes)
            {
                if (((NMF.Models.Meta.ReferenceType)type).References.Count() == 0)
                    continue;
                foreach (var reference in ((NMF.Models.Meta.ReferenceType)type).References)
                {
                    if (!myFinalModelTypes.Any(x => x.Name == reference.ReferenceType.Name))
                        tempModelTypesTobeRemoved.Add(type);
                }
            }
            myFinalModelTypes = myFinalModelTypes.Except(tempModelTypesTobeRemoved).ToList();

            if (myFinalModelTypes.Count == 0)
                return 1000; //so bad in minization objective. Igonre it
            return 1 / myFinalModelTypes.Count; //because of minimization
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              