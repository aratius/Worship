using UnityEngine;

namespace Worship.Gulu
{

  public enum Sign
  {
    Hand,
    Foot,
    HandsUp,
    HandsDown,
    StandUp,
    SitDown,
    Tutorial,
    Finish,
    Stop
  }

  public enum Shape
  {
    Horizontal,
    Vertical,
    Circle,
    Cursor
  }

  public static class ColorSet
  {
    public static Color safe = new Color(46f / 255f, 191f / 255f, 73f / 255f);
    public static Color danger = new Color(188f / 255f, 47f / 255f, 47f / 255f);
  }

}