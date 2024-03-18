using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    [SerializeField] private float shakeIntensity = 5.0f;
    [SerializeField] private float shakeTime = 0.1f;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;

    private void Awake() {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera (float intensity, float time){
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update() {
        if (shakeTimer > 0){
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f){
                // Time over!
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }

    public void Triggershake() {
        float predefinedIntensity = 5.0f;
        float predefinedTime = 0.1f;
        ShakeCamera(predefinedIntensity, predefinedTime);
    }

    public void TriggerShakeWithSerializedValues() {
        ShakeCamera(shakeIntensity, shakeTimer);
    }
}
