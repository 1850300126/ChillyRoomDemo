using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public Transform left_follow_point;
    public Transform right_follow_point;

    public CinemachineVirtualCamera main_cm;
    private CinemachineBasicMultiChannelPerlin noiseProfile;

    private void Awake()
    {
        noiseProfile = main_cm.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Start()
    {

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
    /// �������
    /// </summary>
    /// <param name="duration">����ʱ��</param>
    /// <param name="amplitude">����</param>
    /// <param name="frequency">Ƶ��</param>
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
}
