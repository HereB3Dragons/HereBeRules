using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HereBeRules.Util
{
    public class SceneLoader: MonoBehaviour
    {
        private string _sceneToLoad;
        private float _elapsedTime;

        private float _delay;

        private bool _isLoading;
        private bool _isRunning;

        private static Action<Transform> _callback;
        private static GameObject _instance;

        public static bool HasCallback => _callback != null;
        public static bool HasParams => _parameters != null && _parameters.Count > 0;

        private static Dictionary<string, string> _parameters;

        public static void Callback(Transform t)
        {
            if (!HasCallback)
                return;
            
            _callback.Invoke(t);
            _callback = null;
            DestroyInstance();
        }

        private static void DestroyInstance()
        {
            if (_instance != null)
            {
                Destroy(_instance);
                _instance = null;
            }
        }

        public static SceneLoader GetInstance(GameObject parent)
        {
            if (_instance != null)
                throw new ApplicationException("Only one loader can run at once");
            
            _instance = new GameObject("Loader");
            SceneLoader loader = _instance.AddComponent<SceneLoader>();
            _instance.transform.parent = parent.transform;
            return loader;
        }

        public void Load(string scene, Dictionary<string, string> args = null)
        {
            Load(scene, 0f, null, args);
        }
        
        public void Load(string scene, float delay = 0, Action<Transform> callback = null, Dictionary<string, string> args = null)
        {
            _sceneToLoad = scene;
            _delay = delay;
            _elapsedTime = 0;
            _callback = callback;
            _isRunning = true;
            _parameters = args;
            StartCoroutine(LoadNewScene(_sceneToLoad));
        }

        public static void GetParams(out Dictionary<string, string> loaderParams)
        {
            if (!HasParams)
            {
                loaderParams = new Dictionary<string, string>();
                DestroyInstance();
                return;
            }

            loaderParams = _parameters;
            _parameters = null;
            DestroyInstance();
        }

        void Update()
        {
            if (_isRunning)
                _elapsedTime += Time.deltaTime;
        }
        
        IEnumerator LoadNewScene(string scene)
        {
           yield return null;
           
           AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
           asyncOperation.allowSceneActivation = false;
           while (!asyncOperation.isDone)
           {
             
               // Check if the load has finished
               if (asyncOperation.progress >= 0.9f)
               {
                   //Wait to you press the space key to activate the Scene
                   if (_elapsedTime > _delay)
                   {
                       //Activate the Scene
                       asyncOperation.allowSceneActivation = true;
                       if (!HasCallback && !HasParams)
                           DestroyInstance();
                   }
               }
   
               yield return null;
           }
        }
    }
}