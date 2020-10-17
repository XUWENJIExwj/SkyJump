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
        rankInfo = new RankInfo[5];

        for (int i = 0; i < rankInfo.Length; i++)
        {
            rankInfo[i].rank = PlayerPrefs.GetInt("RANK" + i.ToString(), 0);
            rankInfo[i].name = PlayerPrefs.GetString("NAME" + i.ToString(), "");
            rankInfo[i].score = PlayerPrefs.GetInt("SCORE" + i.ToString(), 0);
        }

        PlayerPrefs.Save();
    }

    public RankInfo GetChampion()
    {
        return rankInfo[0];
    }

    public bool CheckIfRankIn(int score)
    {
        if (score > rankInfo[rankInfo.Length - 1].score)
        {
            return true;
        }
        return false;
    }

    public void SortRank()
    {
        RankInfo temp = newRank;

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
        PlayerPrefs.Save();
    }

    public void SetRank(int idx)
    {
        PlayerPrefs.SetInt("RANK" + idx.ToString(), rankInfo[idx].rank);
        PlayerPrefs.SetString("NAME" + idx.ToString(), rankInfo[idx].name);
        PlayerPrefs.SetInt("SCORE" + idx.ToString(), rankInfo[idx].score);
    }

    public void SetNewRankInfo(string name, int score)
    {
        newRank.name = name;
        newRank.score = score;
    }

    public void RankDebug()
    {
        for (int i = 0; i < rankInfo.Length; i++)
        {
            Debug.Log("Rank: " + rankInfo[i].rank);
            Debug.Log("Name: " + rankInfo[i].name);
            Debug.Log("Score:" + rankInfo[i].score);
        }
    }
}
