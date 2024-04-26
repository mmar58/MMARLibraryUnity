using UnityEngine;

namespace MMAR.BaseClasses
{
    public class BaseBehavior : MonoBehaviour
    {
        public bool debugThis;

        public void DebugLog(object msg)
        {
            if (debugThis)
            {
                Debug.Log(msg);
            }
        }
    }
}
