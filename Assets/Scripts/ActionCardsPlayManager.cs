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
    public GameObject pointGridPlayManager;
    public GameObject player0;
    public GameObject player1;

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

        if (playerNo == 0)
        {
            player0.GetComponent<PlayerManager>().SetPlayerStatus(playerStatus.Action, selectedCoinCost, false, false);
            player1.GetComponent<PlayerManager>().SetPlayerStatus(playerStatus.Hold, 0, true, false);
        }
        else if (playerNo == 1)
        {
            player1.GetComponent<PlayerManager>().SetPlayerStatus(playerStatus.Hold, 0, true, false);
            player1.GetComponent<PlayerManager>().SetPlayerStatus(playerStatus.Action, selectedCoinCost, false, false);
        }
    }

    public void CompleteActions(int playerNo)
    {
        if (playerNo == 0)
        {
            // change cost
            player0.GetComponent<PlayerManager>().SetPlayerStatus(playerStatus.Hold, selectedCoinCost, true, true);
            
            // reset UI
            player0.GetComponent<PlayerManager>().SetPlayerStatus(playerStatus.Hold, 0, true, false);
            player1.GetComponent<PlayerManager>().SetPlayerStatus(playerStatus.Action, 0, false, false);
            
            // reset action cards
            deSelectAllCards();
            selectingPlayerNo = 1;
        }
        else if (playerNo == 1)
        {
            // change cost
            player1.GetComponent<PlayerManager>().SetPlayerStatus(playerStatus.Action, selectedCoinCost, false, true);           
            
            //reset UI
            player0.GetComponent<PlayerManager>().SetPlayerStatus(playerStatus.Selecting, 0, true, false);            
            player1.GetComponent<PlayerManager>().SetPlayerStatus(playerStatus.Selecting, 0, true, false);
            
            //reset action cards
            deSelectAllCards();
            selectingPlayerNo = 0;
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
