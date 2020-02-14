using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] Scripts scriptToRun;
    [SerializeField] TextMeshProUGUI dialogue;
    [Space]
    [SerializeField] [Range(0.01f, 0.5f)] float typeSpeed = 0.03f;

    [SerializeField] Animator anim;
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] Image image;

    public static Action OnDialogueEnabled;
    public static Action OnDialogueDisabled;

    string stringToDisplay;
    bool isDialogueFinished = false; 
    bool isDialogueActive = false;
   
    bool isTyping = false;
   

    private void OnEnable()
    {
        if (OnDialogueEnabled != null)
            OnDialogueEnabled();

        anim.SetBool("isOpen", true);
        SoundManager.instance.PlayRaw("MenuWindow");
        scriptToRun.ResetDialogue();
        name.text = scriptToRun.GetCharaDialogueName();
        image.sprite = scriptToRun.GetCharaDialogueImage();
        StartDialogue();
    }

    private void OnDisable()
    {
        scriptToRun.ResetDialogue();
        isDialogueFinished = false;
        isDialogueActive = false;

        if (OnDialogueDisabled != null)
            OnDialogueDisabled();
    }

    public void ContinueButton()
    {
        SoundManager.instance.PlayRaw("ButtonClick");
        if (gameObject.activeSelf == false)
            return;

        if(isTyping)
        {
            StopAllCoroutines();
            dialogue.text = scriptToRun.GetCharaDialogueText();
            isTyping = false;
            return;
        }

        if (!isTyping)
        {
            StopAllCoroutines();
            scriptToRun.NextCharaDialogue();
            
            //update the img and name
            name.text = scriptToRun.GetCharaDialogueName();
            image.sprite = scriptToRun.GetCharaDialogueImage();

            if (scriptToRun.GetDialogueStatus())
            {
                StartCoroutine(CloseDialogue());
                return;
            }
            StartDialogue();
        }
    }

    public void StartDialogue()
    {
        StartCoroutine(TypeText(scriptToRun.GetCharaDialogueText()));
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
       
        dialogue.text = "";
        yield return new WaitForSeconds(0.5f);
        foreach (char let in text.ToCharArray())
        {
            dialogue.text += let;
            yield return new WaitForSeconds(typeSpeed);
            SoundManager.instance.Play("DialogueBeep");
        }
        isTyping = false;
    }
   
   

    IEnumerator CloseDialogue()
    {
        anim.SetBool("isOpen", false);
        SoundManager.instance.PlayRaw("MenuWindow");
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }

}