using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Worship.Gulu;

namespace Worship.Gulu
{

    public struct MemberOSCData
    {
        public string ip;
        public int portIncomming;
        public int portOutgoing;
        public string bars;

        public MemberOSCData(string _ip, int _portIncomming, int _portOutgoing, string _bars)
        {
            ip = _ip;
            portIncomming = _portIncomming;
            portOutgoing = _portOutgoing;
            bars = _bars;
        }

    }

    public class EditMode : MonoBehaviour
    {

        [SerializeField] EditModeUI m_EditModeUI;
        [SerializeField] MemberUIManager m_MemberUIManager;

        List<MemberOSCData> m_MemberOSCConfigLsit = new List<MemberOSCData>();
        int? m_CurrentOSCConfigIndex;

        public List<MemberOSCData> memberOSCConfigLsit => m_MemberOSCConfigLsit;

        void Awake()
        {
            m_MemberUIManager.onChangeSelected.AddListener(onChangeSelected);
            m_EditModeUI.onChange.AddListener(onEditOSCConfig);
        }

        void onChangeSelected(int? i)
        {
            if(i == null)
            {
                // run before m_EditModeUI.Empty()()
                m_CurrentOSCConfigIndex = null;
                m_EditModeUI.DeActivate();
                m_EditModeUI.Empty();
            }
            else
            {
                m_EditModeUI.Activate();
                while(i > m_MemberOSCConfigLsit.Count - 1)
                {
                    // new one
                    m_MemberOSCConfigLsit.Add(new MemberOSCData("127.0.0.1", 9998, 9999, "0,1,2"));
                }
                m_CurrentOSCConfigIndex = i ?? 0;
                MemberOSCData crr = m_MemberOSCConfigLsit[m_CurrentOSCConfigIndex ?? 0];
                Debug.Log($"{crr.ip}:{crr.portIncomming}:{crr.portOutgoing}:{crr.bars}");

                m_EditModeUI.Fill(crr.ip, crr.portIncomming, crr.portOutgoing, crr.bars);
            }
        }

        void onEditOSCConfig(string ip, int portIncomming, int portOutgoing, string bars)
        {
            if(m_CurrentOSCConfigIndex == null) return;
            MemberOSCData crr = m_MemberOSCConfigLsit[m_CurrentOSCConfigIndex ?? 0];
            crr.ip = ip;
            crr.portIncomming = portIncomming;
            crr.portOutgoing = portOutgoing;
            crr.bars = bars;
            m_MemberOSCConfigLsit[m_CurrentOSCConfigIndex ?? 0] = crr;

        }
    }

}