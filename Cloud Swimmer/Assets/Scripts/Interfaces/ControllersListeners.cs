﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using CloudSwimmer.Controllers;

namespace Assets.Scripts.Interfaces
{
    internal interface ControllersListeners
    {
        void Update(InputController);
    }
}
