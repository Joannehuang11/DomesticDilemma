using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public List<GameObject> scenes;
    public int currentSceneIndex = 0;

    private GameObject progressManagerObj;
    ProgressManager progressManager;

    private GameObject audioManagerObj;
    AudioManager audioManager;
    
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
        if (index == scenes.Count - 1)
        {
            progressManager.setIsInputBlock(false);
            // Debug.Log("End of intro.");
        }
        
        for (int i = 0; i < scenes.Count; i++)
        {
            if (i != index)
            {
                scenes[i].SetActive(false);
            }
            else 
            {
                scenes[i].SetActive(true);
            }
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
    }
}
