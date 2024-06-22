using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// For formatiing files related strings
/// </summary>
namespace MMAR.StringFormatters
{
    public class FileStringFormatter : MonoBehaviour
    {
        public static string GetFileName(string fileLink)
        {
            string[] filedata = fileLink.Split("/");

            return filedata[filedata.Length - 1];
        }
    }
}
