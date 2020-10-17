using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rank : MonoBehaviour
{
    public struct RankInfo
    {
        public int rank;
        public string name;
        public int score;
    }

    public RankInfo[] rankInfo;
    public RankInfo newRank;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetRank()
    {
        for (int i = 0; i < rankInfo.Length; i++)
        {
            rankInfo[i].rank = PlayerPrefs.GetInt("RANK" + i.ToString(), 0);
            rankInfo[i].name = PlayerPrefs.GetString("NAME" + i.ToString(), "");
            rankInfo[i].score = PlayerPrefs.GetInt("SCORE" + i.ToString(), 0);
        }
    }

    public void SortRank(RankInfo rank_info)
    {
        RankInfo temp = rank_info;

        for (int i = 0; i < rankInfo.Length; i++)
        {
            if (rankInfo[i].score < temp.score)
            {
                RankInfo work = rankInfo[i];

                rankInfo[i].rank = i + 1;
                rankInfo[i].name = temp.name;
                rankInfo[i].score = temp.score;

                temp = work;

                SetRank(i);
            }
        }
    }

    public void SetRank(int idx)
    {
        PlayerPrefs.SetInt("RANK" + idx.ToString(), rankInfo[idx].rank);
        PlayerPrefs.GetString("NAME" + idx.ToString(), rankInfo[idx].name);
        PlayerPrefs.GetInt("SCORE" + idx.ToString(), rankInfo[idx].score);
    }
}
