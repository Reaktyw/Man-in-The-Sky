using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{
    private const string GAMEMANAGER = "GameManager";
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag(GAMEMANAGER).GetComponent<GameManager>();
        transform.position = gameManager.lastCheckPointPos;
    }
}
