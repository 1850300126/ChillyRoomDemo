using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public static CamaraController Instance;

    public PlayerControler PlayerControler;

    public Transform left_follow_point;
    public Transform right_follow_point;

    public CinemachineVirtualCamera main_cm;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if(PlayerControler.transform.transform.localEulerAngles == Vector3.zero)
        {
            SwitchFollowPoint(true);
        }
        else
        {
            SwitchFollowPoint(false);
        }
    }

    public void SwitchFollowPoint(bool left)
    {
        if(left)
        {
            main_cm.m_Follow = left_follow_point;
        }
        else
        {
            main_cm.m_Follow = right_follow_point;
        }
    }
}
