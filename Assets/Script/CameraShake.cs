using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Transform camera;
    [SerializeField] private Vector3 positionStrength;
    [SerializeField] private Vector3 rotationStrength;
    [SerializeField] TDEvent tdEvent;

    public float shakeDuration = 3;
    void Update()
    {
        if (tdEvent != null && tdEvent.isCamShake == true)
        {
            CameraShaker();
            tdEvent.isCamShake = false;
        }
    }

    private void CameraShaker()
    {
        //DOTween
        //.To(value => { }, 0, 1, 1)
        //.SetUpdate(true);
        camera.DOShakePosition(shakeDuration, positionStrength);
        camera.DOShakeRotation(shakeDuration, rotationStrength)/*.OnComplete(StartTime)*/;
    }

    //void StartTime()
    //{
    //    DOTween
    //    .To(value => { }, 0, 1, 1)
    //    .SetUpdate(false);
    //}

}
