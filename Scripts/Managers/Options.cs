using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Options : MonoBehaviour
{
    [SerializeField] Settings setData;
    [SerializeField] GameObject applyButton;

    public static Action OnSettingChange;

    public void TogggleBottomNoise(bool newValue)
    {
        setData.ToggleUseBottomNoise(newValue);
        applyButton.SetActive(true);

    }

    public void ToggleScreenGlitch(bool newValue)
    {
        setData.ToggleUseRandomGlitches(newValue);
        applyButton.SetActive(true);

        
    }

    public void AdjustGlitch(float newValue)
    {
        setData.AdjustGlitchIntensity(newValue);
        applyButton.SetActive(true);

        
    }

    public void AdjustChromatic(float newValue)
    {
        setData.AdjustChromaticAbrerration(newValue);
        applyButton.SetActive(true);

        
    }

    public void AdjustMasterVolume(float newValue)
    {
        setData.AdjustMasterVolume(newValue);
        applyButton.SetActive(true);

        
    }

    public void AdjustSoundFxVolume(float newValue)
    {
        setData.AdjustSoundFXVolume(newValue);
        applyButton.SetActive(true);

        
    }

    public void AdjustMusicVolume(float newValue)
    {
        setData.AdjustMusicVolume(newValue);
        applyButton.SetActive(true);

        
    }

    public void ApplyChanges()
    {
        var camRef = GameObject.FindWithTag("MainCamera");
        camRef.GetComponent<RetroAesthetics.RetroCameraEffect>().enabled = false;
        camRef.GetComponent<RetroAesthetics.RetroCameraEffect>().enabled = true;
        

        //When things are applied then update the sounds in main or anywhere else
        if (OnSettingChange != null)
            OnSettingChange();
    }
}
