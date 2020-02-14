using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject optionsCanvas;
    [SerializeField] GameObject loadingScreen;

    [SerializeField] AudioSource mainMusicTrack;
    [SerializeField] AudioSource soundFx;
    [SerializeField] AudioSource loadingSound;
    [SerializeField] Animator anim;

    public Settings settingsData;

    Camera mainCamera;
    bool stop;

    // Start is called before the first frame update
    void Start()
    {
        stop = false;
        mainMusicTrack.Play();
        mainCamera = Camera.main;

        mainMusicTrack.volume = settingsData.musicVolume;
        soundFx.volume = settingsData.soundFxVolume;
        loadingSound.volume = settingsData.soundFxVolume;

        
    }

    //Update the settings in the event that has been adjusted
    private void OnEnable()
    {
        Options.OnSettingChange += UpdateSettingChange;
    }

    private void OnDisable()
    {
        Options.OnSettingChange -= UpdateSettingChange;
    }

    public void UpdateSettingChange()
    {
        mainMusicTrack.volume = settingsData.musicVolume;
        soundFx.volume = settingsData.soundFxVolume;
        loadingSound.volume = settingsData.soundFxVolume;
        AudioListener.volume = settingsData.masterVolume;
    }

    public void TurnLeftOptions()
    {
        soundFx.Play();
        mainCanvas.SetActive(false);
        anim.SetBool("turnLeft", true);
        
    }

    public void ActivateMain()
    {
        
        mainCanvas.SetActive(true);
        anim.SetBool("turnRight", false);
        anim.SetBool("turnLeft", false);
    }

    public void ActivateOptions()
    {
        optionsCanvas.SetActive(true);
    }

    public void BackToMain()
    {
        optionsCanvas.SetActive(false);
        anim.SetBool("turnRight", true);
        soundFx.Play();
    }

    public void TurnToLoad()
    {
        mainCanvas.SetActive(false);
        anim.SetBool("turnToLoading", true);
        soundFx.Play();
    }

    public void ActivateLoading()
    {
        mainMusicTrack.Stop();
        loadingScreen.SetActive(true);
        loadingSound.Play();
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {

        yield return new WaitForSeconds(6.0f);
        GameServices.ServiceManager.Ads.HideBannerContent(); //Hide the banner in the transition of the next screen
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);      

    }
}
