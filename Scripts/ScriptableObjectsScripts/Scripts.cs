using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Script")]
public class Scripts : ScriptableObject
{
    [SerializeField] private DialogueCharacter[] characterDialogue;
    private int iter = 0;
    private bool isScriptFinished = false;

    public void NextCharaDialogue()
    {
        if (iter < characterDialogue.Length - 1)
            iter++;
        else
        {
            isScriptFinished = true;
            Debug.Log("Finished Dialogue");
        }
    }

    public void PreviousCharaDialogue()
    {
        if (iter > 0)
            iter--;
        else
            Debug.LogError("At start of Dialogue Script");
    }

    public string GetCharaDialogueText()
    {
        return characterDialogue[iter].characterText;
    }

    public string GetCharaDialogueName()
    {
        return characterDialogue[iter].characterName;
    }

    public Sprite GetCharaDialogueImage()
    {
        return characterDialogue[iter].characterImage;
    }
    
    public bool GetDialogueStatus()
    {
        return isScriptFinished;
    }

    public void ResetDialogue()
    {
        isScriptFinished = false;
        iter = 0;
    }
}
