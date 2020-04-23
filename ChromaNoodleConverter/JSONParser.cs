using SimpleJson;
using System;
using System.Collections.Generic;
using System.IO;

namespace ChromaNoodleConverter
{
    internal class JSONParser
    {
        private int convertedCount = 0;

        public JSONParser(String[] inputData)
        {
            foreach (String path in inputData)
            {
                if (Directory.Exists(path))
                {
                    ProcessDirectory(path);
                }
                else
                {
                    Console.WriteLine("Invalid directory");
                }
            }
            if (convertedCount > 0)
                Console.WriteLine("Converted " + convertedCount + " maps");
            else
                Console.WriteLine("No maps were converted\n" +
                    "Please add \"Chroma\" or \"Noodle Extensions\" to the difficulties you want converted in the info.dat\n" +
                    "or these were already converted"
                    );
        }

        public void ProcessDirectory(string targetDirectory)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                if (fileName.EndsWith("info.dat"))
                    ProcessFile(fileName, targetDirectory);
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }

        public void ProcessFile(string path, string targetDirectory)
        {
            JSONNode info = JSON.Parse(File.ReadAllText(path));
            foreach (JSONNode characteristics in info["_difficultyBeatmapSets"])
            {
                foreach (JSONNode diff in characteristics["_difficultyBeatmaps"])
                {
                    List<string> suggestionsArray = GetList(diff["_customData"]["_suggestions"]);
                    List<string> requirementsArray = GetList(diff["_customData"]["_requirements"]);
                    string filePath = targetDirectory + "\\" + diff["_beatmapFilename"];
                    if (!File.Exists(filePath))
                    {
                        Console.WriteLine("ERROR: File listed in info.dat not found");
                        continue;
                    }
                    JSONNode originalMap = JSON.Parse(File.ReadAllText(filePath));
                    JSONNode newMap = originalMap;
                    if (suggestionsArray.Exists(x => x.Equals("Chroma")) || requirementsArray.Exists(x => x.Equals("Chroma")))
                    {
                        //newMap = new ChromaConverter(newMap).start();
                        //convertedCount++;
                    }

                    if (suggestionsArray.Exists(x => x.Equals("Noodle Extensions")) || requirementsArray.Exists(x => x.Equals("Noodle Extensions")))
                    {
                        newMap = new NoodleConverter(newMap).start();
                        convertedCount++;
                    }
                    if (convertedCount > 0)
                    {
                        //todo - copy everything to a new folder instead of appending _new
                        File.WriteAllText(filePath.Replace("\"", "").Replace(".dat", "") + "_new.dat", newMap.ToString());
                    }
                }
            }
        }

        public List<string> GetList(JSONNode N)
        {
            List<string> l = new List<string>();
            foreach (JSONString value in N)
            {
                l.Add(value);
            }
            return l;
        }
    }
}