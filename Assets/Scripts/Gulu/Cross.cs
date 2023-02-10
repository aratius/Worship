using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Worship.Gulu;

namespace Worship.Gulu
{

    public class Cross : MonoBehaviour
    {

        [SerializeField] Button m_Button;
        [SerializeField] RectTransform m_Cross;

        Image m_ButtonImage;
        Sequence m_Sequence;

        void Awake()
        {
            m_ButtonImage = m_Button.gameObject.GetComponent<Image>();
        }

        /// <summary>
        ///
        /// </summary>
        public void Addmode()
        {
            if(m_Sequence != null) m_Sequence.Kill();
            m_Sequence = DOTween.Sequence();
            m_Sequence.Append(m_ButtonImage.DOColor(ColorSet.safe, .3f).SetEase(Ease.Linear));
            m_Sequence.Join(m_Cross.DORotate(new Vector3(0, 0, 0f), .25f).SetEase(Ease.OutBack));
        }

        /// <summary>
        ///
        /// </summary>
        public void Editmode()
        {
            if(m_Sequence != null) m_Sequence.Kill();
            m_Sequence = DOTween.Sequence();
            m_Sequence.Append(m_ButtonImage.DOColor(ColorSet.danger, .3f).SetEase(Ease.Linear));
            m_Sequence.Join(m_Cross.DORotate(new Vector3(0, 0, 45f), .25f).SetEase(Ease.OutBack));
        }

    }

}