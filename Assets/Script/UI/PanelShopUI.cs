using UnityEngine;
using UnityEngine.UI;

public class PanelShopUI : MonoBehaviour
{

    [SerializeField] private Button btnReturn;
    [SerializeField] private Button btnEquip;
    [SerializeField] private Button btnNextChar;
    [SerializeField] private Button btnPreChar;
    [SerializeField] private Button bthBuyChar;

    private void OnEnable()
    {
        btnReturn.onClick.AddListener(OnReturnClick);
        btnEquip.onClick.AddListener(OnEquipChar);
        btnNextChar.onClick.AddListener(OnNextChar);
        btnPreChar.onClick.AddListener(OnpreChar);
        bthBuyChar.onClick.AddListener(OnBuyChar);
    }
    private void OnDisable()
    {
        btnReturn.onClick.RemoveListener(OnReturnClick);
        btnEquip.onClick.RemoveListener(OnReturnClick);
        btnNextChar.onClick.RemoveListener(OnNextChar);
        btnPreChar.onClick.RemoveListener(OnpreChar);
        bthBuyChar.onClick.RemoveListener(OnBuyChar);
    }
    private void OnBuyChar()
    {
        ShopManager.Instance.UnlockChar();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buying);
    }

    private void OnpreChar()
    {
        ShopManager.Instance.ChangePre();
    }

    private void OnNextChar()
    {
        ShopManager.Instance.ChangeNext();
    }

    private void OnEquipChar()
    {
        ShopManager.Instance.SaveChange();
    }

    private void OnReturnClick()
    {
        UIManager.Instance.OnHideAllPanel();
        UIManager.Instance.OnShowPanelGameplay(true);
        ShopManager.Instance.ExitShop();
    }
}
