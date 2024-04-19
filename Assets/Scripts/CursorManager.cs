using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public Texture2D defaultCursor;
    public Texture2D interactableCursor;
    private bool isMainGameScene = false;

    private GameObject sceneManagerObj;
    private SceneManager sceneManager;

    public GameObject mainGameSceneObj;

    
    // Start is called before the first frame update
    void Start()
    {
        sceneManagerObj = GameObject.Find("SceneManager");
        sceneManager = sceneManagerObj.GetComponent<SceneManager>();

        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {   
        if (IsMouseOverInteractableUI(sceneManager.isMainGameScene))
        {
            Cursor.SetCursor(interactableCursor, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        }
    }

    bool IsMouseOverInteractableUI(bool isMainGame)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        bool foundInteractable = false;
        
        if (isMainGame)
        {
            // Debug.Log("Main game scene.");
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.GetComponent<Button>() != null)
                {
                    if (result.gameObject.GetComponent<Button>().enabled == true)
                    {
                        // Debug.Log("Found interactable button: " + result.gameObject.name);
                        foundInteractable = true;
                        break;
                    }
                }
            } 
        }
        else
        {
            // Debug.Log("Intro scene.");
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.layer == LayerMask.NameToLayer("MainBTNs"))
                {
                    foundInteractable = true;
                    break;
                }
            } 
        }

        return foundInteractable;
    }

    public void setIsMainGameScene(bool isMainGame)
    {
        isMainGameScene = isMainGame;
        Debug.Log("Main game scene: " + isMainGameScene);
    }
}
