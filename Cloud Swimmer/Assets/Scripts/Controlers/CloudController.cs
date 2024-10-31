using CloudSwimmer.Entities;
using CloudSwimmer.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    private IBlockCreator blockCreator;

    private Vector3 mousePosition;



    void Start()
    {
        blockCreator = CloudBlockCreator.Instance;
    }
    void Update()
    {
        
    }
}
