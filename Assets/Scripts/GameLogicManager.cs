using System.Collections;
using System.Collections.Generic;
using EasyUpdateDemoSDK;
using Unity.VisualScripting;
using UnityEngine;

public class GameLogicManager : MonoBehaviour
{
    public static GameLogicManager instance;

    private void Awake()
    {
        instance = this;
    }
    public int killCount;

    public void OnLoaded()
    {   
        killCount = 0;
        MsgSystem.instance.RegistMsgAction("EnemyDead", MaskCount);
    }

    public void DestroySelf()
    {
        MsgSystem.instance.RemoveMsgAction("EnemyDead", MaskCount);
        Destroy(this.gameObject);
    }

    public void MaskCount(object[] param)
    {
        killCount++;
        GameUIManager.Instance.UpdataKillCount(killCount);
    }
}
