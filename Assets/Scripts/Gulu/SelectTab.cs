using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTab : MonoBehaviour
{

    [SerializeField] Button m_ButtonPlaymode;
    [SerializeField] Button m_ButtonEditmode;
    [SerializeField] GameObject[] m_GameObjectsEnableOnPlaymode;
    [SerializeField] GameObject[] m_GameObjectsEnableOnEditmode;

    void Start()
    {
        m_ButtonPlaymode.onClick.AddListener(Playmode);
        m_ButtonEditmode.onClick.AddListener(Editmode);
        Editmode();
    }

    void Playmode()
    {
        m_ButtonPlaymode.transform.parent.SetAsFirstSibling();
        m_ButtonPlaymode.transform.parent.GetComponent<CanvasGroup>().alpha = 1f;
        m_ButtonEditmode.transform.parent.GetComponent<CanvasGroup>().alpha = .2f;
        foreach(GameObject go in m_GameObjectsEnableOnPlaymode) go.SetActive(true);
        foreach(GameObject go in m_GameObjectsEnableOnEditmode) go.SetActive(false);
    }

    void Editmode()
    {
        m_ButtonEditmode.transform.parent.SetAsFirstSibling();
        m_ButtonEditmode.transform.parent.GetComponent<CanvasGroup>().alpha = 1f;
        m_ButtonPlaymode.transform.parent.GetComponent<CanvasGroup>().alpha = .2f;
        foreach(GameObject go in m_GameObjectsEnableOnEditmode) go.SetActive(true);
        foreach(GameObject go in m_GameObjectsEnableOnPlaymode) go.SetActive(false);
    }

}
