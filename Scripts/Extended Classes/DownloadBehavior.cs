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
        /// If download completed
        /// </summary>
        bool downloadCompleted = true;
        /// <summary>
        /// Last download url
        /// </summary>
        string lastDownloadUrl = "";
        /// <summary>
        /// If download failed
        /// </summary>
        bool downloadFailed = false;
        /// <summary>
        /// download data handler
        /// </summary>
        DownloadHandler downloadHandler;
        /// <summary>
        /// Download starting IEnumerator
        /// </summary>
        /// <param name="DownloadUrl">Download url</param>
        /// <returns></returns>
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
        /// <summary>
        /// Get download result as text
        /// </summary>
        string downloadedText
        {
            get { return downloadHandler.text; }
        }
        /// <summary>
        /// Save downloaded file
        /// </summary>
        /// <param name="filePath">File path</param>
        void SaveDownloadedFile(string filePath)
        {
            File.WriteAllBytes(filePath, downloadHandler.data);
        }
    }
}
