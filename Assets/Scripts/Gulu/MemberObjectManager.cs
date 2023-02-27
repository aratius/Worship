using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Worship.Gulu;

namespace Worship.Gulu
{

    public class MemberObjectManager : MonoBehaviour
    {

        [SerializeField] GameObject m_MemberPrefab;
        [SerializeField] EditMode m_EditMode;
        [SerializeField] MemberUIManager m_MemberUI;

        List<GameObject> m_MemberObjecs = new List<GameObject>();

        void Start()
        {
            ModeManager.Instance.onPlayMode.AddListener(PlayMode);
            ModeManager.Instance.onEditMode.AddListener(EditMode);
        }

        void PlayMode()
        {
            // TODO: オブジェクトとOSCクライアント&サーバ作る
            // TODO: OSCは死活監視イベントとってMemberUIにstatusトリガー
            // TODO:  衝突はMemberUI色変化と音プログレスのトリガー送る

            bool success = true;
            for(int i = 0; i < m_EditMode.memberOSCConfigLsit.Count; i++)
            {
                MemberOSCData memberOSCData = m_EditMode.memberOSCConfigLsit[i];

                GameObject member = Instantiate(m_MemberPrefab, transform);
                m_MemberObjecs.Add(member);
                MemberOSC memberOSC = member.GetComponent<MemberOSC>();
                MemberObject memberObject = member.GetComponent<MemberObject>();

                int index = i;

                RectTransform rectTransform = m_MemberUI.GetPosition(index);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(rectTransform.position);
                worldPosition.z = 0f;
                member.transform.localPosition = worldPosition;

                success = memberOSC.Init(memberOSCData.ip, memberOSCData.portIncomming, memberOSCData.portOutgoing, memberOSCData.bars);
                if(!success) break;
                memberOSC.onLive.AddListener(() => OnLive(index));
                memberOSC.onDie.AddListener(() => OnDie(index));

                // Objectセットアップ
                memberObject.onCollided.AddListener((Sign sign) => OnCollided(index, sign));
            }

            if(!success)
            {
                Modal.Instance.Open("OSC Setup failed.", "OK", "OK");
            }

        }

        void EditMode()
        {
            foreach(GameObject member in m_MemberObjecs) Destroy(member);
            m_MemberObjecs.Clear();
        }

        void OnLive(int i)
        {
            m_MemberUI.Live(i);
        }

        void OnDie(int i)
        {
            m_MemberUI.Die(i);
        }

        void OnCollided(int i, Sign sign)
        {
            m_MemberUI.Collide(i);
            bool needEffect = m_MemberUI.Check(i, sign);

            // TODO: 各所へOSCを送る
        }

    }

}