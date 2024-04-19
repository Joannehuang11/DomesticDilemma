using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ActionCardManager : MonoBehaviour, IPointerClickHandler
//IPointerEnterHandler, IPointerExitHandler
{
    public int cardNo;


    // interface
    public GameObject player0Obj;
    PlayerManager player0Manager;
    public GameObject player1Obj;
    PlayerManager player1Manager;
    public GameObject actionCardsPlayManagerObj;
    ActionCardsPlayManager actionCardsPlayManager;
    public GameObject territoryPlayManagerObj;
    TerritoryPlayManager territoryPlayManager;
    public GameObject progressManagerObj;
    ProgressManager progressManager;
    public GameObject actionCardsDatasObj;
    ActionCardsDatas actionCardsDatas;
    public GameObject pointGridPlayManagerObj;
    PointGridPlayManager pointGridPlayManager;
    Button buttonComponent;

    private string actionName;
    private List<Sprite> images;
    public int coinCost;
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
        player0Manager = player0Obj.GetComponent<PlayerManager>();
        player1Manager = player1Obj.GetComponent<PlayerManager>();
        actionCardsPlayManager = actionCardsPlayManagerObj.GetComponent<ActionCardsPlayManager>();
        territoryPlayManager = territoryPlayManagerObj.GetComponent<TerritoryPlayManager>();
        actionCardsDatas = actionCardsDatasObj.GetComponent<ActionCardsDatas>();
        progressManager = progressManagerObj.GetComponent<ProgressManager>();
        pointGridPlayManager = pointGridPlayManagerObj.GetComponent<PointGridPlayManager>();
        buttonComponent = GetComponent<Button>();
    
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
        // Debug.Log("Click on ActionCard: " + cardNo);

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
                        //if selected cannot afford, then wait
                        if (player0Manager.checkBudget(coinCost))
                        {
                            territoryPlayManager.setTerritoryPlayingState(territoryPlayingState.Waiting);
                            SetActionCardsPlayingState(actionCardsPlayingState.P0Selected, true);

                            actionCardsPlayManager.clickActionCardSoundPlay(true);
                        } 
                        else
                        {
                            territoryPlayManager.setTerritoryPlayingState(territoryPlayingState.None);
                            SetActionCardsPlayingState(actionCardsPlayingState.Waiting, false);

                            actionCardsPlayManager.clickActionCardSoundPlay(false);
                            // player0Manager.actionDoneUI.GetComponent<ButtonShaker>().ShakeButton();
                            player0Manager.warningTextUI.GetComponent<ButtonShaker>().ShakeButton();
                        }
                    }
                    else if (selectingPlayerNo == 1)
                    {
                        //if selected cannot afford, then wait
                        if (player1Manager.checkBudget(coinCost))
                        {
                            territoryPlayManager.setTerritoryPlayingState(territoryPlayingState.Waiting);
                            SetActionCardsPlayingState(actionCardsPlayingState.P1Selected, true);

                            actionCardsPlayManager.clickActionCardSoundPlay(true);
                        }
                        else
                        {
                            territoryPlayManager.setTerritoryPlayingState(territoryPlayingState.None);
                            SetActionCardsPlayingState(actionCardsPlayingState.Waiting, false);

                            actionCardsPlayManager.clickActionCardSoundPlay(false);
                            // player1Manager.actionDoneUI.GetComponent<ButtonShaker>().ShakeButton();
                            player1Manager.warningTextUI.GetComponent<ButtonShaker>().ShakeButton();
                        }
                    }
                    pointGridPlayManager.SetGridPlayResult(pointGridPlayResult.None);
                    break;
                case actionCardsPlayingState.P0Selected:
                    //set this card selected
                    actionCardsPlayManager.deSelectAllCards();
                    SetSelected(true);
                    actionCardsPlayManager.SetSelectedCard(selectingPlayerNo, cardNo, coinCost);
                    
                    //update game states
                    //if selected cannot afford, then wait
                    if (player0Manager.checkBudget(coinCost))
                    {
                        territoryPlayManager.setTerritoryPlayingState(territoryPlayingState.Waiting);
                        SetActionCardsPlayingState(actionCardsPlayingState.P0Selected, true);

                        actionCardsPlayManager.clickActionCardSoundPlay(true);
                    }
                    else
                    {
                        territoryPlayManager.setTerritoryPlayingState(territoryPlayingState.None);
                        SetActionCardsPlayingState(actionCardsPlayingState.P0Selected, false);

                        actionCardsPlayManager.clickActionCardSoundPlay(false);
                        // player0Manager.actionDoneUI.GetComponent<ButtonShaker>().ShakeButton();
                        player0Manager.warningTextUI.GetComponent<ButtonShaker>().ShakeButton();
                    }
                    break;
                case actionCardsPlayingState.P1Selected:
                    //set this card selected
                    actionCardsPlayManager.deSelectAllCards();
                    SetSelected(true);
                    actionCardsPlayManager.SetSelectedCard(selectingPlayerNo, cardNo, coinCost);
                    
                    //update game states
                    if (player1Manager.checkBudget(coinCost))
                    {
                        territoryPlayManager.setTerritoryPlayingState(territoryPlayingState.Waiting);
                        SetActionCardsPlayingState(actionCardsPlayingState.P1Selected, true);

                        actionCardsPlayManager.clickActionCardSoundPlay(true);
                    }
                    else
                    {
                        territoryPlayManager.setTerritoryPlayingState(territoryPlayingState.None);
                        SetActionCardsPlayingState(actionCardsPlayingState.P1Selected, false);    

                        actionCardsPlayManager.clickActionCardSoundPlay(false);    
                        // player1Manager.actionDoneUI.GetComponent<ButtonShaker>().ShakeButton();    
                        player1Manager.warningTextUI.GetComponent<ButtonShaker>().ShakeButton();                
                    }
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

    public void SetActionCardsPlayingState(actionCardsPlayingState newState, bool canMoveOn)
    {
        actionCardsPlayManager.setActionCardsPlayingState(newState, canMoveOn);
    }

    public List<Sprite> GetCardImages()
    {
        return images;
    }

    public void setButtonActive(bool isEnable)
    {
        buttonComponent.enabled = isEnable;
    }
    
    public int getCoinCost()
    {
        return coinCost;
    }
}
