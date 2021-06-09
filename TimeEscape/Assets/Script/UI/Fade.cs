using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fade : MonoBehaviour
{
    public float FadeTime = 2f; // Fadeȿ�� ����ð�

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
        StartCoroutine("fadeoutplay");    //�ڷ�ƾ ����
    }
    public void InStartFadeAnim()

    {
        
        if (isPlaying == true) //�ߺ��������

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
