using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class ResultManager : MonoBehaviour
{
    public float screenWidth;
    public float screenHeight;

    private SpriteRenderer resultSpriteRenderer;

    public GameObject audioManagerPrefab;
    public AudioManager audioManager;

    public GameObject canvasOldScene;
    public CanvasManager canvasManager;
    public GameObject scoreDisplay;
    public Score score;
    public Score scoreBest;
    public Score scoreOther;
    public GameObject scoreFrame;
    public GameObject lightObj;
    public float lightSize;
    public bool hasCreatedSoul;
    public GameObject player;
    public GameObject soulPrefab;
    public ResultFade fade;

    // バイナリ
    //public struct RankInfo
    //{
    //    public int rank;
    //    public string name;
    //    public int score;
    //}

    //public List<RankInfo> rankInfo;
    //

    public Rank rank;

    // Start is called before the first frame update
    void Start()
    {
        screenWidth = (float)Screen.width / 100;
        screenHeight = (float)Screen.height / 100;

        resultSpriteRenderer = GetComponent<SpriteRenderer>();
        resultSpriteRenderer.size = new Vector2(6.4f * screenHeight / 11.36f, screenHeight);

        RectTransform frame = scoreFrame.GetComponent<RectTransform>();

        // AudioManagerへのアタッチ
        if (!GameObject.Find("AudioManager"))
        {
            Instantiate(audioManagerPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).name = "AudioManager";
        }
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        if(canvasOldScene = GameObject.Find("Canvas"))
        {
            score = canvasOldScene.GetComponentInChildren<Score>();
            Destroy(canvasOldScene);
        }
        else
        {
            scoreOther.gameObject.SetActive(true);
            score = scoreOther;
            score.tag = "Score";
            score.score = 100;
        }

        score.transform.SetParent(scoreDisplay.transform);

        scoreBest.scoreIndex = score.scoreIndex;

        canvasManager.SetScorePosition(-40.0f, frame.anchoredPosition.y - 15.0f);
        canvasManager.SetScoreSize(0.65f, 0.65f);

        rank.GetRank();

        rank.RankDebug();

        if (rank.CheckIfRankIn(score.score))
        {
            // 入力処理
            rank.SetNewRankInfo("kyo", score.score);
            rank.SortRank();
        }

        scoreBest.SetScore((float)rank.GetChampion().score / score.scoreIndex);

        canvasManager.SetScoreBestPosition(45.0f, frame.anchoredPosition.y - 175.0f);
        canvasManager.SetScoreBestSize(0.48f, 0.48f);

        // BGMの再生
        audioManager.PlayBGM(AudioManager.BGM.BGM_RESULT);

        hasCreatedSoul = false;

        fade.SetFadeState(Fade.FadeState.FADE_STATE_IN);
    }

    private void FixedUpdate()
    {
        switch (fade.GetFadeState())
        {
            case Fade.FadeState.FADE_STATE_IN:
                fade.FadeIn();
                break;
            case Fade.FadeState.FADE_STATE_OUT:
                fade.FadeOut();
                break;
            case Fade.FadeState.FADE_STATE_NEXT_SCENE:
                Destroy(canvasManager.gameObject);
                SceneManager.LoadScene("Title");
                break;
            default:
                SetLightSize();

                if (lightObj.transform.localScale.x >= 1.0f && !hasCreatedSoul)
                {
                    CreateSoul();
                }
                break;
        }
    }

    // Update is called once per frame
    public void OnClickStart()
    {
        fade.SetFadeState(Fade.FadeState.FADE_STATE_OUT);
        audioManager.PlaySE(AudioManager.SE.SE_RESULT, 1, 0.5f);
    }

    public int LoadScore()
    {
        string path;
        string filename = "/score.txt";

        if (Application.isEditor)
        {
            path = Application.dataPath + filename;
        }
        else
        {
#if UNITY_IOS

#elif UNITY_ANDROID

            path = Application.persistentDataPath + filename;
#endif
        }

        if (File.Exists(path))
        {
            StreamReader sr = new StreamReader(path);
            int s = int.Parse(sr.ReadLine());
            sr.Close();
            return s;
        }
        else
        {
            return 0;
        }
    }

//    public void LoadRankInfo()
//    {
//        string path;
//        string filename = "/score.txt";

//        if (Application.isEditor)
//        {
//            path = Application.dataPath + filename;
//        }
//        else
//        {
//#if UNITY_IOS

//#elif UNITY_ANDROID

//            path = Application.persistentDataPath + filename;
//#endif
//        }

//        FileStream fs = new FileStream(path, FileMode.Open);
//        BinaryReader br = new BinaryReader(fs); //true=追記 false=上書き

//        for (int i = 0; i < rank_info.Count; i++)
//        {
//            bw.Write(rank_info[i].name);
//            bw.Write(rank_info[i].score);
//        }


//        bw.Flush();
//        bw.Close();
//        fs.Flush();
//        fs.Close();
//    }

//    public void SaveRankInfo(List<RankInfo> rank_info)
//    {
//        string path;
//        string filename = "/score.txt";

//        if (Application.isEditor)
//        {
//            path = Application.dataPath + filename;
//        }
//        else
//        {
//#if UNITY_IOS

//#elif UNITY_ANDROID

//            path = Application.persistentDataPath + filename;
//#endif
//        }

//        FileStream fs = new FileStream(path, FileMode.Create);
//        BinaryWriter bw = new BinaryWriter(fs); //true=追記 false=上書き

//        for (int i = 0; i < rank_info.Count; i++)
//        {
//            bw.Write(rank_info[i].name);
//            bw.Write(rank_info[i].score);
//        }
        

//        bw.Flush();
//        bw.Close();
//        fs.Flush();
//        fs.Close();
//    }

    public void SaveScore(string s)
    {
        string path;
        string filename = "/score.txt";

        if (Application.isEditor)
        {
            path = Application.dataPath + filename;
        }
        else
        {
#if UNITY_IOS

#elif UNITY_ANDROID

            path = Application.persistentDataPath + filename;
#endif
        }
        
        StreamWriter sw = new StreamWriter(path, false); //true=追記 false=上書き
        sw.WriteLine(s);
        sw.Flush();
        sw.Close();
    }

    public void SetLightSize()
    {
        if (lightObj.transform.localScale.x < 1.0f && Time.fixedTime >= 0.3f)
        {
            lightObj.transform.localScale = new Vector3(lightObj.transform.localScale.x + lightSize, lightObj.transform.localScale.y, lightObj.transform.localScale.z);
        }
    }

    public void CreateSoul()
    {
        hasCreatedSoul = true;
        Vector3 pos = new Vector3(-0.6f, -3.1f, -2.0f);
        Instantiate(soulPrefab, pos, Quaternion.identity);
    }
}
