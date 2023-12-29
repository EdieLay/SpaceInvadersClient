namespace SpaceInvadersClient
{
    public class DataManager
    {
        const string ResultsFile = "Results.txt";

        public DataManager() 
        {
            if (!File.Exists(ResultsFile))
            {
                File.Create(ResultsFile);
            }
        }

        public void AddResult(int score)
        {
            using StreamWriter ofstream = new(ResultsFile, true);
            ofstream.WriteLine($"{DateTime.Now} : {score}");
        }
        public string GetResults()
        {
            using StreamReader ofstream = new StreamReader(ResultsFile);
            string results = String.Empty;
            while (ofstream.EndOfStream == false)
            {
                results = string.Concat(results, ofstream.ReadLine(), Environment.NewLine);
            }
            return results;
        }
    }
}
