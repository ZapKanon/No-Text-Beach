using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{

    public static UIManager manager;

    public GameObject Tutorial;
    public GameObject ButtonGroup;
    public GameObject UpgradeMenu;
    public UnityEngine.UI.Image hold_bar_image;
    public GameObject NetGain;

    private float upgrade_button_return_pos;
    private float upgrade_menu_return_pos;

    private float hold_bar_fill;
    private float damp_vel;

    [Range(0,1f)]
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

        UpdateHoldBar(debug_fill_value);
    }

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

    public void UpdateHoldBar(float value)
    {
        value = Mathf.Clamp(value, 0f, 1f);
        //hold_bar_image.fillAmount = Mathf.Lerp(hold_bar_fill, 1f, value);
        hold_bar_image.fillAmount = Mathf.SmoothDamp(hold_bar_image.fillAmount, value, ref damp_vel, 0.5f);

    }

    public void ShowNetGain()
    {
        LeanTween.moveLocalY(NetGain, 370f, 1f).setEaseInOutQuad();
    }
}
