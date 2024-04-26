using MMAR.BaseClasses;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace MMAR.ExtendedClasses
{
    public class DownloadBehavior : BaseBehavior
    {
        /// <summary>
        /// Return download progress
        /// </summary>
        float downloadProgress = 0;
        /// <summary>
        /// If download progress completed
        /// </summary>
        bool downloadCompleted = true;
        string lastDownloadUrl = "";
        bool downloadFailed = false;
        DownloadHandler downloadHandler;
        
        IEnumerator startDownload(string DownloadUrl)
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
                downloadFailed = true;
                downloadCompleted = true;
            }
            else
            {
                downloadHandler = www.downloadHandler;
                downloadFailed = false;
                downloadCompleted = true;
            }
        }
        string downloadedText
        {
            get { return downloadHandler.text; }
        }
        void SaveDownloadedFile(string filePath)
        {
            File.WriteAllBytes(filePath, downloadHandler.data);
        }
    }
}
