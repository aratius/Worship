using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Worship.Gulu
{

    public abstract class SelectBase<T> : MonoBehaviour
    {

        [SerializeField] Button[] m_Buttons;

        protected Button m_SelectedButton;

        void Start()
        {
            foreach (Button button in m_Buttons)
            {
                button.onClick.AddListener(() => {
                    OnClick(button);
                });
            }
            OnClick(m_Buttons[0]);

        }

        void OnClick(Button clickedButton)
        {
            foreach (Button button in m_Buttons)
            {
                if(button == clickedButton)
                {
                    m_SelectedButton = button;
                    OnFocus(button);
                }
                else OnBlur(button);
            }
        }

        internal abstract void OnFocus(Button button);
        internal abstract void OnBlur(Button button);

    }

}