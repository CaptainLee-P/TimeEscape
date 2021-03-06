using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fade : MonoBehaviour
{
    public float FadeTime = 2f; // Fade효과 재생시간

    Image fadeImg;

    float start;

    float end;

    float time = 0f;

    bool isPlaying = false;
    // Start is called before the first frame update
    private void Awake()
    {
        fadeImg = GetComponent<Image>();

        InStartFadeAnim();
    }
    

    public void OutStartFadeAnim()
    {
        if (isPlaying)
        {
            return;
        }
        start = 1f;
        end = 0f;
        StartCoroutine("fadeoutplay");    //코루틴 실행
    }
    public void InStartFadeAnim()

    {
        
        if (isPlaying == true) //중복재생방지

        {

            return;

        }

        StartCoroutine("fadeIntanim");

    }

    IEnumerator fadeIntanim()

    {
        Debug.Log("OK");
        isPlaying = true;

        Color fadecolor = fadeImg.color;
        time = 0f;
        fadecolor.a = Mathf.Lerp(start, end, time);



        while (fadecolor.a > 0f)

        {

            time += Time.deltaTime / FadeTime;

            fadecolor.a = Mathf.Lerp(start, end, time);

            fadeImg.color = fadecolor;

            yield return null;

        }

        isPlaying = false;
        //StartCoroutine("fadeoutplay");

    }
    IEnumerator fadeoutplay()

    {

        isPlaying = true;

        Color fadecolor = fadeImg.color;
        time = 0f;
        fadecolor.a = Mathf.Lerp(start, end, time);



        while (fadecolor.a > 0f)

        {

            time += Time.deltaTime / FadeTime;

            fadecolor.a = Mathf.Lerp(start, end, time);

            fadeImg.color = fadecolor;

            yield return null;

        }

        isPlaying = false;

    }
}
