using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGameplayUI : MonoBehaviour
{
    [SerializeField] private Button btnShop;
    [SerializeField] private Button btnSetting;
    [SerializeField] private Button btnPlay;

    private void OnEnable()
    {
        btnShop.onClick.AddListener(OnShopClick);
        btnSetting.onClick.AddListener(OnSettinglick);
        btnPlay.onClick.AddListener(OnPlayClick);
    }
    private void OnDisable()
    {
        btnShop.onClick.RemoveListener(OnShopClick);
        btnSetting.onClick.RemoveListener(OnSettinglick);
        btnPlay.onClick.RemoveListener(OnPlayClick);
    }
    private void OnShopClick()
    {
       
        UIManager.Instance.OnHideAllPanel();
        UIManager.Instance.OnShowPanelShop(true);
        Debug.Log("AAAAAAAAAAA");
    }
    private void OnSettinglick()
    {
        UIManager.Instance.OnHideAllPanel();
        UIManager.Instance.OnShowPanelSetting(true);
    }
    private void OnPlayClick()
    {
        AudioManager.Instance.ButtonClick();
        MainMenu.Instance.PlayGame();
    }


}
