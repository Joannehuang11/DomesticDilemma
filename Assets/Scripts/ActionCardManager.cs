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
    public GameObject progressManagerObj;
    ProgressManager progressManager;
    public GameObject actionCardsDatasObj;
    ActionCardsDatas actionCardsDatas;

    private string actionName;
    private List<Sprite> images;
    private int coinCost;
    private int gridCount;
    public bool isSelected;

    //UI
    public List<Image> thumbnailImgs;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI coinCostText;
    public Image bgImg;
    public Sprite activeBgImg;
    public Sprite inactiveBgImg;
    
    // Start is called before the first frame update
    void Start()
    {
        // player0Manager = player0Obj.GetComponent<PlayerManager>();
        // player1Manager = player1Obj.GetComponent<PlayerManager>();
        actionCardsPlayManager = actionCardsPlayManagerObj.GetComponent<ActionCardsPlayManager>();
        territoryPlayManager = territoryPlayManagerObj.GetComponent<TerritoryPlayManager>();
        actionCardsDatas = actionCardsDatasObj.GetComponent<ActionCardsDatas>();
        progressManager = progressManagerObj.GetComponent<ProgressManager>();
    
        actionName = actionCardsDatas.getName(cardNo);
        images = actionCardsDatas.getImgs(cardNo);
        coinCost = - actionCardsDatas.getCoinCost(cardNo);
        gridCount = actionCardsDatas.getGridCount(cardNo);
        
        isSelected = false;
        if (thumbnailImgs.Count > 1)
        {
            thumbnailImgs[0].sprite = images[0];
            thumbnailImgs[1].sprite = images[1];
        }
        else
        {
            thumbnailImgs[0].sprite = images[0];
        }
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
        Debug.Log("Click on ActionCard: " + cardNo);

        if (!progressManager.isInputBlock)
        {
            int selectingPlayerNo = actionCardsPlayManager.selectingPlayerNo;
            actionCardsPlayingState currentActionCardsPlayingState = actionCardsPlayManager.currentActionCardsPlayingState;
        
            switch (currentActionCardsPlayingState)
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
                    territoryPlayManager.setTerritoryPlayingState(territoryPlayingState.Waiting);

                    break;
                case actionCardsPlayingState.P0Selected:
                    //set this card selected
                    actionCardsPlayManager.deSelectAllCards();
                    SetSelected(true);
                    actionCardsPlayManager.SetSelectedCard(selectingPlayerNo, cardNo, coinCost);
                    
                    //update game states
                    SetActionCardsPlayingState(actionCardsPlayingState.P0Selected);
                    territoryPlayManager.setTerritoryPlayingState(territoryPlayingState.Waiting);
                    break;
                case actionCardsPlayingState.P1Selected:
                    //set this card selected
                    actionCardsPlayManager.deSelectAllCards();
                    SetSelected(true);
                    actionCardsPlayManager.SetSelectedCard(selectingPlayerNo, cardNo, coinCost);
                    
                    //update game states
                    SetActionCardsPlayingState(actionCardsPlayingState.P1Selected);
                    territoryPlayManager.setTerritoryPlayingState(territoryPlayingState.Waiting);
                    break;
            }
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

    public List<Sprite> GetCardImages()
    {
        return images;
    }
}
