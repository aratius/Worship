using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Worship.Gulu;

namespace Worship.Gulu
{

    public class Vertical : ShapeBase
    {

        float m_Direction = 0;

        void Start()
        {
            transform.localScale = new Vector3(
                .1f,
                Camera.main.orthographicSize * 2f,
                1f
            );
        }

        void Update()
        {
            if(!m_HasReleased) return;

            Vector3 pos = transform.localPosition;
            pos.x += .1f * m_Direction * m_Speed;  // TODO: *speed  *dir
            transform.localPosition = pos;

            // アスペクト2より大きい端末は考慮しない
            if(pos.x > Camera.main.orthographicSize * 2f || pos.x < -Camera.main.orthographicSize * 2f) Destroy(gameObject);
        }

        public override void Move(Vector2 position)
        {
            transform.localPosition = new Vector3(
                position.x,
                0f,
                0f
            );
        }

        public override void SetDirection(Vector2 direction)
        {
            m_Direction = direction.x;
        }

    }

}