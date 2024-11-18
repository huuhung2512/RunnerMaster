using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class PanelSettingUI : SingletonBehavior<PanelSettingUI>
{
    [SerializeField] private Button btnOnOffMusic;
    [SerializeField] private Button btnOnOffSound;
    [SerializeField] private Button btnReturn;
    [SerializeField] private Button btnShowTutorial;
    [SerializeField] private Button btnShowLinkFace;
    [SerializeField] private Button btnShowLinkInfo;
    [SerializeField] private Transform panelTutorial;
    [SerializeField] private Transform panelMainSetting;
    [SerializeField] private TextMeshProUGUI btnOnOffMusicTxt;
    [SerializeField] private TextMeshProUGUI btnOnOffSoundTxt;


    private void Start()
    {
        bool isMusicOn = PlayerPrefs.GetInt("MusicSetting", 1) == 1;
        bool isSFXOn = PlayerPrefs.GetInt("SFXSetting", 1) == 1;
        string musicText = PlayerPrefs.GetString("MusicText", isMusicOn ? "OFF" : "ON");
        string sfxText = PlayerPrefs.GetString("SFXText", isSFXOn ? "OFF" : "ON");
        SetTextMusic(musicText);
        SetTextSound(sfxText);
    }

    private void OnEnable()
    {
        btnOnOffMusic.onClick.AddListener(OnOnOffMusicClick);
        btnOnOffSound.onClick.AddListener(OnOnOffSoundClick);
        btnReturn.onClick.AddListener(OnReturnClick);
        btnShowTutorial.onClick.AddListener(OnShowTutorialClick);
        btnShowLinkFace.onClick.AddListener(OnShowLinkFace);
        btnShowLinkInfo.onClick.AddListener(OnShowLinkInfo);
    }


    private void OnDisable()
    {
        btnOnOffMusic.onClick.RemoveListener(OnOnOffMusicClick);
        btnOnOffSound.onClick.RemoveListener(OnOnOffSoundClick);
        btnReturn.onClick.RemoveListener(OnReturnClick);
        btnShowTutorial.onClick.RemoveListener(OnShowTutorialClick);
        btnShowLinkFace.onClick.RemoveListener(OnShowLinkFace);
        btnShowLinkInfo.onClick.RemoveListener(OnShowLinkInfo);
    }
    private void OnShowLinkInfo()
    {
        AudioManager.Instance.ButtonClick();
        MainMenu.Instance.OpenURL("https://www.facebook.com/hunghuu2512");
    }

    private void OnShowLinkFace()
    {
        AudioManager.Instance.ButtonClick();
        MainMenu.Instance.OpenURL("https://huuhung2512.itch.io/");
    }

    private void OnShowTutorialClick()
    {
        AudioManager.Instance.ButtonClick();
        panelMainSetting.gameObject.SetActive(false);
        panelTutorial.gameObject.SetActive(true);
    }

    public void SetTextSound(string text)
    {
        btnOnOffSoundTxt.text = text;
    }

    public void SetTextMusic(string text)
    {
        btnOnOffMusicTxt.text = text;
    }
    public void OnReturnClick()
    {
        AudioManager.Instance.ButtonClick();
        UIManager.Instance.OnHideAllPanel();
        UIManager.Instance.OnShowPanelGameplay(true);
    }

    private void OnOnOffSoundClick()
    {
        AudioManager.Instance.ToggleSFX();
        AudioManager.Instance.ButtonClick();
    }

    private void OnOnOffMusicClick()
    {
        AudioManager.Instance.ToggleMusic();
        AudioManager.Instance.ButtonClick();
    }

}
