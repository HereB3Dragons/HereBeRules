
using System;
using UnityEngine;
using Object = System.Object;

namespace HereBeRules
{
    [Serializable]
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Full Game State Object</typeparam>
    /// <typeparam name="V">Object from which GameSpec is generated</typeparam>
    /// <typeparam name="Y">Object sent when player makes an action</typeparam>
    public abstract class GameSpecification<T, V, Y>
    {
        private T _gameState;

        private Object _lock;

        private bool _isUpdating = false;

        public delegate void GameUpdateHandler(T newState);

        public GameUpdateHandler GameUpdateEvent;

        public delegate void GameActionHandler(Y action);

        public GameActionHandler ActEvent;

        protected GameSpecification(V data)
        {
            _gameState = InitializeGameState(data);
        }

        protected abstract T InitializeGameState(V data);

        public T GetStateBeforeUpdate()
        {
            return _gameState;
        }

        public virtual void UpdateState(T state)
        {
            //lock (_lock)
            //{
                if (_isUpdating)
                {
                    Debug.LogError("Received update before previous finished. Skipping:\n" + state.ToString());
                    return;
                }

                _isUpdating = true;
                if (GameUpdateEvent != null)
                    GameUpdateEvent.Invoke(state);
                _gameState = state;
                _isUpdating = false;
            //}
        }

        public virtual void SendGameAction(Y action)
        {
            if (ActEvent != null)
                ActEvent.Invoke(action);
        }
    }
}