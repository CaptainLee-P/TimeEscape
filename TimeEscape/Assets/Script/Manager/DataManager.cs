using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

using System.Runtime.Serialization.Formatters.Binary;

public class DataManager : MonoBehaviour
{

    private static DataManager _instance = null;

    public static DataManager instance { get { return _instance; } }
    public AudioSource audioSource;
    public AudioClip dieSound;
    //UI
    public int stageNumber
    {
        get;
        private set;
    }
    //UI
    public int lifePoint
    {
        get;
        private set;
    }
    public int lifePointMax
    {
        get;
        private set;
    }
    //player,monsterAI
    public int playerHP
    {
        get;
        private set;
    }
    public int playerMaxHP
    {
        get;
        private set;
    }

    public string currentScene
    {
        get;
        private set;
    }

    public int currentDamage
    {
        get;
        private set;
    }
    private bool playerAttackable;
    private void Awake()
    {
        //중복생성방지
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != null)
            {
                Destroy(this.gameObject);
            }
        }
        currentScene = SceneManager.GetActiveScene().name;

    }
    
   
    // Start is called before the first frame update
    void Start()
    {
        //Load로 매시작마다 변경
        stageNumber = 1;
        lifePoint = 2;
        lifePointMax = 2; 
        //Load로 매시작마다 변경 or 이어하기 때만 변경
        playerMaxHP = 100;
        playerHP = playerMaxHP;
        playerAttackable = true;

        Debug.Log(currentScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackToPlayer(int Damage)
    {
        currentDamage = Damage;
        if (playerAttackable){ 
            playerHP -= Damage;
            playerAttackable = false;
            if (playerHP <= 0)
            {
                audioSource.PlayOneShot(dieSound);
                playerHP = 0;
                //각 스테이지의 원점으로 이동
                GameManager.instance.StageReset();
            }
            Debug.Log(playerHP);
            StartCoroutine("AttackToPlayerDelay");
        }
    }
    public void PlayerDeathZone()
    {
        playerHP = 0;
        GameManager.instance.StageReset();
    }
    IEnumerator AttackToPlayerDelay()
    {
        yield return new WaitForSeconds(1f);
        playerAttackable = true;
    }
    public void PlayerStatusReset()
    {
        if (lifePoint > 0)
        {
            lifePoint--;
            playerHP = playerMaxHP;
        }
        else
        {
            lifePoint = lifePointMax;
            playerHP = playerMaxHP;
        }
        
    }
    public void stageDataUpdate()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }
    public void Save()
    {
        SaveData saveData = new SaveData();
        saveData.sceneName = currentScene;
        saveData.playerHP = playerHP;
        saveData.lifePoint = lifePoint;
        //파일 생성
        FileStream fileStream = File.Create(Application.persistentDataPath + "/save.dat");

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, saveData);

        fileStream.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/save.dat"))
        {
            FileStream fileStream = File.Open(Application.persistentDataPath + "/save.dat", FileMode.Open);

            if (fileStream != null && fileStream.Length > 0)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                SaveData saveData = (SaveData)binaryFormatter.Deserialize(fileStream);
                playerHP = saveData.playerHP;
                currentScene = saveData.sceneName;
                lifePoint = saveData.lifePoint;
            }
            fileStream.Close();
        }
    }
}
