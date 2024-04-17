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

    
    // Start is called before the first frame update
    void Start()
    {
        player0Manager = player0Obj.GetComponent<PlayerManager>();
        player1Manager = player1Obj.GetComponent<PlayerManager>();

        actionCardsPlayManager = actionCardsPlayManagerObj.GetComponent<ActionCardsPlayManager>();
        territoryPlayManager = territoryPlayManagerObj.GetComponent<TerritoryPlayManager>();
        progressManager = progressManagerObj.GetComponent<ProgressManager>();   
        landCardsDatas = landCardsDatasObj.GetComponent<LandCardsDatas>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click on LandUnit: " + landNo);

        if (!progressManager.isInputBlock)
        {
            int selectingPlayerNo = actionCardsPlayManager.getSelectingPlayerNo();
            int selectingCoinCost = actionCardsPlayManager.getSelectedCoinCost();
            List<Sprite> selectedCardImgs = actionCardsPlayManager.getSelectedCardImgs();
            territoryPlayingState currentTerritoryPlayingState = territoryPlayManager.currentTerritoryPlayingState;

            if (ownerNo == -1)
            {
                ownerNo = selectingPlayerNo;

                switch (currentTerritoryPlayingState)
                {
                    case territoryPlayingState.None:
                        // Debug.Log("territoryPlayingState is None");
                        break;
                    case territoryPlayingState.Waiting:
                        //set own
                        setOwnLandCards(selectingPlayerNo, selectedCardImgs);
                        
                        if (selectingPlayerNo == 0)
                        {
                            territoryPlayManager.setTerritoryPlayingState(territoryPlayingState.P0Placed);

                            //cost coin
                            player0Manager.SetPlayerStatus(playerStatus.Action, selectingCoinCost, false, true);

                            //reset status card
                            actionCardsPlayManager.deSelectAllCards();
                            player0Manager.SetPlayerStatus(playerStatus.Action, 0, true, false);
                            actionCardsPlayManager.setActionCardsPlayingState(actionCardsPlayingState.Waiting);

                        }
                        else if (selectingPlayerNo == 1)
                        {
                            territoryPlayManager.setTerritoryPlayingState(territoryPlayingState.P1Placed);

                            //cost coin
                            player1Manager.SetPlayerStatus(playerStatus.Action, selectingCoinCost, false, true);

                            //reset status card
                            actionCardsPlayManager.deSelectAllCards();
                            player1Manager.SetPlayerStatus(playerStatus.Action, 0, true, false);
                            actionCardsPlayManager.setActionCardsPlayingState(actionCardsPlayingState.Waiting);
                        }
                        // Debug.Log("territoryPlayingState is Waiting");
                        break;
                    case territoryPlayingState.P0Placed:
                        // Debug.Log("territoryPlayingState is P0Placed");
                        break;
                    case territoryPlayingState.P1Placed:                    
                        // Debug.Log("territoryPlayingState is P1Placed");
                        break;
                }
            }
        }
    }
    
    public void setStartLandCard(int no, int playerNo)
    {
        landNo = no;
        ownerNo = playerNo;
    }

    public void setOwnALandCard(int playerNo, Sprite img)
    {
        ownerNo = playerNo;
        landImg.sprite = img;
    }

    public void setOwnLandCards(int playerNo, List<Sprite> imgs)
    {
        if (imgs.Count > 1 && landNo % 9 < 7)
        {
            GameObject nextLandCard = landCardsDatas.GetLandCard(landNo + 1);
            setOwnALandCard(playerNo, imgs[0]);
            nextLandCard.GetComponent<LandUnit>().setOwnALandCard(playerNo, imgs[1]);
        } 
        else 
        {
            setOwnALandCard(playerNo, imgs[0]);
        }
    }
}
