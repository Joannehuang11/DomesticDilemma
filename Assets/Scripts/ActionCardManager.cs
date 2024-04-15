using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionCardManager : MonoBehaviour
{
    public string name;
    public int coinCost;
    public int gridCount;
    public Sprite image;
    public bool isSelected;

    //UI
    public Image thumbnailImg;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI coinCostText;
    
    // Start is called before the first frame update
    void Start()
    {
        thumbnailImg.sprite = image;
        nameText.text = name;
        coinCostText.text = coinCost.ToString();
        isSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
