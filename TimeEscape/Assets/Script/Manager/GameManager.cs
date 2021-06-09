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
    //        Debug.Log("�Ͻ�����");
    //    }
    //    else
    //    {
    //        if (bPaused)
    //        {
    //            bPaused = false;
    //            Debug.Log("�Ͻ����� ��ü");
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
            //�÷��̾� ü�°� ��� �ʱ�ȭ (���� �ʱ�ȭ ����)
            Restart();
        }
        else
        {
            Debug.Log("pausemenu ����");
            bPaused = true;
        }
    }
    //��� ������ Restart �޴� ���� �г�
    
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
