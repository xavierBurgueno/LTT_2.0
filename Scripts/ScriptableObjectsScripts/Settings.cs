using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RetroAesthetics;

[CreateAssetMenu(menuName ="SettingsData")]
public class Settings : ScriptableObject
{
    [Header("Sound Settings")]
    [Range(0,1)] public float masterVolume = 1.0f;
    [Range(0, 1)] public float soundFxVolume = 1.0f; 
    [Range(0, 1)] public float musicVolume = 0.45f;
   

    [Header("Camera Setting")]
    public bool useRandomGlitches = true;
    [Range(0.01f, 0.5f)] public float glitchIntesity = 0.01f;
    private RetroCameraEffect.GlitchDirections glitchDirection = RetroCameraEffect.GlitchDirections.Horizontal;
    [Range(0, 8)] public float chromaticAbreration = 3.5f;
    public bool useBottomNoise = true;



    //Toggle methods
    public void ToggleUseRandomGlitches(bool change)
    {
        useRandomGlitches = change;
        if (useRandomGlitches)
        {
            glitchDirection = RetroCameraEffect.GlitchDirections.Horizontal;
           
        }
        else if(!useRandomGlitches)
        {
            glitchDirection = RetroCameraEffect.GlitchDirections.None;
        }
    }

    public void ToggleUseBottomNoise(bool change)
    {
        useBottomNoise = change;    
    }

    public void AdjustGlitchIntensity(float newValue)
    {
        if (newValue < 0.01f || newValue > 0.5f)
            glitchIntesity = 0.01f; //it's set to default if not in range
        else
            glitchIntesity = newValue;

    }

    public void AdjustChromaticAbrerration(float newValue)
    {
        if (newValue < 0.0f || newValue > 8.0f)
            chromaticAbreration = 3.5f;
        else
            chromaticAbreration = newValue;
    }


    //Volume adjustments
    public void AdjustMasterVolume(float newVolume)
    {
        if (newVolume < 0 || newVolume > 1)
            newVolume = 1.0f; //Set Back to Default. Might make a constant, but IDK yet
        else
            masterVolume = newVolume;
    }

    public void AdjustSoundFXVolume(float newVolume)
    {
        if (newVolume < 0 || newVolume > 1)
            newVolume = 1.0f;
        else
            soundFxVolume = newVolume;
    }

    public void AdjustMusicVolume(float newVolume)
    {
        if (newVolume < 0 || newVolume > 1)
            newVolume = 1.0f;
        else
            musicVolume = newVolume;
    }


    //Accessor Methods
    public RetroCameraEffect.GlitchDirections GetGlitchDirection()
    {
        return glitchDirection;
    }
}
