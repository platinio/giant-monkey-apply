
using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;

namespace GiantMonkey
{
    public class JsonHandler : MonoBehaviour
    {
        [SerializeField] private TextAsset json = null;

        private UIManager UIManager = null;

        private void Awake()
        {
            UIManager = FindObjectOfType<UIManager>();
        }

        private void Start()
        {
            string jsonString = TextAssetToJson(json);
            Table table = new Table(jsonString);
            UIManager.CreateTable(table);
        }

        private string TextAssetToJson(TextAsset asset)
        {
            return asset.text.Replace(System.Environment.NewLine , "");
        }

    }

    public class Table
    {
        public string Title { get; set; }
        public string[] ColumnHeaders { get; set; }
        public string[] Data { get; set; }

        public Table(string json)
        {
            JSONNode rootNode = JSON.Parse(json);            
            JSONNode ColumnHeadersNode = rootNode["ColumnHeaders"];
            ColumnHeaders = new string[ColumnHeadersNode.Count];

            for (int n = 0; n < ColumnHeadersNode.Count; n++)
            {
                ColumnHeaders[n] = ColumnHeadersNode[n].Value;               
            }

            JSONNode DataNode = rootNode["Data"];
            Data = new string[ DataNode.Count * DataNode[0].Count ];
            int dataIndex = 0;

            for (int n = 0; n < DataNode.Count; n++)
            {
                for (int j = 0; j < DataNode[n].Count ; j++  )
                {
                    Data[dataIndex] = DataNode[n][j].Value;                    
                    dataIndex++;
                }
            }



        }

    }


}

