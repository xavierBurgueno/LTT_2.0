using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimatedText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] string loadingText = "Loading...  ";
    int iter = 0;
    float timer = 1f;
    [SerializeField] [Range(0.1f, 1.0f)] float animatedTextSpeed = 0.3f; //default speed per char


    private void OnEnable()
    {
        textMesh.text = "";
        timer = animatedTextSpeed;
        iter = 0;
    }


    // Start is called before the first frame update
    void Start()
    {
        textMesh.text = "";
        timer = animatedTextSpeed;
        iter = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (loadingText.Length > 0)
            LoadingAnimated();
        else
            Debug.LogError("No text entered to animated, please enter text");

    }

    private void LoadingAnimated()
    {
        string text = textMesh.text;
        timer -= Time.deltaTime;
        if (text.Length < loadingText.Length)
        {
            if (timer <= 0)
            {
                textMesh.text += loadingText[iter];
                iter++;
                timer = animatedTextSpeed;
            }
        }
        else if (text.Length >= loadingText.Length)
        {
            textMesh.text = "";
            iter = 0;
        }
    }
}
