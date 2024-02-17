using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUpdate : MonoBehaviour
{
    public int gold = 1;
    private int kills = 0;
    private GameObject statsPanel;
    private Text goldtext;
    // Start is called before the first frame update
    void Start()
    {
        statsPanel = GameObject.FindGameObjectWithTag("StatsPanel");
        statsPanel.SetActive(false);
        goldtext = GameObject.FindGameObjectWithTag("GoldText").GetComponent<Text>();
        goldtext.text = gold.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //remove gold
    public void RemoveGold(int amount)
    {
        if (gold - amount <= 0)
        {
            gold = 0;
            EndGame();
        }
        else
        {
            gold -= amount;
        }
        goldtext.text = gold.ToString();
    }

    public void AddKill()
    {
        kills++;
    }

    public void EndGame()
    {
        DisplayStats();
        // Stop the game
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (GameObject spawner in spawners)
        {
            spawner.SetActive(false);
        }
    }

    public void DisplayStats()
    {
        statsPanel.SetActive(true);
        statsPanel.GetNamedChild("MobsKilled").GetComponent<Text>().text = "Kills : " + kills.ToString();
        statsPanel.GetNamedChild("Money").GetComponent<Text>().text = "Actual money : " + gold.ToString();
    }
}
