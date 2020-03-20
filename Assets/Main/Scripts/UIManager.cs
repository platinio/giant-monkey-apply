using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GiantMonkey
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TableCell headerPrefab = null;
        [SerializeField] private TableCell dataPrefab = null;
        [SerializeField] private Transform headerParent = null;

        public void CreateTable(Table table)
        {
            for (int n = 0; n < table.ColumnHeaders.Length; n++)
            {
                TableCell cell = Instantiate( headerPrefab );
                cell.transform.parent = headerParent;
                cell.transform.localScale = Vector3.one;
                cell.Construct( table.ColumnHeaders[n] );
            }
        }
    }

}
