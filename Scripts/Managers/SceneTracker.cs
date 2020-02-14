using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{

    private int previousSceneIndex;
    private int currentSceneIndex;
    private int nextScene;

    public static SceneTracker instance;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }
    void Start()
    {
        


      
    }

    //TODO: Uncomment after initial Test

    public void LoadNextScene()
    {
                var nextScene = previousSceneIndex + 1;
                 SceneManager.LoadScene(nextScene);

        Debug.Log("Loading Next");
    }

    public void ReplayScene()
    {
         SceneManager.LoadScene(previousSceneIndex); 
        Debug.Log("Replaying Scene");
    }

    public void RecordCurrentScene()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void UpdateIndex()
    {
        previousSceneIndex = currentSceneIndex;
    }

    public void LoadWinScreen()
    {
        SceneManager.LoadScene("WinTransition");
    }

}

//TODO There is bug in here Loads wrong scenes