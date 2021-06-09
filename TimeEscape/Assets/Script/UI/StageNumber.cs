using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StageNumber : MonoBehaviour
{
    string stage = null;
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {

        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

        stage = DataManager.instance.currentScene;
        text.text = stage;

    }
}
