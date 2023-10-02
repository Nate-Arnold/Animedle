using AnimedleWeb.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;

namespace AnimedleWeb.API
{
    /// <summary>
    /// Gateway class to communicate with the AniList API
    /// </summary>
    public class AniList
    {
        /// <summary>
        /// Retreives the top anime(by popularity) from AniList
        /// </summary>
        /// <returns>The top anime results as an AniListResults object</returns>
        public AniListResults GetTopAnime()
        {
            string pythonScriptLocation = "D:\\Coding\\AnimedleWeb\\API\\AniListQuery.py";
            string arguments = string.Format("{0} {1}", pythonScriptLocation, 1);

            ProcessStartInfo processInfo = new ProcessStartInfo("python3")
            {
                //Make sure we can read the output from stdout 
                UseShellExecute = false,
                RedirectStandardOutput = true,

                Arguments = arguments
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