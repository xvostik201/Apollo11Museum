using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CassetteReceiver : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private float _animationTime = 1f;
    [SerializeField] private Vector3 _animationEndPosition;
    [SerializeField] private Transform _animationStartPosition;
    
    private Sequence _animationSequence;
    
    private MuseumData _museumData;
    private bool _hasCassete = false;
    
    private Cassette _currentCassette;

    public event Action<bool> OnCassetteChanged;
    public event Action<bool> OnCassetteEnter;

    private void Start()
    {
        OnCassetteChanged?.Invoke(false);
        OnCassetteEnter?.Invoke(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Cassette cassette = other.GetComponent<Cassette>();

        if (cassette != null)
        {
            _hasCassete = true;
            _museumData = cassette.GetMuseumData();
            _currentCassette = cassette;
    
            _currentCassette.transform.SetParent(this.transform);
            _currentCassette.transform.position = _animationStartPosition.position;
    
            OnCassetteChanged?.Invoke(true);
    
            AnimateCassette(_currentCassette, _animationTime, true);
        }
    }

    private void AnimateCassette(Cassette cassette, float animationTime, bool enter)
    {
        _animationSequence?.Kill();
        
        _animationSequence = DOTween.Sequence();

        _animationSequence.Append(_currentCassette.transform.DOLocalMove(
            enter ? _animationEndPosition
                : _animationStartPosition.position
                ,_animationTime))
            .OnComplete(() => OnCassetteEnter?.Invoke(enter));;
        if (!enter)
        {
            _hasCassete = false;
            if (_currentCassette != null) 
            {
                _currentCassette.transform.SetParent(null);
            }
            _currentCassette = null;
            OnCassetteChanged?.Invoke(false);
        }
        
    }

    public void RejectCassette()
    {
        AnimateCassette(_currentCassette, _animationTime, false);
    }

    public VideoClip GetCassetteVideoClip()
    {
        return _museumData.VideoClip;
    }

    public Image GetCassetteImage()
    {
        return _museumData.Image;
    }
}
