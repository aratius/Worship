using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Modal : SingletonMonoBehaviour<Modal>
{

    [SerializeField] GameObject m_Background;
    [SerializeField] GameObject m_Content;
    [SerializeField] TextMeshProUGUI m_Message;
    [SerializeField] TextMeshProUGUI m_NoMessage;
    [SerializeField] TextMeshProUGUI m_YesMessage;

    CanvasGroup m_BackgroundCanvas;
    CanvasGroup m_ContentCanvas;

    RectTransform m_ContentTransform;

    Vector3 m_ContentDefaultPosition;

    Sequence m_Sequence;

    void Awake()
    {
        m_BackgroundCanvas = m_Background.GetComponent<CanvasGroup>();
        m_ContentCanvas = m_Content.GetComponent<CanvasGroup>();
        m_ContentTransform = m_Content.GetComponent<RectTransform>();

        m_BackgroundCanvas.alpha = 0f;
        m_ContentCanvas.alpha = 0f;

        m_Background.SetActive(false);
        m_Content.SetActive(false);

        m_ContentDefaultPosition = m_ContentTransform.localPosition;

        Invoke("_Open", 2f);
        Invoke("Close", 4f);
    }

    void _Open()
    {
        Open("hello");
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="message"></param>
    /// <param name="noText"></param>
    /// <param name="yesText"></param>
    public void Open(string message, string noText = "No", string yesText = "Yes")
    {
        if(m_Sequence != null) m_Sequence.Kill();
        m_Sequence = DOTween.Sequence().OnStart(() => {
            m_Message.text = message;
            m_NoMessage.text = noText;
            m_YesMessage.text = yesText;
            m_ContentTransform.localPosition = m_ContentDefaultPosition + new Vector3(0f, -10f, 0f);
            m_Background.SetActive(true);
            m_Content.SetActive(true);
        });
        m_Sequence.Append(
            m_BackgroundCanvas.DOFade(1f, .5f)
        ).Join(
            DOTween.Sequence().SetDelay(.3f)
                .Append(
                    m_ContentTransform.DOLocalMove(m_ContentDefaultPosition, .5f).SetEase(Ease.OutExpo)
                ).Join(
                    m_ContentCanvas.DOFade(1f, .5f)
                )
        );
    }

    public void Close()
    {
        if(m_Sequence != null) m_Sequence.Kill();
        m_Sequence = DOTween.Sequence().OnComplete(() => {
            m_Background.SetActive(false);
            m_Content.SetActive(false);
        });
        m_Sequence.Append(
            m_BackgroundCanvas.DOFade(0f, .5f).SetDelay(.3f)
        ).Join(
            DOTween.Sequence()
                .Append(
                    m_ContentTransform.DOLocalMove(m_ContentDefaultPosition + new Vector3(0f, -10f, 0f), .5f).SetEase(Ease.OutExpo)
                ).Join(
                    m_ContentCanvas.DOFade(0f, .5f)
                )
        );
    }

}
