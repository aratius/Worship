using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Worship.Member;

public class OscSenderSetupper : MonoBehaviour
{

    [SerializeField] private Text m_IpText;
    [SerializeField] private Text m_PortText;
    [SerializeField] private Button m_SubmitButton;

    void Awake()
    {
        int port = Int32.Parse(m_PortText.text);
        OscSender.Instance.Set(m_IpText.text, port);

        m_SubmitButton.onClick.AddListener(OnClick);
    }

    /// <summary>
    /// on click
    /// </summary>
    private void OnClick()
    {
        try {
            int port = Int32.Parse(m_PortText.text);
            OscSender.Instance.Set(m_IpText.text, port);
        } catch(FormatException)
        {
            Debug.LogWarning("Unable to parse");
        }
    }

}
