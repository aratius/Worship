using UnityEngine;
using UnityEngine.UI;
using Worship.Gulu;

namespace Worship.Gulu
{

  public class SelectSign : SelectBase<Sign>
  {

    public Sign current
    {
      get => m_SelectedButton.GetComponent<SignIdentifier>().value;
    }

    internal override void OnFocus(Button button)
    {
      button.gameObject.GetComponent<CanvasGroup>().alpha = 1f;
    }

    internal override void OnBlur(Button button)
    {
      button.gameObject.GetComponent<CanvasGroup>().alpha = .2f;
    }
  }
}