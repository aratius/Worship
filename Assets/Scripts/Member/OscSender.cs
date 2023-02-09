using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OscJack;
using Cysharp.Threading.Tasks;

namespace Worship.Member
{

    public class OscSender : SingletonMonoBehaviour<OscSender>
    {

        List<string> m_History = new List<string>();

        OscClient m_Client;

        /// <summary>
        /// set
        /// </summary>
        /// <param name="port"></param>
        public void Set(string ip, int port)
        {
            if(m_Client != null) m_Client.Dispose();

            m_Client = new OscClient(ip, port);
        }

        public void Send(string address, string data)
        {
            m_Client.Send(address, data);
        }

        public void Send(string address, int data)
        {
            m_Client.Send(address, data);
        }

        public void Send(string address)
        {
            m_Client.Send(address);
        }

    }

}