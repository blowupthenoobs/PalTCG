using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    // Make sure only one instance of the Game Manager exists.
    private static GameManager _instance;

    // Make access limited when loading.
    public static GameManager Instance
    {
        get 
        { 
            if (_instance == null)
            {
                Debug.LogError("GameManager is NULL.");
            }
            return _instance; 
        }
    }

    // Check to see if another Game Manager exists. If another exists, destory the Game Object.
    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        // If no other exists, don't destory the original.
        DontDestroyOnLoad(this);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
