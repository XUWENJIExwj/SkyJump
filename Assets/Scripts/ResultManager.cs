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
    public Score score;
    public Score scoreBest;
    public Score scoreOther;
    public GameObject scoreFrame;
    public GameObject lightObj;
    public float lightSize;
    public bool hasCreatedSoul;
    public GameObject player;
    public GameObject soulPrefab;

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
            //canvasOldScene = GameObject.Find("Canvas");

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

        score.transform.SetParent(canvasManager.gameObject.transform);

        scoreBest.scoreIndex = score.scoreIndex;

        canvasManager.SetScorePosition(-40.0f, frame.anchoredPosition.y - 15.0f);
        canvasManager.SetScoreSize(0.65f, 0.65f);

        int score_work = LoadScore();

        if (score.score >= score_work)
        {
            SaveScore(score.score.ToString());
            scoreBest.SetScore((float)score.score / score.scoreIndex);
        }
        else
        {
            scoreBest.SetScore((float)score_work / score.scoreIndex);
        }

        canvasManager.SetScoreBestPosition(45.0f, frame.anchoredPosition.y - 175.0f);
        canvasManager.SetScoreBestSize(0.48f, 0.48f);

        // BGMの再生
        audioManager.PlayBGM(AudioManager.BGM.BGM_RESULT);

        hasCreatedSoul = false;
    }

    private void FixedUpdate()
    {
        SetLightSize();

        if (lightObj.transform.localScale.x >= 1.0f && !hasCreatedSoul)
        {
            CreateSoul();
        }
    }

    // Update is called once per frame
    public void OnClickStart()
    {
        Destroy(canvasManager.gameObject);
        audioManager.PlaySE(AudioManager.SE.SE_RESULT, 0.5f);
        SceneManager.LoadScene("Title");
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
