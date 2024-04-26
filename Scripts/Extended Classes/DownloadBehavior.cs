using MMAR.BaseClasses;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace MMAR.ExtendedClasses
{
    public class DownloadBehavior : BaseBehavior
    {
        /// <summary>
        /// Return download progress
        /// </summary>
        public float downloadProgress = 0;
        /// <summary>
        /// Last download url
        /// </summary>
        public string lastDownloadUrl = "";
        /// <summary>
        /// download data handler
        /// </summary>
        DownloadHandler downloadHandler;
        /// <summary>
        /// Download starting IEnumerator
        /// </summary>
        /// <param name="DownloadUrl">Download url</param>
        /// <returns></returns>
        /// 
        public UnityEvent downloadCompleted, downloadFailed;
        protected IEnumerator startDownload(string DownloadUrl)
        {
            DebugLog("Downloading " + DownloadUrl);
            lastDownloadUrl = DownloadUrl;
            UnityWebRequest www = UnityWebRequest.Get(DownloadUrl);
            www.SendWebRequest();
            while (!www.isDone)
            {
                float progress = www.downloadProgress;
                downloadProgress = progress;
                DebugLog("Download Progress: " + progress);
                yield return new WaitForSecondsRealtime(.1f);
            }
            DebugLog("Done downloading");
            if (www.result != UnityWebRequest.Result.Success)
            {
                downloadFailed.Invoke();
            }
            else
            {
                downloadHandler = www.downloadHandler;
                downloadCompleted.Invoke();
            }
        }
        /// <summary>
        /// Get download result as text
        /// </summary>
        protected string downloadedText
        {
            get { return downloadHandler.text; }
        }
        /// <summary>
        /// Save downloaded file
        /// </summary>
        /// <param name="filePath">File path</param>
        protected void SaveDownloadedFile(string filePath)
        {
            File.WriteAllBytes(filePath, downloadHandler.data);
        }
    }
}
