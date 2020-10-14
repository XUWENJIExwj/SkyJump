using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public float screenWidth;
    public float screenHeight;

    private SpriteRenderer titleSpriteRenderer;

    public GameObject audioManagerPrefab;
    public AudioManager audioManager;

    public GameObject canvas;
    public Text scoreBest;
    public GameObject best;

    // Start is called before the first frame update
    void Start()
    {
        screenWidth = (float)Screen.width / 100;
        screenHeight = (float)Screen.height / 100;

        titleSpriteRenderer = GetComponent<SpriteRenderer>();
        titleSpriteRenderer.size = new Vector2(6.4f * screenHeight / 11.36f, screenHeight);

        // AudioManagerへのアタッチ
        if (!GameObject.Find("AudioManager"))
        {
            Instantiate(audioManagerPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).name = "AudioManager";
        }
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        // BGMの再生
        audioManager.PlayBGM(AudioManager.BGM.BGM_TITLE);

        //CreateScoreFile();

        if (LoadScore() != 0)
        {
            best.SetActive(true);
            scoreBest.gameObject.SetActive(true);
            scoreBest.text = "Best Score : " + LoadScore().ToString() + " m";
        }
        else
        {
            best.SetActive(false);
            scoreBest.gameObject.SetActive(false);
        }
    }

    public void OnClickStart()
    {
        Destroy(canvas);
        audioManager.PlaySE(AudioManager.SE.SE_TITLE, 0.5f);
        SceneManager.LoadScene("Game");
    }

//    public void CreateScoreFile()
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

//        if (!File.Exists(path))
//        {
//            StreamWriter sw = new StreamWriter(path, false); //true=追記 false=上書き
//            sw.WriteLine(5000.ToString());
//            sw.Flush();
//            sw.Close();
//        }
//    }

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
}
