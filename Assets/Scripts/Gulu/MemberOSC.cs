using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OscJack;

public class MemberOSC : MonoBehaviour
{

    public UnityEvent onLive = new UnityEvent();
    public UnityEvent onDie = new UnityEvent();

    [SerializeField] int m_DeadThresholdMilliSecond = 2000;

    OscServer m_Server;
    OscClient m_Client;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Send()
    {

    }

    void OnDestroy()
    {
        // TODO: クライアント削除
        m_Client.Dispose();
        m_Server.Dispose();
    }

    public void Init(string ip, int portIncomming, int portOutgoing, string bars)
    {
        // TODO: クライアント作成
        m_Client = new OscClient(ip, portOutgoing);
        m_Server = new OscServer(portIncomming);
        m_Server.MessageDispatcher.AddCallback("/live", (string address, OscDataHandle data) => Live());
    }

    void Live()
    {
        onLive.Invoke();
        CancelInvoke("Die");
        Invoke("Die", m_DeadThresholdMilliSecond);
    }

    void Die()
    {
        onDie.Invoke();
    }

}
