using UnityEngine;

namespace HereBeRules.Util
{
    public class Loaded: MonoBehaviour
    {
        protected virtual void Start()
        {
            SceneLoader.Callback(transform);
        }
    }
}