using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum territoryPlayingState
{
    None,
    Waiting,
    P0Placed,
    P1Placed,
}

public class TerritoryPlayManager : MonoBehaviour
{
    public GameObject actionCardsPlayManagerObj;
    ActionCardsPlayManager actionCardsPlayManager;
    private GameObject audioManagerObj;
    AudioManager audioManager;
    public GameObject player0Obj;
    PlayerManager player0Manager;
    public GameObject player1Obj;
    PlayerManager player1Manager;
    public GameObject LandCardsDatasObk;
    LandCardsDatas landCardsDatas;
    private List<GameObject> landCards;
    public List<GameObject> lastCards;

    public territoryPlayingState currentTerritoryPlayingState;

    // public GameObject landCardsDatasObj;

    //UI
    public GameObject GamePlayUI;
    public GameObject UndoUI;
    public Sprite resetLandImg;
    
    // Start is called before the first frame update
    void Start()
    {   
        audioManagerObj = GameObject.Find("AudioManager");
        audioManager = audioManagerObj.GetComponent<AudioManager>();
        actionCardsPlayManager = actionCardsPlayManagerObj.GetComponent<ActionCardsPlayManager>();
        player0Manager = player0Obj.GetComponent<PlayerManager>();
        player1Manager = player1Obj.GetComponent<PlayerManager>();
        landCardsDatas = LandCardsDatasObk.GetComponent<LandCardsDatas>();
        landCards = landCardsDatas.landCards;

        setTerritoryPlayingState(territoryPlayingState.None);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTerritoryPlayingState(territoryPlayingState newState)
    {
        currentTerritoryPlayingState = newState;
        int selectedActionCardNo = actionCardsPlayManager.selectedActionCardNo;

        //update UI
        GamePlayUI.GetComponent<UpdateTerritoryPlayBg>().UpdateBg(currentTerritoryPlayingState);

        switch (currentTerritoryPlayingState)
        {
            case territoryPlayingState.None:  
                setLandCardsButtonEnabled(false);
                setLineCardButtonEnabled(false);
                // Debug.Log("territoryPlayingState is None");

                updateRedoUI();
                break;
            case territoryPlayingState.Waiting:
                if (selectedActionCardNo == 0)
                {
                    setLineCardButtonEnabled(true);
                    setLandCardsButtonEnabled(false);
                }
                else
                {
                    setLineCardButtonEnabled(false);
                    setLandCardsButtonEnabled(true);
                }

                updateRedoUI();
                // Debug.Log("territoryPlayingState is Waiting");
                break;
            case territoryPlayingState.P0Placed:
                setLandCardsButtonEnabled(false);
                setLineCardButtonEnabled(false);

                updateRedoUI();
                // Debug.Log("territoryPlayingState is P0Placed");
                break;
            case territoryPlayingState.P1Placed:
                setLandCardsButtonEnabled(false);
                setLineCardButtonEnabled(false);

                updateRedoUI();
                // Debug.Log("territoryPlayingState is P1Placed");
                break;
        }
    }

    
    public territoryPlayingState GetTerritoryPlayingState()
    {
        return currentTerritoryPlayingState;
    }

    public void clickLandCardSoundPlay()
    {
        audioManager.playClickSound();
        // Debug.Log("clickLandCardSoundPlay");
    }

    public void setLandCardsButtonEnabled(bool isEnable)
    {
        foreach (GameObject card in landCards)
        {
            card.GetComponent<LandUnit>().setButtonEnabled(isEnable);
        }
    }

    public void setLineCardButtonEnabled(bool isEnable)
    {
        foreach (GameObject card in landCards)
        {
            foreach (GameObject line in card.GetComponent<LandUnit>().lineObjs)
            {
                line.GetComponent<LineUnitManager>().setButtonEnabled(isEnable);
            }
            
        }
    }

    public void addPlacedCard(GameObject card)
    {
        lastCards.Add(card);
        Debug.Log("addPlacedCard: " + card.name);
        updateRedoUI();
    }

    public void redoLastCards()
    {
        GameObject lastCard = lastCards[lastCards.Count - 1];
        PlayerManager currentPlayerManager = (actionCardsPlayManager.getSelectingPlayerNo() == 0) ? player0Manager : player1Manager;

        if (lastCard.GetComponent<LandUnit>() != null)
        {
            int lastPlacedCardCoin = lastCard.GetComponent<LandUnit>().coinCost;
            int lastPlacedCardGridCount = lastCard.GetComponent<LandUnit>().gridCount;
            Debug.Log("lastPlacedCardGridCount: " + lastPlacedCardGridCount);

            lastCard.GetComponent<LandUnit>().setOwnALandCard(-1, resetLandImg, 0, 0);

            //if gridCount 2
            if (lastPlacedCardGridCount > 1)
            {
                GameObject lastSecondCard = lastCards[lastCards.Count - 2];
                lastSecondCard.GetComponent<LandUnit>().setOwnALandCard(-1, resetLandImg, 0, 0);
                lastCards.Remove(lastSecondCard);
            }

            currentPlayerManager.SetPlayerStatus(playerStatus.Action, -lastPlacedCardCoin, false, true);
            currentPlayerManager.SetPlayerStatus(playerStatus.Action, 0, false, false);
        }
        else if (lastCard.GetComponent<LineUnitManager>() != null)
        {
            int lastPlacedCardCoin = lastCard.GetComponent<LineUnitManager>().coinCost;
            
            lastCard.GetComponent<LineUnitManager>().setOwnLine(-1);

            currentPlayerManager.SetPlayerStatus(playerStatus.Action, -lastPlacedCardCoin, false, true);
            currentPlayerManager.SetPlayerStatus(playerStatus.Action, 0, false, false);
        }
        
        if (lastCards.Count == 1)
        {
            List<GameObject> emptyList = new List<GameObject>();
            lastCards = emptyList;
        }
        else 
        {
            lastCards.Remove(lastCard);
        }
        // Debug.Log("redo lastCard: " + lastCard.name);
        updateRedoUI();
    }

    public void resetPlacedCards()
    {
        List<GameObject> emptyList = new List<GameObject>();
        lastCards = emptyList;
        Debug.Log("resetPlacedCards");
        updateRedoUI();
    }

    public void updateRedoUI()
    {
        if (lastCards.Count > 0)
        {
            UndoUI.SetActive(true);
        }
        else
        {
            UndoUI.SetActive(false);
        }
    }
}
