using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Worship.Gulu
{

    public enum Mode
    {
        Play,
        Edit
    }

    public class ModeManager : SingletonMonoBehaviour<ModeManager>
    {
        public UnityEvent onPlayMode = new UnityEvent();
        public UnityEvent onEditMode = new UnityEvent();

        Mode m_Mode;

        public Mode mode => m_Mode;

        void Start()
        {
            EditMode();
        }

        public void PlayMode()
        {
            if(m_Mode == Mode.Play) return;
            m_Mode = Mode.Play;
            onPlayMode.Invoke();
        }

        public void EditMode()
        {
            if(m_Mode == Mode.Edit) return;
            m_Mode = Mode.Edit;
            onEditMode.Invoke();
        }
    }

}