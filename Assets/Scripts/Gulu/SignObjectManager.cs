using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Worship.Gulu
{

    [System.Serializable]
    struct ShapePrefabSet
    {
        public Shape shape;
        public GameObject go;
    }

    public class SignObjectManager : MonoBehaviour
    {
        [SerializeField] List<ShapePrefabSet> m_Prefabs;
        [SerializeField] SelectShape m_SelectShape;
        [SerializeField] SelectSign m_SelectSign;
        [SerializeField] Slider m_Speed;

        ShapeBase m_Current;
        bool m_IsGrabbing = false;
        Dictionary<Shape, GameObject> m_PrefabTable = new Dictionary<Shape, GameObject>();
        Vector2 m_MoveSpeed = Vector2.zero;
        Vector2 m_LastPosition = Vector2.zero;

        void Start()
        {
            foreach(ShapePrefabSet shapePrefabSet in m_Prefabs) m_PrefabTable.Add(shapePrefabSet.shape, shapePrefabSet.go);
        }

        void Update()
        {

            // UI などが操作されている場合は処理しない
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Up();
                return;
            }

            if(Input.GetMouseButtonDown(0))
            {
                Down(ScreenToWorldPoint(Input.mousePosition));
            }
            else if(Input.GetMouseButtonUp(0))
            {
                Up();
            }
            else if(Input.GetMouseButton(0) && m_IsGrabbing)
            {
                Move(ScreenToWorldPoint(Input.mousePosition));
            }
            else if(Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Began)
                {
                    Down(ScreenToWorldPoint(touch.position));
                }
                else if(touch.phase == TouchPhase.Ended)
                {
                    Up();
                }
                else if(m_IsGrabbing)
                {
                    Move(ScreenToWorldPoint(touch.position));
                }
            }

        }

        void Down(Vector2 position)
        {
            Shape shape = m_SelectShape.current;
            Sign sign = m_SelectSign.current;
            GameObject signObject = Instantiate(m_PrefabTable[shape], transform);
            signObject.GetComponent<SignIdentifier>().value = sign;
            if(m_Current) Destroy(m_Current.gameObject);
            m_Current = signObject.GetComponent<ShapeBase>();
            m_Current.Move(position);

            m_IsGrabbing = true;

            m_LastPosition = position;
        }

        void Move(Vector2 position)
        {
            m_Current.Move(position);
            m_MoveSpeed += position - m_LastPosition;
            m_MoveSpeed *= .9f;
            m_LastPosition = new Vector2(position.x, position.y);
        }

        void Up()
        {
            if(m_Current == null) return;
            m_Current.Release(m_Speed.value);
            m_Current.SetDirection(new Vector2(Mathf.Sign(m_MoveSpeed.x), Mathf.Sign(m_MoveSpeed.y)));
            m_Current = null;
            m_MoveSpeed = Vector2.zero;
            m_IsGrabbing = false;
        }

        Vector3 ScreenToWorldPoint(Vector2 position)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(position);
            pos.z = 0f;
            return pos;
        }

    }

}