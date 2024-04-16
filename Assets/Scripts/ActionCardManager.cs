using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ActionCardManager : MonoBehaviour, IPointerClickHandler
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
        coinCost = - ActionCardsDatas.GetComponent<ActionCardsDatas>().getCoinCost(cardNo);
        gridCount = ActionCardsDatas.GetComponent<ActionCardsDatas>().getGridCount(cardNo);
        bgImg = GetComponent<Image>();
        
        isSelected = false;
        thumbnailImg.sprite = image;
        nameText.text = actionName;
        coinCostText.text = (coinCost* -1).ToString();
        UpdateBg(isSelected);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int selectingPlayerNo = ActionCardsPlayManager.GetComponent<ActionCardsPlayManager>().selectingPlayerNo;
        
        switch (ActionCardsPlayManager.GetComponent<ActionCardsPlayManager>().currentActionCardsPlayingState)
        {
            case actionCardsPlayingState.None:
                // Debug.Log("OnPointerClick fail");
                break;
            case actionCardsPlayingState.Waiting:
                ActionCardsPlayManager.GetComponent<ActionCardsPlayManager>().deSelectAllCards();
                SetSelected(true);
                SetActionCardsPlayingState(actionCardsPlayingState.P0Selected);
                ActionCardsPlayManager.GetComponent<ActionCardsPlayManager>().SetSelectedCard(selectingPlayerNo, cardNo, coinCost);
                // Debug.Log("OnPointerClick");
                break;
            case actionCardsPlayingState.P0Selected:
                ActionCardsPlayManager.GetComponent<ActionCardsPlayManager>().deSelectAllCards();
                SetSelected(true);
                ActionCardsPlayManager.GetComponent<ActionCardsPlayManager>().SetSelectedCard(selectingPlayerNo, cardNo, coinCost);
                // Debug.Log("OnPointerClick");
                break;
            case actionCardsPlayingState.P1Selected:
                ActionCardsPlayManager.GetComponent<ActionCardsPlayManager>().deSelectAllCards();
                SetSelected(true);
                ActionCardsPlayManager.GetComponent<ActionCardsPlayManager>().SetSelectedCard(selectingPlayerNo, cardNo, coinCost);
                // Debug.Log("OnPointerClick");
                break;
        }
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

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        UpdateBg(isSelected);

        if (selected)
        {
             Debug.Log("Card " + cardNo + "  " + actionName + " isSelected: " + isSelected);
        }
    }

    public void SetActionCardsPlayingState(actionCardsPlayingState newState)
    {
        ActionCardsPlayManager.GetComponent<ActionCardsPlayManager>().setActionCardsPlayingState(newState);
    }
}
