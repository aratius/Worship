using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Worship.Gulu;

namespace Worship.Gulu
{

    public class MemberObject : MonoBehaviour
    {

        public UnityEvent<Sign> onCollided = new UnityEvent<Sign>();

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("### OnTriggerEnter");
            // TODO: タグつけた方が良さそう
            Sign sign = other.gameObject.GetComponent<SignIdentifier>().value;
            onCollided.Invoke(sign);
        }

    }

}