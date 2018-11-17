using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInput1", menuName = "Player Input" )]
public class PlayerInputSO : ScriptableObject {
    public string InputLeftX;
    public string InputLeftY;

    public string InputRightX;
    public string InputRightY;
}
