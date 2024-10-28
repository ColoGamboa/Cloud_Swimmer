using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CloudSwimmer.Entities;
using CloudSwimmer.Interfaces;

namespace CloudSwimmer.Controllers
{
    public class InputController : MonoBehaviour
    {
        private IBlockCreator blockCreator; 

        //deberia tener un metodo constructor e inyectar el controlador? 
        void Start()
        {
            IBlockCreator blockCreator = GetComponent<IBlockCreator>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

