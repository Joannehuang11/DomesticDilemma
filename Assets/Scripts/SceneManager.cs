using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public List<GameObject> scenes;
    public int currentSceneIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        setScene(currentSceneIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setScene(int index)
    {
        for (int i = 0; i < scenes.Count; i++)
        {
            if (i == scenes.Count)
            {
                scenes[i].SetActive(false);
            }
            else
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
    }
    
    public void moveToNextScene()
    {
        currentSceneIndex++;
        setScene(currentSceneIndex);
    }
}
