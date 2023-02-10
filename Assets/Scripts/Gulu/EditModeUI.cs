using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Worship.Gulu
{

    public class EditModeUI : MonoBehaviour
    {

        public UnityEvent<string, int, int, string> onChange = new UnityEvent<string, int, int, string>();

        [SerializeField] InputField m_IpInput;
        [SerializeField] InputField m_PortIncommingInput;
        [SerializeField] InputField m_PortOutgoingInput;
        [SerializeField] InputField m_BarsInput;

        public string ip
        {
            get => m_IpInput.text;
        }

        public int portIncomming
        {
            get => int.Parse(m_PortIncommingInput.text);
        }

        public int portOutgoing
        {
            get => int.Parse(m_PortOutgoingInput.text);
        }

        public string bars
        {
            get => m_BarsInput.text;
        }

        void Awake()
        {
            m_IpInput.onValueChanged.AddListener(OnChange);
            m_PortIncommingInput.onValueChanged.AddListener(OnChange);
            m_PortOutgoingInput.onValueChanged.AddListener(OnChange);
            m_BarsInput.onValueChanged.AddListener(OnChange);
        }

        public void Activate()
        {
            m_IpInput.readOnly = false;
            m_PortIncommingInput.readOnly = false;
            m_PortOutgoingInput.readOnly = false;
            m_BarsInput.readOnly = false;
        }

        public void DeActivate()
        {
            m_IpInput.readOnly = true;
            m_PortIncommingInput.readOnly = true;
            m_PortOutgoingInput.readOnly = true;
            m_BarsInput.readOnly = true;
        }

        public void Fill(string ip, int portIncomming, int portOutgoing, string bars)
        {
            m_IpInput.text = ip;
            m_PortIncommingInput.text = portIncomming.ToString();
            m_PortOutgoingInput.text = portOutgoing.ToString();
            m_BarsInput.text = bars;
        }

        public void Empty()
        {
            m_IpInput.text = "";
            m_PortIncommingInput.text = "-9999";
            m_PortOutgoingInput.text = "-9999";
            m_BarsInput.text = "";
        }

        void OnChange(string _)
        {
            onChange.Invoke(
                m_IpInput.text,
                int.Parse(m_PortIncommingInput.text),
                int.Parse(m_PortOutgoingInput.text),
                m_BarsInput.text
            );
        }

    }

}