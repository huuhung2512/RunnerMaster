using System.Collections;
using System.Collections.Generic;
using TMPro; // Thư viện cho TextMeshPro
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public int savedCharacter;
    public int currentCharacter; 
    public bool isEquip; 
    public GameObject[] characterModels; 
    public GameObject equipText; 
    public GameObject costText; 
    public Character[] chars; 

    void Start()
    {
        HideCostCharacter(); 
        savedCharacter = PlayerPrefs.GetInt("SelectedCar", 0); 
        currentCharacter = savedCharacter; 

        foreach (GameObject car in characterModels)
        {
            car.SetActive(false); 
        }
        characterModels[savedCharacter].SetActive(true); 
        UpdateEquipText(); 
    }

    public void ChangeNext()
    {
        characterModels[currentCharacter].SetActive(false); 
        currentCharacter++; 
        if (currentCharacter == characterModels.Length)
        {
            currentCharacter = 0; 
        }
        characterModels[currentCharacter].SetActive(true); 
        UpdateEquipText(); 
        HideCostCharacter(); 
    }

    public void ChangePre()
    {
        characterModels[currentCharacter].SetActive(false); 
        currentCharacter--; 
        if (currentCharacter == -1)
        {
            currentCharacter = characterModels.Length - 1; 
        }
        characterModels[currentCharacter].SetActive(true); 
        UpdateEquipText(); 
        HideCostCharacter(); 
    }

    public void SaveChange()
    {
        savedCharacter = currentCharacter; 
        PlayerPrefs.SetInt("SelectedCar", savedCharacter); 
        UpdateEquipText(); 
        HideCostCharacter(); 
        PlayerPrefs.Save(); 
    }

    public void ExitShop()
    {
        if (currentCharacter != savedCharacter) 
        {
            characterModels[currentCharacter].SetActive(false); 
            characterModels[savedCharacter].SetActive(true); 
            currentCharacter = savedCharacter; 
            UpdateEquipText(); 
        }
    }

    void HideCostCharacter()
    {
        if (currentCharacter == savedCharacter)
        {
            costText.SetActive(false); 
            isEquip = true; 
        }
        else
        {
            costText.SetActive(true); 
            isEquip = false; 
        }
    }

    void UpdateEquipText()
    {
        if (currentCharacter == savedCharacter)
        {
            equipText.SetActive(true); 
            isEquip = true; 
        }
        else
        {
            equipText.SetActive(false); 
            isEquip = false; 
        }
    }
}
