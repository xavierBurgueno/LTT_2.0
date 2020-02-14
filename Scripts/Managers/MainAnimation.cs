using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] MainMenuManager mainMan;

    public void CallActivateMain()
    {
        mainMan.ActivateMain();
    }

    public void CallActivateOptions()
    {
        mainMan.ActivateOptions();
    }

    public void CallLoadingScreen()
    {
        mainMan.ActivateLoading();
    }

}
