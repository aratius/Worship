using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

namespace Worship.Gulu
{

    public class MemberUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {

        public UnityEvent<MemberUI> onGrab = new UnityEvent<MemberUI>();

        [SerializeField] TextMeshProUGUI m_NameText;

        RectTransform m_RectTransform;
        CanvasGroup m_Canvas;
        bool m_IsGrabbing = false;

        void Awake()
        {
            m_RectTransform = GetComponent<RectTransform>();
            m_Canvas = GetComponent<CanvasGroup>();
        }

        void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Moved)
                {
                    Move(touch.position);
                }
            }
            else
            {
                Move(Input.mousePosition);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_IsGrabbing = true;
            onGrab.Invoke(this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_IsGrabbing = false;
        }

        public void Name(string name)
        {
            gameObject.name = name;
            m_NameText.text = name;
        }

        public void Select()
        {
            m_Canvas.alpha = 1f;
        }

        public void DeSelect()
        {
            m_Canvas.alpha = .2f;
        }

        void Move(Vector2 position)
        {
            if(m_IsGrabbing) m_RectTransform.position = position;
        }

    }

}