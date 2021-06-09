using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HP : MonoBehaviour
{
    private GameObject player;
    public Image HPbar;
    public TextMeshProUGUI HpText;
    
    private int Hp;
    private int MaxHp;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Hp = DataManager.instance.playerHP;
        MaxHp = DataManager.instance.playerMaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        Hp = DataManager.instance.playerHP;
        MaxHp = DataManager.instance.playerMaxHP;
        HPbar.fillAmount = Hp / 100f;

        HpText.text = Hp.ToString()+"/"+MaxHp.ToString();
        
    }
}
