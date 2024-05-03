using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    internal class ObjectPool : MonoBehaviour
    {
        [SerializeField] private List<PoolSlot> _poolSlots = new List<PoolSlot>();
        private Dictionary<string, Queue<PoolObject>> tagToQueueDict = new Dictionary<string, Queue<PoolObject>>();
        private Dictionary<string, PoolSlot> _tagToPoolSlotDict = new Dictionary<string, PoolSlot>();
        private List<PoolObject> _subscriptions = new List<PoolObject>(); 

        private void Awake() 
        {
            IObjectPool.Initialize(this);
            BuildTagToPoolSlotDict();
            InitializePool();
        }

        private void BuildTagToPoolSlotDict()
        {
            for (int i = 0; i < _poolSlots.Count; i++)
            {
                _tagToPoolSlotDict.Add(_poolSlots[i].SlotTag,_poolSlots[i]);
            }
        }

        private void InitializePool()
        {
            for (int i = 0; i < _poolSlots.Count; i++)
            {
                string poolSlotTag = _poolSlots[i].SlotTag;
                CreateNewQueueForSlot(poolSlotTag);
                PopulizeQueueForSlot(poolSlotTag);
            }
        }

        private Queue<PoolObject> CreateNewQueueForSlot(string poolSlotTag)
        {
            Queue<PoolObject> slotQueue = new Queue<PoolObject>();
            tagToQueueDict.Add(poolSlotTag, slotQueue);
            return slotQueue;
        }

        private void PopulizeQueueForSlot(string poolSlotTag)
        {
            int slotSize = GetPoolSlotFromTag(poolSlotTag).SlotSize;

            for (int i = 0; i < slotSize; i++)
            {
                PoolObject pooledObject = InstantiatePoolObject(poolSlotTag);
                EnqueuePoolObject(poolSlotTag,pooledObject);
            }
        }

        private PoolObject InstantiatePoolObject(string poolSlotTag)
        {
            GameObject gameObjectInstance = Instantiate(GetPoolSlotFromTag(poolSlotTag).ObjectToPool);
            PoolObject pooledObject = new PoolObject(gameObjectInstance);
            pooledObject.SetPoolSlotTag(poolSlotTag);
            pooledObject.ReadyForQueue += PooledObject_OnReadyForQueue;
            _subscriptions.Add(pooledObject);
            return pooledObject;
        }

        private void EnqueuePoolObject(string poolSlotTag, PoolObject pooledObject)
        {
            GetQueueFromTag(poolSlotTag).Enqueue(pooledObject);
            pooledObject.SetActive(false);
            pooledObject.SetParent(transform);
        }

        internal PoolObject GetFromPool(string poolSlotTag, bool shouldActivate)
        {
            TryExpandPool(poolSlotTag);

            PoolObject poolObject = GetQueueFromTag(poolSlotTag).Dequeue();
            poolObject.SetActive(shouldActivate);
            return poolObject;
        }

        private void TryExpandPool(string poolSlotTag)
        {
            if (IsQueueEmpty(poolSlotTag))
            {
                PoolObject pooledObject = InstantiatePoolObject(poolSlotTag);
                EnqueuePoolObject(poolSlotTag, pooledObject);
            }
        }

        private void PooledObject_OnReadyForQueue(PoolObject poolObject)
        {
            poolObject.SetActive(false);
            poolObject.SetParent(transform);
            poolObject.SetLocalPosition(Vector3.zero);
            GetQueueFromTag(poolObject.GetPoolSlotTag()).Enqueue(poolObject);
        }

        private PoolSlot GetPoolSlotFromTag(string poolSlotTag)
        {
            return _tagToPoolSlotDict[poolSlotTag];
        }

        private Queue<PoolObject> GetQueueFromTag(string poolSlotTag)
        {
            return tagToQueueDict[poolSlotTag];
        }

        private bool IsQueueEmpty(string poolSlotTag)
        {
            return GetQueueFromTag(poolSlotTag).Count <= 0;
        }

        private void OnDisable() 
        {
            for (int i = 0; i < _subscriptions.Count; i++)
            {
                _subscriptions[i].ReadyForQueue -= PooledObject_OnReadyForQueue;
            }
        }

        [Serializable]
        public struct PoolSlot
        {
            [SerializeField] private string _slotTag;
            [SerializeField] private int _slotSize;
            [SerializeField] private GameObject _objectToPool;


            public string SlotTag { get { return _slotTag; } }
            public int SlotSize { get { return _slotSize; } }
            public GameObject ObjectToPool { get { return _objectToPool; } }
        }
    }
}

