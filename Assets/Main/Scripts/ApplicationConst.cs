using System.IO;
using UnityEngine;

namespace GiantMonkey
{
    public static class ApplicationConst
    {
        public static string JsonFilePath 
        {
            get 
            {
                if(jsonFilePath == null)
                    jsonFilePath = Path.Combine(Application.streamingAssetsPath, "JsonChanllenge.json");
                return jsonFilePath;
            }
        }

        private static string jsonFilePath = null;

    }

}
