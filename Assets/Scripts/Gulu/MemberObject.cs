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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("### OnTriggerEnter");
            // TODO: タグつけた方が良さそう
            // TODO: 相手のShape取得
            onCollided.Invoke(Sign.Hand);
        }

    }

}