using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum actionCardsPlayingState
{
    None,
    Waiting,
    P0Selected,
    P1Selected,
}

public class ActionCardsPlayManager : MonoBehaviour
{    
    // interface
    public GameObject player0Obj;
    PlayerManager player0Manager;
    public GameObject player1Obj;
    PlayerManager player1Manager;
    public GameObject pointGridPlayManagerObj;
    PointGridPlayManager pointGridPlayManager;
    public GameObject territoryPlayManagerObj;
    TerritoryPlayManager territoryPlayManager;
    private GameObject progressManagerObj;
    ProgressManager progressManager;
    private GameObject audioManagerObj;
    AudioManager audioManager;

    public actionCardsPlayingState currentActionCardsPlayingState;

    public List<GameObject> actionCards;
    
    public int selectedCoinCost;
    public int selectedActionCardNo;
    public int selectingPlayerNo;
    //UI
    public GameObject GamePlayUI;

    
    // Start is called before the first frame update
    void Start()
    {
        player0Manager = player0Obj.GetComponent<PlayerManager>();
        player1Manager = player1Obj.GetComponent<PlayerManager>();
        
        pointGridPlayManager = pointGridPlayManagerObj.GetComponent<PointGridPlayManager>();
        territoryPlayManager = territoryPlayManagerObj.GetComponent<TerritoryPlayManager>();
        
        progressManagerObj = GameObject.Find("ProgressManager");  
        progressManager = progressManagerObj.GetComponent<ProgressManager>();
        audioManagerObj = GameObject.Find("AudioManager");
        audioManager = audioManagerObj.GetComponent<AudioManager>();

        setActionCardsPlayingState(actionCardsPlayingState.None, false);
        selectedActionCardNo = -1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setActionCardsPlayingState(actionCardsPlayingState newState, bool canMoveOn)
    {
        currentActionCardsPlayingState = newState;
        
        //update UI
        GamePlayUI.GetComponent<UpdateActionCardsPlayBg>().UpdateBg(currentActionCardsPlayingState, canMoveOn);

        switch (currentActionCardsPlayingState)
        {
            case actionCardsPlayingState.None:
                deSelectAllCards();
                setActionCardsButtonEnabled(false);
                // Debug.Log("actionCardsPlayingState is None");
                break;
            case actionCardsPlayingState.Waiting:
                setActionCardsButtonEnabled(true);
                // Debug.Log("actionCardsPlayingState is Waiting");
                break;
            case actionCardsPlayingState.P0Selected:
                setActionCardsButtonEnabled(true);
                // Debug.Log("actionCardsPlayingState is P0Selected");
                break;
            case actionCardsPlayingState.P1Selected:
                setActionCardsButtonEnabled(true);
                // Debug.Log("actionCardsPlayingState is P1Selected");
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
        selectedActionCardNo = cardNo;
        selectedCoinCost = Cost;

        if (playerNo == 0)
        {
            player0Manager.SetPlayerStatus(playerStatus.Action, selectedCoinCost, false, false);
            player1Manager.SetPlayerStatus(playerStatus.Hold, 0, true, false);
        }
        else if (playerNo == 1)
        {
            player0Manager.SetPlayerStatus(playerStatus.Hold, 0, true, false);
            player1Manager.SetPlayerStatus(playerStatus.Action, selectedCoinCost, false, false);
        }
    }

    public int getSelectedCoinCost()
    {
        return selectedCoinCost;
    }

    public void CompleteActions(int playerNo)
    {
        if (playerNo == 0)
        {   
            //reset UI
            player0Manager.SetPlayerStatus(playerStatus.Hold, 0, true, false);
            player1Manager.SetPlayerStatus(playerStatus.Action, 0, false, false);
            player0Manager.setNeedMoreCoinsText(false);
            player1Manager.setNeedMoreCoinsText(false);
            
            // reset action cards
            deSelectAllCards();
            selectingPlayerNo = 1;

            //update games
            setActionCardsPlayingState(actionCardsPlayingState.Waiting, true);
            territoryPlayManager.setTerritoryPlayingState(territoryPlayingState.None);
            // pointGridPlayManager.SetGridPlayingState(pointGridPlayingState.ResultAction);
            territoryPlayManager.resetPlacedCards();

            //play click sound
            audioManager.playClickSound();
        }
        else if (playerNo == 1)
        {           
            //reset UI
            player0Manager.SetPlayerStatus(playerStatus.Selecting, 0, true, false);            
            player1Manager.SetPlayerStatus(playerStatus.Selecting, 0, true, false);
            player0Manager.setNeedMoreCoinsText(false);
            player1Manager.setNeedMoreCoinsText(false);
            
            //reset action cards
            deSelectAllCards();
            selectingPlayerNo = 0;

            //update games
            setActionCardsPlayingState(actionCardsPlayingState.None, true);
            territoryPlayManager.setTerritoryPlayingState(territoryPlayingState.None);
            pointGridPlayManager.SetGridPlayingState(pointGridPlayingState.None);
            territoryPlayManager.resetPlacedCards();

            //restart the game
            progressManager.startGame();

            //play click sound
            audioManager.playClickSound();
        }
    }

    public void deSelectAllCards()
    {
        selectedActionCardNo = -1;
        selectedCoinCost = 0;
        
        foreach (GameObject card in actionCards)
        {
            card.GetComponent<ActionCardManager>().SetSelected(false);
        }
    }

    public List<Sprite> getSelectedCardImgs()
    {
        return actionCards[selectedActionCardNo].GetComponent<ActionCardManager>().GetCardImages();
    }

    public void clickActionCardSoundPlay(bool isSelected)
    {
        if (isSelected)
        {
            audioManager.playClickSound();
        }
        else
        {
            audioManager.playErrorSound();
        }
    }

    public void setActionCardsButtonEnabled(bool isEnable)
    {
        foreach (GameObject card in actionCards)
        {
            card.GetComponent<ActionCardManager>().setButtonActive(isEnable);
        }
    }
}
