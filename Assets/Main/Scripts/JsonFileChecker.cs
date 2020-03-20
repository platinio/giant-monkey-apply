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
        private const float checkRate = 1.0f;

        private void Start()
        {
            lastFileWriteDate = new DateTime(1 , 1 , 1);
            StartCoroutine( FileCheckRoutine() );
        }

        private IEnumerator FileCheckRoutine()
        {
            while (true)
            {
                FileHasChanged( Path.Combine(Application.streamingAssetsPath, "JsonChanllenge.json") );
                yield return new WaitForSeconds( checkRate );
            }
        }

        private bool FileHasChanged(string path)
        {
            DateTime currentWriteTime = File.GetLastWriteTime(path);
            int comp = DateTime.Compare(currentWriteTime, lastFileWriteDate);
            if (comp > 0)
            {
                lastFileWriteDate = currentWriteTime;
                onFileChanged.Invoke();
                
            }
            
            return false;
        }


    }

}
