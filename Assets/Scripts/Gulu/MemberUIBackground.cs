using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Worship.Gulu
{

    public class MemberUIBackground : MonoBehaviour, IPointerDownHandler
    {
        public UnityEvent onClick = new UnityEvent();

        public void OnPointerDown(PointerEventData eventData)
        {
            onClick.Invoke();
        }

    }

}