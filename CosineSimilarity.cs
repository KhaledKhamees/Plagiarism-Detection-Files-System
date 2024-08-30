using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Plagiarism_Detection_Files_System
{
    internal class CosineSimilarity
    {
        private string Text;
        Dictionary<string, double> VectorValues = new Dictionary<string, double>();
        public void GetTextFromFile(FileInfo file)
        {
            try
            {
                string text = File.ReadAllText(file.FullName);
                Text = text;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
        }
        public void VectorizeText()
        {
            if(string.IsNullOrEmpty(Text))
            {
                Console.WriteLine("Text is empty. Please load a file first.");
                return;
            }
            var words = Regex.Replace(Text.ToLower(), @"\p{P}", "").Split(' ', StringSplitOptions.RemoveEmptyEntries);
            VectorValues = words.GroupBy(word => word).ToDictionary(group => group.Key, group => (double)group.Count());
        }
        public double CalculateCosineSimilarity(CosineSimilarity other)
        {
            double dotProduct = 0.0;
            double magnitudeA = 0.0;
            double magnitudeB = 0.0;
            foreach(var pair in VectorValues) 
            {
                double valueB = other.VectorValues.ContainsKey(pair.Key) ? other.VectorValues[pair.Key] :0.0 ;
                dotProduct += pair.Value * valueB;
                magnitudeA += Math.Pow(pair.Value, 2);
            }
            foreach(var value in other.VectorValues.Values)
            {
                magnitudeB += Math.Pow(value, 2);
            }
            magnitudeA = Math.Sqrt(magnitudeA);
            magnitudeB = Math.Sqrt(magnitudeB);
            return dotProduct / (magnitudeA * magnitudeB);
        }
    }
}
