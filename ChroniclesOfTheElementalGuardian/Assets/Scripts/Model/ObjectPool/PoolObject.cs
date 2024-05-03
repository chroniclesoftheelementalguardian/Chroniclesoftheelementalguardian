using UnityEngine;
using System;

namespace ObjectPooling
{
    [Serializable]
    public class PoolObject 
    {
        private GameObject _gameObject;
        private Transform _transform;
        private string PoolSlotTag;
        
        public event Action<PoolObject> ReadyForQueue;


        public PoolObject(GameObject gameObject)
        {
            _gameObject = gameObject;
            _transform = gameObject.transform;
        }

        public void Release()
        {
            ReadyForQueue?.Invoke(this);
        }

        public void SetPoolSlotTag(string poolSlotTag)
        {
            PoolSlotTag = poolSlotTag;
        }

        public void SetActive(bool isActive)
        {
            _gameObject.SetActive(isActive);
        }

        public void SetParent(Transform parent)
        {
            _transform.parent = parent;
        }

        public void SetLocalPosition(Vector3 localPosition)
        {
            _transform.localPosition = localPosition;
        }

        public void SetWorldPosition(Vector3 worldPosition)
        {
            _transform.position = worldPosition;
        }

        public string GetPoolSlotTag()
        {
            return PoolSlotTag;
        }

        public Transform GetTransform()
        {
            return _transform;
        }

        public GameObject GetGameObject()
        {
            return _gameObject;
        }
    }
}
