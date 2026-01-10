using System;
using DG.Tweening;
using UnityEngine;

public class Button : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Vector3 _animationEndPosition = new Vector3(0, 0, -0.1f);
    [SerializeField] private float _animationDuration = 0.5f;
    
    public event Action OnButtonPressed;

    private Vector3 _startlocalPos;

    private void Awake()
    {
        _startlocalPos = transform.localPosition;
    }

    private void AnimateButton()
    {
        transform.DOKill(); 
    
        Sequence seq = DOTween.Sequence();
        float time = _animationDuration / 2;

        seq.Append(transform.DOLocalMoveZ(_startlocalPos.z + _animationEndPosition.z, time));
    
        seq.Append(transform.DOLocalMoveZ(_startlocalPos.z, time));
    
        seq.OnComplete(() => OnButtonPressed?.Invoke());
    }

    public void PressButton()
    {
        AnimateButton();
    }
}
