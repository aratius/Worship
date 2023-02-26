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

        Mode m_Mode = Mode.Edit;

        public Mode mode => m_Mode;

        void Start()
        {
            EditMode();
        }

        public void PlayMode()
        {
            onPlayMode.Invoke();
            m_Mode = Mode.Play;
        }

        public void EditMode()
        {
            onEditMode.Invoke();
            m_Mode = Mode.Edit;
        }
    }

}