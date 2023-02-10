using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Worship.Gulu
{

    public class MemberProgress : MonoBehaviour
    {

        [SerializeField] Material m_MaterialPrefab;

        Material m_Material;

        void Awake()
        {
            Material material = new Material(m_MaterialPrefab.shader);
            GetComponent<RawImage>().material = material;
            m_Material = material;
        }

        public void SetFloat(string propName, float val)
        {
            m_Material.SetFloat(propName, val);
        }

    }

}