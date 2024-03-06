using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class UITweener : MonoBehaviour
{
    public LeanTweenType inType;
    public LeanTweenType outType;
    public float duration;
    public float delay;
    public float sizeXBefore;
    public float sizeYBefore;
    public float sizeXAfter;
    public float sizeYAfter;
    public UnityEvent onCompleteCallBack;

    public void OnEnable() {
        transform.localScale = new Vector2(0,0);
        LeanTween.scale(gameObject, new Vector2(sizeXBefore,sizeYBefore), duration).setDelay(delay).setOnComplete(OnComplete).setEase(inType);
    }

    public void OnMouseEnter() {
        LeanTween.scale(gameObject, new Vector2(sizeXAfter,sizeYAfter), duration).setEase(inType);
    }
    public void OnMouseExit() {
        LeanTween.scale(gameObject, new Vector2(sizeXBefore,sizeYBefore), duration).setEase(outType);
    }

    public void OnComplete() {
        if (onCompleteCallBack != null)
        {
            onCompleteCallBack.Invoke();
        }
    }

    public void OnClose(){
        LeanTween.scale(gameObject, new Vector3(0,0,0), duration).setEase(outType).setOnComplete(DestroyMe);
    }

    void DestroyMe() {
        Destroy(gameObject);
    }
}
