using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;

    private void Awake()
    {
       Instance = this; 
    }

    public PlayerHPUI playerHPUI;
}
