using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum lineType
{
    Top,
    Right,
    Bottom,
    Left
}
public class LineUnitManager : MonoBehaviour, IPointerClickHandler
{
    public int ownerNo;
    
    public lineType lineType;
    public float placedLineThickness = 3f;
    private RectTransform lineRectTransform;
    private Vector3 hoScale;
    private Vector3 veScale;

    public GameObject landCardObj;
    private GameObject progressManagerObj;
    ProgressManager progressManager;
    private GameObject territoryPlayManagerObj;
    TerritoryPlayManager territoryPlayManager;
    private GameObject actionCardsPlayManagerObj;
    ActionCardsPlayManager actionCardsPlayManager;
    private GameObject player0Obj;
    PlayerManager player0Manager;
    private GameObject player1Obj;
    PlayerManager player1Manager;
    Button buttonComponent;
    
    
    // Start is called before the first frame update
    void Start()
    {
        lineRectTransform = GetComponent<RectTransform>();
        buttonComponent = GetComponent<Button>();
        hoScale = new Vector3(1, placedLineThickness, 1);
        veScale = new Vector3(placedLineThickness, 1, 1);

        progressManagerObj = GameObject.Find("ProgressManager");
        progressManager = progressManagerObj.GetComponent<ProgressManager>();
        territoryPlayManagerObj = GameObject.Find("TerritoryPlayManager");
        territoryPlayManager = territoryPlayManagerObj.GetComponent<TerritoryPlayManager>();
        actionCardsPlayManagerObj = GameObject.Find("ActionCardsPlayManager");
        actionCardsPlayManager = actionCardsPlayManagerObj.GetComponent<ActionCardsPlayManager>();
        player0Obj = GameObject.Find("Player0");
        player0Manager = player0Obj.GetComponent<PlayerManager>();
        player1Obj = GameObject.Find("Player1");
        player1Manager = player1Obj.GetComponent<PlayerManager>();

        ownerNo = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Debug.Log("Click on LineUnitType: " + lineType);

        if(!progressManager.isInputBlock)
        {
            territoryPlayingState currentTerritoryPlayingState = territoryPlayManager.currentTerritoryPlayingState;
            int selectedActionCardNo = actionCardsPlayManager.selectedActionCardNo;

            if (selectedActionCardNo == 0 && currentTerritoryPlayingState == territoryPlayingState.Waiting)
            {
                int selectingPlayerNo = actionCardsPlayManager.getSelectingPlayerNo();
                int selectingCoinCost = actionCardsPlayManager.getSelectedCoinCost();

                if (ownerNo < 0)
                {
                    ownerNo = selectingPlayerNo;
                    setOwnLine(lineType);

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

                    //play audio
                    territoryPlayManager.clickLandCardSoundPlay();
                }

            }
        }
    }

    public void setOwnLine(lineType lineType)
    {
        Debug.Log("setOwnLine: " + lineType);
        switch(lineType)
        {
            case lineType.Top:
                Debug.Log("setOwnLine case type: " + lineType);
                lineRectTransform.localScale = hoScale;
                break;
            case lineType.Right:
                lineRectTransform.localScale = veScale;
                break;
            case lineType.Bottom:
                lineRectTransform.localScale = hoScale;
                break;
            case lineType.Left:
                lineRectTransform.localScale = veScale;
                break;
        }
        
    }

    public void setButtonEnabled(bool isEnable)
    {
        if (ownerNo < 0)
        {
            buttonComponent.enabled = false;
        }
        else 
        {
            buttonComponent.enabled = isEnable;
        }
    }
}
