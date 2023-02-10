using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Worship.Gulu
{

    public class Mode : SingletonMonoBehaviour<Mode>
    {
        public UnityEvent onPlayMode = new UnityEvent();
        public UnityEvent onEditMode = new UnityEvent();

        void Start()
        {
            EditMode();
        }

        public void PlayMode()
        {
            onPlayMode.Invoke();
        }

        public void EditMode()
        {
            onEditMode.Invoke();
        }
    }

}