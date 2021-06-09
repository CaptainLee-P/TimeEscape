using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private GameObject player;

    public int totalPoint;
    public int stagePoint;

    public int stage;
    public int lifePoint;

    public Image Fade;

    public bool bPaused
    {
        get;
        private set;
    }

    public GameObject PauseUI;
    //private void OnApplicationPause(bool pause)
    //{
    //    if (pause)
    //    {
    //        bPaused = true;
    //        Debug.Log("일시정지");
    //    }
    //    else
    //    {
    //        if (bPaused)
    //        {
    //            bPaused = false;
    //            Debug.Log("일시정지 해체");
    //        }
    //    }
    //}

    private static GameManager _instance;
    public static GameManager instance { get { return _instance; } }
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(_instance != null)
            {
                Destroy(this.gameObject);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject PauseUI = GameObject.Find("Canvas").transform.Find("PauseMenu").gameObject;
        player = GameObject.Find("Player");
       // stage = 1;
       // lifePoint = 2;
    }

    // Update is called once per frame
    void Update()
    {
       
        player = GameObject.Find("Player");
        
    }


    IEnumerator fadeDelay()
    {
        yield return new WaitForSeconds(1f);
    }
    public void StageReset()
    {
        if (DataManager.instance.lifePoint > 0)
        {
            //플레이어 체력과 목숨 초기화 (월드 초기화 포함)
            Restart();
        }
        else
        {
            Debug.Log("pausemenu 실행");
            bPaused = true;
        }
    }
    //목숨 소진시 Restart 메뉴 생성 패널
    
    public void Restart()
    {
        Debug.Log("Restart");
        
        bPaused = false;
        SceneManager.LoadScene(DataManager.instance.currentScene);
        DataManager.instance.PlayerStatusReset();
        DataManager.instance.Save();
        DataManager.instance.stageDataUpdate();
    }
    public void GameOver() 
    {
        bPaused = false;
        DataManager.instance.Save();
        Debug.Log("Exit");
        Application.Quit();
    }

    public void NextStage()
    {
        int currentStageIndex = SceneManager.GetActiveScene().buildIndex;
        DataManager.instance.Save();
        DataManager.instance.stageDataUpdate();
        SceneManager.LoadScene(++currentStageIndex);
        
    }
}
