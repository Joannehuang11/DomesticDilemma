using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionCardManager : MonoBehaviour
{
    public int cardNo;


    public GameObject ActionCardsPlayManager;
    public GameObject ActionCardsDatas;
    private string actionName;
    private Sprite image;
    private int coinCost;
    private int gridCount;
    public bool isSelected;

    //UI
    public Image thumbnailImg;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI coinCostText;
    private Image bgImg;
    public Sprite activeBgImg;
    public Sprite inactiveBgImg;
    
    // Start is called before the first frame update
    void Start()
    {
        actionName = ActionCardsDatas.GetComponent<ActionCardsDatas>().getName(cardNo);
        image = ActionCardsDatas.GetComponent<ActionCardsDatas>().getImg(cardNo);
        coinCost = ActionCardsDatas.GetComponent<ActionCardsDatas>().getCoinCost(cardNo);
        gridCount = ActionCardsDatas.GetComponent<ActionCardsDatas>().getGridCount(cardNo);
        bgImg = GetComponent<Image>();
        
        isSelected = false;
        thumbnailImg.sprite = image;
        nameText.text = actionName;
        coinCostText.text = coinCost.ToString();
        UpdateBg(isSelected);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateBg(bool isSelected)
    {
        if (isSelected)
        {
            bgImg.sprite = activeBgImg;
        }
        else
        {
            bgImg.sprite = inactiveBgImg;
        }
    }
}
