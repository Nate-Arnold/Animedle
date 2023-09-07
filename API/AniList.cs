using AnimedleWeb.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;

namespace AnimedleWeb.API
{
    public class AniList
    {
        public AniListResults GetTopAnime()
        {
            string pythonScriptLocation = "D:\\Coding\\AnimedleWeb\\API\\AniListQuery.py";
            ProcessStartInfo processInfo = new ProcessStartInfo("python3")
            {
                //Make sure we can read the output from stdout 
                UseShellExecute = false,
                RedirectStandardOutput = true,

                Arguments = pythonScriptLocation
            };

            Process getTopAnime = new Process();

            //Assign start information to the process 
            getTopAnime.StartInfo = processInfo;

            //Start the process 
            getTopAnime.Start();

            //Read the output of python script
            StreamReader myStreamReader = getTopAnime.StandardOutput;
            string? processResult = myStreamReader.ReadLine();

            //Wait for process to end and close it
            getTopAnime.WaitForExit();
            getTopAnime.Close();

            //Print out the results retrieved from the StreamReader
            Console.WriteLine(processResult);

            //Convert the JSON string to an AniListResults Object
            AniListResults results = JsonConvert.DeserializeObject<AniListResults>(processResult);

            return results;
        }
    }
}