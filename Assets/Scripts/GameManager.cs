using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Win()
    {
        FindAnyObjectByType<PlayerUI>().EndState(0);
    }

    public void Lose()
    {
        FindAnyObjectByType<PlayerUI>().EndState(1);
    }
}
