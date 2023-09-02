using System;
using UnityEngine;
using XLua;

namespace Script
{
    public class MainScript : MonoBehaviour
    {
        private LuaEnv _luaEnv;
        void Start()
        {
            _luaEnv = new LuaEnv();
        }

        void Update()
        {
            
        }

        private void OnDestroy()
        {
            _luaEnv.Dispose();
        }
    }
}
