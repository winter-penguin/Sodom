using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIState : MonoBehaviour
{
    GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    private void OnEnable()
    {
        gameManager.isUI = true;
    }

    private void OnDisable()
    {
        gameManager.isUI = false;
    }
}
