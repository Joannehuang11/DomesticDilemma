 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LandUnit : MonoBehaviour, IPointerClickHandler
{
    public int landNo;
    public int ownerNo;
    public Image landImg;
    public int coinCost;

    //interface
    public GameObject player0Obj;
    PlayerManager player0Manager;
    public GameObject player1Obj;
    PlayerManager player1Manager;
    public GameObject territoryPlayManagerObj;
    TerritoryPlayManager territoryPlayManager;
    public GameObject progressManagerObj;
    ProgressManager progressManager;
    public GameObject actionCardsPlayManagerObj;
    ActionCardsPlayManager actionCardsPlayManager;
    public GameObject landCardsDatasObj;
    LandCardsDatas landCardsDatas;
    Button buttonComponent;
    public List<GameObject> lineObjs;

    
    // Start is called before the first frame update
    void Start()
    {
        player0Manager = player0Obj.GetComponent<PlayerManager>();
        player1Manager = player1Obj.GetComponent<PlayerManager>();

        actionCardsPlayManager = actionCardsPlayManagerObj.GetComponent<ActionCardsPlayManager>();
        territoryPlayManager = territoryPlayManagerObj.GetComponent<TerritoryPlayManager>();
        progressManager = progressManagerObj.GetComponent<ProgressManager>();   
        landCardsDatas = landCardsDatasObj.GetComponent<LandCardsDatas>();   
        buttonComponent = GetComponent<Button>();

        coinCost = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Debug.Log("Click on LandUnit: " + landNo);

        if (!progressManager.isInputBlock)
        {
            territoryPlayingState currentTerritoryPlayingState = territoryPlayManager.currentTerritoryPlayingState;
            int selectedActionCardNo = actionCardsPlayManager.selectedActionCardNo;

            if (selectedActionCardNo > 0 && currentTerritoryPlayingState == territoryPlayingState.Waiting)
            {
                int selectingPlayerNo = actionCardsPlayManager.getSelectingPlayerNo();
                int selectingCoinCost = actionCardsPlayManager.getSelectedCoinCost();
                List<Sprite> selectedCardImgs = actionCardsPlayManager.getSelectedCardImgs();

                if (ownerNo < 0)
                {
                    ownerNo = selectingPlayerNo;

                    if (setOwnLandCards(selectingPlayerNo, selectedCardImgs))
                    {
                        if (selectingPlayerNo == 0)
                        {
                            territoryPlayManager.setTerritoryPlayingState(territoryPlayingState.P0Placed);

                            //cost coin
                            player0Manager.SetPlayerStatus(playerStatus.Action, selectingCoinCost, false, true);

                            //reset status card
                            actionCardsPlayManager.deSelectAllCards();
                            player0Manager.SetPlayerStatus(playerStatus.Action, 0, false, false);
                            actionCardsPlayManager.setActionCardsPlayingState(actionCardsPlayingState.Waiting, false);
                        }
                        else if (selectingPlayerNo == 1)
                        {
                            territoryPlayManager.setTerritoryPlayingState(territoryPlayingState.P1Placed);

                            //cost coin
                            player1Manager.SetPlayerStatus(playerStatus.Action, selectingCoinCost, false, true);

                            //reset status card
                            actionCardsPlayManager.deSelectAllCards();
                            player1Manager.SetPlayerStatus(playerStatus.Action, 0, false, false);
                            actionCardsPlayManager.setActionCardsPlayingState(actionCardsPlayingState.Waiting, false);
                        }

                        //play click sound
                        territoryPlayManager.clickLandCardSoundPlay();
                    }
                }
            }
        }
    }
    
    public void setStartLandCard(int no, int playerNo)
    {
        landNo = no;
        ownerNo = playerNo;
    }

    public void setOwnALandCard(int playerNo, Sprite img, int coin)
    {
        ownerNo = playerNo;
        landImg.sprite = img;
        coinCost = coin;
        
        if (playerNo > -1)
        {
            territoryPlayManager.addPlacedCard(gameObject);
        }
        Debug.Log("setOwnALandCard: "+ gameObject.name);
    }

    public bool setOwnLandCards(int playerNo, List<Sprite> imgs)
    {
        // Debug.Log("setOwnLandCards: PlayerNo"+ playerNo + "LandNo: "+ landNo + "ImgCount: "+ imgs.Count);
        PlayerManager playerManager = (playerNo == 0) ? player0Manager : player1Manager;
        int selectingCoinCost = actionCardsPlayManager.getSelectedCoinCost();

        if (playerManager.checkBudget(selectingCoinCost))
        {
            if (imgs.Count > 1 && landNo % 9 < 8)
            {
                GameObject nextLandCard = landCardsDatas.GetLandCard(landNo + 1);
                setOwnALandCard(playerNo, imgs[0], selectingCoinCost/2);
                nextLandCard.GetComponent<LandUnit>().setOwnALandCard(playerNo, imgs[1], selectingCoinCost/2);
            } 
            else if (imgs.Count == 1)
            {
                setOwnALandCard(playerNo, imgs[0], selectingCoinCost);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public void setButtonEnabled(bool isEnable)
    {
        int selectedActionCardNo = actionCardsPlayManager.selectedActionCardNo;

        if (ownerNo < 0 || selectedActionCardNo == 0)
        {
            buttonComponent.enabled = false;
        }
        else 
        {
            buttonComponent.enabled = isEnable;
        }
    }
}
