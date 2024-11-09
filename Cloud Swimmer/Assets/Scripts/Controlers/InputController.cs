using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CloudSwimmer.Entities;
using CloudSwimmer.Interfaces;
using Assets.Scripts.Interfaces;

namespace CloudSwimmer.Controllers
{
    public class InputController : MonoBehaviour
    {
        //this is a publisher of a observer pattern
        private List<IControllersListeners> _listeners;

        void Subscribe(IControllersListeners listener)
        {
            _listeners.Add(listener);
        }
        void UnSubscribe(IControllersListeners listener)
        {
            _listeners.Remove(listener);
        }
        void Notify()
        {
            foreach (IControllersListeners listener in _listeners)
            {
                listener.Update(this);
            }
        }

    }
}

