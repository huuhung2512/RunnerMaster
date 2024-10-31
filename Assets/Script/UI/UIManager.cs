using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonBehavior<UIManager>
{
    [SerializeField] private Transform panelGameplay;
    [SerializeField] private Transform panelShop;
    [SerializeField] private Transform panelSetting;

    private void Start()
    {
        Init();
        OnHideAllPanel();
        panelGameplay.gameObject.SetActive(true);
    }

    public void OnShowPanelGameplay(bool show)
    {
        panelGameplay.gameObject.SetActive(show);
    }
    public void OnShowPanelShop(bool show)
    {
        panelShop.gameObject.SetActive(show);
    }
    public void OnShowPanelSetting(bool show)
    {
        panelSetting.gameObject.SetActive(show);
    }
   
    internal void OnHideAllPanel()
    {
        panelShop.gameObject.SetActive(false);
        panelGameplay.gameObject.SetActive(false);
        panelSetting.gameObject.SetActive(false);
    }

    internal void Init()
    {
        panelShop.gameObject.SetActive(true);
        panelGameplay.gameObject.SetActive(true);
        panelSetting.gameObject.SetActive(true);
    }
}
