using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Worship.Gulu;

namespace Worship.Gulu
{

    public class Horizontal : ShapeBase
    {

        float m_Direction = 0;

        void Start()
        {
            transform.localScale = new Vector3(
                Camera.main.orthographicSize * (float)Screen.width / (float)Screen.height * 2f,
                .1f,
                1f
            );
        }

        void Update()
        {
            if(!m_HasReleased) return;

            Vector3 pos = transform.localPosition;
            pos.y += .1f * m_Direction * m_Speed;  // TODO: *speed  *dir
            transform.localPosition = pos;

            if(pos.y > Camera.main.orthographicSize || pos.y < -Camera.main.orthographicSize) Destroy(gameObject);
        }

        public override void Move(Vector2 position)
        {
            transform.localPosition = new Vector3(
                0f,
                position.y,
                0f
            );
        }

        public override void SetDirection(Vector2 direction)
        {
            m_Direction = direction.y;
        }

    }

}