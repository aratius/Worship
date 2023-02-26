using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Worship.Gulu;

namespace Worship.Gulu
{

    public class MemberUIManager : MonoBehaviour
    {

        public UnityEvent<int?> onChangeSelected = new UnityEvent<int?>();

        [SerializeField] GameObject m_MemberUIPrefab;
        [SerializeField] MemberUIBackground m_MemberUIBackground;
        [SerializeField] Cross m_Cross;

        List<MemberUI> m_Members = new List<MemberUI>();
        MemberUI? m_Selected;

        void Start()
        {
            m_MemberUIBackground.onClick.AddListener(DeSelectAll);
            m_Cross.onAdd.AddListener(OnAdd);
            m_Cross.onRemove.AddListener(OnRemove);

            ModeManager.Instance.onPlayMode.AddListener(PlayMode);
            ModeManager.Instance.onEditMode.AddListener(EditMode);

            onChangeSelected.Invoke(null);
        }

        /// <summary>
        /// Common
        /// </summary>

        void PlayMode()
        {
            foreach (MemberUI m in m_Members) m.PlayMode();
        }

        void EditMode()
        {
            foreach (MemberUI m in m_Members) m.EditMode();
        }

        /// <summary>
        /// Play Mode
        /// </summary>

        public RectTransform GetPosition(int i)
        {
            return m_Members[i].GetComponent<RectTransform>();
        }

        public void Live(int i)
        {
            Debug.Log("### Live");
            // TODO: 緑点滅
            m_Members[i].Live();
        }

        public void Die(int i)
        {
            Debug.Log("### Die");
            // TODO: 赤点滅
            m_Members[i].Die();
        }

        public void Collide(int i)
        {
            Debug.Log("### Collide");
            m_Members[i].Collide();
        }

        public bool Check(int i, Sign sign)
        {
            return m_Members[i].Check(sign);
        }

        /// <summary>
        /// Edit Mode
        /// </summary>

        void OnAdd()
        {
            GameObject memberUI = Instantiate(m_MemberUIPrefab, this.transform);
            MemberUI memberUIScript = memberUI.GetComponent<MemberUI>();
            memberUIScript.onGrab.AddListener(OnGrab);
            m_Members.Add(memberUIScript);
            memberUI.GetComponent<RectTransform>().localPosition = new Vector2(0f, 0f);

            m_Selected = memberUIScript;
            m_Selected.Select();
            foreach (MemberUI m in m_Members)
            {
                if(m != memberUIScript) m.DeSelect();
            }
            m_Cross.RemoveMode();

            RenameAll();
            onChangeSelected.Invoke(m_Members.Count - 1);
        }

        void OnRemove()
        {
            m_Members.Remove(m_Selected);
            Destroy(m_Selected.gameObject);
            m_Cross.AddMode();

            RenameAll();
            onChangeSelected.Invoke(null);
        }

        void OnGrab(MemberUI memberUIScript)
        {
            m_Selected = memberUIScript;
            m_Selected.Select();
            for(int i = 0; i < m_Members.Count; i++)
            {
                MemberUI m = m_Members[i];
                if(m != memberUIScript) m.DeSelect();
                else onChangeSelected.Invoke(i);
            }
            m_Cross.RemoveMode();
        }

        void DeSelectAll()
        {
            if(ModeManager.Instance.mode == Mode.Play) return;
            foreach (MemberUI m in m_Members) m.DeSelect();
            m_Cross.AddMode();
            onChangeSelected.Invoke(null);
        }

        void RenameAll()
        {
            for(int i = 0; i < m_Members.Count; i++) m_Members[i].Name($"Member_{i}");
        }

    }

}