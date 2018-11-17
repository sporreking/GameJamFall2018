﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInputs", menuName = "Player/Player Input")]
public class PlayerInputSO : ScriptableObject
{
    public List<string> InputLeftX;
    public List<string> InputLeftY;

    public List<string> InputRightX;
    public List<string> InputRightY;

    public List<string> InputJump;
}