using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Worship.Member;

public class AliveMessage : MonoBehaviour
{

    [SerializeField] float m_MessageInterval = .5f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SendMessage", m_MessageInterval);
    }

    void SendMessage()
    {
        OscSender.Instance.Send("/alive");
        OscSender.Instance.Send("/alive");
        OscSender.Instance.Send("/alive");
        Invoke("SendMessage", m_MessageInterval);
    }
}
