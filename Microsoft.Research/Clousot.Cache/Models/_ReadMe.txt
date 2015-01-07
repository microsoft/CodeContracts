The types here are POCO types and need to be public in order for the EntityFramework to do proper proxying and lazy loading.

Since we use ILMerge with /internalize, we need to make sure all the Model types stay public after ilmerging. 
The ClousotILMerge.internexceptions file in the ManagedContracts.Setup directory contains these types. When adding new types,
make sure they are covered by the patterns in there. Currently, we keep everything in the following namespace public:

Microsoft.Research.CodeAnalysis.Caching.Models.*

