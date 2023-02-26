using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayIp : MonoBehaviour
{

    [SerializeField] GetIp m_IpGetter;
    int _crr = 0;

    void Start()
    {
        Show();
    }

    public void Next()
    {
        _crr++;
        Show();
    }

    private void Show()
    {
        if(GetComponent<Text>() != null)
        {
            GetComponent<Text>().text = this.m_IpGetter.ipList[this._crr % this.m_IpGetter.ipList.Count];
        }
        else if(GetComponent<TMP_Text>() != null)
        {
            GetComponent<TMP_Text>().text = this.m_IpGetter.ipList[this._crr % this.m_IpGetter.ipList.Count];
        }
    }

}
