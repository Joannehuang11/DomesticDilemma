using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LandUnit : MonoBehaviour, IPointerClickHandler
{
    public int landNo;
    public int ownerNo;
    public Image landImg;


    //interface
    // public GameObject player0Obj;
    // PlayerManager player0Manager;
    // public GameObject player1Obj;
    // PlayerManager player1Manager;
    // public GameObject territoryPlayManagerObj;
    // TerritoryPlayManager territoryPlayManager;
    // public GameObject progressManagerObj;
    // ProgressManager progressManager;
    // public GameObject actionCardsPlayManagerObj;
    // ActionCardsPlayManager actionCardsPlayManager;

    
    // Start is called before the first frame update
    void Start()
    {
        // player0Manager = player0Obj.GetComponent<PlayerManager>();
        // player1Manager = player1Obj.GetComponent<PlayerManager>();

        // actionCardsPlayManager = actionCardsPlayManagerObj.GetComponent<ActionCardsPlayManager>();
        // territoryPlayManager = territoryPlayManagerObj.GetComponent<TerritoryPlayManager>();
        // progressManager = progressManagerObj.GetComponent<ProgressManager>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // if (!progressManager.isInputBlock)
        // {
        //     if (owner == -1)
        //     {
        //         if (actionCardsPlayManager.currentActionCardsPlayingState == actionCardsPlayingState.P0Selected)
        //         {
        //             setLandCard(landNo, 0, actionCardsPlayManager.actionCards[actionCardsPlayManager.selectedCardNo].GetComponent<ActionCardManager>().thumbnailImg.sprite);
        //             actionCardsPlayManager.setCoinCost(actionCardsPlayManager.selectedCoinCost);
        //             actionCardsPlayManager.setSelectingPlayerNo(1);
        //             actionCardsPlayManager.setActionCardsPlayingState(actionCardsPlayingState.P1Selected);
        //         }
        //         else if (actionCardsPlayManager.currentActionCardsPlayingState == actionCardsPlayingState.P1Selected)
        //         {
        //             setLandCard(landNo, 1, actionCardsPlayManager.actionCards[actionCardsPlayManager.selectedCardNo].GetComponent<ActionCardManager>().thumbnailImg.sprite);
        //             actionCardsPlayManager.setCoinCost(actionCardsPlayManager.selectedCoinCost);
        //             actionCardsPlayManager.setSelectingPlayerNo(0);
        //             actionCardsPlayManager.setActionCardsPlayingState(actionCardsPlayingState.P0Selected);
        //         }
        //     }
        // }

    }
    
    public void setStartLandCard(int no, int playerNo)
    {
        landNo = no;
        ownerNo = playerNo;
    }

    public void setOwnLandCard(int playerNo, Sprite img)
    {
        ownerNo = playerNo;
        landImg.sprite = img;
    }
}
