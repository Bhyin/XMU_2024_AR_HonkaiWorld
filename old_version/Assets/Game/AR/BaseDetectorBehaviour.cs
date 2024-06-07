using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AR
{
    public class BaseDetectorBehaviour : MonoBehaviour
    {
        public virtual Type type => Type.PLANE;

        public virtual void Pause()
        {

        }

        public virtual void Continue()
        {

        }


        public virtual void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public virtual void Load()
        {
            
        }

        public virtual void Release()
        {

        }
    }
}

