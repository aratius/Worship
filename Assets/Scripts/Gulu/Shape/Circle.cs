using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Worship.Gulu;

namespace Worship.Gulu
{

    public class Circle : ShapeBase
    {

        Material m_Material;

        void Start()
        {
            transform.localScale = new Vector3(.5f, .5f, .5f);

            GetComponent<Renderer>().material = new Material(GetComponent<Renderer>().material.shader);
            m_Material = GetComponent<Renderer>().material;
        }

        void Update()
        {
            if(!m_HasReleased) return;

            Vector3 scale = transform.localScale;
            scale.x += .1f * m_Speed;
            scale.z += .1f * m_Speed;
            transform.localScale = scale;

            m_Material.SetFloat("_Scale", scale.x);

            if(scale.x > Camera.main.orthographicSize * 4f) Destroy(gameObject);
        }

        public override void Move(Vector2 position)
        {
            transform.localPosition = new Vector3(
                position.x,
                position.y,
                0f
            );
        }

    }

}