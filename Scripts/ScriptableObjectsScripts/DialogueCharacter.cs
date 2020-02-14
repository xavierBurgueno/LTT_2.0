using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueCharacter {
    
    public string characterName;
    public Sprite characterImage;
    [TextArea(2, 5)] public string characterText;
}
