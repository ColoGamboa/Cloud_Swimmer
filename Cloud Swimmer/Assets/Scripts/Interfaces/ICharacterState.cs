﻿using CloudSwimmer.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface ICharacterState
    {
        public void Init(Collider2D collider, Vector2 _position, Vector2 _direction);
        public void SetContext(CharacterMovement _movement);
        public void Move(float _horizontal, float _vertical);
        public void KeyInput(char _key);
        public void CheckTriggerEnter(Collider2D collider);
        public void CheckTriggerExit(Collider2D collider);
    }
}
