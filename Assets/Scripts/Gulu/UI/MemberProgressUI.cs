using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Worship.Gulu
{

    public class MemberProgressUI : MonoBehaviour
    {
        [SerializeField] RawImage m_Image;

        Material m_Material;
        float m_Progress = 0;

        public float progress
        {
            set
            {
                m_Progress = value;
                SetProgress(m_Progress);
            }
            get => m_Progress;
        }

        void Awake()
        {
            m_Image.material = new Material(m_Image.material.shader);
            m_Material = m_Image.material;
            SetProgress(m_Progress);
        }

        void SetProgress(float progress)
        {
            m_Image.material.SetFloat("_Progress", progress);
        }

    }

}