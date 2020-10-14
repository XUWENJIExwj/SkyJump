using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ObjectCreator objectCreator;
    public ObjectWithFlick playerObjWithFlick;
    public GameObject audioManagerPrefab;
    public AudioManager audioManager;
    public CanvasManager canvasManager;

    public GameObject score;
    public GameObject scoreFrame;

    // Start is called before the first frame update
    void Awake()
    {
        // AudioManagerへのアタッチ
        if (!GameObject.Find("AudioManager"))
        {
            Instantiate(audioManagerPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).name = "AudioManager";
        }
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        objectCreator.audioManager = audioManager;
        playerObjWithFlick.audioManager = audioManager;

        // BGMの再生
        audioManager.PlayBGM(AudioManager.BGM.BGM_GAME);

        canvasManager.SetScorePosition(Screen.width / 2 - 300.0f, Screen.height / 2 * 0.9f);

        scoreFrame.transform.localPosition = score.transform.localPosition;
        scoreFrame.transform.localScale = score.transform.localScale;
    }

    private void Update()
    {
        if(playerObjWithFlick.GetGameFlag())
        {
            audioManager.PlaySE(AudioManager.SE.SE_GAME_OVER, 0.2f);
            SceneManager.LoadScene("Result");
        }
    }
}
