using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public PlayerContro playerContro;

    private void Awake()
    {
        instance = this;
    }

    public Transform GetPlayerTransform()
    {
        return playerContro.transform;
    }
}
