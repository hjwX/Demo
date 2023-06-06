﻿using System;
using UnityEngine;

namespace MKFramework.Pool.Unity
{
    [Serializable]
    public class UnityPool
    {
        public string Name;
        public GameObject Prefab;
        public bool AllowExpand;
        public bool AllowRecycle;
        public int Size;
        
    }
}
