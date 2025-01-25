using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePrefab : MonoBehaviour
{
    [SerializeField] public Animator anim;
    public CanvasGroup fadeGroup;

    // Start is called before the first frame update
    void Start()
    {
        fadeGroup = GetComponent<CanvasGroup>();
        StartCoroutine(FadeIn());
    }

    private void Update()
    {
        Debug.Log(fadeGroup.alpha);
    }

    public IEnumerator FadeIn()
    {
        Debug.Log("PLAY ANIM FADE IN");
        this.gameObject.SetActive(true);
        fadeGroup.alpha = 1.0f;
        anim.Play("FadeIn", 0, 0.0f);
        yield return new WaitForSeconds(1.0f);
    }

    public IEnumerator FadeOut()
    {
        Debug.Log("PLAY ANIM FADE OUT");
        this.gameObject.SetActive(true);
        fadeGroup.alpha = 0.0f;
        anim.Play("FadeOut", 0, 0.0f);
        yield return null;
    }
}
