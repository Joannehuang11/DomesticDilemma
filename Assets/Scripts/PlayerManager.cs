using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum playerCall
{
    None,
    Collab,
    NotCollab
}

public enum playerStatus
{
    Selecting,
    Ready,
    Action,
    Hold,
    Break,
}

public class PlayerManager : MonoBehaviour
{
    public int playerNo;
    public int playerCoin;
    public playerCall playerCall;
    public playerStatus playerStatus;

    //UI
    public TextMeshProUGUI playerIDtext;
    public TextMeshProUGUI playerCoinText;
    public TextMeshProUGUI playerStatusText;
    
    public GameObject playerStatusSignCoinTextObj;
    private TextMeshProUGUI playerStatusSignCoinText;
    public GameObject playerStatusSignSymbol;
    public GameObject playerStatusCard;
    public GameObject instructionsUI;
    public GameObject actionDoneUI;
    public GameObject actionSkipUI;
    public GameObject warningTextUI;
    private TextMeshProUGUI warningTextTMP;
    public string moreCoinsText = "Need more coins";
    public string moreSpaceText = "Need more space";
    // public string actionDoneBTNText = "Done";
    // public string actionSkipBTNText = "Skip";

    private Transform playerStatusSignCoin;


    //game costing
    private int currentCost;

    
    // Start is called before the first frame update
    void Start()
    {
        //Get component
        playerStatusSignCoinText = playerStatusSignCoinTextObj.GetComponent<TextMeshProUGUI>();
        warningTextTMP = warningTextUI.GetComponent<TextMeshProUGUI>();
        
        playerCoin = 0;
        playerCall = playerCall.None;

        //UI
        playerIDtext.text = "Player " + playerNo.ToString();
        playerCoinText.text = playerCoin.ToString();        
        SetPlayerStatus(playerStatus.Selecting, 0, true, false);
        playerStatusSignSymbol.GetComponent<UpdateStatusSignSymbol>().setSymbol(playerStatus);
    
        //game costing
        currentCost = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerCall(playerCall call)
    {
        playerCall = call;
        // Debug.Log("Set Player " + playerNo + " call:" + call);
        if (call != playerCall.None)
        {
            SetPlayerStatus(playerStatus.Ready, 0, true, true);
            playerStatusSignSymbol.GetComponent<UpdateStatusSignSymbol>().setSymbol(playerStatus);
        }
    }

    public playerCall GetPlayerCall()
    {
        return playerCall;
    }

    public void SetPlayerCoin(int coin)
    {
        playerCoin = coin;
        playerCoinText.text = playerCoin.ToString();
    }
    public int GetPlayerCoin()
    {
        return playerCoin;
    }

    public void SetPlayerStatus(playerStatus state, int statusCoin, bool isSymbolShow, bool isCostChange)
    {
        playerStatus = state;
        playerStatusSignCoinText.text = statusCoin.ToString("+0;-0;0");

        currentCost = statusCoin;

        playerStatusCard.GetComponent<UpdateStatusCardBg>().UpdateBg(playerStatus, playerNo);
        playerStatusSignSymbol.GetComponent<UpdateStatusSignSymbol>().setSymbol(playerStatus);

        if (isSymbolShow)
        {
            playerStatusSignCoinTextObj.SetActive(false);
            playerStatusSignSymbol.SetActive(true);
        }
        else
        {
            playerStatusSignCoinTextObj.SetActive(true);
            playerStatusSignSymbol.SetActive(false);
        }

        switch (state)
        {
            case playerStatus.Selecting:
                playerStatusText.text = "Selecting";
                instructionsUI.SetActive(true);
                setActionBTN(false, false);
                // Debug.Log("Player " + playerNo + " is selecting");
                break;
            case playerStatus.Ready:
                playerStatusText.text = "Ready";
                instructionsUI.SetActive(false);
                setActionBTN(false, false);
                // Debug.Log("Player " + playerNo + " is ready");
                break;
            case playerStatus.Action:
                playerStatusText.text = "Action";
                instructionsUI.SetActive(false);
                setActionBTN(true, false);
                // Debug.Log("Player " + playerNo + " is action");
                break;
            case playerStatus.Hold:
                playerStatusText.text = "Hold";
                instructionsUI.SetActive(false);
                setActionBTN(false, false);
                // Debug.Log("Player " + playerNo + " is hold");
                break;
            case playerStatus.Break:
                playerStatusText.text = "Break";
                instructionsUI.SetActive(false);
                setActionBTN(false, true);
                // Debug.Log("Player " + playerNo + " is hold");
                break;                
        }

        if (isCostChange)
        {
            SetPlayerCoin(playerCoin += currentCost);
        }
    }

    public playerStatus GetPlayerState()
    {
        return playerStatus;
    }

    public bool checkBudget(int cost)
    {
        // Debug.Log("PlayerCoin is " + playerCoin + " and cost is " + cost + " so the remaining is " + (playerCoin + cost));
        if (playerCoin + cost >= 0)
        {
            setWarningText(false, moreCoinsText);
            // Debug.Log("Player " + playerNo + " has enough budget");
            return true;
        }
        else
        {
            setWarningText(true, moreCoinsText);
            // Debug.Log("Player " + playerNo + " has not enough budget");
            return false;
        }
    }

    public void setWarningText(bool isShow, string warningText)
    {
        if (isShow)
        {
            warningTextTMP.text = warningText;
        }
        else
        {
            warningTextTMP.text = "";
        }
    }

    public void setActionBTN(bool isDoneShow, bool isSkipShow)
    {
        actionDoneUI.SetActive(isDoneShow);
        actionSkipUI.SetActive(isSkipShow);
    }
}
