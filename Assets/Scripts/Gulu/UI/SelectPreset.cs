using UnityEngine;
using UnityEngine.UI;
using Worship.Gulu;
using DG.Tweening;

namespace Worship.Gulu
{

  public class SelectPreset : SelectBase<Shape>
  {

    public int current
    {
      get => m_SelectedButton.GetComponent<PresetIdentifier>().value;
    }

    internal override void OnFocus(Button button)
    {
      button.gameObject.GetComponent<CanvasGroup>().alpha = 1f;
      RectTransform rect = button.gameObject.GetComponent<RectTransform>();
      DOTween.To(
        () => rect.sizeDelta.x,
        v => rect.sizeDelta = new Vector2(v, v),
        60f,
        .2f
      ).SetEase(Ease.OutExpo);
    }

    internal override void OnBlur(Button button)
    {
      button.gameObject.GetComponent<CanvasGroup>().alpha = .2f;
      RectTransform rect = button.gameObject.GetComponent<RectTransform>();
      DOTween.To(
        () => rect.sizeDelta.x,
        v => rect.sizeDelta = new Vector2(v, v),
        40f,
        .5f
      ).SetEase(Ease.OutExpo);
    }
  }
}