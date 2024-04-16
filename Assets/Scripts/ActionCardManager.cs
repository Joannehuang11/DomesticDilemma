using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ActionCardManager : MonoBehaviour, IPointerClickHandler
{
    public int cardNo;


    // interface
    // public GameObject player0Obj;
    // PlayerManager player0Manager;
    // public GameObject player1Obj;
    // PlayerManager player1Manager;
    public GameObject actionCardsPlayManagerObj;
    ActionCardsPlayManager actionCardsPlayManager;
    public GameObject territoryPlayManagerObj;
    TerritoryPlayManager territoryPlayManager;
    // public GameObject progressManagerObj;
    // ProgressManager progressManager;
    public GameObject actionCardsDatasObj;
    ActionCardsDatas actionCardsDatas;

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
        actionCardsPlayManager = actionCardsPlayManagerObj.GetComponent<ActionCardsPlayManager>();
        territoryPlayManager = territoryPlayManagerObj.GetComponent<TerritoryPlayManager>();
        actionCardsDatas = actionCardsDatasObj.GetComponent<ActionCardsDatas>();

        
        actionName = actionCardsDatas.getName(cardNo);
        image = actionCardsDatas.getImg(cardNo);
        coinCost = - actionCardsDatas.getCoinCost(cardNo);
        gridCount = actionCardsDatas.getGridCount(cardNo);
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
        int selectingPlayerNo = actionCardsPlayManager.selectingPlayerNo;
        
        switch (actionCardsPlayManager.currentActionCardsPlayingState)
        {
            case actionCardsPlayingState.None:
                // Debug.Log("OnPointerClick fail");
                break;
            case actionCardsPlayingState.Waiting:
                //set this card selected
                actionCardsPlayManager.deSelectAllCards();
                SetSelected(true);
                actionCardsPlayManager.SetSelectedCard(selectingPlayerNo, cardNo, coinCost);

                //update game states
                if (selectingPlayerNo == 0)
                {
                    SetActionCardsPlayingState(actionCardsPlayingState.P0Selected);
                }
                else if (selectingPlayerNo == 1)
                {
                    SetActionCardsPlayingState(actionCardsPlayingState.P1Selected);
                }
                territoryPlayManager.SetTerritoryPlayingState(territoryPlayingState.Waiting);

                break;
            case actionCardsPlayingState.P0Selected:
                //set this card selected
                actionCardsPlayManager.deSelectAllCards();
                SetSelected(true);
                actionCardsPlayManager.SetSelectedCard(selectingPlayerNo, cardNo, coinCost);
                
                //update game states
                SetActionCardsPlayingState(actionCardsPlayingState.P0Selected);
                territoryPlayManager.SetTerritoryPlayingState(territoryPlayingState.Waiting);
                break;
            case actionCardsPlayingState.P1Selected:
                //set this card selected
                actionCardsPlayManager.deSelectAllCards();
                SetSelected(true);
                actionCardsPlayManager.SetSelectedCard(selectingPlayerNo, cardNo, coinCost);
                
                //update game states
                SetActionCardsPlayingState(actionCardsPlayingState.P1Selected);
                territoryPlayManager.SetTerritoryPlayingState(territoryPlayingState.Waiting);
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
            //  Debug.Log("Card " + cardNo + "  " + actionName + " isSelected: " + isSelected);
        }
    }

    public void SetActionCardsPlayingState(actionCardsPlayingState newState)
    {
        actionCardsPlayManager.setActionCardsPlayingState(newState);
    }
}
