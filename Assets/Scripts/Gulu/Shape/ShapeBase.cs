using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Worship.Gulu
{

    public abstract class ShapeBase : MonoBehaviour
    {

        protected bool m_HasReleased = false;
        protected float m_Speed = 1f;

        public abstract void Move(Vector2 position);

        public void Release(float speed)
        {
            m_HasReleased = true;
            m_Speed = speed;
        }

        public virtual void SetDirection(Vector2 direction)
        {

        }

    }

}