using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using Worship.Gulu;

namespace Worship.Gulu
{

    public class Cross : MonoBehaviour
    {

        public UnityEvent onAdd = new UnityEvent();
        public UnityEvent onRemove = new UnityEvent();

        [SerializeField] Button m_Button;
        [SerializeField] RectTransform m_Cross;

        Image m_ButtonImage;
        Sequence m_Sequence;
        bool m_IsAddMode = true;

        void Awake()
        {
            m_ButtonImage = m_Button.gameObject.GetComponent<Image>();
            m_Button.onClick.AddListener(() => {
                if(m_IsAddMode) onAdd.Invoke();
                else onRemove.Invoke();
            });

            AddMode();
        }

        /// <summary>
        ///
        /// </summary>
        public void AddMode()
        {
            if(m_Sequence != null) m_Sequence.Kill();
            m_Sequence = DOTween.Sequence().OnComplete(() => m_IsAddMode = true);
            m_Sequence.Append(m_ButtonImage.DOColor(ColorSet.safe, .3f).SetEase(Ease.Linear));
            m_Sequence.Join(m_Cross.DORotate(new Vector3(0, 0, 0f), .25f).SetEase(Ease.OutBack));
        }

        /// <summary>
        ///
        /// </summary>
        public void RemoveMode()
        {
            if(m_Sequence != null) m_Sequence.Kill();
            m_Sequence = DOTween.Sequence().OnComplete(() => m_IsAddMode = false);
            m_Sequence.Append(m_ButtonImage.DOColor(ColorSet.danger, .3f).SetEase(Ease.Linear));
            m_Sequence.Join(m_Cross.DORotate(new Vector3(0, 0, 45f), .25f).SetEase(Ease.OutBack));
        }

    }

}