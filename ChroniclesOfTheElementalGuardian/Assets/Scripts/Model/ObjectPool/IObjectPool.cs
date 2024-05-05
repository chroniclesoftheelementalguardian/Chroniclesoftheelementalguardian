using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

namespace ObjectPooling
{
    public static class IObjectPool 
    {
        private static ObjectPool _objectPool = null;
        private static List<PoolObject> _activePoolObjects = new List<PoolObject>();

        internal static void Initialize(ObjectPool objectPool)
        {
            if(_objectPool != null)
            {
                Debug.LogWarning($"There are more than 1 ObjectPool in the Scene Destroying {objectPool.name}");
                GameObject.Destroy(objectPool.gameObject);
                return;
            }
            _objectPool = objectPool;
        }

        private static PoolObject GetFromTransform(Transform poolObjectTransform)
        {
            for (int i = 0; i < _activePoolObjects.Count; i++)
            {
                if(poolObjectTransform == _activePoolObjects[i].GetTransform())
                {
                    //Debug.Log("Found Transform inside activePoolObjects list");
                    return _activePoolObjects[i];
                }
            }
            //Debug.Log("No Transform is Found Returning Null");
            return null;
        }
        
        //PUBLIC

        public static PoolObject GetFromPool(string poolSlotTag, bool shouldActivate)
        {
            Debug.Log("Getting From Pool");
            PoolObject poolObject = _objectPool.GetFromPool(poolSlotTag, shouldActivate);
            _activePoolObjects.Add(poolObject);
            Debug.Log("Added PoolObject To List");
            return poolObject;
        }

        public static void Release(Transform poolObjectTransform)
        {
            Debug.Log($"IObjectPool.Release(Transform transform), Transform: {poolObjectTransform.name}");
            PoolObject poolObject = GetFromTransform(poolObjectTransform);
            //Debug.Log($"Acquired PoolObject = {poolObject}");
            Release(poolObject);
        }

        private static void StopTracking(PoolObject poolObject)
        {
            _activePoolObjects.Remove(poolObject);
        }

        public static void Release(PoolObject poolObject)
        {
            StopTracking(poolObject);
            poolObject.Release();
        }

        public static void ReleaseAll()
        {
            for (int i = 0; i < _activePoolObjects.Count; i++)
            {
                if(_activePoolObjects[i].GetGameObject() == null) continue;
                Release(_activePoolObjects[i]);
            }
            _activePoolObjects.Clear();
        }

        public static void Destroy(Transform poolObjectTransform)
        {
            PoolObject poolObject = GetFromTransform(poolObjectTransform);
            StopTracking(poolObject);
            GameObject.Destroy(poolObjectTransform.gameObject);
        }
    }
}
