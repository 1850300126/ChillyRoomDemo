using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public Transform left_follow_point;
    public Transform right_follow_point;

    public CinemachineVirtualCamera main_cm;
    public CinemachineVirtualCamera death_cm;
    private CinemachineBasicMultiChannelPerlin noiseProfile;

    private void Awake()
    {
        noiseProfile = main_cm.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        main_cm.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = GameObject.Find("CinemachineCollider").GetComponent<PolygonCollider2D>();
        death_cm.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = GameObject.Find("CinemachineCollider").GetComponent<PolygonCollider2D>();
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
    /// <summary>
    /// 摄像机振动
    /// </summary>
    /// <param name="duration">持续时间</param>
    /// <param name="amplitude">幅度</param>
    /// <param name="frequency">频率</param>
    public void ShakeCamera(float duration = 0.25f, float amplitude = 2, float frequency = 1)
    {
        if (noiseProfile != null)
        {
            noiseProfile.m_AmplitudeGain = amplitude;
            noiseProfile.m_FrequencyGain = frequency;
            Invoke(nameof(StopShaking), duration);
        }
    }

    private void StopShaking()
    {
        if (noiseProfile != null)
        {
            noiseProfile.m_AmplitudeGain = 0;
            noiseProfile.m_FrequencyGain = 0;
        }
    }

    public void DeathCamera()
    {

        death_cm.gameObject.SetActive(true);
        // main_cm.m_Follow = PlayerManager.instance.GetPlayerTransform();
        /*        DOVirtual.Float
                (
                    main_cm.m_Lens.OrthographicSize, 3, 1f, (current_frame_value) =>
                    {
                        main_cm.m_Lens.OrthographicSize = current_frame_value;
                    }
                );*/
    }
}
