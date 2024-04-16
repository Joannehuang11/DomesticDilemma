using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public enum actionCardsPlayingState
{
    None,
    Waiting,
    P0Selected,
    P1Selected,
}

public class ActionCardsPlayManager : MonoBehaviour
{
    public GameObject pointGridPlayManagerObj;
    public GameObject territoryPlayManagerObj;
    public GameObject player0Obj;
    public GameObject player1Obj;

    public actionCardsPlayingState currentActionCardsPlayingState;

    public List<GameObject> actionCards;
    
    public int selectedCoinCost;
    public int selectedCardNo;
    public int selectingPlayerNo;

    //UI
    public GameObject GamePlayUI;

    
    // Start is called before the first frame update
    void Start()
    {
        setActionCardsPlayingState(actionCardsPlayingState.None);
        selectedCardNo = -1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setActionCardsPlayingState(actionCardsPlayingState newState)
    {
        currentActionCardsPlayingState = newState;
        
        //update UI
        GamePlayUI.GetComponent<UpdateActionCardsPlayBg>().UpdateBg(currentActionCardsPlayingState);

        switch (currentActionCardsPlayingState)
        {
            case actionCardsPlayingState.None:
                Debug.Log("actionCardsPlayingState is None");
                break;
            case actionCardsPlayingState.Waiting:
                Debug.Log("actionCardsPlayingState is Waiting");
                break;
            case actionCardsPlayingState.P0Selected:
                Debug.Log("actionCardsPlayingState is P0Selected");
                break;
            case actionCardsPlayingState.P1Selected:
                Debug.Log("actionCardsPlayingState is P1Selected");
                break;
        }
    }

    public actionCardsPlayingState getActionCardsPlayingState()
    {
        return currentActionCardsPlayingState;
    }

    public GameObject getActionCard(int cardNo)
    {
        return actionCards[cardNo];
    }

    public void setSelectingPlayerNo(int playerNo)
    {
        selectingPlayerNo = playerNo;
    }

    public int getSelectingPlayerNo()
    {
        return selectingPlayerNo;
    }
    
    public void SetSelectedCard(int playerNo, int cardNo, int Cost)
    {
        selectedCardNo = cardNo;
        selectedCoinCost = Cost;

        PlayerManager player0Manager = player0Obj.GetComponent<PlayerManager>();
        PlayerManager player1Manager = player1Obj.GetComponent<PlayerManager>();

        if (playerNo == 0)
        {
            player0Manager.SetPlayerStatus(playerStatus.Action, selectedCoinCost, false, false);
            player0Manager.SetPlayerStatus(playerStatus.Hold, 0, true, false);
        }
        else if (playerNo == 1)
        {
            player1Manager.SetPlayerStatus(playerStatus.Hold, 0, true, false);
            player1Manager.SetPlayerStatus(playerStatus.Action, selectedCoinCost, false, false);
        }
    }

    public void CompleteActions(int playerNo)
    {
        PlayerManager player0Manager = player0Obj.GetComponent<PlayerManager>();
        PlayerManager player1Manager = player1Obj.GetComponent<PlayerManager>();

        PointGridPlayManager pointGridManager = pointGridPlayManagerObj.GetComponent<PointGridPlayManager>();
        TerritoryPlayManager territoryManager = territoryPlayManagerObj.GetComponent<TerritoryPlayManager>();
        
        if (playerNo == 0)
        {
            //change cost -> onClick Land
            player0Manager.SetPlayerStatus(playerStatus.Hold, selectedCoinCost, true, true);
            
            //reset UI
            player0Manager.SetPlayerStatus(playerStatus.Hold, 0, true, false);
            player1Manager.SetPlayerStatus(playerStatus.Action, 0, false, false);
            
            // reset action cards
            deSelectAllCards();
            selectingPlayerNo = 1;

            //update games
            setActionCardsPlayingState(actionCardsPlayingState.Waiting);
            territoryManager.SetTerritoryPlayingState(territoryPlayingState.None);
            pointGridManager.SetGridPlayingState(pointGridPlayingState.ResultAction);

        }
        else if (playerNo == 1)
        {
            //change cost -> onClick Land
            player1Manager.SetPlayerStatus(playerStatus.Action, selectedCoinCost, false, true);           
            
            //reset UI
            player0Manager.SetPlayerStatus(playerStatus.Selecting, 0, true, false);            
            player1Manager.SetPlayerStatus(playerStatus.Selecting, 0, true, false);
            
            //reset action cards
            deSelectAllCards();
            selectingPlayerNo = 0;

            //update games
            setActionCardsPlayingState(actionCardsPlayingState.None);
            territoryManager.SetTerritoryPlayingState(territoryPlayingState.None);
            pointGridManager.SetGridPlayingState(pointGridPlayingState.None);

            //restart the game
            pointGridManager.StartPointGridGame();
        }
    }

    public void deSelectAllCards()
    {
        selectedCardNo = -1;
        selectedCoinCost = 0;
        
        foreach (GameObject card in actionCards)
        {
            card.GetComponent<ActionCardManager>().SetSelected(false);
        }
    }
}
