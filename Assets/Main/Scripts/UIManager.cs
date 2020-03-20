using UnityEngine;
using UnityEngine.UI;


namespace GiantMonkey
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Text titleLabel = null;
        [SerializeField] private TableCell headerPrefab = null;
        [SerializeField] private TableCell dataPrefab = null;
        [SerializeField] private Transform tableSpace = null;
        [SerializeField] private GameObject rowPrefab = null;
        

        public void CreateTable(Table table)
        {
            if (IsTableDirty())
            {
                CleanTable();
            }                

            if (!IsTableValid(table))
            {
                Debug.LogError("Table data is invalid!");
                return;
            }

            titleLabel.text = table.Title;
            SpawnCells( table.ColumnHeaders , headerPrefab);
            SpawnCells( table.Data.ToArray() , dataPrefab , table.ColumnHeaders.Length );
            
        }

        private bool IsTableDirty()
        {
            return tableSpace.childCount > 0;
        }

        private void CleanTable()
        {
            for (int n = 0; n < tableSpace.childCount; n++)
            {
                Destroy(tableSpace.GetChild(n).gameObject);
            }
        }

        private bool IsTableValid(Table table)
        {            
            if ( !IsTableHeaderValid( table ) )
                return false;
            
            if ( !IsTableDataValid(table) )
                return false;

            return true;
        }

        private bool IsTableHeaderValid(Table table)
        {
            return table.ColumnHeaders.Length > 0;
        }

        private bool IsTableDataValid( Table table )
        {
            return table.Data.Count % table.ColumnHeaders.Length == 0;
        }


        private void SpawnCells( string[] values , TableCell prefab , int headerSize = int.MaxValue )
        {
            int index = 0;

            headerSize = Mathf.Min(headerSize , values.Length);

            while (index < values.Length)
            {
                Transform parent = CreateRow().transform;                        

                for (int j = 0; j < headerSize; j++)
                {
                    TableCell cell = Instantiate(prefab);
                    cell.transform.parent = parent;
                    cell.transform.localScale = Vector3.one;                   
                    cell.Construct(values[index]);
                    index++;
                }
            }            
        }       

        private GameObject CreateRow()
        {
            GameObject go = Instantiate(rowPrefab);
            go.transform.parent = tableSpace;
            go.transform.localScale = Vector3.one;

            return go;
        }

    }

}
