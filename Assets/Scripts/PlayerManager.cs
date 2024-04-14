using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

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
    public GameObject playerStatusSignSymbol;

    
    // Start is called before the first frame update
    void Start()
    {
        playerCoin = 0;
        playerCall = playerCall.None;

        playerIDtext.text = "Player " + playerNo.ToString();
        playerCoinText.text = playerCoin.ToString();        
        SetPlayerStatus(playerStatus.Selecting);
        playerStatusSignSymbol.GetComponent<UpdateStatusSignSymbol>().SetStatusSignSymbolImg(playerStatus);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerCall(playerCall call)
    {
        playerCall = call;
        // Debug.Log("Set Player " + playerNo + " call:" + call);
    }

    public playerCall GetPlayerCall()
    {
        return playerCall;
    }

    public void SetPlayerCoin(int coin)
    {
        playerCoin = coin;
    }
    public int GetPlayerCoin()
    {
        return playerCoin;
    }

    public void SetPlayerStatus(playerStatus state)
    {
        playerStatus = state;
        
        switch (state)
        {
            case playerStatus.Selecting:
                playerStatusText.text = "Selecting";
                // Debug.Log("Player " + playerNo + " is selecting");
                break;
            case playerStatus.Ready:
                playerStatusText.text = "Ready";
                // Debug.Log("Player " + playerNo + " is ready");
                break;
            case playerStatus.Action:
                playerStatusText.text = "Action"; 
                // Debug.Log("Player " + playerNo + " is action");
                break;
            case playerStatus.Hold:
                playerStatusText.text = "Hold";
                // Debug.Log("Player " + playerNo + " is hold");
                break;
        }
    }

    public playerStatus GetPlayerState()
    {
        return playerStatus;
    }


}
