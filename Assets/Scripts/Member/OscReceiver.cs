using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OscJack;
using Cysharp.Threading.Tasks;

namespace Worship.Member
{

    public class OscReceiver : SingletonMonoBehaviour<OscReceiver>
    {

        public UnityEvent<int> onInstruction = new UnityEvent<int>();
        [SerializeField] int m_DefaultPort;
        List<string> m_History = new List<string>();

        OscServer m_Server;

        void Start()
        {
            Set(m_DefaultPort);
        }

        /// <summary>
        /// set
        /// </summary>
        /// <param name="port"></param>
        public void Set(int port)
        {
            if(m_Server != null) m_Server.Dispose();

            m_Server = new OscServer(port);
            m_Server.MessageDispatcher.AddCallback(
                "/instruction",
                _OnReceiveSign
            );
            Debug.Log($"Start listening, port : {port}");
        }

        /// <summary>
        /// On receive osc
        /// </summary>
        /// <param name="address"></param>
        /// <param name="data"></param>
        private async void _OnReceiveSign(string address, OscDataHandle data)
        {
            await UniTask.WaitForFixedUpdate();
            if(!m_History.Contains(address))
            {
                Debug.Log("add");
                onInstruction.Invoke(data.GetElementAsInt(0));
                m_History.Add(address);
                _AvoidSameTimer(address);
            }
        }

        private async void _AvoidSameTimer(string address)
        {
            await UniTask.Delay(100);
            m_History.Remove(address);
        }

    }

}