using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType { PlayerSpeed, PlayerRange, PlayerCapacity, Net }

/// <summary>
/// Manages The score, applies bought upgrades to the player, and keeps game statistics for Qualtrics.
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Fields
    public static GameManager gm;
    public int score;
    public Player player;

    [SerializeField] private int speedUpgradeValue = 1;
    [SerializeField] private int rangeUpgradeValue = 1;
    [SerializeField] private int capacityUpgradeValue = 1;

    // Qualtrics data
    private int speedUpgradesPurchased = 0;
    private int rangeUpgradesPurchased = 0;
    private int capacityUpgradesPurchased = 0;
    private float timeUntilFirstUpgrade = 0.0f;
    private int firstUpgradePurchased = -1;
    #endregion


    private void Awake()
    {
        gm = this;
    }

    void Start()
    {
        if(player == null)
        {
            Debug.Log("Hook up the player, idiot - searching by tag is expensive.");
        }
    }
    
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            QuitTheGame();
        }
    }


    #region Helper Methods
    /// <summary>
    /// Called when an upgrade is purchased in the menu.
    /// </summary>
    /// <param name="upgradeType">The type of upgrade purchased.</param>
    /// <param name="cost">The cost of the purchased upgrade.</param>
    public bool Upgrade(UpgradeType upgradeType, int cost)
    {
        if(timeUntilFirstUpgrade == 0.0f)
        {
            timeUntilFirstUpgrade = Time.time;
            firstUpgradePurchased = (int)upgradeType;
        }

        if(score < cost) { return false; }
        score -= cost;

        // should probably use getters and setters instead of calling fields outright
        switch (upgradeType)
        {
            case UpgradeType.PlayerSpeed:
                player.moveSpeed += speedUpgradeValue;
                speedUpgradesPurchased++;
                break;
            case UpgradeType.PlayerRange:
                player.collectRange += rangeUpgradeValue;
                rangeUpgradesPurchased++;
                break;
            case UpgradeType.PlayerCapacity:
                player.collectCapacity += capacityUpgradeValue;
                capacityUpgradesPurchased++;
                break;
            default:
                Debug.Log("Upgrade not implemented in GameManager.");
                break;
        }

        player.UpdateCollectorValues();
        return true;
    }


    public void addToScore(int amount)
    {
        score += amount;
    }


    /// <summary>
    /// Creates a query for Qualtrics based on game statistics, then quits the application.
    /// </summary>
    public void QuitTheGame()
    {
        string query = "";

        foreach (KeyValuePair<string, float> entry in GetData())
        {
            query += "&" + entry.Key + "=" + entry.Value;
        }

        Application.OpenURL("https://rit.az1.qualtrics.com/jfe/form/SV_aVnF456JZzbo6rk?gameName=Beach" + query);

        Application.Quit();
    }


    /// <summary>
    /// For use with Qualtrics querying.
    /// </summary>
    /// <returns>Dictionary containing current game statistics.</returns>
    public Dictionary<string, float> GetData()
    {
        Dictionary<string, float> gameData = new Dictionary<string, float>();

        gameData.Add("score", score);
        gameData.Add("speedUpgradesPurchased", speedUpgradesPurchased);
        gameData.Add("rangeUpgradesPurchased", rangeUpgradesPurchased);
        gameData.Add("capacityUpgradesPurchased", capacityUpgradesPurchased);
        gameData.Add("timePlayed", Time.time);
        gameData.Add("timeUntilFirstUpgrade", timeUntilFirstUpgrade);
        gameData.Add("firstUpgradePurchased", firstUpgradePurchased);

        return gameData;
    }
    #endregion
}
