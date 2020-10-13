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
    public GameObject scoreFrame;

    // Start is called before the first frame update
    void Start()
    {
        screenWidth = (float)Screen.width / 100;
        screenHeight = (float)Screen.height / 100;

        resultSpriteRenderer = GetComponent<SpriteRenderer>();
        resultSpriteRenderer.size = new Vector2(6.4f * screenHeight / 11.36f, screenHeight);

        // AudioManagerへのアタッチ
        if (!GameObject.Find("AudioManager"))
        {
            Instantiate(audioManagerPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).name = "AudioManager";
        }
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        canvasOldScene = GameObject.Find("Canvas");
        score = canvasOldScene.GetComponentInChildren<Score>();
        Destroy(canvasOldScene);

        score.transform.SetParent(canvasManager.gameObject.transform);

        scoreBest.scoreIndex = score.scoreIndex;

        canvasManager.SetScorePosition(-40.0f, 680.0f);
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

        canvasManager.SetScoreBestPosition(45.0f, 520.0f);
        canvasManager.SetScoreBestSize(0.48f, 0.48f);

        // BGMの再生
        audioManager.PlayBGM(AudioManager.BGM.BGM_RESULT);
    }

    // Update is called once per frame
    public void OnClickStart()
    {
        Destroy(canvasManager.gameObject);
        audioManager.PlaySE(AudioManager.SE.SE_RESULT);
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
}
