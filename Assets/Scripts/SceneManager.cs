using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public List<GameObject> introScenes;
    public GameObject endScene;
    public int currentSceneIndex = 0;

    private GameObject progressManagerObj;
    ProgressManager progressManager;

    private GameObject audioManagerObj;
    AudioManager audioManager;
    public bool isMainGameScene;

    
    // Start is called before the first frame update
    void Start()
    {
        audioManagerObj = GameObject.Find("AudioManager");
        audioManager = audioManagerObj.GetComponent<AudioManager>();

        progressManagerObj = GameObject.Find("ProgressManager");
        progressManager = progressManagerObj.GetComponent<ProgressManager>();
        progressManager.setIsInputBlock(true);

        setScene(currentSceneIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setScene(int index)
    {
        if (index == introScenes.Count - 1)
        {
            progressManager.setIsInputBlock(false);
            // Debug.Log("End of intro.");
        }

        if (index == introScenes.Count)
        {
            isMainGameScene = true;
            // Debug.Log("Main game scene.");
        }
        else
        {
            isMainGameScene = false;
        }
        
        for (int i = 0; i < introScenes.Count; i++)
        {
            if (i != index)
            {
                introScenes[i].SetActive(false);
                
            }
            else 
            {
                introScenes[i].SetActive(true);
            }
            endScene.SetActive(false);
        }
    }
    
    public void moveToNextScene()
    {
        currentSceneIndex++;
        setScene(currentSceneIndex);

        audioManager.playClickSound();
    }

    public void skipIntro()
    {
        currentSceneIndex = 4;
        setScene(currentSceneIndex);

        audioManager.playClickSound();
    }

    public void endGameScene()
    {
        progressManager.setIsInputBlock(true);
        endScene.SetActive(true);

        isMainGameScene = false;
    }

    public void restartGameScene()
    {
        currentSceneIndex = introScenes.Count;
        setScene(currentSceneIndex);

        audioManager.playClickSound();
    }

    public void backHomeScene()
    {
        currentSceneIndex = 0;
        endScene.SetActive(false);
        setScene(currentSceneIndex);

        audioManager.playClickSound();
    }
}
