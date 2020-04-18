using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController
{
    private static GameController instance;

    public PlayerController PlayerController;

    private GameController()
    {
    }

    public static GameController getInstance()
    {
        if (instance == null)
            instance = new GameController();
        return instance;
    }
}