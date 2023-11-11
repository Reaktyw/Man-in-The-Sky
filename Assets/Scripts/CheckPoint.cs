using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private const string PLAYER = "Player";
    private const string GAMEMANAGER = "GameManager";

    private GameManager gameManager;
    private bool isClose = false;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag(GAMEMANAGER).GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER))
        {
            isClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER))
        {
            isClose = false;
        }
    }

    private void Update()
    {
        if (isClose && Input.GetKeyDown(KeyCode.E))
        {
            gameManager.lastCheckPointPos = transform.position;
        }
    }
}
