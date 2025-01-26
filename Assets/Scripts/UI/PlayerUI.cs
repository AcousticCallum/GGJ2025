using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    public bool isPaused;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;
    public bool hasGameEnded;

    public FadePrefab uiFade;

    // Start is called before the first frame update
    void Start()
    {
        uiFade = FindAnyObjectByType<FadePrefab>();
        isPaused = false;
        hasGameEnded = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused && !hasGameEnded)
            {
                UnPause();
            }
            else if (!hasGameEnded)
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        pauseScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.0f;
    }

    public void UnPause()
    {
        Time.timeScale = 1.0f;
        pauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }

    public void EndState(int state)
    {
        hasGameEnded = true;

        switch (state)
        {
            case 0:
                winScreen.SetActive(true);
                break;
            case 1:
                loseScreen.SetActive(true);
                break;
        }

        Time.timeScale = 0.0f;
    }

    public void UITransition(int type)
    {
        StartCoroutine(UITransitionWithFade(type));
    }

    public IEnumerator UITransitionWithFade(int type)
    {
        UnPause();
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        StartCoroutine(uiFade.FadeOut());
        yield return new WaitForSeconds(2.0f);

        Player player = FindAnyObjectByType<Player>();
        CinemachineVirtualCamera cinemachineCam = FindAnyObjectByType<CinemachineVirtualCamera>();
        CameraTarget camTarget = FindAnyObjectByType<CameraTarget>();
        FollowTarget followTarget = FindAnyObjectByType<FollowTarget>();

        Destroy(player.gameObject);
        Destroy(cinemachineCam.gameObject);
        Destroy(camTarget.gameObject);
        Destroy(followTarget.gameObject);

        switch (type) {
            case 0:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case 1:
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }
}
