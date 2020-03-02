using MDEMGTT.ArchitectureCRA;
using MDEMGTT.Fragments;
using MOEA.ComponentModels.SolutionModels;
using MOEA.Core.ComponentModels;
using MOEA.Core.ProblemModels;
using NMF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDEMGTT.Problems
{
    public class CRAProblem : IMOOProblem
    {        

        private Dictionary<Model, int> modelFragmentVector;
        private List<Dictionary<Model, int>> population;
        private int[][] DistanceBetweenModels_jaggedArray;

        public CRAProblem(List<Dictionary<Model, int>> population, Dictionary<Model, int> modelFragmentVector)
        {
            this.population = population;
            this.modelFragmentVector = modelFragmentVector;
            DistanceBetweenModels_jaggedArray = new int[population.Count * 2][];
        }

        public int GetObjectiveCount()
        {
            return 2;
        }

        public int GetDimensionCount()
        {
            return 2;
        }

        public bool IsFeasible(MOOSolution s)
        {
            return true;
        }

        public bool IsMaximizing()
        {            
            return false;
        }

        public double CalcObjective(MOOSolution s, int objective_index)
        {
            ContinuousVector x = (ContinuousVector)s;

            double f1 = 1;
            double f2 = 1;

            if (objective_index == 0)
            {
                //f1 = CalculateCoupling(s.Rank);
                f1 = MinimizeNoOfTestCases(s.Rank);                
                return f1;
            }
            else
            {
                //f2 = CalculateCohesion(s.Rank);                
                f2 = MaximizeMMCoverage(s.Rank);
                return f2;
            }
        }

        private double MinimizeNoOfTestCases(int rank)
        {
            return 1;
        }

        private double MaximizeMMCoverage(int rank)
        {            
            var modelElemetVector = (population[rank] as Dictionary<Model, int>);
            List< NMF.Models.Meta.Type> myFinalModelTypes = new List<NMF.Models.Meta.Type>();
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

        private double MaximizeMMCoverage(Dictionary<Model, int> modelElemetVector)
        {            
            List<NMF.Models.Meta.Type> myFinalModelTypes = new List<NMF.Models.Meta.Type>();
            foreach (var fragment in modelElemetVector.Keys)
            {
                if (modelElemetVector[fragment] == 0 || fragment.RootElements.Count == 0) // fragment is not selected
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
            return (double)1 / myFinalModelTypes.Count; //because of minimization
        }

        internal double CalcObjective(Dictionary<Model, int> modelElemetVector, int objectiveRank, List<Dictionary<Model, int>> PtQt, int modelRankInPopulation)
        {
            if (objectiveRank == 0)
                return MaximizeMMCoverage(modelElemetVector);
            else if (objectiveRank == 1)
                return MaximizeDistance(modelElemetVector,PtQt, modelRankInPopulation);
            else
                return 1;
        }


        private double MaximizeDistance(Dictionary<Model, int> modelElemetVector, List<Dictionary<Model, int>> PtQt, int modelRankInPopulation)
        {
            DistanceBetweenModels_jaggedArray[modelRankInPopulation] = new int[PtQt.Count - modelRankInPopulation - 1];
            int j = 0;
            for (int i = modelRankInPopulation + 1; i < PtQt.Count; i++)
            {                
                DistanceBetweenModels_jaggedArray[modelRankInPopulation][j++] = CompareTwoModels(modelElemetVector, PtQt[i]);                
            }
            double myDistance = 0;
            for (int i = 0; i < PtQt.Count - modelRankInPopulation - 1; i++)
            {                 
                if (i < modelRankInPopulation)
                    myDistance += DistanceBetweenModels_jaggedArray[i][modelRankInPopulation];
                else
                    myDistance += DistanceBetweenModels_jaggedArray[modelRankInPopulation][i];
            }            
            return (double)1 / myDistance; //because of minimization
        }

        private int CompareTwoModels(Dictionary<Model, int> sourceModel, Dictionary<Model, int> destinationModel)
        {
            int differentValue = 0;
            foreach (var sourceFragment in sourceModel.Keys)
            {
                if (sourceModel[sourceFragment] == 0 || sourceFragment.RootElements.Count == 0) // fragment is not selected
                    continue;
                var sourceFragmentClassModel = sourceFragment.RootElements[0] as NMF.Models.Meta.Namespace;  // for trace purpose only                                
                foreach (var sourceType in sourceFragmentClassModel.Types)
                {
                    if (destinationModel[sourceFragment] == 0)
                    //foreach (var destinationFragment in destinationModel.Keys)                                        
                    {                        
                        differentValue += ((NMF.Models.Meta.Class)(sourceType)).BaseTypes.Count
                            + ((NMF.Models.Meta.Class)(sourceType)).BaseTypes.Count
                            + ((NMF.Models.Meta.Class)(sourceType)).Attributes.Count
                            + ((NMF.Models.Meta.Class)(sourceType)).References.Count;
                        continue;
                    }

                    //foreach (var destinationFragment in destinationModel.Keys)
                    //{
                    //    var destinationFragmentClassModel = destinationFragment.RootElements[0] as NMF.Models.Meta.Namespace;  // for trace purpose only                                
                    //    foreach (var type in sourceFragmentClassModel.Types)
                    //    { }
                    //}
                }
            }
            return differentValue;
        }

        private double CalculateCohesion(int rank)
        {
            int cohesion = 0;
            var modelElemetVector = (population[rank] as Dictionary<Model, int>);
            foreach (var fragment in modelElemetVector.Keys)
            {
                if (modelElemetVector[fragment] == 0) // fragment is not selected
                    continue;
                var fragmentClassModel = fragment.RootElements[0] as ClassModel;  // for trace purpose only                
                foreach (var item in fragmentClassModel.Features)
                {
                    if (item.Name.ToUpper().StartsWith("M")) //Method
                    {
                        cohesion += ((Method)item).DataDependency.Count + ((Method)item).FunctionalDependency.Count;
                    }
                }
            }
            if (cohesion == 0)
                return 2; //so bad in minization objective. Igonre it
            return 1 / cohesion; //because of minimization
        }

        private int LookForOwnAttributesAndMethods(Student item)
        {
            return 1;
        }

        private double CalculateCoupling(int rank)
        {
            
            int coupling = 0;
            var modelElemetVector = (population[rank] as Dictionary<Model, int>);
            foreach (var fragment in modelElemetVector.Keys)
            {
                if (modelElemetVector[fragment] == 0) // fragment is not selected
                    continue;
                var fragmentClassModel = fragment.RootElements[0] as ClassModel;  // for trace purpose only                
                foreach (var item in fragmentClassModel.Features)
                {
                    if (item.Name.ToUpper().StartsWith("M")) //Method
                    {
                        coupling += ((Method)item).DataDependency.Count + ((Method)item).FunctionalDependency.Count;
                    }
                    //else if (item.Name.ToUpper().StartsWith("A")) // Attribute
                    //{
                    //    coupling += ((Attribute)item).DataDependency.Count + ((Attribute)item).FunctionalDependency.Count;
                    //}
                }
            }
            return coupling;
        }

        private int LookForAttributesAndMethods(Student student, Dictionary<int, IFragmentElemet> fragmentElemets)
        {
            int coupling = 0;
            foreach (var item in fragmentElemets.Values)
            {
                switch (item.GetType().Name)
                {
                    case "Student": //mishe group
                        if (((MDEMGTT.Fragments.Student)item).studentId == student.studentId)
                            coupling++;
                        break;
                }
            }
            return coupling;
        }

        public double GetUpperBound(int dimension_index)
        {
            return 1;
        }

        public double GetLowerBound(int dimension_index)
        {
            return 0;
        }
    }
}