using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public UnityEngine.UI.Text text;
    public UnityEngine.UI.Image bar;

    public UpgradeType type;

    public int level = 1;
    public int cost = 10;
    public float level_max = 5;

    private float damp_vel;

    [SerializeField]
    [Range(1,10)]
    private float cost_multiplier;

    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBar();
    }

    /// <summary>
    /// Call the upgrade method in the game manager, if success, increment cost and update UI.
    /// </summary>
    public void DoUpgrade()
    {
        if (GameManager.gm.Upgrade(type, cost) && level < level_max)
        {
            IncrementCost();
            UpdateText();
        }
    }

    /// <summary>
    /// Update progress bar animation
    /// </summary>
    public void UpdateBar()
    {
        bar.fillAmount = Mathf.SmoothDamp(bar.fillAmount, (float)level / level_max, ref damp_vel, 0.1f);
    }

    /// <summary>
    /// Increment cost based on a parabolic equation, with minimum at x = 1, y = 10
    /// </summary>
    private void IncrementCost()
    {
        level++;
        cost = (int)(cost_multiplier * (float)(level * level)) - 2 * level + 11; //A parabolic function with a minimum at x = 1, y = 10
        
        //cheap way to handle level max out
        if (level == level_max)
            cost = 0;
    }

    /// <summary>
    /// Update cost text in the UI
    /// </summary>
    private void UpdateText()
    {
        text.text = cost.ToString();
        if (cost == 0)
            text.text = "-";
    }
}
