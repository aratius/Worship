using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using Worship.Common;

namespace Worship.Gulu
{

    struct Status
    {
        public bool isLiving;
        public bool isStanding;
        public bool isRaisingHand;
        public Status(
            bool _isLiving,
            bool _isStanding,
            bool _isRaisingHand
        )
        {
            isLiving = _isLiving;
            isStanding = _isStanding;
            isRaisingHand = _isRaisingHand;
        }
    }

    public class MemberUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {

        public UnityEvent<MemberUI> onGrab = new UnityEvent<MemberUI>();

        [SerializeField] TextMeshProUGUI m_NameText;
        [SerializeField] MemberProgressUI m_MemberProgress;
        [SerializeField] GameObject m_Hand;
        [SerializeField] GameObject m_Foot;
        [SerializeField] RawImage m_Body;
        [SerializeField] RawImage m_StatusLamp;

        // Play mode
        Sequence m_BodySeq;
        Sequence m_StatusSeq;
        Tween m_ProgressTween;
        Status m_Status = new Status(false, false, false);

        // Edit mode
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

        /// <summary>
        /// Common
        /// </summary>
        public void PlayMode()
        {
            m_MemberProgress.gameObject.SetActive(true);
            m_Hand.SetActive(true);
            m_Foot.SetActive(true);
            m_Body.raycastTarget = false;
            Select();
            Die();
        }

        public void EditMode()
        {
            m_MemberProgress.gameObject.SetActive(false);
            m_Hand.SetActive(false);
            m_Foot.SetActive(false);
            m_Body.raycastTarget = true;
        }

        /// <summary>
        /// Play mode
        /// </summary>

        public void Live()
        {
            if(m_Status.isLiving && m_StatusSeq != null) return;
            if(m_StatusSeq != null) m_StatusSeq.Kill();
            m_StatusSeq = DOTween.Sequence();
            m_StatusSeq.Append(m_StatusLamp.DOColor(ColorSet.safe, .5f));
        }

        public void Die()
        {
            if(!m_Status.isLiving && m_StatusSeq != null) return;
            if(m_StatusSeq != null) m_StatusSeq.Kill();
            m_StatusSeq = DOTween.Sequence();
            m_StatusSeq.Append(m_StatusLamp.DOColor(ColorSet.danger, .5f));
        }

        public void Collide()
        {
            if(m_BodySeq != null) m_BodySeq.Kill();
            m_BodySeq = DOTween.Sequence();
            m_BodySeq.Append(m_Body.DOColor(Color.white, .1f));
            m_BodySeq.Append(m_Body.DOColor(new Color(170f / 255f, 170f / 255f, 170f / 255f), 1f));
        }

        public bool Check(Sign sign)
        {
            bool needEffect = false;
            if(sign == Sign.Hand)
            {
                Hand(!m_Status.isRaisingHand);
                needEffect = true;
            }
            if(sign == Sign.Foot)
            {
                Stand(!m_Status.isStanding);
                needEffect = true;
            }
            if(sign == Sign.HandsUp)
            {
                if(m_Status.isRaisingHand) return false;
                Hand(true);
                needEffect = true;
            }
            if(sign == Sign.HandsDown)
            {
                if(!m_Status.isRaisingHand) return false;
                Hand(false);
                needEffect = true;
            }
            if(sign == Sign.StandUp)
            {
                if(m_Status.isStanding) return false;
                Stand(true);
                needEffect = true;
            }
            if(sign == Sign.SitDown)
            {
                if(!m_Status.isStanding) return false;
                Stand(false);
                needEffect = true;
            }
            if(sign == Sign.Tutorial)
            {
                Tutorial();
            }
            if(sign == Sign.Finish)
            {
                Finish();
            }
            if(sign == Sign.Stop)
            {
                Stop();
            }

            return needEffect;
        }

        void Hand(bool isUp)
        {
            m_Status.isRaisingHand = isUp;
            m_Hand.GetComponent<CanvasGroup>().alpha = isUp ? 1f : .2f;
            Instruction instruction = isUp ? Instruction.HandsUp : Instruction.HandsDown;
            float duration = InstructionUtils.instructionDurationTable[instruction];
            Progress(duration);
        }

        void Stand(bool isUp)
        {
            m_Status.isStanding = isUp;
            m_Foot.GetComponent<CanvasGroup>().alpha = isUp ? 1f : .2f;
            Instruction instruction = isUp ? Instruction.StandUp : Instruction.SitDown;
            float duration = InstructionUtils.instructionDurationTable[instruction];
            Progress(duration);
        }

        void Tutorial()
        {
            float duration = InstructionUtils.instructionDurationTable[Instruction.Tutorial];
            Progress(duration);
        }

        void Finish()
        {
            float duration = InstructionUtils.instructionDurationTable[Instruction.Finish];
            Progress(duration);
        }

        void Stop()
        {
            float duration = InstructionUtils.instructionDurationTable[Instruction.Stop];
            Progress(duration);
        }

        void Progress(float duration)
        {
            if(m_ProgressTween != null) m_ProgressTween.Kill();
            m_MemberProgress.progress = 0f;
            m_ProgressTween = DOTween.To(
                () => m_MemberProgress.progress,
                v => m_MemberProgress.progress = v,
                1f,
                duration
            ).SetEase(Ease.Linear).OnComplete(() => m_MemberProgress.progress = 0f);
        }

        /// <summary>
        /// Edit mode
        /// </summary>

        public void OnPointerDown(PointerEventData eventData)
        {
            if(ModeManager.Instance.mode == Mode.Play) return;
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