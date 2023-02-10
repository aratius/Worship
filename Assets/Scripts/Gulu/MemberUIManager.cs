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

            Mode.Instance.onPlayMode.AddListener(PlayMode);
            Mode.Instance.onEditMode.AddListener(EditMode);

            onChangeSelected.Invoke(null);
        }

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
            foreach (MemberUI m in m_Members) m.DeSelect();
            m_Cross.AddMode();
            onChangeSelected.Invoke(null);
        }

        void PlayMode()
        {
            foreach (MemberUI m in m_Members) m.PlayMode();
            m_Cross.gameObject.SetActive(false);
        }

        void EditMode()
        {
            foreach (MemberUI m in m_Members) m.EditMode();
            m_Cross.gameObject.SetActive(true);
        }

        void RenameAll()
        {
            for(int i = 0; i < m_Members.Count; i++) m_Members[i].Name($"Member_{i}");
        }

    }

}