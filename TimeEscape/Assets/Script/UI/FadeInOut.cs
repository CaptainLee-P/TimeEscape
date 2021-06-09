using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public Image image;
    private float startAlpha = 0;
    private float endAlpha = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FadeIn()
    {
        StartCoroutine("FadeInCor");
    }
    public void FadeOut()
    {
        StartCoroutine("FadeOutCor");
    }
    IEnumerator FadeInCor()
    {
        float alphaCount = 1;
        image.color = new Color(0, 0, 0, alphaCount);
        while (alphaCount > startAlpha)
        {
            alphaCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, alphaCount);
        }
        
    }
    IEnumerator FadeOutCor()
    {
        float alphaCount = 0;
        image.color = new Color(0, 0, 0, alphaCount);
        while (alphaCount < endAlpha)
        {
            alphaCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, alphaCount);
        }
    }
}
