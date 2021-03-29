using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CanvasManager : MonoBehaviour
{

    public static CanvasManager manager;

    public Player player;

    public GameObject Tutorial;
    public GameObject ButtonGroup;
    public GameObject UpgradeMenu;
    public GameObject NetGain;

    public UnityEngine.UI.Image hold_bar_image;
    public UnityEngine.UI.Image net_gain_image;

    public UnityEngine.UI.Text text_score;
    public UnityEngine.UI.Text text_net_gain;

    private float upgrade_button_return_pos;
    private float upgrade_menu_return_pos;

    private float hold_bar_fill;
    private float damp_vel;

    private bool has_net = false;

    [Range(0, 1f)]
    public float debug_fill_value;

    private void Awake()
    {
        manager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        upgrade_button_return_pos = ButtonGroup.transform.localPosition.y;
        upgrade_menu_return_pos = UpgradeMenu.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug
        if (Input.GetKeyDown(KeyCode.T))
            ShowTutorial();

        //Debug
        if (Input.GetKeyDown(KeyCode.N))
            ShowNetGain();

        UpdateHoldBar(player.gameObject.GetComponent<Collector>().carrying / player.gameObject.GetComponent<Collector>().capacity);

        UpdateScore();
    }

    /// <summary>
    /// Methods that move UI elements
    /// </summary>
    #region UI Trisitions
    public void ShowTutorial()
    {
        LeanTween.moveLocalY(Tutorial, 0, 1f).setEaseInOutQuad();
    }

    public void CloseTutorial()
    {
        LeanTween.moveLocalY(Tutorial, 800, 1f).setEaseInOutQuad();
    }

    private void HideButtonGroup()
    {
        LeanTween.moveLocalY(ButtonGroup, 590, 1f).setEaseInOutQuad();
    }

    private void ShowButtonGroup()
    {
        LeanTween.moveLocalY(ButtonGroup, upgrade_button_return_pos, 1f).setEaseInOutQuad();
    }

    public void ShowUpgrades()
    {
        HideButtonGroup();
        LeanTween.moveLocalY(UpgradeMenu, 0, 1f).setEaseInOutQuad();
    }

    public void CloseUpgrades()
    {
        ShowButtonGroup();
        LeanTween.moveLocalY(UpgradeMenu, upgrade_menu_return_pos, 1f).setEaseInOutQuad();
    }

    public void ShowNetGain()
    {
        if (!has_net)
        {
            LeanTween.moveLocalY(NetGain, 370f, 1f).setEaseInOutQuad();
            has_net = true;
        }
    }

    #endregion


    #region UI Update
    public void UpdateHoldBar(float value)
    {
        value = Mathf.Clamp(value, 0f, 1f);
        //hold_bar_image.fillAmount = Mathf.Lerp(hold_bar_fill, 1f, value);
        hold_bar_image.fillAmount = Mathf.SmoothDamp(hold_bar_image.fillAmount, value, ref damp_vel, 0.25f);

    }

    public void UpdateNetGainProgress(float value)
    {
        value = Mathf.Clamp(value, 0f, 1f);
        net_gain_image.fillAmount = Mathf.SmoothDamp(hold_bar_image.fillAmount, value, ref damp_vel, 0.1f);
    }

    public void UpdateScore()
    {
        text_score.text = GameManager.gm.getScore().ToString();
    }

    public void SetNet(int net)
    {
        text_net_gain.text = net.ToString();
    }

    #endregion



}
