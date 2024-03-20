using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CameraShaker : MonoBehaviour{
    [SerializeField] private Transform _camera;
    [SerializeField] private Vector3 _positionStrength;
    [SerializeField] private Vector3 _rotationStrength;
    [SerializeField] private float shakeDuration = 0.3f;
    
    private static event Action Shake;

    public static void Invoke() {
        Shake?.Invoke();
    }

    private void OnEnable() => Shake += CameraShake;
    private void OnDisable() => Shake -= CameraShake;
    
    private void CameraShake(){
        _camera.DOComplete();
        _camera.DOShakePosition(shakeDuration, _positionStrength);
        _camera.DOShakeRotation(shakeDuration, _rotationStrength);
    }
}