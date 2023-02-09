using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class GetIp : MonoBehaviour
{
    List<string> m_IpList = new List<string>();

    public List<string> ipList {
        get => this.m_IpList;
    }

    void Awake()
    {
        // ホスト名を取得する
        string hostname = Dns.GetHostName();
        Debug.Log($"hostname = {hostname}");

        // ホスト名からIPアドレスを取得する
        IPAddress[] adrList = Dns.GetHostAddresses(hostname);
        string ip = "";
        foreach (IPAddress address in adrList)
        {
            string addressStr = address.ToString();
            if(Regex.IsMatch(addressStr, @"\d{1,3}(\.\d{1,3}){3}(/\d{1,2})?"))
            {
                Debug.Log($"ip = {addressStr}");
                this.m_IpList.Add(addressStr);
            }
        }
    }

}
