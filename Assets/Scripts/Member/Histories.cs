using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Worship.Member;

namespace Worship.Member
{

  public struct HistoryValues
  {
    public string ts;
    public string data;
    public string value;
    public HistoryValues(string _ts, string _data, string _value)
    {
      ts = _ts;
      data = _data;
      value = _value;
    }
  }

  public class Histories : MonoBehaviour
  {

    [SerializeField] private History[] m_Histories;

    /// <summary>
    /// set newest value
    /// </summary>
    /// <param name="ts"></param>
    /// <param name="data"></param>
    /// <param name="value"></param>
    public void Set(string ts, string data, string value)
    {
      HistoryValues lastHistoryValues = new HistoryValues(ts, data, value);
      foreach(History history in m_Histories)
      {
        HistoryValues thisHistoryValues = history.Get();
        history.Set(lastHistoryValues);
        lastHistoryValues = thisHistoryValues;
      }
    }

  }

}