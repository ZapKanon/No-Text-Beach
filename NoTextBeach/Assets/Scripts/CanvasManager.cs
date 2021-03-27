using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{

    public static CanvasManager manager;

    public GameObject Tutorial;
    public GameObject UpgradeButton;
    public GameObject UpgradeMenu;

    private float upgrade_button_return_pos;
    private float upgrade_menu_return_pos;

    private void Awake()
    {
        manager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        upgrade_button_return_pos = UpgradeButton.transform.localPosition.y;
        upgrade_menu_return_pos = UpgradeMenu.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTutorial()
    {
        LeanTween.moveLocalY(Tutorial, 0, 1f).setEaseInOutQuad();
    }

    public void CloseTutorial()
    {
        LeanTween.moveLocalY(Tutorial, 800, 1f).setEaseInOutQuad();
    }

    private void HideUpgradeButton()
    {
        LeanTween.moveLocalY(UpgradeButton, 590, 1f).setEaseInOutQuad();
    }

    private void ShowUpgradeButton()
    {
        LeanTween.moveLocalY(UpgradeButton, upgrade_button_return_pos, 1f).setEaseInOutQuad();
    }

    public void ShowUpgrades()
    {
        HideUpgradeButton();
        LeanTween.moveLocalY(UpgradeMenu, 0, 1f).setEaseInOutQuad();
    }

    public void CloseUpgrades()
    {
        ShowUpgradeButton();
        LeanTween.moveLocalY(UpgradeMenu, upgrade_menu_return_pos, 1f).setEaseInOutQuad();
    }

    /* Upgrade buttons TO DO:
     * 1 button per upgrade type
     * If selected, check upgrade cost against the player's score (kept in GameManager). Only do actions if score >= cost
     * reduce player score by cost
     * call the GameManager's Upgrade method
     * increase cost of the purchased upgrade by a set amount
     */
}
