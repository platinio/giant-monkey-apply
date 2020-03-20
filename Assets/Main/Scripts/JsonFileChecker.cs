using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace GiantMonkey
{
    public class JsonFileChecker : MonoBehaviour
    {
        [SerializeField] private UnityEvent onFileChanged = null;

        private DateTime lastFileWriteDate;
        private const float checkRate = 0.5f;

        private void Start()
        {
            lastFileWriteDate = new DateTime(1 , 1 , 1);
            StartCoroutine( FileCheckRoutine() );
        }

        private IEnumerator FileCheckRoutine()
        {
            while (true)
            {
                if (FileHasChanged(ApplicationConst.JsonFilePath))
                {
                    lastFileWriteDate = File.GetLastWriteTime( ApplicationConst.JsonFilePath ); ;
                    onFileChanged.Invoke();
                }
                yield return new WaitForSeconds( checkRate );
            }
        }

        private bool FileHasChanged(string path)
        {
            DateTime currentWriteTime = File.GetLastWriteTime(path);
            int comp = DateTime.Compare(currentWriteTime, lastFileWriteDate);
            return comp > 0;
        }


    }

}
