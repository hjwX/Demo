using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MKFramework;

public class Driver : MonoBehaviour
{
    private ServiceContainer _container = new ServiceContainer();
    
    void Start()
    {
        IdService idService = new IdService();
        _container.RegisterService(idService);
        
    }


    void Update()
    {
        
    }

    private void LateUpdate()
    {
        
    }
}
