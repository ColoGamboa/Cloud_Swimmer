using CloudSwimmer.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this interface is the "creator" interface of an Factory Methon pattern aplication
//

namespace CloudSwimmer.Interfaces { 
    public interface IBlockCreator 
    {
        //this interface is tha base of all the blocks than can be constructed dinamicaly 

        void CreateBlock(Vector3 mousePosition);

        void DebugPosition(Vector3 mousePosition);
    }

}
