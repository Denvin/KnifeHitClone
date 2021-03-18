using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] string nameSkin;
    [SerializeField] int indexSkin;
    [SerializeField] int priceSkin = 10;
    [SerializeField] Button buttonBuy;
    [SerializeField] Button buttonActive;


    private bool isBuy = false;
    private bool isCurrentSkin;
    private int currentSkin;


    private void Start()
    {
        
        if (PlayerPrefs.GetInt(name) == 1)
        {
            isBuy = true;
        }
        else if (PlayerPrefs.GetInt(name) == 0)
        {
            isBuy = false;
        }
        CheckSkin();
        CheckBuy();
        
    }
    private void Update()
    {
        CheckSkin();
        CheckBuy();
    }

    private void CheckSkin()
    {
        currentSkin = PlayerPrefs.GetInt("PlayerSkin");
        if (currentSkin == indexSkin)
        {
            isCurrentSkin = true;
            buttonActive.interactable = false;
        }        
        else
        {
            buttonActive.interactable = true;
        }
    }
    private void CheckBuy()
    {
        if (buttonBuy == null)
        {
            return;
        }
        if (isBuy)
        {
            buttonBuy.gameObject.SetActive(false);
        }
        else
        {
            buttonBuy.gameObject.SetActive(true);
        }
    }

    public void SetSkin()
    {
        if (isBuy || indexSkin == 0)
        {
            PlayerPrefs.SetInt("PlayerSkin", indexSkin);
            buttonActive.interactable = false;
        }
        
    }
    public void BuySkin()
    {
        if (ScoreInfo.Instance.Coin >= priceSkin)
        {
            isBuy = true;
            PlayerPrefs.SetInt(name, 1);
            buttonBuy.gameObject.SetActive(false);
            ScoreInfo.Instance.Purchase(priceSkin);
        }
    }
}
