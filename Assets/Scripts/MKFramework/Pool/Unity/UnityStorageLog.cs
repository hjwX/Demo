﻿using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MKFramework.Pool.Unity
{
    public class UnityStorageLog : MonoBehaviour
    {
        [SerializeField] private List<TypeLog> LogTypes;

        [SerializeField] private int Count;

        public Dictionary<Type, TypeLog> TypeDic;

        private void Awake()
        {
            TypeDic = new Dictionary<Type, TypeLog>();
            var pool = Cache<GameObject>.DefaultPool;
            var intPool = Cache<Rigidbody>.DefaultPool;
            Initialize();
            Cache<GameObject>.Generate(2);
            Cache<List<GameObject>>.Generate(2);
//        Cache<Texture2D>.Generate(2);//Generate No COnstructor found Exception to implement
        }

        public void Initialize()
        {
            foreach (var cacheType in CacheLog.GetCacheClosedTypes())
            {
                var argumentType = cacheType.GetGenericArguments()[0];
                var log = new TypeLog()
                {
                    Type = argumentType.Name
                };

                LogTypes.Add(log);
                TypeDic[argumentType] = log;
                log.Count = CacheLog.CacheCounter[argumentType];
//            SubscribeToCacheEvents(cacheType);
            }

            CacheLog.AnyAdded += AddedHandler;
            CacheLog.AnyRemoved += RemovedHandler;
        }

        public void AddedHandler(object sender, object arg)
        {
            var argumentType = sender.GetType().GetGenericArguments()[0];
            if (!TypeDic.ContainsKey(argumentType))
            {
                var log = new TypeLog()
                {
                    Type = argumentType.Name
                };
                LogTypes.Add(log);
                TypeDic[argumentType] = log;
                log.Count = CacheLog.CacheCounter[argumentType];
            }
            else
            {
                TypeDic[argumentType].Count++;
            }
        }

        public void RemovedHandler(object sender, object arg)
        {
            var argumentType = sender.GetType().GetGenericArguments()[0];
            TypeDic[argumentType].Count--;
        }

        private void SubscribeToCacheEvents(Type cacheType)
        {
            var eventInfo = cacheType.GetEvent("AddedAny");
            SubscriveToEvent(eventInfo, this, "AddedHandler");
            var removedEventInfo = cacheType.GetEvent("RemovedAny");
            SubscriveToEvent(removedEventInfo, this, "RemovedHandler");
            /*var handlerType = eventInfo.EventHandlerType.GetGenericArguments()[0];
            ConstructorInfo constructor = eventInfo.EventHandlerType.GetConstructors()[0];
    //            cacheType.GetGenericTypeDefinition().GetEvent("Added").EventHandlerType
    //            .MakeGenericType(handlerType).GetConstructors()[0];
            Delegate handler = (Delegate)constructor
               .Invoke(new object[]
                   {
                        this,
                        GetType().GetMethod("SuccessMethod").MethodHandle.GetFunctionPointer()
                   });
            eventInfo.AddEventHandler(null, handler);*/
        }

        private void SubscriveToEvent(EventInfo eventInfo, object targetObject, string targetMethod)
        {
            ConstructorInfo constructor = eventInfo.EventHandlerType.GetConstructors()[0];
            Delegate handler = (Delegate) constructor.Invoke(new object[]
            {
                targetObject,
                targetObject.GetType().GetMethod(targetMethod).MethodHandle.GetFunctionPointer()
            });
            eventInfo.AddEventHandler(null, handler);
        }
    }

    [System.Serializable]
    public class TypeLog
    {
        public string Type;

        public int Count;
//    public int InUse;
//    public int TotalCached;
    }
}