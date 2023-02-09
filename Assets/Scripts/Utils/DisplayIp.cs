using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class DisplayIp : MonoBehaviour
{

    [SerializeField] GetIp m_IpGetter;
    int _crr = 0;

    void Start()
    {
        this._Show();
    }

    public void Next()
    {
        this._crr++;
        this._Show();
    }

    private void _Show()
    {
        this.GetComponent<Text>().text = this.m_IpGetter.ipList[this._crr % this.m_IpGetter.ipList.Count];
    }

}
