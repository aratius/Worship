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
        [SerializeField] GameObject m_MemberProgress;
        [SerializeField] GameObject m_Hand;
        [SerializeField] GameObject m_Foot;

        RectTransform m_RectTransform;
        CanvasGroup m_Canvas;
        CanvasGroup m_HandCanvas;
        CanvasGroup m_FootCanvas;
        bool m_IsGrabbing = false;

        void Awake()
        {
            m_RectTransform = GetComponent<RectTransform>();
            m_Canvas = GetComponent<CanvasGroup>();
            m_HandCanvas = m_Hand.GetComponent<CanvasGroup>();
            m_FootCanvas = m_Foot.GetComponent<CanvasGroup>();

            // 生まれる瞬間は必ずEditMode
            EditMode();
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

        public void PlayMode()
        {
            m_MemberProgress.SetActive(true);
            m_Hand.SetActive(true);
            m_Foot.SetActive(true);
            Select();
        }

        public void EditMode()
        {
            m_MemberProgress.SetActive(false);
            m_Hand.SetActive(false);
            m_Foot.SetActive(false);
        }

        void Move(Vector2 position)
        {
            if(m_IsGrabbing) m_RectTransform.position = position;
        }

    }

}