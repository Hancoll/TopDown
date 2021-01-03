using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static PlayerController PlayerController { get; private set; }
    public static bool GameMode {get; private set; }

    private void Awake()
    {
        GameObject player = Instantiate(Resources.Load("Player") as GameObject, transform);
        PlayerController = player.GetComponent<PlayerController>();
    }

    private void Start()
    {
        GameMode = true;
    }

    //Подписка на события
    private void OnEnable() => PlayerController.death += OnPlayerDie;
    private void OnDisable() => PlayerController.death -= OnPlayerDie;

    private void OnPlayerDie()
    {
        GameMode = false;
    }
}
