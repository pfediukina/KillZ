using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    [SerializeField] private CanvasGroup _touch;

    private void Start()
    {
#if UNITY_ANDROID
        
            _touch.blocksRaycasts = true;
            _touch.alpha = 1;
#endif
    }
}
