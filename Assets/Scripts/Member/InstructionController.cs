using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Worship.Member;
using Worship.Common;

public class InstructionController : MonoBehaviour
{

  [SerializeField] private Histories _histories;

  void Start()
  {
    OscReceiver.Instance.onInstruction.AddListener(OnInstruction);
    Sound.LoadSe("standUp", "standUp");
    Sound.LoadSe("sitDown", "sitDown");
    Sound.LoadSe("handsUp", "handsUp");
    Sound.LoadSe("handsDown", "handsDown");
    Sound.LoadSe("tutorial", "tutorial");
    Sound.LoadSe("finish", "finish");
  }

  /// <summary>
  /// on sign
  /// </summary>
  /// <param name="address"></param>
  void OnInstruction(int data)
  {
    // TODO: 音再生

    Instruction? instruction = data as Instruction?;
    if(instruction == null) return;
    if(instruction == Instruction.HandsUp)
    {
      Sound.PlaySe("handsUp");
    } else if (instruction == Instruction.HandsDown)
    {
      Sound.PlaySe("handsDown");
    } else if (instruction == Instruction.StandUp)
    {
      Sound.PlaySe("standUp");
    } else if (instruction == Instruction.SitDown)
    {
      Sound.PlaySe("sitDown");
    } else if(instruction == Instruction.Tutorial)
    {
      Sound.PlaySe("tutorial");
    } else if (instruction == Instruction.Finish)
    {
      Sound.PlaySe("finish");
    } else if(instruction == Instruction.Stop) {
      Sound.StopSe();
    } {
      Debug.LogWarning("undefined instruction");
      return;
    };

    this._histories.Set(DateTime.Now.ToString("HH:mm:ss"), data.ToString(), "undefined");
  }

}
