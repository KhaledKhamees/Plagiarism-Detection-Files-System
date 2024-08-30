using System.Reflection.Metadata.Ecma335;

namespace Plagiarism_Detection_Files_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, To Plagiarism Detection Files System!");
            Console.WriteLine("Please Enter the Directory Path");
            string DierctoryPath = Console.ReadLine();
            DirectoryInfo directoryInfo= new DirectoryInfo(DierctoryPath);
            if (directoryInfo.Exists == false )
            {
                Console.WriteLine("No files here , please enter the correct Path");
                return;
            }
            var filesList = directoryInfo.GetFiles("*.txt"); 
            if(filesList.Length < 2 )
            {
                Console.WriteLine("Not enough files to compare. Please ensure the directory contains at least two text files.");
                return; 
            }
            List<CosineSimilarity> cosineSimilaritiesList = new List<CosineSimilarity>();  
            foreach ( FileInfo file in filesList )
            {
                CosineSimilarity cosineSimilarity = new CosineSimilarity();
                cosineSimilarity.GetTextFromFile(file);
                cosineSimilarity.VectorizeText();
                cosineSimilaritiesList.Add(cosineSimilarity);
            }

            Console.WriteLine("\nPairwise Cosine Similarity (1 indicates identical files, while 0 indicates completely different files):");

            for(int i = 0;i < cosineSimilaritiesList.Count;i++)
            {
                for(int j =i+1; j<cosineSimilaritiesList.Count; j++)
                {
                    double similarity = cosineSimilaritiesList[i].CalculateCosineSimilarity(cosineSimilaritiesList[j]);
                    Console.WriteLine($"The Cosine Similarity between {filesList[i].Name} and {filesList[j].Name} = {similarity:F4}");
                }
            }
            Console.WriteLine("Processing completed.");
            Console.ReadLine();
        }
    }
}
