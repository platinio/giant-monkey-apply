using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;
using System.IO;
using System;

namespace GiantMonkey
{
    public class JsonHandler : MonoBehaviour
    {
        
        private UIManager UIManager = null;

        private void Awake()
        {
            UIManager = FindObjectOfType<UIManager>();           
        }

        public void DeserializeJsonFile()
        {
            string path = Path.Combine( ApplicationConst.JsonFilePath );
            StreamReader reader = new StreamReader(path);
            string json = StringToJson(reader.ReadToEnd());
            reader.Close();

            JSONNode rootNode = null;

            try
            {
                rootNode = JSON.Parse(json);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return;
            }

            if (rootNode == null)
                return;

            Table table = new Table(rootNode);
            UIManager.CreateTable(table);
        }

        private string StringToJson(string txt)
        {
            return txt.Replace(System.Environment.NewLine , "");
        }

    }

    public class Table
    {
        public string Title { get; set; }
        public string[] ColumnHeaders { get; set; }
        public List<string> Data { get; set; }
       
        public Table(JSONNode rootNode)
        {
            
            Title = rootNode["Title"];
            JSONNode ColumnHeadersNode = rootNode["ColumnHeaders"];            
            ColumnHeaders = new string[ColumnHeadersNode.Count];

            for (int n = 0; n < ColumnHeadersNode.Count; n++)
            {
                ColumnHeaders[n] = ColumnHeadersNode[n].Value;               
            }

            JSONNode DataNode = rootNode["Data"];
            Data = new List<string>();
           
            for (int n = 0; n < DataNode.Count; n++)
            {
                for (int j = 0; j < DataNode[n].Count ; j++  )
                {
                    Data.Add(DataNode[n][j].Value);                    
                }
            }



        }

    }


}

