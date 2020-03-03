# MDEMGTT

This project is about input test case generation for model transformation testing in the field of Model-Driven Software Engineering. 
This is an implementation of a submitted paper in ECMFA 2020 titled "Test case generation for model transformation testing: a multi-objective approach".

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

For running the program, you need to install/have following prerequisites:
	1- .Net Framework 4.5.2 or higher
	2- Microsoft Visual Studio or any preferred IDE for .net projects.
	3- ```.Net Modelling Framework (NMF)``` Package. Find more about NMF package [here](https://github.com/NMFCode/NMF).
	4- A meta-model that has been created in ```Ecore``` for testing purpose. The generated models conform the given meta-model.

### Installing

To install the project, you need to do following steps:
	1- Download the source code from github repository [here](https://github.com/MeysamKarimi/MDEMGTT).
	2- Open it in your preferred IDE. I will explain rest of steps in Visual Studio 2019.
	3- Install .NMF Modelling Framework via Solution NuGet Package Manager or via NuGet Package Manager Console by following command. You can take a look at NMF configuration []here] (https://www.nuget.org/packages/NMF-Basics/). It can be easily installed via NuGet.
	```
	PM> Install-Package NMF-Basics -Version 2.0.157 
	```	
	4- Convert your input Ecore meta-model to NMF meta-model. For doing this:	
		4-1- Copy ```Ecore``` meta-model to root of the MDEMGTT folder in the solution.
		4-2- Run the following command in NuGet Package Manager Console:
				```
				PM> Ecore2Code -f -n MDEMGTT.Metamodels -m PetriNet.nmf -o Metamodels\PetriNets PetriNet.ecore
				```	
		4-2-3 Include generated NMF meta-model in Solution in VS and make it as ```Embedded Resource```.
		4-2-4 Include  generated NMF meta-model classes in you Solution in VS.		
		4-2-5 Open ```MDEMGTT\Properties\AssemblyInfo.cs``` file and add append following line:
			```
			[assembly: ModelMetadata("http://petrinet/1.0", "MDEMGTT.PetriNet.nmf")]
			```
			** Please note that**, ```PetriNet``` is an example. Replace it with your input meta-model.
	5- Create a folder with name ```Output``` in bin/Debug folder and make three empty folders in that folder:
			```
			1- MMFragments //generated meta-model fragments save here
			2- MFragments //generated model fragments save here
			3- GeneratedModels //final generated models save here
			```
	6- Build the solution. You should get ```Build Successfully``` message.

## Running the tests

Just run the program and select one of the meta-models and your algorithm. In the paper, ```NSGA-II``` has been developed. You can develop any algorithm you like.

### Break down into end to end tests

The generated models in ``` Output\GeneratedModels``` are in ```NMF``` file format and can be easily using other projects.
However, I am working hard to generate ``XMI``` extension because it is a well-known file format when using ```Ecore``` models.

## Contributing

Please contact [me](mailto:Meysam.Karimi84@gmail.com) if you like to contribute on this project.

## Versioning

This is the very first version of the project.

## Authors

* **Meysam Karimi** - *Owner* - [Contact](https://github.com/MeysamKarimi)

See also the list of [contributors](https://github.com/MeysamKarimi/MDEMGTT/graphs/contributors) who participated in this project.

## License

This project is licensed under the MIT License.

## Acknowledgments

* Really thanks to my dear professors, [Shekoufeh Kolahdouz-Rahimi](https://mdse.ui.ac.ir/member/shekoufeh-kolahdouz-rahimi/) and [Javier Troya](http://www.lsi.us.es/~jtroya/) for inspiring and helping me to propose this approach.


