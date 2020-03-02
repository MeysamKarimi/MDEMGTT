using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MOEA.AlgorithmModels;
using MOEA.ComponentModels.SolutionModels;
using MOEA.ComponentModels;
using ZedGraph;
using MOEA.Benchmarks;

namespace MOEA.AnalyzerGUI
{
    using System.IO;
    using MOEA.Core.ProblemModels;
    using MOEA.Core.ComponentModels;
    using NMF.Models;
    using NMF.Models.Repository;
    using MDEMGTT.ArchitectureCRA;
    using MDEMGTT.Problems;
    using NMF.Models.Meta;
    using MDEMGTT;
    using Force.DeepCloner;
    using System.Threading.Tasks;
    using MDEMGTT.PetriNet;

    public partial class FrmAnalyzer : Form
    {
        public enum AlgorithmType
        {
            NSGAII,
            HybridGame,
            HAPMOEA,
            GDE3
        }

        public enum ProblemType
        {
            //CRA,
            //BibTeX,
            //CPL,
            //Class,
            //ecore,
            PetriNet,
            SimpleUML,
            HSM,
            Grafcet
            //NDND,
            //NGPD,
            //TNK,
            //OKA2,
            //SYMPART
        }

        public FrmAnalyzer()
        {
            InitializeComponent();
        }

        private void btnEvaluate_Click(object sender, EventArgs e)
        {
            AlgorithmType algorithm_type = (AlgorithmType)cboAlgorithm.SelectedItem;

            if (algorithm_type == AlgorithmType.NSGAII)
            {
                ProblemType problem_type = (ProblemType)cboProblem.SelectedItem;
                //if (problem_type == ProblemType.CRA)
                //    MeiiNSGAII();               
                //else if (problem_type == ProblemType.BibTeX)
                //    BibTexNSGAII<ContinuousVector>();
                ////BibTexNSGAII();
                //else if (problem_type == ProblemType.CPL)
                //    CPLNSGAII();
                //else if (problem_type == ProblemType.Class)
                //    ClassNSGAII();
                if (problem_type == ProblemType.PetriNet)
                    PetriNetNSGAII();
                else if (problem_type == ProblemType.HSM)
                    HSMNSGAII();
                else if (problem_type == ProblemType.SimpleUML)
                    SimpleUMLNSGAII();
                else if (problem_type == ProblemType.Grafcet)
                    GrafcetNSGAII();
                else
                    RunNSGAII<ContinuousVector>();

            }
            else if (algorithm_type == AlgorithmType.HybridGame)
            {
                RunHybridGame<ContinuousVector>();
            }
            else if (algorithm_type == AlgorithmType.HAPMOEA)
            {
                RunHAPMOEA<ContinuousVector>();
            }
            else if (algorithm_type == AlgorithmType.GDE3)
            {
                RunGDE3<ContinuousVector>();
            }
        }

        private void MeiiNSGAII()
        {
            //#region ...
            //File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"\de\z1.dll", "Output\\Output1.xmi", true);
            File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"\de\z2.dll", "Output\\Output1.xmi", true);
            //File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"\de\z3.dll", "Output\\Output3.xmi", true);
            //File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"\de\z4.dll", "Output\\Output4.xmi", true);
            //File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"\de\z5.dll", "Output\\Output5.xmi", true);
            //#endregion

            #region variables
            int nPopulation = 100;
            Dictionary<Model, int> ModelFragmentVector = new Dictionary<Model, int>();
            List<Dictionary<Model, int>> population = new List<Dictionary<Model, int>>();
            System.Random rnd = new System.Random();
            #endregion

            //#region work with model
            var repository = new ModelRepository();

            var model_b = repository.Resolve("b.xmi");
            var classModel_b = model_b.RootElements[0] as ClassModel;

            //var model_c = repository.Resolve("c.xmi");
            //var classModel_c = model_c.RootElements[0] as ClassModel;

            //var model_d = repository.Resolve("d.xmi");
            //var classModel_d = model_d.RootElements[0] as ClassModel;

            //var model_e = repository.Resolve("e.xmi");
            //var classModel_e = model_e.RootElements[0] as ClassModel;

            //step 0: Create Random Model Fragments from Metamodel           
            var metaModel = repository.Resolve("architectureCRA.nmf");

            if (metaModel.RootElements.Count < 0)
            {
                //hichi bargard
            }


            #region Types

            int typesCount = ((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModel.RootElements).Items[0])).Types.Count;
            //extract all availabe permutations from types of rootelement
            List<byte> tempPermutationsInt = new List<byte>();
            for (byte i = 1; i <= typesCount; i++)
                tempPermutationsInt.Add(i);
            List<List<byte>> mainPermutationList = GetCombination(tempPermutationsInt);

            List<Model> MMFragments = new List<Model>();
            List<Model> MFragments = new List<Model>();
            var allTypes = (((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModel.RootElements).Items[0])).Types);
            List<string> primitiveDataTypes = new List<string>();

            // loop over permutations and create a MM fragment based on permuration
            for (int i = 0; i < mainPermutationList.Count; i++)
            {
                Model myModel = new Model();
                #region rootElement
                NMF.Models.Meta.Namespace rootElement = new NMF.Models.Meta.Namespace();
                rootElement.Uri = metaModel.RootElements[0].AbsoluteUri;
                rootElement.Name = "MetaModelFragment" + i.ToString();
                //rootElement.IsFrozen = false;
                //rootElement.IsLocked = false;                    
                #endregion rootElement                    
                List<byte> current = mainPermutationList[i];
                for (int j = 0; j < mainPermutationList[i].Count; j++)
                {
                    NMF.Models.Meta.Class currentType = (NMF.Models.Meta.Class)allTypes.ElementAt(current[j] - 1); //jth elemet which is clarifyed by current permutation                        
                    if (currentType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                    {
                        for (int k = 0; k < currentType.BaseTypes.Count; k++)
                        {
                            //add its parent[k] to rootElement                                
                            bool shouldAddBase = true;
                            for (int z = 0; z < rootElement.Types.Count; z++)
                            {
                                if (rootElement.Types.ElementAt(z).Name == currentType.BaseTypes.ElementAt(k).Name)
                                    shouldAddBase = false;
                            }
                            if (shouldAddBase)
                                //rootElement.Types.Add(FastDeepCloner.DeepCloner.Clone(currentType.BaseTypes.ElementAt(k)));
                                rootElement.Types.Add(currentType.BaseTypes.ElementAt(k).DeepClone());

                        }
                    }
                    #region save Primitive data type Permurations
                    if (currentType.Attributes.Count > 0)
                    {
                        for (int k = 0; k < currentType.Attributes.Count; k++)
                        {
                            if (!primitiveDataTypes.Contains(currentType.Attributes.ElementAt(k).Type.Name.Trim()))
                                primitiveDataTypes.Add(currentType.Attributes.ElementAt(k).Type.Name.Trim());
                        }
                    }
                    #endregion
                    //var x = NClone.Clone.ObjectGraph(currentType);
                    //var x = currentType.DeepClone();
                    bool shouldAdd = true;
                    for (int z = 0; z < rootElement.Types.Count; z++)
                    {
                        if (rootElement.Types.ElementAt(z).Name == currentType.Name)
                            shouldAdd = false;
                    }
                    if (shouldAdd)
                        rootElement.Types.Add(currentType.DeepClone());
                }
                myModel.RootElements.Add(rootElement);
                MMFragments.Add(myModel);
            }

            #region Creat Model Fragments form MM-Fragments and Primitive data values
            for (int i = 0; i < MMFragments.Count; i++)
            {
                NMF.Models.Meta.Namespace currentMetaModelRootElement = (NMF.Models.Meta.Namespace)MMFragments[i].RootElements[0];
                var currentMMTypes = (((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(MMFragments[i].RootElements).Items[0])).Types);

                for (int j = 0; j < currentMMTypes.Count; j++) //NamedElement
                {
                    NMF.Models.Meta.Class currentModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(j); //jth elemet which is clarifyed by current permutation                        
                    for (int z = 0; z < currentModelType.Attributes.Count; z++) //string name
                    {
                        switch (currentModelType.Attributes.ElementAt(z).Type.Name.Trim().ToLower())
                        {
                            case "string":
                                Model myModel = new Model();
                                #region rootElement
                                NMF.Models.Meta.Namespace rootElement = new NMF.Models.Meta.Namespace();
                                rootElement.Uri = metaModel.RootElements[0].AbsoluteUri;
                                rootElement.Name = "ModelFragment" + j.ToString();
                                #endregion rootElement 
                                if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                {
                                    for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                    {
                                        //add its parent[k] to rootElement                                
                                        bool shouldAddBase = true;
                                        for (int y = 0; y < rootElement.Types.Count; y++)
                                        {
                                            if (rootElement.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                shouldAddBase = false;
                                        }
                                        if (shouldAddBase)
                                            rootElement.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());
                                        //rootElement.Types.Add(FastDeepCloner.DeepCloner.Clone(currentModelType.BaseTypes.ElementAt(k)));
                                    }
                                }
                                NMF.Models.Meta.Class newType = currentModelType.DeepClone();
                                newType.Attributes.ElementAt(z).DefaultValue = "random_" + newType.Attributes.ElementAt(z).Name;
                                bool shouldAdd = true;
                                for (int u = 0; u < rootElement.Types.Count; u++)
                                {
                                    if (rootElement.Types.ElementAt(u).Name == newType.Name)
                                        shouldAdd = false;
                                }
                                if (shouldAdd)
                                    rootElement.Types.Add(newType);
                                for (int l = 0; l < currentMMTypes.Count; l++)
                                {
                                    NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                    NMF.Models.Meta.Class newOtherType = otherModelType.DeepClone();
                                    shouldAdd = true;
                                    for (int u = 0; u < rootElement.Types.Count; u++)
                                    {
                                        if (rootElement.Types.ElementAt(u).Name == newOtherType.Name)
                                            shouldAdd = false;
                                    }
                                    if (shouldAdd)
                                        rootElement.Types.Add(newOtherType);
                                }
                                myModel.RootElements.Add(rootElement);
                                MFragments.Add(myModel);
                                break;
                            case "int":
                                for (int b = -1; b < 1; b++)
                                {
                                    Model myModell = new Model();
                                    #region rootElement
                                    NMF.Models.Meta.Namespace rootElementt = new NMF.Models.Meta.Namespace();
                                    rootElementt.Uri = metaModel.RootElements[0].AbsoluteUri;
                                    rootElementt.Name = "ModelFragment" + j.ToString();
                                    #endregion rootElement 
                                    if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                    {
                                        for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                        {
                                            //add its parent[k] to rootElement                                
                                            bool shouldAddBase = true;
                                            for (int y = 0; y < rootElementt.Types.Count; y++)
                                            {
                                                if (rootElementt.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                    shouldAddBase = false;
                                            }
                                            if (shouldAddBase)
                                                rootElementt.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());
                                        }
                                    }
                                    NMF.Models.Meta.Class newTypet = currentModelType.DeepClone();
                                    newTypet.Attributes.ElementAt(z).DefaultValue = b.ToString();//"random_" + newTypet.Attributes.ElementAt(z).Name;
                                    bool shouldAddt = true;
                                    for (int u = 0; u < rootElementt.Types.Count; u++)
                                    {
                                        if (rootElementt.Types.ElementAt(u).Name == newTypet.Name)
                                            shouldAddt = false;
                                    }
                                    if (shouldAddt)
                                        rootElementt.Types.Add(newTypet);
                                    for (int l = 0; l < currentMMTypes.Count; l++)
                                    {
                                        NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                        NMF.Models.Meta.Class newOtherType = otherModelType.DeepClone();
                                        shouldAddt = true;
                                        for (int u = 0; u < rootElementt.Types.Count; u++)
                                        {
                                            if (rootElementt.Types.ElementAt(u).Name == newOtherType.Name)
                                                shouldAddt = false;
                                        }
                                        if (shouldAddt)
                                            rootElementt.Types.Add(newOtherType);
                                    }
                                    myModell.RootElements.Add(rootElementt);
                                    MFragments.Add(myModell);
                                }
                                break;
                            case "bool":
                                for (int b = 0; b <= 1; b++)
                                {
                                    Model myModell = new Model();
                                    #region rootElement
                                    NMF.Models.Meta.Namespace rootElementt = new NMF.Models.Meta.Namespace();
                                    rootElementt.Uri = metaModel.RootElements[0].AbsoluteUri;
                                    rootElementt.Name = "ModelFragment" + j.ToString();
                                    #endregion rootElement 
                                    if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                    {
                                        for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                        {
                                            //add its parent[k] to rootElement                                
                                            bool shouldAddBase = true;
                                            for (int y = 0; y < rootElementt.Types.Count; y++)
                                            {
                                                if (rootElementt.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                    shouldAddBase = false;
                                            }
                                            if (shouldAddBase)
                                                rootElementt.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());
                                        }
                                    }
                                    NMF.Models.Meta.Class newTypet = currentModelType.DeepClone();
                                    newTypet.Attributes.ElementAt(z).DefaultValue = (b == 0) ? "false" : "true";//"random_" + newTypet.Attributes.ElementAt(z).Name;
                                    bool shouldAddt = true;
                                    for (int u = 0; u < rootElementt.Types.Count; u++)
                                    {
                                        if (rootElementt.Types.ElementAt(u).Name == newTypet.Name)
                                            shouldAddt = false;
                                    }
                                    if (shouldAddt)
                                        rootElementt.Types.Add(newTypet);
                                    for (int l = 0; l < currentMMTypes.Count; l++)
                                    {
                                        NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                        NMF.Models.Meta.Class newOtherType = otherModelType.DeepClone();
                                        shouldAddt = true;
                                        for (int u = 0; u < rootElementt.Types.Count; u++)
                                        {
                                            if (rootElementt.Types.ElementAt(u).Name == newOtherType.Name)
                                                shouldAddt = false;
                                        }
                                        if (shouldAddt)
                                            rootElementt.Types.Add(newOtherType);
                                    }
                                    myModell.RootElements.Add(rootElementt);
                                    MFragments.Add(myModell);
                                }
                                break;
                        }
                    }

                    #region save Primitive data type Permurations                                                                        
                }

            }
            #endregion

            //NMF.Models.Meta.Class[] mmTypes = new NMF.Models.Meta.Class[typesCount];
            //List<NMF.Models.Meta.IType> mmTypes = new List<NMF.Models.Meta.IType>();
            //foreach (var item in ((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModel.RootElements).Items[0])).Types)
            ////{
            ////    mmTypes.Add(item);
            ////}               

            #endregion
            foreach (var type in ((NMF.Models.Meta.Namespace)metaModel.RootElements[0]).Types)
            {
                // 1- if type has baseType : add to to MM-fragment
                MDEMGTT.ArchitectureCRA.Attribute attribute = new MDEMGTT.ArchitectureCRA.Attribute();
                attribute.Name = "aa";
                //model.SetAttributeValue()
                //repository.Models.Add()
                // 2- check for references
                // 2-1 iscontainment ==1 => 2 halat darad, ye seri mishan containements ke ovorde mishan, ye seri ham nayarim. all permutations
                // 2-2 other references => mese 2 halate bala
                // 3- check for attributes
                // 3-1 foreach attr -> make fragment for each type. include all permutations
                // 4- check for operations
                // 4-1 foreach operation -> make fragment for each method. include all permutations

            }

            foreach (var item in MFragments)
            {
                ModelFragmentVector.Add(item, 0); //temp

            }
            //Tree.RootElement = model;
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

            MDEMGTT.Algorithms.NSGAII algorithm = new MDEMGTT.Algorithms.NSGAII(new CRAProblem(population, ModelFragmentVector));

            algorithm.PopulationSize = 100;

            algorithm.Initialize();

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (s1, e1) =>
            {
                while (!algorithm.IsTerminated)
                {
                    algorithm.Evolve();
                    worker.ReportProgress(0);
                }
            };
            worker.ProgressChanged += (s1, e1) =>
            {
                txtStatus1.Text = string.Format("Current Generation: {0}", algorithm.CurrentGeneration);
                txtStatus2.Text = string.Format("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            };
            worker.RunWorkerCompleted += (s1, e1) =>
            {
                try
                {
                    ContinuousVector finalSolution = algorithm.GlobalBestSolution;
                    NondominatedPopulation<ContinuousVector> archive = algorithm.NondominatedArchive;
                    Dictionary<int, List<double>> nonDominated = WhoIsTheBest(algorithm.objectives);
                    string final = string.Empty;
                    //foreach (var item in nonDominated)
                    {
                        //Dictionary<Model, int> cur = ((Dictionary<Model, int>)population[item.Key]);
                        Dictionary<Model, int> cur = ((Dictionary<Model, int>)population[0]);
                        string fragments = string.Empty;
                        foreach (var model in cur.Keys)
                        {
                            fragments += model.AbsoluteUri + ";";
                        }
                        //final += $"solution: {item.Key}, fragmens: {fragments} {Environment.NewLine}" +
                        final += $"solution: {new Random().Next(0, 100)}, fragmens: {fragments} {Environment.NewLine}" +
                        $"--------------{Environment.NewLine}";
                    }
                    DisplayParetoFront(archive);
                    MessageBox.Show(final);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
            worker.RunWorkerAsync();

            #endregion algorithm play
        }

        private void CPLNSGAII()
        {
            #region variables
            int nPopulation = 100;
            Dictionary<Model, int> ModelFragmentVector = new Dictionary<Model, int>();
            List<Dictionary<Model, int>> population = new List<Dictionary<Model, int>>();
            System.Random rnd = new System.Random();
            #endregion

            //#region work with model
            var repository = new ModelRepository();
            //var xxx = repository.Resolve(@"D:\EMFtoCSP\MyProj\Model\MyCPL\ok1\result52_30.xmi");
            //repository.Save(xxx.RootElements[0], @"Output\GeneratedModels\Meii_generatedModel.nmf");
            //string[] allfiles = Directory.GetFiles(@"C:\Users\asus\Desktop\case studies\Grafcet\", "*.xmi", SearchOption.AllDirectories);
            //foreach (var filePath in allfiles)
            //{
            //    FileInfo file = new FileInfo(filePath);
            //    var model = repository.Resolve(file.FullName);                
            //    if (model.RootElements.Count == 1 && model.RootElements[0].ToString().Split(' ')[0] == "Grafcet")
            //        repository.Save(model.RootElements[0], @"D:\ok2\" + Path.GetFileName(filePath));
            //    continue;
            //    //var toRemove = model.RootElements.Where(x => x.ToString() != "CPLModel");
            //    var myModel = model.RootElements.Where(x => x.ToString() == "CPLModel").FirstOrDefault();

            //    foreach (var item in model.RootElements.ToList())
            //    {
            //        if(item.ToString() != "CPLModel")
            //        model.RootElements.Remove(item);                    
            //    }
            //    repository.Save(model.RootElements[0], filePath);
            //}
            //for (int i = 0; i < MMFragments.Count(); i++)            


            //var model_b = repository.Resolve("b.xmi");
            //var classModel_b = model_b.RootElements[0] as ClassModel;

            //var model_c = repository.Resolve("c.xmi");
            //var classModel_c = model_c.RootElements[0] as ClassModel;

            //var model_d = repository.Resolve("d.xmi");
            //var classModel_d = model_d.RootElements[0] as ClassModel;

            //var model_e = repository.Resolve("e.xmi");
            //var classModel_e = model_e.RootElements[0] as ClassModel;

            //step 0: Create Random Model Fragments from Metamodel           
            //var metaModel = repository.Resolve("BibTeX.nmf");
            var metaModel = repository.Resolve("PetriNet.nmf");
            //var emptyHsmModel = repository.Resolve("emptyPetrinetModel.xmi");
            //repository.Save(emptyHsmModel, "khaw.xmi");
            //var metaModel = repository.Resolve("CPL.nmf");            
            if (metaModel.RootElements.Count < 0)
            {
                //hichi bargard
            }


            #region Types

            int typesCount = ((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModel.RootElements).Items[0])).Types.Count;
            //extract all availabe permutations from types of rootelement
            List<byte> tempPermutationsInt = new List<byte>();
            for (byte i = 1; i <= typesCount; i++)
                tempPermutationsInt.Add(i);
            List<List<byte>> mainPermutationList = GetCombination(tempPermutationsInt);

            List<Model> MMFragments = new List<Model>();
            List<Model> MFragments = new List<Model>();
            var allTypes = (((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModel.RootElements).Items[0])).Types);
            // List<string> primitiveDataTypes = new List<string>();

            // loop over permutations and create a MM fragment based on permuration
            for (int i = 0; i < mainPermutationList.Count; i++)
            //Parallel.For(0, mainPermutationList.Count, i =>
            {
                Model myModel = new Model();
                #region rootElement
                NMF.Models.Meta.Namespace rootElement = new NMF.Models.Meta.Namespace();
                rootElement.Uri = metaModel.RootElements[0].AbsoluteUri;
                rootElement.Name = "MetaModelFragment" + i.ToString();
                //rootElement.IsFrozen = false;
                //rootElement.IsLocked = false;                    
                #endregion rootElement                    
                List<byte> current = mainPermutationList[i];
                if (mainPermutationList[i] != null)
                    for (int j = 0; j < mainPermutationList[i].Count; j++)
                    {
                        NMF.Models.Meta.Class currentType = (NMF.Models.Meta.Class)allTypes.ElementAt(current[j] - 1); //jth elemet which is clarifyed by current permutation                        
                        if (currentType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                        {
                            for (int k = 0; k < currentType.BaseTypes.Count; k++)
                            {
                                //add its parent[k] to rootElement                                
                                bool shouldAddBase = true;
                                for (int z = 0; z < rootElement.Types.Count; z++)
                                {
                                    if (rootElement.Types.ElementAt(z).Name == currentType.BaseTypes.ElementAt(k).Name)
                                        shouldAddBase = false;
                                }
                                try
                                {
                                    if (shouldAddBase)
                                    {
                                        var x = currentType.BaseTypes.ElementAt(k).DeepClone();
                                        //var x = FastDeepCloner.DeepCloner.Clone(currentType.BaseTypes.ElementAt(k));
                                        //var y = currentType.BaseTypes.ElementAt(k).DeepClone();
                                        rootElement.Types.Add(x);
                                        x = null;
                                    }
                                }
                                catch (Exception ex)
                                {
                                }

                            }
                        }
                        #region save Primitive data type Permurations
                        //if (currentType.Attributes.Count > 0)
                        //{
                        //    for (int k = 0; k < currentType.Attributes.Count; k++)
                        //    {
                        //        if (!primitiveDataTypes.Contains(currentType.Attributes.ElementAt(k).Type.Name.Trim()))
                        //            primitiveDataTypes.Add(currentType.Attributes.ElementAt(k).Type.Name.Trim());
                        //    }
                        //}
                        #endregion
                        //var x = NClone.Clone.ObjectGraph(currentType);
                        //var x = currentType.DeepClone();
                        bool shouldAdd = true;
                        for (int z = 0; z < rootElement.Types.Count; z++)
                        {
                            if (rootElement.Types.ElementAt(z).Name == currentType.Name)
                                shouldAdd = false;
                        }
                        try
                        {
                            if (shouldAdd)
                                rootElement.Types.Add(currentType.DeepClone());
                            //rootElement.Types.Add(FastDeepCloner.DeepCloner.Clone(currentType));
                        }
                        catch (Exception ex)
                        {
                        }
                        currentType = null;
                    }
                try
                {
                    //                    var zyz = rootElement.Types.Select(x => x.Name == "CPLModel");
                    //if (rootElement.Types.Select(x => x.Name == "CPLModel").Any())                    
                    myModel.RootElements.Add(rootElement);
                    repository.Save(myModel, @"Output\MMFragments\" + rootElement.Name + ".nmf");
                    //repository.Save(rootElement, @"Output\MMFragments\" + rootElement.Name + "faree.xmi");
                    //var m = repository.Resolve(@"Output\MMFragments\" + rootElement.Name + "asli.nmf");
                    //var mm = repository.Resolve(@"Output\MMFragments\" + rootElement.Name + "faree.nmf");
                    myModel.RootElements.Clear();
                    current.Clear();
                    myModel = null;
                    current = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    Application.DoEvents();
                }
                catch (Exception ex)
                { }
                //MMFragments.Add(myModel);  

            }//);

            #region Creat Model Fragments form MM-Fragments and Primitive data values
            DirectoryInfo d = new DirectoryInfo("Output\\MMFragments");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.nmf").OrderBy(fi => fi.Name).ToArray();

            Dictionary<string, List<string>> CreatedModelFragments = new Dictionary<string, List<string>>();
            while (CreatedModelFragments.Count < nPopulation)
            {
                //string currentMmFragmentName = "MetaModelFragment" + new Random().Next(Files.Length).ToString();
                string currentMmFragmentName = Files[new Random().Next(Files.Length)].FullName;
                if (!CreatedModelFragments.ContainsKey(currentMmFragmentName))
                    CreatedModelFragments.Add(currentMmFragmentName, new List<string>());
            }


            //DirectoryInfo d = new DirectoryInfo("Output\\MMFragments");//Assuming Test is your Folder
            //FileInfo[] Files = d.GetFiles("*.nmf").OrderBy(fi => fi.Name).ToArray();
            //foreach (FileInfo file in Files)
            //for (int cc = 0; cc < CreatedModelFragments.Count(); cc++)
            foreach (var MMFr in CreatedModelFragments.Keys)            
            {
                //var metaModelFragmentFile = repository.Resolve(file.FullName);
                var metaModelFragmentFile = repository.Resolve(MMFr);
                if (metaModelFragmentFile.RootElements.Count == 0)
                    continue;
                NMF.Models.Meta.Namespace currentMetaModelRootElement = (NMF.Models.Meta.Namespace)metaModelFragmentFile.RootElements[0];
                var currentMMTypes = (((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModelFragmentFile.RootElements).Items[0])).Types);

                #region rootElement
                NMF.Models.Meta.Namespace rootElement = new NMF.Models.Meta.Namespace();
                rootElement.Uri = metaModel.RootElements[0].AbsoluteUri;
                //rootElement.Name = "ModelFragment" + i.ToString();
                rootElement.Name = currentMetaModelRootElement.Name.Replace("Meta", "");
                Model myModel = new Model();
                #endregion rootElement 

                for (int j = 0; j < currentMMTypes.Count; j++) //NamedElement
                {
                    NMF.Models.Meta.Class currentModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(j); //jth elemet which is clarifyed by current permutation
                    if (currentModelType.Attributes.Count == 0)
                    {
                        NMF.Models.Meta.Class newType = currentModelType.DeepClone();
                        rootElement.Types.Add(newType);
                        newType = null;
                    }
                    for (int z = 0; z < currentModelType.Attributes.Count; z++) //string name
                    {
                        switch (currentModelType.Attributes.ElementAt(z).Type.Name.Trim().ToLower())
                        {
                            case "string":
                                //Model myModel = new Model();
                                //#region rootElement
                                //NMF.Models.Meta.Namespace rootElement = new NMF.Models.Meta.Namespace();
                                //rootElement.Uri = metaModel.RootElements[0].AbsoluteUri;
                                //rootElement.Name = "ModelFragment" + j.ToString();
                                //#endregion rootElement 
                                if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                {
                                    for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                    {
                                        //add its parent[k] to rootElement                                
                                        bool shouldAddBase = true;
                                        for (int y = 0; y < rootElement.Types.Count; y++)
                                        {
                                            if (rootElement.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                shouldAddBase = false;
                                        }
                                        if (shouldAddBase)
                                            rootElement.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());
                                    }
                                }
                                NMF.Models.Meta.Class newType;
                                try
                                {
                                    newType = currentModelType.DeepClone();
                                }
                                catch (Exception ex)
                                { continue; }
                                if (new Random().Next(0, 2) == 1)
                                    newType.Attributes.ElementAt(z).DefaultValue = "random_" + newType.Attributes.ElementAt(z).Name;
                                else
                                    newType.Attributes.ElementAt(z).DefaultValue = string.Empty;
                                bool shouldAdd = true;
                                for (int u = 0; u < rootElement.Types.Count; u++)
                                {
                                    if (rootElement.Types.ElementAt(u).Name == newType.Name)
                                        shouldAdd = false;
                                }
                                if (shouldAdd)
                                    rootElement.Types.Add(newType);
                                for (int l = 0; l < currentMMTypes.Count; l++)
                                {
                                    NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                    NMF.Models.Meta.Class newOtherType = null;
                                    try
                                    {
                                        newOtherType = otherModelType.DeepClone();
                                    }
                                    catch (Exception ex)
                                    { }
                                    shouldAdd = true;
                                    for (int u = 0; u < rootElement.Types.Count; u++)
                                    {
                                        if (rootElement.Types.ElementAt(u).Name == newOtherType.Name)
                                            shouldAdd = false;
                                    }
                                    if (shouldAdd)
                                        rootElement.Types.Add(newOtherType);

                                    newOtherType = null;
                                }
                                newType = null;
                                //myModel.RootElements.Add(rootElement);
                                //MFragments.Add(myModel);
                                break;
                            case "int":
                                for (int b = -1; b < 1; b++)
                                {
                                    //Model myModell = new Model();
                                    //#region rootElement
                                    //NMF.Models.Meta.Namespace rootElementt = new NMF.Models.Meta.Namespace();
                                    //rootElementt.Uri = metaModel.RootElements[0].AbsoluteUri;
                                    //rootElementt.Name = "ModelFragment" + j.ToString();
                                    //#endregion rootElement 
                                    if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                    {
                                        for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                        {
                                            //add its parent[k] to rootElement                                
                                            bool shouldAddBase = true;
                                            for (int y = 0; y < rootElement.Types.Count; y++)
                                            {
                                                if (rootElement.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                    shouldAddBase = false;
                                            }
                                            if (shouldAddBase)
                                                rootElement.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());
                                        }
                                    }
                                    NMF.Models.Meta.Class newTypet = currentModelType.DeepClone();
                                    newTypet.Attributes.ElementAt(z).DefaultValue = b.ToString();//"random_" + newTypet.Attributes.ElementAt(z).Name;
                                    bool shouldAddt = true;
                                    for (int u = 0; u < rootElement.Types.Count; u++)
                                    {
                                        if (rootElement.Types.ElementAt(u).Name == newTypet.Name)
                                            shouldAddt = false;
                                    }
                                    if (shouldAddt)
                                        rootElement.Types.Add(newTypet);
                                    for (int l = 0; l < currentMMTypes.Count; l++)
                                    {
                                        NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                        NMF.Models.Meta.Class newOtherType = otherModelType.DeepClone();
                                        shouldAddt = true;
                                        for (int u = 0; u < rootElement.Types.Count; u++)
                                        {
                                            if (rootElement.Types.ElementAt(u).Name == newOtherType.Name)
                                                shouldAddt = false;
                                        }
                                        if (shouldAddt)
                                            rootElement.Types.Add(newOtherType);

                                        newOtherType = null;
                                    }
                                    newType = null;
                                    //myModell.RootElements.Add(rootElement);
                                    //MFragments.Add(myModell);
                                }
                                break;
                            case "bool":
                                for (int b = 0; b <= 1; b++)
                                {
                                    //Model myModell = new Model();
                                    //#region rootElement
                                    //NMF.Models.Meta.Namespace rootElementt = new NMF.Models.Meta.Namespace();
                                    //rootElementt.Uri = metaModel.RootElements[0].AbsoluteUri;
                                    //rootElementt.Name = "ModelFragment" + j.ToString();
                                    //#endregion rootElement 
                                    if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                    {
                                        for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                        {
                                            //add its parent[k] to rootElement                                
                                            bool shouldAddBase = true;
                                            for (int y = 0; y < rootElement.Types.Count; y++)
                                            {
                                                if (rootElement.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                    shouldAddBase = false;
                                            }
                                            if (shouldAddBase)
                                            {
                                                //var x = FastDeepCloner.DeepCloner.Clone(currentModelType.BaseTypes.ElementAt(k));
                                                //rootElement.Types.Add(x);
                                                rootElement.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());
                                            }
                                        }
                                    }
                                    NMF.Models.Meta.Class newTypet = currentModelType.DeepClone();
                                    newTypet.Attributes.ElementAt(z).DefaultValue = (b == 0) ? "false" : "true";//"random_" + newTypet.Attributes.ElementAt(z).Name;
                                    bool shouldAddt = true;
                                    for (int u = 0; u < rootElement.Types.Count; u++)
                                    {
                                        if (rootElement.Types.ElementAt(u).Name == newTypet.Name)
                                            shouldAddt = false;
                                    }
                                    if (shouldAddt)
                                        rootElement.Types.Add(newTypet);
                                    for (int l = 0; l < currentMMTypes.Count; l++)
                                    {
                                        NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                        NMF.Models.Meta.Class newOtherType = otherModelType.DeepClone();
                                        shouldAddt = true;
                                        for (int u = 0; u < rootElement.Types.Count; u++)
                                        {
                                            if (rootElement.Types.ElementAt(u).Name == newOtherType.Name)
                                                shouldAddt = false;
                                        }
                                        if (shouldAddt)
                                            rootElement.Types.Add(newOtherType);

                                        newOtherType = null;
                                    }

                                    newTypet = null;
                                    //myModell.RootElements.Add(rootElement);
                                    //GC.Collect();
                                    //Application.DoEvents();
                                    //MFragments.Add(myModell);
                                }
                                break;
                        }
                    }
                    currentModelType = null;
                    #region save Primitive data type Permurations                                                                        
                }

                myModel.RootElements.Add(rootElement);
                repository.Save(myModel, @"Output\MFragments\" + rootElement.Name + ".nmf");
                if (!CreatedModelFragments[MMFr].Contains(rootElement.Name))
                    CreatedModelFragments[MMFr].Add(rootElement.Name);
                myModel.RootElements.Clear();
                currentMMTypes.Clear();
                myModel = null;
                currentMetaModelRootElement = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Application.DoEvents();
                //MFragments.Add(myModel);
            }
            #endregion

            //NMF.Models.Meta.Class[] mmTypes = new NMF.Models.Meta.Class[typesCount];
                //List<NMF.Models.Meta.IType> mmTypes = new List<NMF.Models.Meta.IType>();
            //foreach (var item in ((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModel.RootElements).Items[0])).Types)
            ////{
            ////    mmTypes.Add(item);
            ////}               

            #endregion
            //foreach (var type in ((NMF.Models.Meta.Namespace)metaModel.RootElements[0]).Types)
            //{
            //    // 1- if type has baseType : add to to MM-fragment
            //    MDEMGTT.ArchitectureCRA.Attribute attribute = new MDEMGTT.ArchitectureCRA.Attribute();
            //    attribute.Name = "aa";
            //    //model.SetAttributeValue()
            //    //repository.Models.Add()
            //    // 2- check for references
            //    // 2-1 iscontainment ==1 => 2 halat darad, ye seri mishan containements ke ovorde mishan, ye seri ham nayarim. all permutations
            //    // 2-2 other references => mese 2 halate bala
            //    // 3- check for attributes
            //    // 3-1 foreach attr -> make fragment for each type. include all permutations
            //    // 4- check for operations
            //    // 4-1 foreach operation -> make fragment for each method. include all permutations

            //}
            Files = null;
            d = null;
            DirectoryInfo dir = new DirectoryInfo("Output\\MFragments");//Assuming Test is your Folder
            FileInfo[] MFragmentsFiles = dir.GetFiles("*.nmf").OrderBy(fi => fi.Name).ToArray();
            //for (int i = 0; i < MMFragments.Count(); i++)
            foreach (FileInfo file in MFragmentsFiles)
            {
                var metaModelFragmentFile = repository.Resolve(file.FullName);
                ModelFragmentVector.Add(metaModelFragmentFile, 0); //temp                
            }
            //Tree.RootElement = model;
            //step 1: Read All Fragments            
            //DirectoryInfo d = new DirectoryInfo("Inputs");//Assuming Test is your Folder
            //FileInfo[] Files = d.GetFiles("*.xmi");
            //foreach (FileInfo file in Files)
            //{
            //    //step2:Make solution space based on loaded Fragments                
            //    try
            //    {
            //        var fragmentModel = repository.Resolve(file.FullName);
            //        var fragmentClassModel = fragmentModel.RootElements[0] as ClassModel;  // for trace purpose only                
            //        ModelFragmentVector.Add(fragmentModel, 0); //temp
            //    }
            //    catch (Exception ex)
            //    { }
            //}
            //step2:Make solution space based on loaded Fragments      
                      
                        
            int generatedFeasibleModelsCount = 0;
            while(generatedFeasibleModelsCount < nPopulation)
            {
                Dictionary<Model, int> solution = new Dictionary<Model, int>();
                foreach (KeyValuePair<Model, int> entry in ModelFragmentVector)
                    solution.Add(entry.Key, rnd.NextDouble() >= 0.5 ? 1 : 0);

                var emptyPetriNet = repository.Resolve("emptyPetrinetModel.xmi");
                foreach (KeyValuePair<Model, int> item in solution)
                {
                    if (item.Value == 0)
                        continue;
                    if (item.Key.RootElements == null || item.Key.RootElements.Count == 0)
                        continue;
                    var allCurrentTypes = (((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(item.Key.RootElements).Items[0])).Types);
                    var petriNetRoot = emptyPetriNet.RootElements[0] as PetriNet;
                    petriNetRoot.Name = Guid.NewGuid().ToString();
                    petriNetRoot.Location = Guid.NewGuid().ToString();
                    foreach (NMF.Models.Meta.IType classItem in allCurrentTypes)
                    {                         
                        switch (classItem.Name.ToString())
                        {
                            case "PlaceToTransition":
                                PlaceToTransition p2t = new PlaceToTransition();
                                p2t.Name = Guid.NewGuid().ToString();
                                p2t.Location = Guid.NewGuid().ToString();
                                p2t.Weight = new Random().Next(int.MinValue, int.MaxValue);                                
                                petriNetRoot.Arcs.Add(p2t);                                
                                break;
                            case "TransitionToPlace":
                                TransitionToPlace t2p = new TransitionToPlace();
                                t2p.Name = Guid.NewGuid().ToString();
                                t2p.Location = Guid.NewGuid().ToString();
                                t2p.Weight = new Random().Next(int.MinValue, int.MaxValue);
                                petriNetRoot.Arcs.Add(t2p);                                
                                break;
                            case "Place":
                                Place p = new Place();
                                p.Name = Guid.NewGuid().ToString();
                                p.Location = Guid.NewGuid().ToString();
                                petriNetRoot.Elements.Add(p);                                
                                break;
                            case "Transition":
                                Transition t = new Transition();
                                t.Name = Guid.NewGuid().ToString();
                                t.Location = Guid.NewGuid().ToString();
                                petriNetRoot.Elements.Add(t);
                                break;
                        }
                        
                    }
                    //check if model is feasible
                    bool isModelFeasible = false;
                    
                    //till here
                    if (isModelFeasible)
                    {
                        repository.Save(emptyPetriNet, @"D:\EMFtoCSP\MyProj\Model\PetriNet\Aha\MyFirstModel" + generatedFeasibleModelsCount.ToString() + ".xmi");
                        population.Add(solution);
                        nPopulation++;
                    }
                    GC.Collect();
                }
            }

            for (int i = 0; i < nPopulation; i++)
            {
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

            NSGAII nSGAII = new NSGAII(population, ModelFragmentVector, 100);
            List<Dictionary<Model, int>> res = nSGAII.Invoke();

            //foreach (Dictionary<Model, int> entry in res)    
            repository = new ModelRepository();
            for (int i = 0; i < res.Count; i++)
            {
                repository.Save(res[i].Keys.ElementAt(0), @"Output\GeneratedModels\generatedModel" + i.ToString() + ".xmi");
                //DisplayParetoFront(res[i].Keys.ElementAt(0), nSGAII.FinalePareto);
            }
            DisplayParetoFront(res, nSGAII.FinalePareto);
            return;

            MDEMGTT.Algorithms.NSGAII algorithm = new MDEMGTT.Algorithms.NSGAII(new CRAProblem(population, ModelFragmentVector));

            algorithm.PopulationSize = 100;

            algorithm.Initialize();

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (s1, e1) =>
            {
                while (!algorithm.IsTerminated)
                {
                    algorithm.Evolve();
                    worker.ReportProgress(0);
                }
            };
            worker.ProgressChanged += (s1, e1) =>
            {
                txtStatus1.Text = string.Format("Current Generation: {0}", algorithm.CurrentGeneration);
                txtStatus2.Text = string.Format("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            };
            worker.RunWorkerCompleted += (s1, e1) =>
            {
                try
                {
                    ContinuousVector finalSolution = algorithm.GlobalBestSolution;
                    NondominatedPopulation<ContinuousVector> archive = algorithm.NondominatedArchive;
                    Dictionary<int, List<double>> nonDominated = WhoIsTheBest(algorithm.objectives);
                    string final = string.Empty;
                    //foreach (var item in nonDominated)
                    {
                        //Dictionary<Model, int> cur = ((Dictionary<Model, int>)population[item.Key]);
                        Dictionary<Model, int> cur = ((Dictionary<Model, int>)population[0]);
                        string fragments = string.Empty;
                        foreach (var model in cur.Keys)
                        {
                            fragments += model.AbsoluteUri + ";";
                        }
                        //final += $"solution: {item.Key}, fragmens: {fragments} {Environment.NewLine}" +
                        final += $"solution: {new Random().Next(0, 100)}, fragmens: {fragments} {Environment.NewLine}" +
                        $"--------------{Environment.NewLine}";
                    }
                    DisplayParetoFront(archive);
                    MessageBox.Show(final);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
            worker.RunWorkerAsync();

            #endregion algorithm play
        }

        private void PetriNetNSGAII()
        {
            #region variables
            int nPopulation = 100;
            Dictionary<Model, int> ModelFragmentVector = new Dictionary<Model, int>();
            List<Dictionary<Model, int>> population = new List<Dictionary<Model, int>>();
            System.Random rnd = new System.Random();
            #endregion

            //#region work with model
            var repository = new ModelRepository();                        
            var metaModel = repository.Resolve("PetriNet.nmf");
            //var metaModel = repository.Resolve("CPL.nmf");            
            if (metaModel.RootElements.Count < 0)
            {
                //hichi bargard
            }

            #region Types

            int typesCount = ((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModel.RootElements).Items[0])).Types.Count;
            //extract all availabe permutations from types of rootelement
            List<byte> tempPermutationsInt = new List<byte>();
            for (byte i = 1; i <= typesCount; i++)
                tempPermutationsInt.Add(i);
            List<List<byte>> mainPermutationList = GetCombination(tempPermutationsInt);

            List<Model> MMFragments = new List<Model>();
            List<Model> MFragments = new List<Model>();
            var allTypes = (((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModel.RootElements).Items[0])).Types);
            // List<string> primitiveDataTypes = new List<string>();

            // loop over permutations and create a MM fragment based on permuration
            for (int i = 0; i < mainPermutationList.Count; i++)
            //Parallel.For(0, mainPermutationList.Count, i =>
            {
                Model myModel = new Model();
                #region rootElement
                NMF.Models.Meta.Namespace rootElement = new NMF.Models.Meta.Namespace();
                rootElement.Uri = metaModel.RootElements[0].AbsoluteUri;
                rootElement.Name = "MetaModelFragment" + i.ToString();
                //rootElement.IsFrozen = false;
                //rootElement.IsLocked = false;                    
                #endregion rootElement                    
                List<byte> current = mainPermutationList[i];
                if (mainPermutationList[i] != null)
                    for (int j = 0; j < mainPermutationList[i].Count; j++)
                    {
                        NMF.Models.Meta.Class currentType = (NMF.Models.Meta.Class)allTypes.ElementAt(current[j] - 1); //jth elemet which is clarifyed by current permutation                        
                        if (currentType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                        {
                            for (int k = 0; k < currentType.BaseTypes.Count; k++)
                            {
                                //add its parent[k] to rootElement                                
                                bool shouldAddBase = true;
                                for (int z = 0; z < rootElement.Types.Count; z++)
                                {
                                    if (rootElement.Types.ElementAt(z).Name == currentType.BaseTypes.ElementAt(k).Name)
                                        shouldAddBase = false;
                                }
                                try
                                {
                                    if (shouldAddBase)
                                    {
                                        var x = currentType.BaseTypes.ElementAt(k).DeepClone();
                                        //var x = FastDeepCloner.DeepCloner.Clone(currentType.BaseTypes.ElementAt(k));
                                        //var y = currentType.BaseTypes.ElementAt(k).DeepClone();
                                        rootElement.Types.Add(x);
                                        x = null;
                                    }
                                }
                                catch (Exception ex)
                                {
                                }

                            }
                        }
                        #region save Primitive data type Permurations
                        //if (currentType.Attributes.Count > 0)
                        //{
                        //    for (int k = 0; k < currentType.Attributes.Count; k++)
                        //    {
                        //        if (!primitiveDataTypes.Contains(currentType.Attributes.ElementAt(k).Type.Name.Trim()))
                        //            primitiveDataTypes.Add(currentType.Attributes.ElementAt(k).Type.Name.Trim());
                        //    }
                        //}
                        #endregion
                        //var x = NClone.Clone.ObjectGraph(currentType);
                        //var x = currentType.DeepClone();
                        bool shouldAdd = true;
                        for (int z = 0; z < rootElement.Types.Count; z++)
                        {
                            if (rootElement.Types.ElementAt(z).Name == currentType.Name)
                                shouldAdd = false;
                        }
                        try
                        {
                            if (shouldAdd)
                                rootElement.Types.Add(currentType.DeepClone());
                            //rootElement.Types.Add(FastDeepCloner.DeepCloner.Clone(currentType));
                        }
                        catch (Exception ex)
                        {
                        }
                        currentType = null;
                    }
                try
                {
                    var zyz = rootElement.Types.Select(x => x.Name == "PetriNet");
                    //if (rootElement.Types.Select(x => x.Name == "CPLModel").Any())
                    {
                        myModel.RootElements.Add(rootElement);
                        repository.Save(myModel, @"Output\MMFragments\" + rootElement.Name + ".nmf");
                        //repository.Save(rootElement, @"Output\MMFragments\" + rootElement.Name + ".xmi");
                        //var m = repository.Resolve(@"D:\MM\" + rootElement.Name + ".nmf");
                        myModel.RootElements.Clear();
                        current.Clear();
                        myModel = null;
                        current = null;
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        Application.DoEvents();
                    }
                }
                catch (Exception ex)
                { }
                //MMFragments.Add(myModel);  

            }//);

            #region Creat Model Fragments form MM-Fragments and Primitive data values
            DirectoryInfo d = new DirectoryInfo("Output\\MMFragments");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.nmf").OrderBy(fi => fi.Name).ToArray();

            Dictionary<string, List<string>> CreatedModelFragments = new Dictionary<string, List<string>>();
            while (CreatedModelFragments.Count < nPopulation)
            {
                //string currentMmFragmentName = "MetaModelFragment" + new Random().Next(Files.Length).ToString();
                string currentMmFragmentName = Files[new Random().Next(Files.Length)].FullName;
                if (!CreatedModelFragments.ContainsKey(currentMmFragmentName))
                    CreatedModelFragments.Add(currentMmFragmentName, new List<string>());
            }


            //DirectoryInfo d = new DirectoryInfo("Output\\MMFragments");//Assuming Test is your Folder
            //FileInfo[] Files = d.GetFiles("*.nmf").OrderBy(fi => fi.Name).ToArray();
            //foreach (FileInfo file in Files)
            //for (int cc = 0; cc < CreatedModelFragments.Count(); cc++)
            foreach (var MMFr in CreatedModelFragments.Keys)
            {
                //var metaModelFragmentFile = repository.Resolve(file.FullName);
                var metaModelFragmentFile = repository.Resolve(MMFr);
                if (metaModelFragmentFile.RootElements.Count == 0)
                    continue;
                NMF.Models.Meta.Namespace currentMetaModelRootElement = (NMF.Models.Meta.Namespace)metaModelFragmentFile.RootElements[0];
                var currentMMTypes = (((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModelFragmentFile.RootElements).Items[0])).Types);

                #region rootElement
                NMF.Models.Meta.Namespace rootElement = new NMF.Models.Meta.Namespace();
                rootElement.Uri = metaModel.RootElements[0].AbsoluteUri;
                //rootElement.Name = "ModelFragment" + i.ToString();
                rootElement.Name = currentMetaModelRootElement.Name.Replace("Meta", "");
                Model myModel = new Model();
                #endregion rootElement 

                for (int j = 0; j < currentMMTypes.Count; j++) //NamedElement
                {
                    NMF.Models.Meta.Class currentModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(j); //jth elemet which is clarifyed by current permutation
                    if (currentModelType.IsAbstract)
                        continue;

                    if (currentModelType.Attributes.Count == 0)
                    {                        
                        NMF.Models.Meta.Class newType = currentModelType.DeepClone();
                        rootElement.Types.Add(newType);
                        newType = null;
                    }
                    for (int z = 0; z < currentModelType.Attributes.Count; z++) //string name
                    {
                        switch (currentModelType.Attributes.ElementAt(z).Type.Name.Trim().ToLower())
                        {
                            case "string":
                                //Model myModel = new Model();
                                //#region rootElement
                                //NMF.Models.Meta.Namespace rootElement = new NMF.Models.Meta.Namespace();
                                //rootElement.Uri = metaModel.RootElements[0].AbsoluteUri;
                                //rootElement.Name = "ModelFragment" + j.ToString();
                                //#endregion rootElement 
                                if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                {
                                    for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                    {
                                        //add its parent[k] to rootElement                                
                                        bool shouldAddBase = true;
                                        for (int y = 0; y < rootElement.Types.Count; y++)
                                        {
                                            if (rootElement.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                shouldAddBase = false;
                                        }
                                        if (shouldAddBase)
                                            rootElement.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());
                                    }
                                }
                                NMF.Models.Meta.Class newType;
                                try
                                {
                                    newType = currentModelType.DeepClone();
                                }
                                catch (Exception ex)
                                { continue; }
                                if (new Random().Next(0, 2) == 1)
                                    newType.Attributes.ElementAt(z).DefaultValue = "random_" + newType.Attributes.ElementAt(z).Name;
                                else
                                    newType.Attributes.ElementAt(z).DefaultValue = string.Empty;
                                bool shouldAdd = true;
                                for (int u = 0; u < rootElement.Types.Count; u++)
                                {
                                    if (rootElement.Types.ElementAt(u).Name == newType.Name)
                                        shouldAdd = false;
                                }
                                if (shouldAdd)
                                    rootElement.Types.Add(newType);
                                for (int l = 0; l < currentMMTypes.Count; l++)
                                {
                                    NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                    NMF.Models.Meta.Class newOtherType = null;
                                    try
                                    {
                                        newOtherType = otherModelType.DeepClone();
                                    }
                                    catch (Exception ex)
                                    { }
                                    shouldAdd = true;
                                    for (int u = 0; u < rootElement.Types.Count; u++)
                                    {
                                        if (rootElement.Types.ElementAt(u).Name == newOtherType.Name)
                                            shouldAdd = false;
                                    }
                                    if (shouldAdd)
                                        rootElement.Types.Add(newOtherType);

                                    newOtherType = null;
                                }
                                newType = null;
                                //myModel.RootElements.Add(rootElement);
                                //MFragments.Add(myModel);
                                break;
                            case "int":
                                for (int b = -1; b < 1; b++)
                                {
                                    //Model myModell = new Model();
                                    //#region rootElement
                                    //NMF.Models.Meta.Namespace rootElementt = new NMF.Models.Meta.Namespace();
                                    //rootElementt.Uri = metaModel.RootElements[0].AbsoluteUri;
                                    //rootElementt.Name = "ModelFragment" + j.ToString();
                                    //#endregion rootElement 
                                    if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                    {
                                        for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                        {
                                            //add its parent[k] to rootElement                                
                                            bool shouldAddBase = true;
                                            for (int y = 0; y < rootElement.Types.Count; y++)
                                            {
                                                if (rootElement.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                    shouldAddBase = false;
                                            }
                                            if (shouldAddBase)
                                                rootElement.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());
                                        }
                                    }
                                    NMF.Models.Meta.Class newTypet = currentModelType.DeepClone();
                                    newTypet.Attributes.ElementAt(z).DefaultValue = b.ToString();//"random_" + newTypet.Attributes.ElementAt(z).Name;
                                    bool shouldAddt = true;
                                    for (int u = 0; u < rootElement.Types.Count; u++)
                                    {
                                        if (rootElement.Types.ElementAt(u).Name == newTypet.Name)
                                            shouldAddt = false;
                                    }
                                    if (shouldAddt)
                                        rootElement.Types.Add(newTypet);
                                    for (int l = 0; l < currentMMTypes.Count; l++)
                                    {
                                        NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                        NMF.Models.Meta.Class newOtherType = otherModelType.DeepClone();
                                        shouldAddt = true;
                                        for (int u = 0; u < rootElement.Types.Count; u++)
                                        {
                                            if (rootElement.Types.ElementAt(u).Name == newOtherType.Name)
                                                shouldAddt = false;
                                        }
                                        if (shouldAddt)
                                            rootElement.Types.Add(newOtherType);

                                        newOtherType = null;
                                    }
                                    newType = null;
                                    //myModell.RootElements.Add(rootElement);
                                    //MFragments.Add(myModell);
                                }
                                break;
                            case "bool":
                                for (int b = 0; b <= 1; b++)
                                {
                                    //Model myModell = new Model();
                                    //#region rootElement
                                    //NMF.Models.Meta.Namespace rootElementt = new NMF.Models.Meta.Namespace();
                                    //rootElementt.Uri = metaModel.RootElements[0].AbsoluteUri;
                                    //rootElementt.Name = "ModelFragment" + j.ToString();
                                    //#endregion rootElement 
                                    if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                    {
                                        for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                        {
                                            //add its parent[k] to rootElement                                
                                            bool shouldAddBase = true;
                                            for (int y = 0; y < rootElement.Types.Count; y++)
                                            {
                                                if (rootElement.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                    shouldAddBase = false;
                                            }
                                            if (shouldAddBase)
                                            {
                                                //var x = FastDeepCloner.DeepCloner.Clone(currentModelType.BaseTypes.ElementAt(k));
                                                //rootElement.Types.Add(x);
                                                rootElement.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());
                                            }
                                        }
                                    }
                                    NMF.Models.Meta.Class newTypet = currentModelType.DeepClone();
                                    newTypet.Attributes.ElementAt(z).DefaultValue = (b == 0) ? "false" : "true";//"random_" + newTypet.Attributes.ElementAt(z).Name;
                                    bool shouldAddt = true;
                                    for (int u = 0; u < rootElement.Types.Count; u++)
                                    {
                                        if (rootElement.Types.ElementAt(u).Name == newTypet.Name)
                                            shouldAddt = false;
                                    }
                                    if (shouldAddt)
                                        rootElement.Types.Add(newTypet);
                                    for (int l = 0; l < currentMMTypes.Count; l++)
                                    {
                                        NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                        NMF.Models.Meta.Class newOtherType = otherModelType.DeepClone();
                                        shouldAddt = true;
                                        for (int u = 0; u < rootElement.Types.Count; u++)
                                        {
                                            if (rootElement.Types.ElementAt(u).Name == newOtherType.Name)
                                                shouldAddt = false;
                                        }
                                        if (shouldAddt)
                                            rootElement.Types.Add(newOtherType);

                                        newOtherType = null;
                                    }

                                    newTypet = null;
                                    //myModell.RootElements.Add(rootElement);
                                    //GC.Collect();
                                    //Application.DoEvents();
                                    //MFragments.Add(myModell);
                                }
                                break;
                        }
                    }                    
                    currentModelType = null;
                    #region save Primitive data type Permurations                                                                        
                }

                foreach (var type in rootElement.Types)
                {
                    foreach (var reference in ((NMF.Models.Meta.Class)type).References) //incomingArc, outgoingArc
                    {
                        if (rootElement.Types.Any(x => x.Name == ((NMF.Models.Meta.Reference)reference).ReferenceType.Name))
                        {
                            var refsClasses = rootElement.Types.Select(y => y.Name == ((NMF.Models.Meta.Reference)reference).ReferenceType.Name);
                            
                            int rnd2 = new Random().Next(refsClasses.Count());
                            if (rnd2 != 0)
                            {                                
                            }
                        }
                    }                         
                }

                myModel.RootElements.Add(rootElement);
                repository.Save(myModel, @"Output\MFragments\" + rootElement.Name + ".nmf");
                if (!CreatedModelFragments[MMFr].Contains(rootElement.Name))
                    CreatedModelFragments[MMFr].Add(rootElement.Name);
                myModel.RootElements.Clear();
                currentMMTypes.Clear();
                myModel = null;
                currentMetaModelRootElement = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Application.DoEvents();
                //MFragments.Add(myModel);
            }
            #endregion

            //NMF.Models.Meta.Class[] mmTypes = new NMF.Models.Meta.Class[typesCount];
            //List<NMF.Models.Meta.IType> mmTypes = new List<NMF.Models.Meta.IType>();
            //foreach (var item in ((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModel.RootElements).Items[0])).Types)
            ////{
            ////    mmTypes.Add(item);
            ////}               

            #endregion
            //foreach (var type in ((NMF.Models.Meta.Namespace)metaModel.RootElements[0]).Types)
            //{
            //    // 1- if type has baseType : add to to MM-fragment
            //    MDEMGTT.ArchitectureCRA.Attribute attribute = new MDEMGTT.ArchitectureCRA.Attribute();
            //    attribute.Name = "aa";
            //    //model.SetAttributeValue()
            //    //repository.Models.Add()
            //    // 2- check for references
            //    // 2-1 iscontainment ==1 => 2 halat darad, ye seri mishan containements ke ovorde mishan, ye seri ham nayarim. all permutations
            //    // 2-2 other references => mese 2 halate bala
            //    // 3- check for attributes
            //    // 3-1 foreach attr -> make fragment for each type. include all permutations
            //    // 4- check for operations
            //    // 4-1 foreach operation -> make fragment for each method. include all permutations

            //}
            Files = null;
            d = null;
            DirectoryInfo dir = new DirectoryInfo("Output\\MFragments");//Assuming Test is your Folder
            FileInfo[] MFragmentsFiles = dir.GetFiles("*.nmf").OrderBy(fi => fi.Name).ToArray();
            //for (int i = 0; i < MMFragments.Count(); i++)
            foreach (FileInfo file in MFragmentsFiles)
            {
                var metaModelFragmentFile = repository.Resolve(file.FullName);
                ModelFragmentVector.Add(metaModelFragmentFile, 0); //temp                
            }
            //Tree.RootElement = model;
            //step 1: Read All Fragments            
            //DirectoryInfo d = new DirectoryInfo("Inputs");//Assuming Test is your Folder
            //FileInfo[] Files = d.GetFiles("*.xmi");
            //foreach (FileInfo file in Files)
            //{
            //    //step2:Make solution space based on loaded Fragments                
            //    try
            //    {
            //        var fragmentModel = repository.Resolve(file.FullName);
            //        var fragmentClassModel = fragmentModel.RootElements[0] as ClassModel;  // for trace purpose only                
            //        ModelFragmentVector.Add(fragmentModel, 0); //temp
            //    }
            //    catch (Exception ex)
            //    { }
            //}
            //step2:Make solution space based on loaded Fragments            
            for (int i = 0; i < nPopulation; i++)
            {
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

            NSGAII nSGAII = new NSGAII(population, ModelFragmentVector, 100);
            List<Dictionary<Model, int>> res = nSGAII.Invoke();

            //foreach (Dictionary<Model, int> entry in res)    
            repository = new ModelRepository();
            for (int i = 0; i < res.Count; i++)
            {
                repository.Save(res[i].Keys.ElementAt(0), @"Output\GeneratedModels\generatedModel" + i.ToString() + ".xmi");
                //DisplayParetoFront(res[i].Keys.ElementAt(0), nSGAII.FinalePareto);
            }
            DisplayParetoFront(res, nSGAII.FinalePareto);
            return;

            MDEMGTT.Algorithms.NSGAII algorithm = new MDEMGTT.Algorithms.NSGAII(new CRAProblem(population, ModelFragmentVector));

            algorithm.PopulationSize = 100;

            algorithm.Initialize();

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (s1, e1) =>
            {
                while (!algorithm.IsTerminated)
                {
                    algorithm.Evolve();
                    worker.ReportProgress(0);
                }
            };
            worker.ProgressChanged += (s1, e1) =>
            {
                txtStatus1.Text = string.Format("Current Generation: {0}", algorithm.CurrentGeneration);
                txtStatus2.Text = string.Format("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            };
            worker.RunWorkerCompleted += (s1, e1) =>
            {
                try
                {
                    ContinuousVector finalSolution = algorithm.GlobalBestSolution;
                    NondominatedPopulation<ContinuousVector> archive = algorithm.NondominatedArchive;
                    Dictionary<int, List<double>> nonDominated = WhoIsTheBest(algorithm.objectives);
                    string final = string.Empty;
                    //foreach (var item in nonDominated)
                    {
                        //Dictionary<Model, int> cur = ((Dictionary<Model, int>)population[item.Key]);
                        Dictionary<Model, int> cur = ((Dictionary<Model, int>)population[0]);
                        string fragments = string.Empty;
                        foreach (var model in cur.Keys)
                        {
                            fragments += model.AbsoluteUri + ";";
                        }
                        //final += $"solution: {item.Key}, fragmens: {fragments} {Environment.NewLine}" +
                        final += $"solution: {new Random().Next(0, 100)}, fragmens: {fragments} {Environment.NewLine}" +
                        $"--------------{Environment.NewLine}";
                    }
                    DisplayParetoFront(archive);
                    MessageBox.Show(final);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
            worker.RunWorkerAsync();

            #endregion algorithm play
        }

        private void HSMNSGAII()
        {
            #region variables
            int nPopulation = 100;
            Dictionary<Model, int> ModelFragmentVector = new Dictionary<Model, int>();
            List<Dictionary<Model, int>> population = new List<Dictionary<Model, int>>();
            System.Random rnd = new System.Random();
            #endregion

            //#region work with model
            var repository = new ModelRepository();
            var metaModel = repository.Resolve("HSM.nmf");
            //var metaModel = repository.Resolve("CPL.nmf");            
            if (metaModel.RootElements.Count < 0)
            {
                //hichi bargard
            }

            #region Types

            int typesCount = ((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModel.RootElements).Items[0])).Types.Count;
            List<byte> tempPermutationsInt = new List<byte>();
            for (byte i = 1; i <= typesCount; i++)
                tempPermutationsInt.Add(i);
            List<List<byte>> mainPermutationList = GetCombination(tempPermutationsInt);

            List<Model> MMFragments = new List<Model>();
            List<Model> MFragments = new List<Model>();
            var allTypes = (((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModel.RootElements).Items[0])).Types);

            // loop over permutations and create a MM fragment based on permuration
            for (int i = 0; i < mainPermutationList.Count; i++)
            //Parallel.For(0, mainPermutationList.Count, i =>
            {
                Model myModel = new Model();
                #region rootElement
                NMF.Models.Meta.Namespace rootElement = new NMF.Models.Meta.Namespace();
                rootElement.Uri = metaModel.RootElements[0].AbsoluteUri;
                rootElement.Name = "MetaModelFragment" + i.ToString();
                #endregion rootElement                    
                List<byte> current = mainPermutationList[i];
                if (mainPermutationList[i] != null)
                    for (int j = 0; j < mainPermutationList[i].Count; j++)
                    {
                        NMF.Models.Meta.Class currentType = (NMF.Models.Meta.Class)allTypes.ElementAt(current[j] - 1); //jth elemet which is clarifyed by current permutation                        
                        if (currentType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                        {
                            for (int k = 0; k < currentType.BaseTypes.Count; k++)
                            {
                                //add its parent[k] to rootElement                                
                                bool shouldAddBase = true;
                                for (int z = 0; z < rootElement.Types.Count; z++)
                                {
                                    if (rootElement.Types.ElementAt(z).Name == currentType.BaseTypes.ElementAt(k).Name)
                                        shouldAddBase = false;
                                }
                                try
                                {
                                    if (shouldAddBase)
                                    {
                                        var x = currentType.BaseTypes.ElementAt(k).DeepClone();
                                        //var x = FastDeepCloner.DeepCloner.Clone(currentType.BaseTypes.ElementAt(k));
                                        //var y = currentType.BaseTypes.ElementAt(k).DeepClone();
                                        rootElement.Types.Add(x);
                                        x = null;
                                    }
                                }
                                catch (Exception ex)
                                {
                                }

                            }
                        }
                        
                        bool shouldAdd = true;
                        for (int z = 0; z < rootElement.Types.Count; z++)
                        {
                            if (rootElement.Types.ElementAt(z).Name == currentType.Name)
                                shouldAdd = false;
                        }
                        try
                        {
                            if (shouldAdd)
                                rootElement.Types.Add(currentType.DeepClone());
                            //rootElement.Types.Add(FastDeepCloner.DeepCloner.Clone(currentType));
                        }
                        catch (Exception ex)
                        {
                        }
                        currentType = null;
                    }
                try
                {
                    myModel.RootElements.Add(rootElement);
                    repository.Save(myModel, @"Output\MMFragments\" + rootElement.Name + ".nmf");
                    //repository.Save(rootElement, @"Output\MMFragments\" + rootElement.Name + ".xmi");
                    //var m = repository.Resolve(@"D:\MM\" + rootElement.Name + ".nmf");
                    myModel.RootElements.Clear();
                    current.Clear();
                    myModel = null;
                    current = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    Application.DoEvents();
                }
                catch (Exception ex)
                { }
                //MMFragments.Add(myModel);  

            }//);

            #region Creat Model Fragments form MM-Fragments and Primitive data values
            DirectoryInfo d = new DirectoryInfo("Output\\MMFragments");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.nmf").OrderBy(fi => fi.Name).ToArray();

            Dictionary<string, List<string>> CreatedModelFragments = new Dictionary<string, List<string>>();
            while (CreatedModelFragments.Count < nPopulation)
            {
                //string currentMmFragmentName = "MetaModelFragment" + new Random().Next(Files.Length).ToString();
                string currentMmFragmentName = Files[new Random().Next(Files.Length)].FullName;
                if (!CreatedModelFragments.ContainsKey(currentMmFragmentName))
                    CreatedModelFragments.Add(currentMmFragmentName, new List<string>());
            }
            
            foreach (var MMFr in CreatedModelFragments.Keys)
            {
                //var metaModelFragmentFile = repository.Resolve(file.FullName);
                var metaModelFragmentFile = repository.Resolve(MMFr);
                if (metaModelFragmentFile.RootElements.Count == 0)
                    continue;
                NMF.Models.Meta.Namespace currentMetaModelRootElement = (NMF.Models.Meta.Namespace)metaModelFragmentFile.RootElements[0];
                var currentMMTypes = (((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModelFragmentFile.RootElements).Items[0])).Types);

                #region rootElement
                NMF.Models.Meta.Namespace rootElement = new NMF.Models.Meta.Namespace();
                rootElement.Uri = metaModel.RootElements[0].AbsoluteUri;
                //rootElement.Name = "ModelFragment" + i.ToString();
                rootElement.Name = currentMetaModelRootElement.Name.Replace("Meta", "");
                Model myModel = new Model();
                #endregion rootElement 

                for (int j = 0; j < currentMMTypes.Count; j++) //NamedElement
                {
                    NMF.Models.Meta.Class currentModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(j); //jth elemet which is clarifyed by current permutation
                    if (currentModelType.IsAbstract)
                        continue;

                    if (currentModelType.Attributes.Count == 0)
                    {
                        NMF.Models.Meta.Class newType = currentModelType.DeepClone();
                        rootElement.Types.Add(newType);
                        newType = null;
                    }
                    for (int z = 0; z < currentModelType.Attributes.Count; z++) //string name
                    {
                        switch (currentModelType.Attributes.ElementAt(z).Type.Name.Trim().ToLower())
                        {
                            case "string":
                                if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                {
                                    for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                    {
                                        //add its parent[k] to rootElement                                
                                        bool shouldAddBase = true;
                                        for (int y = 0; y < rootElement.Types.Count; y++)
                                        {
                                            if (rootElement.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                shouldAddBase = false;
                                        }
                                        if (shouldAddBase)
                                            rootElement.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());
                                    }
                                }
                                NMF.Models.Meta.Class newType;
                                try
                                {
                                    newType = currentModelType.DeepClone();
                                }
                                catch (Exception ex)
                                { continue; }
                                if (new Random().Next(0, 2) == 1)
                                    newType.Attributes.ElementAt(z).DefaultValue = Guid.NewGuid().ToString().Replace("_","");
                                //else
                                //    newType.Attributes.ElementAt(z).DefaultValue = string.Empty;
                                bool shouldAdd = true;
                                for (int u = 0; u < rootElement.Types.Count; u++)
                                {
                                    if (rootElement.Types.ElementAt(u).Name == newType.Name)
                                        shouldAdd = false;
                                }
                                if (shouldAdd)
                                    rootElement.Types.Add(newType);
                                for (int l = 0; l < currentMMTypes.Count; l++)
                                {
                                    NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                    NMF.Models.Meta.Class newOtherType = null;
                                    try
                                    {
                                        newOtherType = otherModelType.DeepClone();
                                    }
                                    catch (Exception ex)
                                    { }
                                    shouldAdd = true;
                                    for (int u = 0; u < rootElement.Types.Count; u++)
                                    {
                                        if (rootElement.Types.ElementAt(u).Name == newOtherType.Name)
                                            shouldAdd = false;
                                    }
                                    if (shouldAdd)
                                        rootElement.Types.Add(newOtherType);

                                    newOtherType = null;
                                }
                                newType = null;
                                //myModel.RootElements.Add(rootElement);
                                //MFragments.Add(myModel);
                                break;
                            case "int":
                                for (int b = -1; b < 1; b++)
                                {                                    
                                    if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                    {
                                        for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                        {
                                            //add its parent[k] to rootElement                                
                                            bool shouldAddBase = true;
                                            for (int y = 0; y < rootElement.Types.Count; y++)
                                            {
                                                if (rootElement.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                    shouldAddBase = false;
                                            }
                                            if (shouldAddBase)
                                                rootElement.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());
                                        }
                                    }
                                    NMF.Models.Meta.Class newTypet = currentModelType.DeepClone();
                                    newTypet.Attributes.ElementAt(z).DefaultValue = new Random().Next(int.MinValue, int.MaxValue).ToString();
                                    bool shouldAddt = true;
                                    for (int u = 0; u < rootElement.Types.Count; u++)
                                    {
                                        if (rootElement.Types.ElementAt(u).Name == newTypet.Name)
                                            shouldAddt = false;
                                    }
                                    if (shouldAddt)
                                        rootElement.Types.Add(newTypet);
                                    for (int l = 0; l < currentMMTypes.Count; l++)
                                    {
                                        NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                        NMF.Models.Meta.Class newOtherType = otherModelType.DeepClone();
                                        shouldAddt = true;
                                        for (int u = 0; u < rootElement.Types.Count; u++)
                                        {
                                            if (rootElement.Types.ElementAt(u).Name == newOtherType.Name)
                                                shouldAddt = false;
                                        }
                                        if (shouldAddt)
                                            rootElement.Types.Add(newOtherType);

                                        newOtherType = null;
                                    }
                                    newType = null;
                                    //myModell.RootElements.Add(rootElement);
                                    //MFragments.Add(myModell);
                                }
                                break;
                            case "bool":
                                for (int b = 0; b <= 1; b++)
                                {                                    
                                    if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                    {
                                        for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                        {
                                            //add its parent[k] to rootElement                                
                                            bool shouldAddBase = true;
                                            for (int y = 0; y < rootElement.Types.Count; y++)
                                            {
                                                if (rootElement.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                    shouldAddBase = false;
                                            }
                                            if (shouldAddBase)
                                            {                                 
                                                rootElement.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());
                                            }
                                        }
                                    }
                                    NMF.Models.Meta.Class newTypet = currentModelType.DeepClone();
                                    newTypet.Attributes.ElementAt(z).DefaultValue = (b == 0) ? "false" : "true";//"random_" + newTypet.Attributes.ElementAt(z).Name;
                                    bool shouldAddt = true;
                                    for (int u = 0; u < rootElement.Types.Count; u++)
                                    {
                                        if (rootElement.Types.ElementAt(u).Name == newTypet.Name)
                                            shouldAddt = false;
                                    }
                                    if (shouldAddt)
                                        rootElement.Types.Add(newTypet);
                                    for (int l = 0; l < currentMMTypes.Count; l++)
                                    {
                                        NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                        NMF.Models.Meta.Class newOtherType = otherModelType.DeepClone();
                                        shouldAddt = true;
                                        for (int u = 0; u < rootElement.Types.Count; u++)
                                        {
                                            if (rootElement.Types.ElementAt(u).Name == newOtherType.Name)
                                                shouldAddt = false;
                                        }
                                        if (shouldAddt)
                                            rootElement.Types.Add(newOtherType);

                                        newOtherType = null;
                                    }

                                    newTypet = null;                                    
                                }
                                break;
                        }
                    }
                    currentModelType = null;
                    #region save Primitive data type Permurations                                                                        
                }
                
                myModel.RootElements.Add(rootElement);
                repository.Save(myModel, @"Output\MFragments\" + rootElement.Name + ".nmf");
                if (!CreatedModelFragments[MMFr].Contains(rootElement.Name))
                    CreatedModelFragments[MMFr].Add(rootElement.Name);
                myModel.RootElements.Clear();
                currentMMTypes.Clear();
                myModel = null;
                currentMetaModelRootElement = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Application.DoEvents();
                //MFragments.Add(myModel);
            }
            #endregion
            
            #endregion            
            Files = null;
            d = null;
            DirectoryInfo dir = new DirectoryInfo("Output\\MFragments");//Assuming Test is your Folder
            FileInfo[] MFragmentsFiles = dir.GetFiles("*.nmf").OrderBy(fi => fi.Name).ToArray();
            //for (int i = 0; i < MMFragments.Count(); i++)
            foreach (FileInfo file in MFragmentsFiles)
            {
                var metaModelFragmentFile = repository.Resolve(file.FullName);
                ModelFragmentVector.Add(metaModelFragmentFile, 0); //temp                
            }
            
            for (int i = 0; i < nPopulation; i++)
            {
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

            NSGAII nSGAII = new NSGAII(population, ModelFragmentVector, 100);
            List<Dictionary<Model, int>> res = nSGAII.Invoke();

            //foreach (Dictionary<Model, int> entry in res)    
            repository = new ModelRepository();
            for (int i = 0; i < res.Count; i++)
            {
                repository.Save(res[i].Keys.ElementAt(0), @"Output\GeneratedModels\generatedModel" + i.ToString() + ".xmi");
                //DisplayParetoFront(res[i].Keys.ElementAt(0), nSGAII.FinalePareto);
            }
            DisplayParetoFront(res, nSGAII.FinalePareto);
            return;

            MDEMGTT.Algorithms.NSGAII algorithm = new MDEMGTT.Algorithms.NSGAII(new CRAProblem(population, ModelFragmentVector));

            algorithm.PopulationSize = 100;

            algorithm.Initialize();

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (s1, e1) =>
            {
                while (!algorithm.IsTerminated)
                {
                    algorithm.Evolve();
                    worker.ReportProgress(0);
                }
            };
            worker.ProgressChanged += (s1, e1) =>
            {
                txtStatus1.Text = string.Format("Current Generation: {0}", algorithm.CurrentGeneration);
                txtStatus2.Text = string.Format("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            };
            worker.RunWorkerCompleted += (s1, e1) =>
            {
                try
                {
                    ContinuousVector finalSolution = algorithm.GlobalBestSolution;
                    NondominatedPopulation<ContinuousVector> archive = algorithm.NondominatedArchive;
                    Dictionary<int, List<double>> nonDominated = WhoIsTheBest(algorithm.objectives);
                    string final = string.Empty;
                    //foreach (var item in nonDominated)
                    {
                        //Dictionary<Model, int> cur = ((Dictionary<Model, int>)population[item.Key]);
                        Dictionary<Model, int> cur = ((Dictionary<Model, int>)population[0]);
                        string fragments = string.Empty;
                        foreach (var model in cur.Keys)
                        {
                            fragments += model.AbsoluteUri + ";";
                        }
                        //final += $"solution: {item.Key}, fragmens: {fragments} {Environment.NewLine}" +
                        final += $"solution: {new Random().Next(0, 100)}, fragmens: {fragments} {Environment.NewLine}" +
                        $"--------------{Environment.NewLine}";
                    }
                    DisplayParetoFront(archive);
                    MessageBox.Show(final);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
            worker.RunWorkerAsync();

            #endregion algorithm play
        }

        private void SimpleUMLNSGAII()
        {
            #region variables
            int nPopulation = 100;
            Dictionary<Model, int> ModelFragmentVector = new Dictionary<Model, int>();
            List<Dictionary<Model, int>> population = new List<Dictionary<Model, int>>();
            System.Random rnd = new System.Random();
            #endregion

            //#region work with model
            var repository = new ModelRepository();
            var metaModel = repository.Resolve("SimpleUML.nmf");
            //var metaModel = repository.Resolve("CPL.nmf");            
            if (metaModel.RootElements.Count < 0)
            {
                //hichi bargard
            }

            #region Types

            int typesCount = ((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModel.RootElements).Items[0])).Types.Count;
            List<byte> tempPermutationsInt = new List<byte>();
            for (byte i = 1; i <= typesCount; i++)
                tempPermutationsInt.Add(i);
            List<List<byte>> mainPermutationList = GetCombination(tempPermutationsInt);

            List<Model> MMFragments = new List<Model>();
            List<Model> MFragments = new List<Model>();
            var allTypes = (((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModel.RootElements).Items[0])).Types);

            // loop over permutations and create a MM fragment based on permuration
            for (int i = 0; i < mainPermutationList.Count; i++)
            //Parallel.For(0, mainPermutationList.Count, i =>
            {
                Model myModel = new Model();
                #region rootElement
                NMF.Models.Meta.Namespace rootElement = new NMF.Models.Meta.Namespace();
                rootElement.Uri = metaModel.RootElements[0].AbsoluteUri;
                rootElement.Name = "MetaModelFragment" + i.ToString();
                #endregion rootElement                    
                List<byte> current = mainPermutationList[i];
                if (mainPermutationList[i] != null)
                    for (int j = 0; j < mainPermutationList[i].Count; j++)
                    {
                        NMF.Models.Meta.Class currentType = (NMF.Models.Meta.Class)allTypes.ElementAt(current[j] - 1); //jth elemet which is clarifyed by current permutation                        
                        if (currentType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                        {
                            for (int k = 0; k < currentType.BaseTypes.Count; k++)
                            {
                                //add its parent[k] to rootElement                                
                                bool shouldAddBase = true;
                                for (int z = 0; z < rootElement.Types.Count; z++)
                                {
                                    if (rootElement.Types.ElementAt(z).Name == currentType.BaseTypes.ElementAt(k).Name)
                                        shouldAddBase = false;
                                }
                                try
                                {
                                    if (shouldAddBase)
                                    {
                                        var x = currentType.BaseTypes.ElementAt(k).DeepClone();
                                        //var x = FastDeepCloner.DeepCloner.Clone(currentType.BaseTypes.ElementAt(k));
                                        //var y = currentType.BaseTypes.ElementAt(k).DeepClone();
                                        rootElement.Types.Add(x);
                                        x = null;
                                    }
                                }
                                catch (Exception ex)
                                {
                                }

                            }
                        }

                        bool shouldAdd = true;
                        for (int z = 0; z < rootElement.Types.Count; z++)
                        {
                            if (rootElement.Types.ElementAt(z).Name == currentType.Name)
                                shouldAdd = false;
                        }
                        try
                        {
                            if (shouldAdd)
                                rootElement.Types.Add(currentType.DeepClone());
                            //rootElement.Types.Add(FastDeepCloner.DeepCloner.Clone(currentType));
                        }
                        catch (Exception ex)
                        {
                        }
                        currentType = null;
                    }
                try
                {
                    myModel.RootElements.Add(rootElement);
                    repository.Save(myModel, @"Output\MMFragments\" + rootElement.Name + ".nmf");
                    //repository.Save(rootElement, @"Output\MMFragments\" + rootElement.Name + ".xmi");
                    //var m = repository.Resolve(@"D:\MM\" + rootElement.Name + ".nmf");
                    myModel.RootElements.Clear();
                    current.Clear();
                    myModel = null;
                    current = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    Application.DoEvents();
                }
                catch (Exception ex)
                { }
                //MMFragments.Add(myModel);  

            }//);

            #region Creat Model Fragments form MM-Fragments and Primitive data values
            DirectoryInfo d = new DirectoryInfo("Output\\MMFragments");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.nmf").OrderBy(fi => fi.Name).ToArray();

            Dictionary<string, List<string>> CreatedModelFragments = new Dictionary<string, List<string>>();
            while (CreatedModelFragments.Count < nPopulation)
            {
                //string currentMmFragmentName = "MetaModelFragment" + new Random().Next(Files.Length).ToString();
                string currentMmFragmentName = Files[new Random().Next(Files.Length)].FullName;
                if (!CreatedModelFragments.ContainsKey(currentMmFragmentName))
                    CreatedModelFragments.Add(currentMmFragmentName, new List<string>());
            }

            foreach (var MMFr in CreatedModelFragments.Keys)
            {
                //var metaModelFragmentFile = repository.Resolve(file.FullName);
                var metaModelFragmentFile = repository.Resolve(MMFr);
                if (metaModelFragmentFile.RootElements.Count == 0)
                    continue;
                NMF.Models.Meta.Namespace currentMetaModelRootElement = (NMF.Models.Meta.Namespace)metaModelFragmentFile.RootElements[0];
                var currentMMTypes = (((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModelFragmentFile.RootElements).Items[0])).Types);

                #region rootElement
                NMF.Models.Meta.Namespace rootElement = new NMF.Models.Meta.Namespace();
                rootElement.Uri = metaModel.RootElements[0].AbsoluteUri;
                //rootElement.Name = "ModelFragment" + i.ToString();
                rootElement.Name = currentMetaModelRootElement.Name.Replace("Meta", "");
                Model myModel = new Model();
                #endregion rootElement 

                for (int j = 0; j < currentMMTypes.Count; j++) //NamedElement
                {
                    NMF.Models.Meta.Class currentModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(j); //jth elemet which is clarifyed by current permutation
                    if (currentModelType.IsAbstract)
                        continue;

                    if (currentModelType.Attributes.Count == 0)
                    {
                        NMF.Models.Meta.Class newType = currentModelType.DeepClone();
                        rootElement.Types.Add(newType);
                        newType = null;
                    }
                    for (int z = 0; z < currentModelType.Attributes.Count; z++) //string name
                    {
                        switch (currentModelType.Attributes.ElementAt(z).Type.Name.Trim().ToLower())
                        {
                            case "string":
                                if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                {
                                    for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                    {
                                        //add its parent[k] to rootElement                                
                                        bool shouldAddBase = true;
                                        for (int y = 0; y < rootElement.Types.Count; y++)
                                        {
                                            if (rootElement.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                shouldAddBase = false;
                                        }
                                        if (shouldAddBase)
                                            rootElement.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());
                                    }
                                }
                                NMF.Models.Meta.Class newType;
                                try
                                {
                                    newType = currentModelType.DeepClone();
                                }
                                catch (Exception ex)
                                { continue; }
                                if (new Random().Next(0, 2) == 1)
                                    newType.Attributes.ElementAt(z).DefaultValue = Guid.NewGuid().ToString().Replace("_", "");
                                //else
                                //    newType.Attributes.ElementAt(z).DefaultValue = string.Empty;
                                bool shouldAdd = true;
                                for (int u = 0; u < rootElement.Types.Count; u++)
                                {
                                    if (rootElement.Types.ElementAt(u).Name == newType.Name)
                                        shouldAdd = false;
                                }
                                if (shouldAdd)
                                    rootElement.Types.Add(newType);
                                for (int l = 0; l < currentMMTypes.Count; l++)
                                {
                                    NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                    NMF.Models.Meta.Class newOtherType = null;
                                    try
                                    {
                                        newOtherType = otherModelType.DeepClone();
                                    }
                                    catch (Exception ex)
                                    { }
                                    shouldAdd = true;
                                    for (int u = 0; u < rootElement.Types.Count; u++)
                                    {
                                        if (rootElement.Types.ElementAt(u).Name == newOtherType.Name)
                                            shouldAdd = false;
                                    }
                                    if (shouldAdd)
                                        rootElement.Types.Add(newOtherType);

                                    newOtherType = null;
                                }
                                newType = null;
                                //myModel.RootElements.Add(rootElement);
                                //MFragments.Add(myModel);
                                break;
                            case "int":
                                for (int b = -1; b < 1; b++)
                                {
                                    if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                    {
                                        for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                        {
                                            //add its parent[k] to rootElement                                
                                            bool shouldAddBase = true;
                                            for (int y = 0; y < rootElement.Types.Count; y++)
                                            {
                                                if (rootElement.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                    shouldAddBase = false;
                                            }
                                            if (shouldAddBase)
                                                rootElement.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());
                                        }
                                    }
                                    NMF.Models.Meta.Class newTypet = currentModelType.DeepClone();
                                    newTypet.Attributes.ElementAt(z).DefaultValue = new Random().Next(int.MinValue, int.MaxValue).ToString();
                                    bool shouldAddt = true;
                                    for (int u = 0; u < rootElement.Types.Count; u++)
                                    {
                                        if (rootElement.Types.ElementAt(u).Name == newTypet.Name)
                                            shouldAddt = false;
                                    }
                                    if (shouldAddt)
                                        rootElement.Types.Add(newTypet);
                                    for (int l = 0; l < currentMMTypes.Count; l++)
                                    {
                                        NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                        NMF.Models.Meta.Class newOtherType = otherModelType.DeepClone();
                                        shouldAddt = true;
                                        for (int u = 0; u < rootElement.Types.Count; u++)
                                        {
                                            if (rootElement.Types.ElementAt(u).Name == newOtherType.Name)
                                                shouldAddt = false;
                                        }
                                        if (shouldAddt)
                                            rootElement.Types.Add(newOtherType);

                                        newOtherType = null;
                                    }
                                    newType = null;
                                    //myModell.RootElements.Add(rootElement);
                                    //MFragments.Add(myModell);
                                }
                                break;
                            case "bool":
                                for (int b = 0; b <= 1; b++)
                                {
                                    if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                    {
                                        for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                        {
                                            //add its parent[k] to rootElement                                
                                            bool shouldAddBase = true;
                                            for (int y = 0; y < rootElement.Types.Count; y++)
                                            {
                                                if (rootElement.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                    shouldAddBase = false;
                                            }
                                            if (shouldAddBase)
                                            {
                                                rootElement.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());
                                            }
                                        }
                                    }
                                    NMF.Models.Meta.Class newTypet = currentModelType.DeepClone();
                                    newTypet.Attributes.ElementAt(z).DefaultValue = (b == 0) ? "false" : "true";//"random_" + newTypet.Attributes.ElementAt(z).Name;
                                    bool shouldAddt = true;
                                    for (int u = 0; u < rootElement.Types.Count; u++)
                                    {
                                        if (rootElement.Types.ElementAt(u).Name == newTypet.Name)
                                            shouldAddt = false;
                                    }
                                    if (shouldAddt)
                                        rootElement.Types.Add(newTypet);
                                    for (int l = 0; l < currentMMTypes.Count; l++)
                                    {
                                        NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                        NMF.Models.Meta.Class newOtherType = otherModelType.DeepClone();
                                        shouldAddt = true;
                                        for (int u = 0; u < rootElement.Types.Count; u++)
                                        {
                                            if (rootElement.Types.ElementAt(u).Name == newOtherType.Name)
                                                shouldAddt = false;
                                        }
                                        if (shouldAddt)
                                            rootElement.Types.Add(newOtherType);

                                        newOtherType = null;
                                    }

                                    newTypet = null;
                                }
                                break;
                        }
                    }
                    currentModelType = null;
                    #region save Primitive data type Permurations                                                                        
                }

                myModel.RootElements.Add(rootElement);
                repository.Save(myModel, @"Output\MFragments\" + rootElement.Name + ".nmf");
                if (!CreatedModelFragments[MMFr].Contains(rootElement.Name))
                    CreatedModelFragments[MMFr].Add(rootElement.Name);
                myModel.RootElements.Clear();
                currentMMTypes.Clear();
                myModel = null;
                currentMetaModelRootElement = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Application.DoEvents();
                //MFragments.Add(myModel);
            }
            #endregion

            #endregion
            Files = null;
            d = null;
            DirectoryInfo dir = new DirectoryInfo("Output\\MFragments");//Assuming Test is your Folder
            FileInfo[] MFragmentsFiles = dir.GetFiles("*.nmf").OrderBy(fi => fi.Name).ToArray();
            //for (int i = 0; i < MMFragments.Count(); i++)
            foreach (FileInfo file in MFragmentsFiles)
            {
                var metaModelFragmentFile = repository.Resolve(file.FullName);
                ModelFragmentVector.Add(metaModelFragmentFile, 0); //temp                
            }

            for (int i = 0; i < nPopulation; i++)
            {
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

            NSGAII nSGAII = new NSGAII(population, ModelFragmentVector, 100);
            List<Dictionary<Model, int>> res = nSGAII.Invoke();

            //foreach (Dictionary<Model, int> entry in res)    
            repository = new ModelRepository();
            for (int i = 0; i < res.Count; i++)
            {
                repository.Save(res[i].Keys.ElementAt(0), @"Output\GeneratedModels\generatedModel" + i.ToString() + ".xmi");
                //DisplayParetoFront(res[i].Keys.ElementAt(0), nSGAII.FinalePareto);
            }
            DisplayParetoFront(res, nSGAII.FinalePareto);
            return;

            MDEMGTT.Algorithms.NSGAII algorithm = new MDEMGTT.Algorithms.NSGAII(new CRAProblem(population, ModelFragmentVector));

            algorithm.PopulationSize = 100;

            algorithm.Initialize();

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (s1, e1) =>
            {
                while (!algorithm.IsTerminated)
                {
                    algorithm.Evolve();
                    worker.ReportProgress(0);
                }
            };
            worker.ProgressChanged += (s1, e1) =>
            {
                txtStatus1.Text = string.Format("Current Generation: {0}", algorithm.CurrentGeneration);
                txtStatus2.Text = string.Format("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            };
            worker.RunWorkerCompleted += (s1, e1) =>
            {
                try
                {
                    ContinuousVector finalSolution = algorithm.GlobalBestSolution;
                    NondominatedPopulation<ContinuousVector> archive = algorithm.NondominatedArchive;
                    Dictionary<int, List<double>> nonDominated = WhoIsTheBest(algorithm.objectives);
                    string final = string.Empty;
                    //foreach (var item in nonDominated)
                    {
                        //Dictionary<Model, int> cur = ((Dictionary<Model, int>)population[item.Key]);
                        Dictionary<Model, int> cur = ((Dictionary<Model, int>)population[0]);
                        string fragments = string.Empty;
                        foreach (var model in cur.Keys)
                        {
                            fragments += model.AbsoluteUri + ";";
                        }
                        //final += $"solution: {item.Key}, fragmens: {fragments} {Environment.NewLine}" +
                        final += $"solution: {new Random().Next(0, 100)}, fragmens: {fragments} {Environment.NewLine}" +
                        $"--------------{Environment.NewLine}";
                    }
                    DisplayParetoFront(archive);
                    MessageBox.Show(final);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
            worker.RunWorkerAsync();

            #endregion algorithm play
        }

        private void GrafcetNSGAII()
        {
            #region variables
            int nPopulation = 100;
            Dictionary<Model, int> ModelFragmentVector = new Dictionary<Model, int>();
            List<Dictionary<Model, int>> population = new List<Dictionary<Model, int>>();
            System.Random rnd = new System.Random();
            #endregion

            //#region work with model
            var repository = new ModelRepository();
            var metaModel = repository.Resolve("Grafcet.nmf");
            //var metaModel = repository.Resolve("CPL.nmf");            
            if (metaModel.RootElements.Count < 0)
            {
                //hichi bargard
            }

            #region Types

            int typesCount = ((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModel.RootElements).Items[0])).Types.Count;
            List<byte> tempPermutationsInt = new List<byte>();
            for (byte i = 1; i <= typesCount; i++)
                tempPermutationsInt.Add(i);
            List<List<byte>> mainPermutationList = GetCombination(tempPermutationsInt);

            List<Model> MMFragments = new List<Model>();
            List<Model> MFragments = new List<Model>();
            var allTypes = (((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModel.RootElements).Items[0])).Types);

            // loop over permutations and create a MM fragment based on permuration
            for (int i = 0; i < mainPermutationList.Count; i++)
            //Parallel.For(0, mainPermutationList.Count, i =>
            {
                Model myModel = new Model();
                #region rootElement
                NMF.Models.Meta.Namespace rootElement = new NMF.Models.Meta.Namespace();
                rootElement.Uri = metaModel.RootElements[0].AbsoluteUri;
                rootElement.Name = "MetaModelFragment" + i.ToString();
                #endregion rootElement                    
                List<byte> current = mainPermutationList[i];
                if (mainPermutationList[i] != null)
                    for (int j = 0; j < mainPermutationList[i].Count; j++)
                    {
                        NMF.Models.Meta.Class currentType = (NMF.Models.Meta.Class)allTypes.ElementAt(current[j] - 1); //jth elemet which is clarifyed by current permutation                        
                        if (currentType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                        {
                            for (int k = 0; k < currentType.BaseTypes.Count; k++)
                            {
                                //add its parent[k] to rootElement                                
                                bool shouldAddBase = true;
                                for (int z = 0; z < rootElement.Types.Count; z++)
                                {
                                    if (rootElement.Types.ElementAt(z).Name == currentType.BaseTypes.ElementAt(k).Name)
                                        shouldAddBase = false;
                                }
                                try
                                {
                                    if (shouldAddBase)
                                    {
                                        var x = currentType.BaseTypes.ElementAt(k).DeepClone();
                                        //var x = FastDeepCloner.DeepCloner.Clone(currentType.BaseTypes.ElementAt(k));
                                        //var y = currentType.BaseTypes.ElementAt(k).DeepClone();
                                        rootElement.Types.Add(x);
                                        x = null;
                                    }
                                }
                                catch (Exception ex)
                                {
                                }

                            }
                        }

                        bool shouldAdd = true;
                        for (int z = 0; z < rootElement.Types.Count; z++)
                        {
                            if (rootElement.Types.ElementAt(z).Name == currentType.Name)
                                shouldAdd = false;
                        }
                        try
                        {
                            if (shouldAdd)
                                rootElement.Types.Add(currentType.DeepClone());
                            //rootElement.Types.Add(FastDeepCloner.DeepCloner.Clone(currentType));
                        }
                        catch (Exception ex)
                        {
                        }
                        currentType = null;
                    }
                try
                {
                    myModel.RootElements.Add(rootElement);
                    repository.Save(myModel, @"Output\MMFragments\" + rootElement.Name + ".nmf");
                    //repository.Save(rootElement, @"Output\MMFragments\" + rootElement.Name + ".xmi");
                    //var m = repository.Resolve(@"D:\MM\" + rootElement.Name + ".nmf");
                    myModel.RootElements.Clear();
                    current.Clear();
                    myModel = null;
                    current = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    Application.DoEvents();
                }
                catch (Exception ex)
                { }
                //MMFragments.Add(myModel);  

            }//);

            #region Creat Model Fragments form MM-Fragments and Primitive data values
            DirectoryInfo d = new DirectoryInfo("Output\\MMFragments");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.nmf").OrderBy(fi => fi.Name).ToArray();

            Dictionary<string, List<string>> CreatedModelFragments = new Dictionary<string, List<string>>();
            while (CreatedModelFragments.Count < nPopulation)
            {
                //string currentMmFragmentName = "MetaModelFragment" + new Random().Next(Files.Length).ToString();
                string currentMmFragmentName = Files[new Random().Next(Files.Length)].FullName;
                if (!CreatedModelFragments.ContainsKey(currentMmFragmentName))
                    CreatedModelFragments.Add(currentMmFragmentName, new List<string>());
            }

            foreach (var MMFr in CreatedModelFragments.Keys)
            {
                //var metaModelFragmentFile = repository.Resolve(file.FullName);
                var metaModelFragmentFile = repository.Resolve(MMFr);
                if (metaModelFragmentFile.RootElements.Count == 0)
                    continue;
                NMF.Models.Meta.Namespace currentMetaModelRootElement = (NMF.Models.Meta.Namespace)metaModelFragmentFile.RootElements[0];
                var currentMMTypes = (((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModelFragmentFile.RootElements).Items[0])).Types);

                #region rootElement
                NMF.Models.Meta.Namespace rootElement = new NMF.Models.Meta.Namespace();
                rootElement.Uri = metaModel.RootElements[0].AbsoluteUri;
                //rootElement.Name = "ModelFragment" + i.ToString();
                rootElement.Name = currentMetaModelRootElement.Name.Replace("Meta", "");
                Model myModel = new Model();
                #endregion rootElement 

                for (int j = 0; j < currentMMTypes.Count; j++) //NamedElement
                {
                    NMF.Models.Meta.Class currentModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(j); //jth elemet which is clarifyed by current permutation
                    if (currentModelType.IsAbstract)
                        continue;

                    if (currentModelType.Attributes.Count == 0)
                    {
                        NMF.Models.Meta.Class newType = currentModelType.DeepClone();
                        rootElement.Types.Add(newType);
                        newType = null;
                    }
                    for (int z = 0; z < currentModelType.Attributes.Count; z++) //string name
                    {
                        switch (currentModelType.Attributes.ElementAt(z).Type.Name.Trim().ToLower())
                        {
                            case "string":
                                if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                {
                                    for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                    {
                                        //add its parent[k] to rootElement                                
                                        bool shouldAddBase = true;
                                        for (int y = 0; y < rootElement.Types.Count; y++)
                                        {
                                            if (rootElement.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                shouldAddBase = false;
                                        }
                                        if (shouldAddBase)
                                            rootElement.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());
                                    }
                                }
                                NMF.Models.Meta.Class newType;
                                try
                                {
                                    newType = currentModelType.DeepClone();
                                }
                                catch (Exception ex)
                                { continue; }
                                if (new Random().Next(0, 2) == 1)
                                    newType.Attributes.ElementAt(z).DefaultValue = Guid.NewGuid().ToString().Replace("_", "");
                                //else
                                //    newType.Attributes.ElementAt(z).DefaultValue = string.Empty;
                                bool shouldAdd = true;
                                for (int u = 0; u < rootElement.Types.Count; u++)
                                {
                                    if (rootElement.Types.ElementAt(u).Name == newType.Name)
                                        shouldAdd = false;
                                }
                                if (shouldAdd)
                                    rootElement.Types.Add(newType);
                                for (int l = 0; l < currentMMTypes.Count; l++)
                                {
                                    NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                    NMF.Models.Meta.Class newOtherType = null;
                                    try
                                    {
                                        newOtherType = otherModelType.DeepClone();
                                    }
                                    catch (Exception ex)
                                    { }
                                    shouldAdd = true;
                                    for (int u = 0; u < rootElement.Types.Count; u++)
                                    {
                                        if (rootElement.Types.ElementAt(u).Name == newOtherType.Name)
                                            shouldAdd = false;
                                    }
                                    if (shouldAdd)
                                        rootElement.Types.Add(newOtherType);

                                    newOtherType = null;
                                }
                                newType = null;
                                //myModel.RootElements.Add(rootElement);
                                //MFragments.Add(myModel);
                                break;
                            case "int":
                                for (int b = -1; b < 1; b++)
                                {
                                    if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                    {
                                        for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                        {
                                            //add its parent[k] to rootElement                                
                                            bool shouldAddBase = true;
                                            for (int y = 0; y < rootElement.Types.Count; y++)
                                            {
                                                if (rootElement.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                    shouldAddBase = false;
                                            }
                                            if (shouldAddBase)
                                                rootElement.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());
                                        }
                                    }
                                    NMF.Models.Meta.Class newTypet = currentModelType.DeepClone();
                                    newTypet.Attributes.ElementAt(z).DefaultValue = new Random().Next(int.MinValue, int.MaxValue).ToString();
                                    bool shouldAddt = true;
                                    for (int u = 0; u < rootElement.Types.Count; u++)
                                    {
                                        if (rootElement.Types.ElementAt(u).Name == newTypet.Name)
                                            shouldAddt = false;
                                    }
                                    if (shouldAddt)
                                        rootElement.Types.Add(newTypet);
                                    for (int l = 0; l < currentMMTypes.Count; l++)
                                    {
                                        NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                        NMF.Models.Meta.Class newOtherType = otherModelType.DeepClone();
                                        shouldAddt = true;
                                        for (int u = 0; u < rootElement.Types.Count; u++)
                                        {
                                            if (rootElement.Types.ElementAt(u).Name == newOtherType.Name)
                                                shouldAddt = false;
                                        }
                                        if (shouldAddt)
                                            rootElement.Types.Add(newOtherType);

                                        newOtherType = null;
                                    }
                                    newType = null;
                                    //myModell.RootElements.Add(rootElement);
                                    //MFragments.Add(myModell);
                                }
                                break;
                            case "bool":
                                for (int b = 0; b <= 1; b++)
                                {
                                    if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                    {
                                        for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                        {
                                            //add its parent[k] to rootElement                                
                                            bool shouldAddBase = true;
                                            for (int y = 0; y < rootElement.Types.Count; y++)
                                            {
                                                if (rootElement.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                    shouldAddBase = false;
                                            }
                                            if (shouldAddBase)
                                            {
                                                rootElement.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());
                                            }
                                        }
                                    }
                                    NMF.Models.Meta.Class newTypet = currentModelType.DeepClone();
                                    newTypet.Attributes.ElementAt(z).DefaultValue = (b == 0) ? "false" : "true";//"random_" + newTypet.Attributes.ElementAt(z).Name;
                                    bool shouldAddt = true;
                                    for (int u = 0; u < rootElement.Types.Count; u++)
                                    {
                                        if (rootElement.Types.ElementAt(u).Name == newTypet.Name)
                                            shouldAddt = false;
                                    }
                                    if (shouldAddt)
                                        rootElement.Types.Add(newTypet);
                                    for (int l = 0; l < currentMMTypes.Count; l++)
                                    {
                                        NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                        NMF.Models.Meta.Class newOtherType = otherModelType.DeepClone();
                                        shouldAddt = true;
                                        for (int u = 0; u < rootElement.Types.Count; u++)
                                        {
                                            if (rootElement.Types.ElementAt(u).Name == newOtherType.Name)
                                                shouldAddt = false;
                                        }
                                        if (shouldAddt)
                                            rootElement.Types.Add(newOtherType);

                                        newOtherType = null;
                                    }

                                    newTypet = null;
                                }
                                break;
                        }
                    }
                    currentModelType = null;
                    #region save Primitive data type Permurations                                                                        
                }

                myModel.RootElements.Add(rootElement);
                repository.Save(myModel, @"Output\MFragments\" + rootElement.Name + ".nmf");
                if (!CreatedModelFragments[MMFr].Contains(rootElement.Name))
                    CreatedModelFragments[MMFr].Add(rootElement.Name);
                myModel.RootElements.Clear();
                currentMMTypes.Clear();
                myModel = null;
                currentMetaModelRootElement = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Application.DoEvents();
                //MFragments.Add(myModel);
            }
            #endregion

            #endregion
            Files = null;
            d = null;
            DirectoryInfo dir = new DirectoryInfo("Output\\MFragments");//Assuming Test is your Folder
            FileInfo[] MFragmentsFiles = dir.GetFiles("*.nmf").OrderBy(fi => fi.Name).ToArray();
            //for (int i = 0; i < MMFragments.Count(); i++)
            foreach (FileInfo file in MFragmentsFiles)
            {
                var metaModelFragmentFile = repository.Resolve(file.FullName);
                ModelFragmentVector.Add(metaModelFragmentFile, 0); //temp                
            }

            for (int i = 0; i < nPopulation; i++)
            {
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

            NSGAII nSGAII = new NSGAII(population, ModelFragmentVector, 100);
            List<Dictionary<Model, int>> res = nSGAII.Invoke();

            //foreach (Dictionary<Model, int> entry in res)    
            repository = new ModelRepository();
            for (int i = 0; i < res.Count; i++)
            {
                repository.Save(res[i].Keys.ElementAt(0), @"Output\GeneratedModels\generatedModel" + i.ToString() + ".xmi");
                //DisplayParetoFront(res[i].Keys.ElementAt(0), nSGAII.FinalePareto);
            }
            DisplayParetoFront(res, nSGAII.FinalePareto);
            return;

            MDEMGTT.Algorithms.NSGAII algorithm = new MDEMGTT.Algorithms.NSGAII(new CRAProblem(population, ModelFragmentVector));

            algorithm.PopulationSize = 100;

            algorithm.Initialize();

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (s1, e1) =>
            {
                while (!algorithm.IsTerminated)
                {
                    algorithm.Evolve();
                    worker.ReportProgress(0);
                }
            };
            worker.ProgressChanged += (s1, e1) =>
            {
                txtStatus1.Text = string.Format("Current Generation: {0}", algorithm.CurrentGeneration);
                txtStatus2.Text = string.Format("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            };
            worker.RunWorkerCompleted += (s1, e1) =>
            {
                try
                {
                    ContinuousVector finalSolution = algorithm.GlobalBestSolution;
                    NondominatedPopulation<ContinuousVector> archive = algorithm.NondominatedArchive;
                    Dictionary<int, List<double>> nonDominated = WhoIsTheBest(algorithm.objectives);
                    string final = string.Empty;
                    //foreach (var item in nonDominated)
                    {
                        //Dictionary<Model, int> cur = ((Dictionary<Model, int>)population[item.Key]);
                        Dictionary<Model, int> cur = ((Dictionary<Model, int>)population[0]);
                        string fragments = string.Empty;
                        foreach (var model in cur.Keys)
                        {
                            fragments += model.AbsoluteUri + ";";
                        }
                        //final += $"solution: {item.Key}, fragmens: {fragments} {Environment.NewLine}" +
                        final += $"solution: {new Random().Next(0, 100)}, fragmens: {fragments} {Environment.NewLine}" +
                        $"--------------{Environment.NewLine}";
                    }
                    DisplayParetoFront(archive);
                    MessageBox.Show(final);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
            worker.RunWorkerAsync();

            #endregion algorithm play
        }

        private void ClassNSGAII()
        {
            #region Meii_temp
            var myRepository = new ModelRepository();
            string path = @"C:\Users\asus\Desktop\case studies\Class\model";
            
            for (int i = 0; i < 200; i++)
            {
                var myModel = myRepository.Resolve(path + i.ToString());

            }
            
            #endregion Meii_temp

            #region variables
            int nPopulation = 100;
            Dictionary<Model, int> ModelFragmentVector = new Dictionary<Model, int>();
            List<Dictionary<Model, int>> population = new List<Dictionary<Model, int>>();
            System.Random rnd = new System.Random();
            #endregion

            //#region work with model
            var repository = new ModelRepository();
            
            var metaModel = repository.Resolve("Class.nmf");
            if (metaModel.RootElements.Count < 0)
            {
                //hichi bargard
            }


            #region Types

            int typesCount = ((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModel.RootElements).Items[0])).Types.Count;
            //extract all availabe permutations from types of rootelement
            List<byte> tempPermutationsInt = new List<byte>();
            for (byte i = 1; i <= typesCount; i++)
                tempPermutationsInt.Add(i);
            List<List<byte>> mainPermutationList = GetCombination(tempPermutationsInt);

            List<Model> MMFragments = new List<Model>();
            List<Model> MFragments = new List<Model>();
            var allTypes = (((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModel.RootElements).Items[0])).Types);
            // List<string> primitiveDataTypes = new List<string>();

            // loop over permutations and create a MM fragment based on permuration
            for (int i = 0; i < mainPermutationList.Count; i++)
            //Parallel.For(0, mainPermutationList.Count, i =>
            {
                Model myModel = new Model();
                #region rootElement
                NMF.Models.Meta.Namespace rootElement = new NMF.Models.Meta.Namespace();
                rootElement.Uri = metaModel.RootElements[0].AbsoluteUri;
                rootElement.Name = "MetaModelFragment" + i.ToString();
                //rootElement.IsFrozen = false;
                //rootElement.IsLocked = false;                    
                #endregion rootElement                    
                List<byte> current = mainPermutationList[i];
                if (mainPermutationList[i] != null)
                    for (int j = 0; j < mainPermutationList[i].Count; j++)
                    {
                        NMF.Models.Meta.Class currentType = (NMF.Models.Meta.Class)allTypes.ElementAt(current[j] - 1); //jth elemet which is clarifyed by current permutation                        
                        if (currentType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                        {
                            for (int k = 0; k < currentType.BaseTypes.Count; k++)
                            {
                                //add its parent[k] to rootElement                                
                                bool shouldAddBase = true;
                                for (int z = 0; z < rootElement.Types.Count; z++)
                                {
                                    if (rootElement.Types.ElementAt(z).Name == currentType.BaseTypes.ElementAt(k).Name)
                                        shouldAddBase = false;
                                }
                                try
                                {
                                    if (shouldAddBase)
                                    {
                                        var x = currentType.BaseTypes.ElementAt(k).DeepClone();
                                        //var x = FastDeepCloner.DeepCloner.Clone(currentType.BaseTypes.ElementAt(k));
                                        //var y = currentType.BaseTypes.ElementAt(k).DeepClone();
                                        rootElement.Types.Add(x);
                                        x = null;
                                    }
                                }
                                catch (Exception ex)
                                {
                                }

                            }
                        }
                        #region save Primitive data type Permurations
                        //if (currentType.Attributes.Count > 0)
                        //{
                        //    for (int k = 0; k < currentType.Attributes.Count; k++)
                        //    {
                        //        if (!primitiveDataTypes.Contains(currentType.Attributes.ElementAt(k).Type.Name.Trim()))
                        //            primitiveDataTypes.Add(currentType.Attributes.ElementAt(k).Type.Name.Trim());
                        //    }
                        //}
                        #endregion
                        //var x = NClone.Clone.ObjectGraph(currentType);
                        //var x = currentType.DeepClone();
                        bool shouldAdd = true;
                        for (int z = 0; z < rootElement.Types.Count; z++)
                        {
                            if (rootElement.Types.ElementAt(z).Name == currentType.Name)
                                shouldAdd = false;
                        }
                        try
                        {
                            if (shouldAdd)
                                rootElement.Types.Add(currentType.DeepClone());
                            //rootElement.Types.Add(FastDeepCloner.DeepCloner.Clone(currentType));
                        }
                        catch (Exception ex)
                        {
                        }
                        currentType = null;
                    }
                try
                {
                    myModel.RootElements.Add(rootElement);
                    repository.Save(myModel, @"Output\MMFragments\" + rootElement.Name + ".nmf");
                    //repository.Save(rootElement, @"Output\MMFragments\" + rootElement.Name + ".xmi");
                    //var m = repository.Resolve(@"D:\MM\" + rootElement.Name + ".nmf");
                    myModel.RootElements.Clear();
                    current.Clear();
                    myModel = null;
                    current = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    Application.DoEvents();
                }
                catch (Exception ex)
                { }
                //MMFragments.Add(myModel);  

            }//);

            #region Creat Model Fragments form MM-Fragments and Primitive data values
            DirectoryInfo d = new DirectoryInfo("Output\\MMFragments");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.nmf").OrderBy(fi => fi.Name).ToArray();
            //for (int i = 0; i < MMFragments.Count(); i++)
            foreach (FileInfo file in Files)
            {
                var metaModelFragmentFile = repository.Resolve(file.FullName);
                //NMF.Models.Meta.Namespace currentMetaModelRootElement = (NMF.Models.Meta.Namespace)MMFragments[i].RootElements[0]; MMFragments[i] -> metaModelFragmentFile
                NMF.Models.Meta.Namespace currentMetaModelRootElement = (NMF.Models.Meta.Namespace)metaModelFragmentFile.RootElements[0];
                var currentMMTypes = (((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModelFragmentFile.RootElements).Items[0])).Types);

                #region rootElement
                NMF.Models.Meta.Namespace rootElement = new NMF.Models.Meta.Namespace();
                rootElement.Uri = metaModel.RootElements[0].AbsoluteUri;
                //rootElement.Name = "ModelFragment" + i.ToString();
                rootElement.Name = currentMetaModelRootElement.Name.Replace("Meta", "");
                Model myModel = new Model();
                #endregion rootElement 

                for (int j = 0; j < currentMMTypes.Count; j++) //NamedElement
                {
                    NMF.Models.Meta.Class currentModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(j); //jth elemet which is clarifyed by current permutation
                    if (currentModelType.Attributes.Count == 0)
                    {
                        NMF.Models.Meta.Class newType = currentModelType.DeepClone();
                        rootElement.Types.Add(newType);
                        newType = null;
                    }
                    for (int z = 0; z < currentModelType.Attributes.Count; z++) //string name
                    {
                        switch (currentModelType.Attributes.ElementAt(z).Type.Name.Trim().ToLower())
                        {
                            case "string":
                                //Model myModel = new Model();
                                //#region rootElement
                                //NMF.Models.Meta.Namespace rootElement = new NMF.Models.Meta.Namespace();
                                //rootElement.Uri = metaModel.RootElements[0].AbsoluteUri;
                                //rootElement.Name = "ModelFragment" + j.ToString();
                                //#endregion rootElement 
                                if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                {
                                    for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                    {
                                        //add its parent[k] to rootElement                                
                                        bool shouldAddBase = true;
                                        for (int y = 0; y < rootElement.Types.Count; y++)
                                        {
                                            if (rootElement.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                shouldAddBase = false;
                                        }
                                        if (shouldAddBase)
                                            rootElement.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());
                                    }
                                }
                                NMF.Models.Meta.Class newType;
                                try
                                {
                                    newType = currentModelType.DeepClone();
                                }
                                catch (Exception ex)
                                { continue; }
                                newType.Attributes.ElementAt(z).DefaultValue = "random_" + newType.Attributes.ElementAt(z).Name;
                                bool shouldAdd = true;
                                for (int u = 0; u < rootElement.Types.Count; u++)
                                {
                                    if (rootElement.Types.ElementAt(u).Name == newType.Name)
                                        shouldAdd = false;
                                }
                                if (shouldAdd)
                                    rootElement.Types.Add(newType);
                                for (int l = 0; l < currentMMTypes.Count; l++)
                                {
                                    NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                    NMF.Models.Meta.Class newOtherType = null;
                                    try
                                    {
                                        newOtherType = otherModelType.DeepClone();
                                    }
                                    catch (Exception ex)
                                    { }
                                    shouldAdd = true;
                                    for (int u = 0; u < rootElement.Types.Count; u++)
                                    {
                                        if (rootElement.Types.ElementAt(u).Name == newOtherType.Name)
                                            shouldAdd = false;
                                    }
                                    if (shouldAdd)
                                        rootElement.Types.Add(newOtherType);

                                    newOtherType = null;
                                }
                                newType = null;
                                //myModel.RootElements.Add(rootElement);
                                //MFragments.Add(myModel);
                                break;
                            case "int":
                                for (int b = -1; b < 1; b++)
                                {
                                    //Model myModell = new Model();
                                    //#region rootElement
                                    //NMF.Models.Meta.Namespace rootElementt = new NMF.Models.Meta.Namespace();
                                    //rootElementt.Uri = metaModel.RootElements[0].AbsoluteUri;
                                    //rootElementt.Name = "ModelFragment" + j.ToString();
                                    //#endregion rootElement 
                                    if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                    {
                                        for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                        {
                                            //add its parent[k] to rootElement                                
                                            bool shouldAddBase = true;
                                            for (int y = 0; y < rootElement.Types.Count; y++)
                                            {
                                                if (rootElement.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                    shouldAddBase = false;
                                            }
                                            if (shouldAddBase)
                                                rootElement.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());
                                        }
                                    }
                                    NMF.Models.Meta.Class newTypet = currentModelType.DeepClone();
                                    newTypet.Attributes.ElementAt(z).DefaultValue = b.ToString();//"random_" + newTypet.Attributes.ElementAt(z).Name;
                                    bool shouldAddt = true;
                                    for (int u = 0; u < rootElement.Types.Count; u++)
                                    {
                                        if (rootElement.Types.ElementAt(u).Name == newTypet.Name)
                                            shouldAddt = false;
                                    }
                                    if (shouldAddt)
                                        rootElement.Types.Add(newTypet);
                                    for (int l = 0; l < currentMMTypes.Count; l++)
                                    {
                                        NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                        NMF.Models.Meta.Class newOtherType = otherModelType.DeepClone();
                                        shouldAddt = true;
                                        for (int u = 0; u < rootElement.Types.Count; u++)
                                        {
                                            if (rootElement.Types.ElementAt(u).Name == newOtherType.Name)
                                                shouldAddt = false;
                                        }
                                        if (shouldAddt)
                                            rootElement.Types.Add(newOtherType);

                                        newOtherType = null;
                                    }
                                    newType = null;
                                    //myModell.RootElements.Add(rootElement);
                                    //MFragments.Add(myModell);
                                }
                                break;
                            case "bool":
                                for (int b = 0; b <= 1; b++)
                                {
                                    //Model myModell = new Model();
                                    //#region rootElement
                                    //NMF.Models.Meta.Namespace rootElementt = new NMF.Models.Meta.Namespace();
                                    //rootElementt.Uri = metaModel.RootElements[0].AbsoluteUri;
                                    //rootElementt.Name = "ModelFragment" + j.ToString();
                                    //#endregion rootElement 
                                    if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                    {
                                        for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                        {
                                            //add its parent[k] to rootElement                                
                                            bool shouldAddBase = true;
                                            for (int y = 0; y < rootElement.Types.Count; y++)
                                            {
                                                if (rootElement.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                    shouldAddBase = false;
                                            }
                                            if (shouldAddBase)
                                            {
                                                //var x = FastDeepCloner.DeepCloner.Clone(currentModelType.BaseTypes.ElementAt(k));
                                                //rootElement.Types.Add(x);
                                                rootElement.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());
                                            }
                                        }
                                    }
                                    NMF.Models.Meta.Class newTypet = currentModelType.DeepClone();
                                    newTypet.Attributes.ElementAt(z).DefaultValue = (b == 0) ? "false" : "true";//"random_" + newTypet.Attributes.ElementAt(z).Name;
                                    bool shouldAddt = true;
                                    for (int u = 0; u < rootElement.Types.Count; u++)
                                    {
                                        if (rootElement.Types.ElementAt(u).Name == newTypet.Name)
                                            shouldAddt = false;
                                    }
                                    if (shouldAddt)
                                        rootElement.Types.Add(newTypet);
                                    for (int l = 0; l < currentMMTypes.Count; l++)
                                    {
                                        NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                        NMF.Models.Meta.Class newOtherType = otherModelType.DeepClone();
                                        shouldAddt = true;
                                        for (int u = 0; u < rootElement.Types.Count; u++)
                                        {
                                            if (rootElement.Types.ElementAt(u).Name == newOtherType.Name)
                                                shouldAddt = false;
                                        }
                                        if (shouldAddt)
                                            rootElement.Types.Add(newOtherType);

                                        newOtherType = null;
                                    }

                                    newTypet = null;
                                    //myModell.RootElements.Add(rootElement);
                                    //GC.Collect();
                                    //Application.DoEvents();
                                    //MFragments.Add(myModell);
                                }
                                break;
                        }
                    }
                    currentModelType = null;
                    #region save Primitive data type Permurations                                                                        
                }

                myModel.RootElements.Add(rootElement);
                repository.Save(myModel, @"Output\MFragments\" + rootElement.Name + ".nmf");
                myModel.RootElements.Clear();
                currentMMTypes.Clear();
                myModel = null;
                currentMetaModelRootElement = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Application.DoEvents();
                //MFragments.Add(myModel);
            }
            #endregion

            //NMF.Models.Meta.Class[] mmTypes = new NMF.Models.Meta.Class[typesCount];
            //List<NMF.Models.Meta.IType> mmTypes = new List<NMF.Models.Meta.IType>();
            //foreach (var item in ((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModel.RootElements).Items[0])).Types)
            ////{
            ////    mmTypes.Add(item);
            ////}               

            #endregion
            //foreach (var type in ((NMF.Models.Meta.Namespace)metaModel.RootElements[0]).Types)
            //{
            //    // 1- if type has baseType : add to to MM-fragment
            //    MDEMGTT.ArchitectureCRA.Attribute attribute = new MDEMGTT.ArchitectureCRA.Attribute();
            //    attribute.Name = "aa";
            //    //model.SetAttributeValue()
            //    //repository.Models.Add()
            //    // 2- check for references
            //    // 2-1 iscontainment ==1 => 2 halat darad, ye seri mishan containements ke ovorde mishan, ye seri ham nayarim. all permutations
            //    // 2-2 other references => mese 2 halate bala
            //    // 3- check for attributes
            //    // 3-1 foreach attr -> make fragment for each type. include all permutations
            //    // 4- check for operations
            //    // 4-1 foreach operation -> make fragment for each method. include all permutations

            //}
            Files = null;
            d = null;
            DirectoryInfo dir = new DirectoryInfo("Output\\MFragments");//Assuming Test is your Folder
            FileInfo[] MFragmentsFiles = dir.GetFiles("*.nmf").OrderBy(fi => fi.Name).ToArray();
            //for (int i = 0; i < MMFragments.Count(); i++)
            foreach (FileInfo file in MFragmentsFiles)
            {
                var metaModelFragmentFile = repository.Resolve(file.FullName);
                ModelFragmentVector.Add(metaModelFragmentFile, 0); //temp                
            }
            //Tree.RootElement = model;
            //step 1: Read All Fragments            
            //DirectoryInfo d = new DirectoryInfo("Inputs");//Assuming Test is your Folder
            //FileInfo[] Files = d.GetFiles("*.xmi");
            //foreach (FileInfo file in Files)
            //{
            //    //step2:Make solution space based on loaded Fragments                
            //    try
            //    {
            //        var fragmentModel = repository.Resolve(file.FullName);
            //        var fragmentClassModel = fragmentModel.RootElements[0] as ClassModel;  // for trace purpose only                
            //        ModelFragmentVector.Add(fragmentModel, 0); //temp
            //    }
            //    catch (Exception ex)
            //    { }
            //}
            //step2:Make solution space based on loaded Fragments            
            for (int i = 0; i < nPopulation; i++)
            {
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

            NSGAII nSGAII = new NSGAII(population, ModelFragmentVector, 100);
            List<Dictionary<Model, int>> res = nSGAII.Invoke();

            //foreach (Dictionary<Model, int> entry in res)    
            repository = new ModelRepository();
            for (int i = 0; i < res.Count; i++)
            {
                repository.Save(res[i].Keys.ElementAt(0), @"Output\GeneratedModels\generatedModel" + i.ToString() + ".xmi");
                //DisplayParetoFront(res[i].Keys.ElementAt(0), nSGAII.FinalePareto);
            }
            DisplayParetoFront(res, nSGAII.FinalePareto);
            return;

            MDEMGTT.Algorithms.NSGAII algorithm = new MDEMGTT.Algorithms.NSGAII(new CRAProblem(population, ModelFragmentVector));

            algorithm.PopulationSize = 100;

            algorithm.Initialize();

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (s1, e1) =>
            {
                while (!algorithm.IsTerminated)
                {
                    algorithm.Evolve();
                    worker.ReportProgress(0);
                }
            };
            worker.ProgressChanged += (s1, e1) =>
            {
                txtStatus1.Text = string.Format("Current Generation: {0}", algorithm.CurrentGeneration);
                txtStatus2.Text = string.Format("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            };
            worker.RunWorkerCompleted += (s1, e1) =>
            {
                try
                {
                    ContinuousVector finalSolution = algorithm.GlobalBestSolution;
                    NondominatedPopulation<ContinuousVector> archive = algorithm.NondominatedArchive;
                    Dictionary<int, List<double>> nonDominated = WhoIsTheBest(algorithm.objectives);
                    string final = string.Empty;
                    //foreach (var item in nonDominated)
                    {
                        //Dictionary<Model, int> cur = ((Dictionary<Model, int>)population[item.Key]);
                        Dictionary<Model, int> cur = ((Dictionary<Model, int>)population[0]);
                        string fragments = string.Empty;
                        foreach (var model in cur.Keys)
                        {
                            fragments += model.AbsoluteUri + ";";
                        }
                        //final += $"solution: {item.Key}, fragmens: {fragments} {Environment.NewLine}" +
                        final += $"solution: {new Random().Next(0, 100)}, fragmens: {fragments} {Environment.NewLine}" +
                        $"--------------{Environment.NewLine}";
                    }
                    DisplayParetoFront(archive);
                    MessageBox.Show(final);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
            worker.RunWorkerAsync();

            #endregion algorithm play
        }
      
        public void BibTexNSGAII<S>()
            where S : ContinuousVector, new()
        {
            #region variables
            int nPopulation = 100;
            Dictionary<Model, int> ModelFragmentVector = new Dictionary<Model, int>();
            List<Dictionary<Model, int>> population = new List<Dictionary<Model, int>>();
            System.Random rnd = new System.Random();
            #endregion

            //#region work with model
            var repository = new ModelRepository();

            //var model_b = repository.Resolve("b.xmi");
            //var classModel_b = model_b.RootElements[0] as ClassModel;

            //var model_c = repository.Resolve("c.xmi");
            //var classModel_c = model_c.RootElements[0] as ClassModel;

            //var model_d = repository.Resolve("d.xmi");
            //var classModel_d = model_d.RootElements[0] as ClassModel;

            //var model_e = repository.Resolve("e.xmi");
            //var classModel_e = model_e.RootElements[0] as ClassModel;

            //step 0: Create Random Model Fragments from Metamodel           
            //var metaModel = repository.Resolve("BibTeX.nmf");
            var metaModel = repository.Resolve("BibTeX.nmf");
            var test = repository.Resolve(@"D:\eclipse\Workspace2\jj\model\BibTeXFile.fsm");
            if (metaModel.RootElements.Count < 0)
            {
                //hichi bargard
            }


            #region Types

            int typesCount = ((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModel.RootElements).Items[0])).Types.Count;
            //extract all availabe permutations from types of rootelement
            List<byte> tempPermutationsInt = new List<byte>();
            for (byte i = 1; i <= typesCount; i++)
                tempPermutationsInt.Add(i);
            List<List<byte>> mainPermutationList = GetCombination(tempPermutationsInt);

            List<Model> MMFragments = new List<Model>();
            List<Model> MFragments = new List<Model>();
            var allTypes = (((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModel.RootElements).Items[0])).Types);
            // List<string> primitiveDataTypes = new List<string>();

            // loop over permutations and create a MM fragment based on permuration
            for (int i = 0; i < mainPermutationList.Count; i++)
            //Parallel.For(0, mainPermutationList.Count, i =>
            {
                Model myModel = new Model();
                #region rootElement
                NMF.Models.Meta.Namespace rootElement = new NMF.Models.Meta.Namespace();
                rootElement.Uri = metaModel.RootElements[0].AbsoluteUri;
                rootElement.Name = "MetaModelFragment" + i.ToString();
                //rootElement.IsFrozen = false;
                //rootElement.IsLocked = false;                    
                #endregion rootElement                    
                List<byte> current = mainPermutationList[i];
                if (mainPermutationList[i] != null)
                    for (int j = 0; j < mainPermutationList[i].Count; j++)
                    {
                        NMF.Models.Meta.Class currentType = (NMF.Models.Meta.Class)allTypes.ElementAt(current[j] - 1); //jth elemet which is clarifyed by current permutation                        
                        if (currentType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                        {
                            for (int k = 0; k < currentType.BaseTypes.Count; k++)
                            {
                                //add its parent[k] to rootElement                                
                                bool shouldAddBase = true;
                                for (int z = 0; z < rootElement.Types.Count; z++)
                                {
                                    if (rootElement.Types.ElementAt(z).Name == currentType.BaseTypes.ElementAt(k).Name)
                                        shouldAddBase = false;
                                }
                                try
                                {
                                    if (shouldAddBase)
                                    {
                                        var x = currentType.BaseTypes.ElementAt(k).DeepClone();
                                        //var x = FastDeepCloner.DeepCloner.Clone(currentType.BaseTypes.ElementAt(k));
                                        //var y = currentType.BaseTypes.ElementAt(k).DeepClone();
                                        rootElement.Types.Add(x);
                                        x = null;
                                    }
                                }
                                catch (Exception ex)
                                {
                                }

                            }
                        }
                        #region save Primitive data type Permurations
                        //if (currentType.Attributes.Count > 0)
                        //{
                        //    for (int k = 0; k < currentType.Attributes.Count; k++)
                        //    {
                        //        if (!primitiveDataTypes.Contains(currentType.Attributes.ElementAt(k).Type.Name.Trim()))
                        //            primitiveDataTypes.Add(currentType.Attributes.ElementAt(k).Type.Name.Trim());
                        //    }
                        //}
                        #endregion
                        //var x = NClone.Clone.ObjectGraph(currentType);
                        //var x = currentType.DeepClone();
                        bool shouldAdd = true;
                        for (int z = 0; z < rootElement.Types.Count; z++)
                        {
                            if (rootElement.Types.ElementAt(z).Name == currentType.Name)
                                shouldAdd = false;
                        }
                        try
                        {
                            if (currentType.Name.ToLower().Trim() == "bibtexfile" || currentType.Name.ToLower().Trim() == "authoredentry")
                            { }
                            if (shouldAdd)
                                rootElement.Types.Add(currentType.DeepClone());
                            //rootElement.Types.Add(FastDeepCloner.DeepCloner.Clone(currentType));
                        }
                        catch (Exception ex)
                        {
                        }
                        currentType = null;
                    }
                try
                {
                    myModel.RootElements.Add(rootElement);
                    repository.Save(myModel, @"Output\MMFragments\" + rootElement.Name + ".nmf");
                    repository.Save(rootElement, @"Output\MMFragments\" + rootElement.Name + ".xmi");
                    //var m = repository.Resolve(@"D:\MM\" + rootElement.Name + ".nmf");
                    myModel.RootElements.Clear();                    
                    current.Clear();
                    myModel = null;
                    current = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    Application.DoEvents();
                }
                catch (Exception ex)
                { }
                //MMFragments.Add(myModel);  

            }//);

            #region Creat Model Fragments form MM-Fragments and Primitive data values
            DirectoryInfo d = new DirectoryInfo("Output\\MMFragments");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.nmf").OrderBy(fi => fi.Name).ToArray();            
            //for (int i = 0; i < MMFragments.Count(); i++)
            foreach (FileInfo file in Files)
            {
                var metaModelFragmentFile = repository.Resolve(file.FullName);
                //NMF.Models.Meta.Namespace currentMetaModelRootElement = (NMF.Models.Meta.Namespace)MMFragments[i].RootElements[0]; MMFragments[i] -> metaModelFragmentFile
                NMF.Models.Meta.Namespace currentMetaModelRootElement = (NMF.Models.Meta.Namespace)metaModelFragmentFile.RootElements[0];
                var currentMMTypes = (((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModelFragmentFile.RootElements).Items[0])).Types);

                #region rootElement
                NMF.Models.Meta.Namespace rootElement = new NMF.Models.Meta.Namespace();
                rootElement.Uri = metaModel.RootElements[0].AbsoluteUri;
                //rootElement.Name = "ModelFragment" + i.ToString();
                rootElement.Name = currentMetaModelRootElement.Name.Replace("Meta", "");
                Model myModel = new Model();
                #endregion rootElement 

                for (int j = 0; j < currentMMTypes.Count; j++) //NamedElement
                {
                    NMF.Models.Meta.Class currentModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(j); //jth elemet which is clarifyed by current permutation
                    if (currentModelType.Attributes.Count == 0)
                    {
                        NMF.Models.Meta.Class newType = currentModelType.DeepClone();
                        rootElement.Types.Add(newType);
                        newType = null;
                    }
                    for (int z = 0; z < currentModelType.Attributes.Count; z++) //string name
                    {
                        switch (currentModelType.Attributes.ElementAt(z).Type.Name.Trim().ToLower())
                        {
                            case "string":
                                //Model myModel = new Model();
                                //#region rootElement
                                //NMF.Models.Meta.Namespace rootElement = new NMF.Models.Meta.Namespace();
                                //rootElement.Uri = metaModel.RootElements[0].AbsoluteUri;
                                //rootElement.Name = "ModelFragment" + j.ToString();
                                //#endregion rootElement 
                                if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                {
                                    for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                    {
                                        //add its parent[k] to rootElement                                
                                        bool shouldAddBase = true;
                                        for (int y = 0; y < rootElement.Types.Count; y++)
                                        {
                                            if (rootElement.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                shouldAddBase = false;
                                        }
                                        if (shouldAddBase)                                        
                                            rootElement.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());                                        
                                    }
                                }
                                NMF.Models.Meta.Class newType;
                                try
                                {
                                    newType = currentModelType.DeepClone();
                                }
                                catch (Exception ex)
                                { continue; }
                                newType.Attributes.ElementAt(z).DefaultValue = "random_" + newType.Attributes.ElementAt(z).Name;
                                bool shouldAdd = true;
                                for (int u = 0; u < rootElement.Types.Count; u++)
                                {
                                    if (rootElement.Types.ElementAt(u).Name == newType.Name)
                                        shouldAdd = false;
                                }
                                if (shouldAdd)
                                    rootElement.Types.Add(newType);
                                for (int l = 0; l < currentMMTypes.Count; l++)
                                {
                                    NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                    NMF.Models.Meta.Class newOtherType = null;
                                    try
                                    {
                                        newOtherType = otherModelType.DeepClone();
                                    }
                                    catch (Exception ex)
                                    { }
                                    shouldAdd = true;
                                    for (int u = 0; u < rootElement.Types.Count; u++)
                                    {
                                        if (rootElement.Types.ElementAt(u).Name == newOtherType.Name)
                                            shouldAdd = false;
                                    }
                                    if (shouldAdd)
                                        rootElement.Types.Add(newOtherType);

                                    newOtherType = null;
                                }
                                newType = null;                                
                                //myModel.RootElements.Add(rootElement);
                                //MFragments.Add(myModel);
                                break;
                            case "int":
                                for (int b = -1; b < 1; b++)
                                {
                                    //Model myModell = new Model();
                                    //#region rootElement
                                    //NMF.Models.Meta.Namespace rootElementt = new NMF.Models.Meta.Namespace();
                                    //rootElementt.Uri = metaModel.RootElements[0].AbsoluteUri;
                                    //rootElementt.Name = "ModelFragment" + j.ToString();
                                    //#endregion rootElement 
                                    if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                    {
                                        for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                        {
                                            //add its parent[k] to rootElement                                
                                            bool shouldAddBase = true;
                                            for (int y = 0; y < rootElement.Types.Count; y++)
                                            {
                                                if (rootElement.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                    shouldAddBase = false;
                                            }
                                            if (shouldAddBase)
                                                rootElement.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());
                                        }
                                    }
                                    NMF.Models.Meta.Class newTypet = currentModelType.DeepClone();
                                    newTypet.Attributes.ElementAt(z).DefaultValue = b.ToString();//"random_" + newTypet.Attributes.ElementAt(z).Name;
                                    bool shouldAddt = true;
                                    for (int u = 0; u < rootElement.Types.Count; u++)
                                    {
                                        if (rootElement.Types.ElementAt(u).Name == newTypet.Name)
                                            shouldAddt = false;
                                    }
                                    if (shouldAddt)
                                        rootElement.Types.Add(newTypet);
                                    for (int l = 0; l < currentMMTypes.Count; l++)
                                    {
                                        NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                        NMF.Models.Meta.Class newOtherType = otherModelType.DeepClone();
                                        shouldAddt = true;
                                        for (int u = 0; u < rootElement.Types.Count; u++)
                                        {
                                            if (rootElement.Types.ElementAt(u).Name == newOtherType.Name)
                                                shouldAddt = false;
                                        }
                                        if (shouldAddt)
                                            rootElement.Types.Add(newOtherType);

                                        newOtherType = null;
                                    }
                                    newType = null;
                                    //myModell.RootElements.Add(rootElement);
                                    //MFragments.Add(myModell);
                                }
                                break;
                            case "bool":
                                for (int b = 0; b <= 1; b++)
                                {
                                    //Model myModell = new Model();
                                    //#region rootElement
                                    //NMF.Models.Meta.Namespace rootElementt = new NMF.Models.Meta.Namespace();
                                    //rootElementt.Uri = metaModel.RootElements[0].AbsoluteUri;
                                    //rootElementt.Name = "ModelFragment" + j.ToString();
                                    //#endregion rootElement 
                                    if (currentModelType.BaseTypes.Count > 0) // has parent(s) and parent(s) should be added to MM fragments either
                                    {
                                        for (int k = 0; k < currentModelType.BaseTypes.Count; k++)
                                        {
                                            //add its parent[k] to rootElement                                
                                            bool shouldAddBase = true;
                                            for (int y = 0; y < rootElement.Types.Count; y++)
                                            {
                                                if (rootElement.Types.ElementAt(y).Name == currentModelType.BaseTypes.ElementAt(k).Name)
                                                    shouldAddBase = false;
                                            }
                                            if (shouldAddBase)
                                            {
                                                //var x = FastDeepCloner.DeepCloner.Clone(currentModelType.BaseTypes.ElementAt(k));
                                                //rootElement.Types.Add(x);
                                                rootElement.Types.Add(currentModelType.BaseTypes.ElementAt(k).DeepClone());
                                            }
                                        }
                                    }
                                    NMF.Models.Meta.Class newTypet = currentModelType.DeepClone();
                                    newTypet.Attributes.ElementAt(z).DefaultValue = (b == 0) ? "false" : "true";//"random_" + newTypet.Attributes.ElementAt(z).Name;
                                    bool shouldAddt = true;
                                    for (int u = 0; u < rootElement.Types.Count; u++)
                                    {
                                        if (rootElement.Types.ElementAt(u).Name == newTypet.Name)
                                            shouldAddt = false;
                                    }
                                    if (shouldAddt)
                                        rootElement.Types.Add(newTypet);
                                    for (int l = 0; l < currentMMTypes.Count; l++)
                                    {
                                        NMF.Models.Meta.Class otherModelType = (NMF.Models.Meta.Class)currentMMTypes.ElementAt(l); //jth elemet which is clarifyed by current permutation                        
                                        NMF.Models.Meta.Class newOtherType = otherModelType.DeepClone();
                                        shouldAddt = true;
                                        for (int u = 0; u < rootElement.Types.Count; u++)
                                        {
                                            if (rootElement.Types.ElementAt(u).Name == newOtherType.Name)
                                                shouldAddt = false;
                                        }
                                        if (shouldAddt)
                                            rootElement.Types.Add(newOtherType);

                                        newOtherType = null;
                                    }

                                    newTypet = null;
                                    //myModell.RootElements.Add(rootElement);
                                    //GC.Collect();
                                    //Application.DoEvents();
                                    //MFragments.Add(myModell);
                                }
                                break;
                        }
                    }
                    currentModelType = null;
                    #region save Primitive data type Permurations                                                                        
                }
                
                myModel.RootElements.Add(rootElement);
                repository.Save(myModel, @"Output\MFragments\" + rootElement.Name + ".nmf");
                myModel.RootElements.Clear();
                currentMMTypes.Clear();
                myModel = null;
                currentMetaModelRootElement = null;                
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Application.DoEvents();
                //MFragments.Add(myModel);
            }
            #endregion

            //NMF.Models.Meta.Class[] mmTypes = new NMF.Models.Meta.Class[typesCount];
            //List<NMF.Models.Meta.IType> mmTypes = new List<NMF.Models.Meta.IType>();
            //foreach (var item in ((NMF.Models.Meta.Namespace)(new NMF.Collections.Generic.EnumerableDebuggerProxy<NMF.Models.IModelElement>(metaModel.RootElements).Items[0])).Types)
            ////{
            ////    mmTypes.Add(item);
            ////}               

            #endregion
            //foreach (var type in ((NMF.Models.Meta.Namespace)metaModel.RootElements[0]).Types)
            //{
            //    // 1- if type has baseType : add to to MM-fragment
            //    MDEMGTT.ArchitectureCRA.Attribute attribute = new MDEMGTT.ArchitectureCRA.Attribute();
            //    attribute.Name = "aa";
            //    //model.SetAttributeValue()
            //    //repository.Models.Add()
            //    // 2- check for references
            //    // 2-1 iscontainment ==1 => 2 halat darad, ye seri mishan containements ke ovorde mishan, ye seri ham nayarim. all permutations
            //    // 2-2 other references => mese 2 halate bala
            //    // 3- check for attributes
            //    // 3-1 foreach attr -> make fragment for each type. include all permutations
            //    // 4- check for operations
            //    // 4-1 foreach operation -> make fragment for each method. include all permutations

            //}
            Files = null;
            d = null;
            DirectoryInfo dir = new DirectoryInfo("Output\\MFragments");//Assuming Test is your Folder
            FileInfo[] MFragmentsFiles = dir.GetFiles("*.nmf").OrderBy(fi => fi.Name).ToArray();
            //for (int i = 0; i < MMFragments.Count(); i++)
            foreach (FileInfo file in MFragmentsFiles)
            {
                var metaModelFragmentFile = repository.Resolve(file.FullName);
                ModelFragmentVector.Add(metaModelFragmentFile, 0); //temp                
            }            
            //Tree.RootElement = model;
            //step 1: Read All Fragments            
            //DirectoryInfo d = new DirectoryInfo("Inputs");//Assuming Test is your Folder
            //FileInfo[] Files = d.GetFiles("*.xmi");
            //foreach (FileInfo file in Files)
            //{
            //    //step2:Make solution space based on loaded Fragments                
            //    try
            //    {
            //        var fragmentModel = repository.Resolve(file.FullName);
            //        var fragmentClassModel = fragmentModel.RootElements[0] as ClassModel;  // for trace purpose only                
            //        ModelFragmentVector.Add(fragmentModel, 0); //temp
            //    }
            //    catch (Exception ex)
            //    { }
            //}
            //step2:Make solution space based on loaded Fragments            
            for (int i = 0; i < nPopulation; i++)
            {
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

            NSGAII nSGAII = new NSGAII(population, ModelFragmentVector, 100);
            List<Dictionary<Model, int>> res = nSGAII.Invoke();

            //foreach (Dictionary<Model, int> entry in res)    
            repository = new ModelRepository();
            for (int i = 0; i < res.Count; i++)
            {
                repository.Save(res[i].Keys.ElementAt(0), @"Output\GeneratedModels\generatedModel" + i.ToString() +".nmf");
                //DisplayParetoFront(res[i].Keys.ElementAt(0), nSGAII.FinalePareto);
            }
            DisplayParetoFront(res, nSGAII.FinalePareto);
            return;

            MDEMGTT.Algorithms.NSGAII algorithm = new MDEMGTT.Algorithms.NSGAII(new CRAProblem(population, ModelFragmentVector));

            algorithm.PopulationSize = 100;

            algorithm.Initialize();

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (s1, e1) =>
            {
                while (!algorithm.IsTerminated)
                {
                    algorithm.Evolve();
                    worker.ReportProgress(0);
                }
            };
            worker.ProgressChanged += (s1, e1) =>
            {
                txtStatus1.Text = string.Format("Current Generation: {0}", algorithm.CurrentGeneration);
                txtStatus2.Text = string.Format("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            };
            worker.RunWorkerCompleted += (s1, e1) =>
            {
                try
                {
                    ContinuousVector finalSolution = algorithm.GlobalBestSolution;
                    NondominatedPopulation<ContinuousVector> archive = algorithm.NondominatedArchive;
                    Dictionary<int, List<double>> nonDominated = WhoIsTheBest(algorithm.objectives);
                    string final = string.Empty;
                    //foreach (var item in nonDominated)
                    {
                        //Dictionary<Model, int> cur = ((Dictionary<Model, int>)population[item.Key]);
                        Dictionary<Model, int> cur = ((Dictionary<Model, int>)population[0]);
                        string fragments = string.Empty;
                        foreach (var model in cur.Keys)
                        {
                            fragments += model.AbsoluteUri + ";";
                        }
                        //final += $"solution: {item.Key}, fragmens: {fragments} {Environment.NewLine}" +
                        final += $"solution: {new Random().Next(0, 100)}, fragmens: {fragments} {Environment.NewLine}" +
                        $"--------------{Environment.NewLine}";
                    }
                    DisplayParetoFront(archive);
                    MessageBox.Show(final);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
            worker.RunWorkerAsync();

            #endregion algorithm play
        }

        private void DisplayParetoFront(List<Dictionary<Model, int>> res, Dictionary<Dictionary<Model, int>, List<double>> finalePareto)
        {
            GraphPane pane = chtParetoFront.GraphPane;
            pane.XAxis.Title.Text = "Maximizing MM";
            pane.YAxis.Title.Text = "Maximizing Different";
            pane.Title.Text = string.Format("Problem: {0}", (ProblemType)cboProblem.SelectedItem);

            PointPairList list = new PointPairList();

            DataTable table = new DataTable();
            table.Columns.Add("#");
            table.Columns.Add(string.Format("Objective {0}", 0));
            table.Columns.Add(string.Format("Objective {0}", 1));
            List<object> values = new List<object>();

            for (int i = 0; i < res.Count; i++)
            {
                var temp = finalePareto.Where(x => x.Key == res[i]);
                list.Add(temp.FirstOrDefault().Value[0], temp.FirstOrDefault().Value[0]);
                foreach (var item in list)
                {
                    table.Rows.Add(item);
                }                
            }                
            AlgorithmType algorithm_type = (AlgorithmType)cboAlgorithm.SelectedItem;
            LineItem myCurve = pane.AddCurve(string.Format("{0} ({1})", algorithm_type.ToString(), cboProblem.SelectedItem), list, GetParetoFrontColor());
            myCurve.Symbol.Type = ZedGraph.SymbolType.Plus;
            myCurve.Line.IsVisible = false;

            pane.AxisChange();
            chtParetoFront.Invalidate();
            
            dgvParetoFront.DataSource = table;
        }

        private Dictionary<int, List<double>> WhoIsTheBest(List<List<double>> objectives)
        {
            Dictionary<int, List<double>> nonDominated = new Dictionary<int, List<double>>();
            List<double> temp = new List<double>();
            for (int i = 0; i < objectives.Count; i++)
            {
                double firstObj = objectives[i][0];
                double secondObj = objectives[i][1];
                bool shouldAdd = true;
                for (int j = i + 1; j < objectives.Count; j++)
                {
                    if (firstObj > objectives[j][0] && secondObj > objectives[j][1])
                    {
                        shouldAdd = false;
                        break;
                    }
                }
                if (shouldAdd)
                    nonDominated.Add(i, objectives[i]);
            }
            return nonDominated;
        }

        public IMOOProblem GetProblem()
        {
            ProblemType problem_type = (ProblemType)cboProblem.SelectedItem;
            //if (problem_type == ProblemType.NDND)
            //{
            //    return new NDNDProblem();
            //}
            //else if (problem_type == ProblemType.NGPD)
            //{
            //    return new NGPDProblem();
            //}
            //else if (problem_type == ProblemType.TNK)
            //{
            //    return new TNKProblem();
            //}
            //else if (problem_type == ProblemType.OKA2)
            //{
            //    return new OKA2Problem();
            //}
            //else if (problem_type == ProblemType.SYMPART)
            //{
            //    return new SYMPARTProblem();
            //}
            return null;
        }

        private void ClearStatusInfo()
        {
            txtStatus1.Text = "";
            txtStatus2.Text = "";
            txtStatus2.Text = "";
        }

        public void RunGDE3<S>()
            where S : ContinuousVector, new()
        {
            GDE3<S> algorithm = new GDE3<S>(GetProblem());

            algorithm.PopulationSize = 100;

            algorithm.Initialize();

            ClearStatusInfo();

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (s1, e1) =>
            {
                while (!algorithm.IsTerminated)
                {
                    algorithm.Evolve();
                    worker.ReportProgress(0);
                }
            };
            worker.ProgressChanged += (s1, e1) =>
            {
                txtStatus1.Text = string.Format("Current Generation: {0}", algorithm.CurrentGeneration);
                txtStatus2.Text = string.Format("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            };
            worker.RunWorkerCompleted += (s1, e1) =>
            {
                NondominatedPopulation<S> archive = algorithm.NondominatedArchive;
                DisplayParetoFront(archive);
            };
            worker.RunWorkerAsync();
        }

        public void RunHAPMOEA<S>()
            where S : ContinuousVector, new()
        {
            HAPMOEA<S> algorithm = new HAPMOEA<S>(GetProblem());

            algorithm.Config.PopulationSize = 100;

            algorithm.Initialize();

            ClearStatusInfo();

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (s1, e1) =>
            {
                while (!algorithm.IsTerminated)
                {
                    algorithm.Evolve();
                    worker.ReportProgress(0);
                }
            };
            worker.ProgressChanged += (s1, e1) =>
            {
                txtStatus1.Text = string.Format("Current Generation: {0}", algorithm.CurrentGeneration);
                txtStatus2.Text = string.Format("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            };
            worker.RunWorkerCompleted += (s1, e1) =>
            {
                NondominatedPopulation<S> archive = algorithm.NondominatedArchive;
                DisplayParetoFront(archive);
            };
            worker.RunWorkerAsync();
        }

        public void RunHybridGame<S>()
            where S : ContinuousVector, new()
        {
            HybridGame<S> algorithm = new HybridGame<S>(GetProblem());

            algorithm.Config.PopulationSize = 100;

            algorithm.Initialize();

            ClearStatusInfo();

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (s1, e1) =>
            {
                while (!algorithm.IsTerminated)
                {
                    algorithm.Evolve();
                    worker.ReportProgress(0);
                }
            };
            worker.ProgressChanged += (s1, e1) =>
            {
                txtStatus1.Text = string.Format("Current Generation: {0}", algorithm.CurrentGeneration);
                txtStatus2.Text = string.Format("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            };
            worker.RunWorkerCompleted += (s1, e1) =>
            {
                NondominatedPopulation<S> archive = algorithm.NondominatedArchive;
                DisplayParetoFront(archive);
            };
            worker.RunWorkerAsync();
        }

        public void RunNSGAII<S>()
            where S : ContinuousVector, new()
        {
            NSGAII<S> algorithm = new NSGAII<S>(GetProblem());

            algorithm.PopulationSize = 100;

            algorithm.Initialize();

            ClearStatusInfo();

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (s1, e1) =>
            {
                while (!algorithm.IsTerminated)
                {
                    algorithm.Evolve();
                    worker.ReportProgress(0);
                }
            };
            worker.ProgressChanged += (s1, e1) =>
            {
                txtStatus1.Text = string.Format("Current Generation: {0}", algorithm.CurrentGeneration);
                //Console.WriteLine("Number of Dominating Improvements: {0}", algorithm.NumberOfDominatingImprovements);
                txtStatus2.Text = string.Format("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            };
            worker.RunWorkerCompleted += (s1, e1) =>
            {
                NondominatedPopulation<S> archive = algorithm.NondominatedArchive;
                DisplayParetoFront(archive);
            };
            worker.RunWorkerAsync();
        }

        private Color GetParetoFrontColor()
        {
            AlgorithmType algorithm_type = (AlgorithmType)cboAlgorithm.SelectedItem;
            if (algorithm_type == AlgorithmType.HybridGame)
            {
                return Color.Blue;
            }
            else if (algorithm_type == AlgorithmType.NSGAII)
            {
                return Color.Green;
            }
            else if (algorithm_type == AlgorithmType.HAPMOEA)
            {
                return Color.Orange;
            }
            else if (algorithm_type == AlgorithmType.GDE3)
            {
                return Color.Pink;
            }
            return Color.Black;
        }

        private void DisplayParetoFront<S>(NondominatedPopulation<S> archive)
            where S : ContinuousVector, new()
        {
            GraphPane pane = chtParetoFront.GraphPane;
            //pane.XAxis.Title.Text = "Objective 1";
            //pane.YAxis.Title.Text = "Objective 2";
            pane.XAxis.Title.Text = "Maximizing MM";
            pane.YAxis.Title.Text = "Maximizing Different";
            pane.Title.Text = string.Format("Problem: {0}", (ProblemType)cboProblem.SelectedItem);

            PointPairList list = new PointPairList();

            foreach (S s in archive.Solutions)
            {
                list.Add(s.FindObjectiveAt(0), s.FindObjectiveAt(1));
            }

            AlgorithmType algorithm_type = (AlgorithmType)cboAlgorithm.SelectedItem;
            LineItem myCurve = pane.AddCurve(string.Format("{0} ({1})", algorithm_type.ToString(), cboProblem.SelectedItem), list, GetParetoFrontColor());
            myCurve.Symbol.Type = ZedGraph.SymbolType.Plus;
            myCurve.Line.IsVisible = false;

            pane.AxisChange();
            chtParetoFront.Invalidate();

            DataTable table = new DataTable();
            table.Columns.Add("#");
            IMOOProblem problem = archive[0].Problem;
            for (int i = 0; i < problem.GetObjectiveCount(); ++i)
            {
                table.Columns.Add(string.Format("Objective {0}", i + 1));
            }
            for (int i = 0; i < problem.GetDimensionCount(); ++i)
            {
                table.Columns.Add(string.Format("x[{0}]", i));
            }

            for (int i = 0; i < archive.Count; ++i)
            {
                S s = archive[i];
                List<object> values = new List<object>();
                values.Add(i + 1);
                for (int j = 0; j < problem.GetObjectiveCount(); ++j)
                {
                    values.Add(s.FindObjectiveAt(j));
                }
                for (int j = 0; j < problem.GetDimensionCount(); ++j)
                {
                    values.Add(s[j]);
                }
                table.Rows.Add(values.ToArray());
            }

            dgvParetoFront.DataSource = table;
        }

        private void FrmMOEA_Load(object sender, EventArgs e)
        {
            cboAlgorithm.DataSource = Enum.GetValues(typeof(AlgorithmType));
            cboProblem.DataSource = Enum.GetValues(typeof(ProblemType));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            GraphPane pane = chtParetoFront.GraphPane;
            pane.CurveList.Clear();
            chtParetoFront.Invalidate();
        }

        private void cboProblem_SelectedIndexChanged(object sender, EventArgs e)
        {
            string problem_name = cboProblem.SelectedItem.ToString();
            string filename = string.Format("{0}.htm", problem_name);
            if (File.Exists(filename))
            {
                wbProblem.Navigate(string.Format("file://{0}", Path.GetFullPath(filename)));
            }
        }

        List<List<byte>> GetCombination(List<byte> list)
        {
            long count = Convert.ToInt64(Math.Pow(2, list.Count));
            //List<int> currentPermutation = new List<int>();
            List<List<byte>> finalResult = new List<List<byte>>();
            for (int i = 1; i <= count - 1; i++)
            //Parallel.For(1, count - 1, i =>
            {
                List<byte> currentPermutation = new List<byte>();
                string str = Convert.ToString(i, 2).PadLeft(list.Count, '0');
                for (byte j = 0; j < str.Length; j++)
                //Parallel.For(0, str.Length -1, j =>
                {
                    if (str[j] == '1')
                    {
                        //Console.Write(list[j]);
                        currentPermutation.Add(list[j]);
                    }
                }//);
                finalResult.Add(currentPermutation);
                GC.Collect();
                currentPermutation = new List<byte>();
            }
            //});

            return finalResult;
        }
    }
    public static class ObjectExtension

    {

        public static object CloneObject(this object objSource)

        {

            //Get the type of source object and create a new instance of that type

            System.Type typeSource = objSource.GetType();

            object objTarget = Activator.CreateInstance(typeSource);



            //Get all the properties of source object type

            System.Reflection.PropertyInfo[] propertyInfo = typeSource.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);



            //Assign all source property to taget object 's properties

            foreach (System.Reflection.PropertyInfo property in propertyInfo)

            {

                //Check whether property can be written to

                if (property.CanWrite)

                {

                    //check whether property type is value type, enum or string type

                    if (property.PropertyType.IsValueType || property.PropertyType.IsEnum || property.PropertyType.Equals(typeof(System.String)))

                    {

                        property.SetValue(objTarget, property.GetValue(objSource, null), null);

                    }

                    //else property type is object/complex types, so need to recursively call this method until the end of the tree is reached

                    else

                    {

                        object objPropertyValue = property.GetValue(objSource, null);

                        if (objPropertyValue == null)

                        {

                            property.SetValue(objTarget, null, null);

                        }

                        else

                        {

                            property.SetValue(objTarget, objPropertyValue.CloneObject(), null);

                        }

                    }

                }

            }

            return objTarget;

        }
    }
}
