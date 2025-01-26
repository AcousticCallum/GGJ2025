using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public FadePrefab uiFade;

    // Start is called before the first frame update
    void Start()
    {
        uiFade = FindAnyObjectByType<FadePrefab>();
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPlay()
    {
        if (uiFade)
        {
            StartCoroutine(uiFade.FadeOut());
            StartCoroutine(Play());
        } else
        {
            Debug.Log("uiFade is null!");
        }
    }

    public void ButtonQuit()
    {
        if (uiFade)
        {
            StartCoroutine(uiFade.FadeOut());
            StartCoroutine(Quit());
        }
        else
        {
            Debug.Log("uiFade is null!");
        }
    }

    public IEnumerator Play()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("LevelTest");
    }

    public IEnumerator Quit()
    {
        yield return new WaitForSeconds(1.5f);
        Application.Quit();
    }
}
