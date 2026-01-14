using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ApolloErrorExhibit : BaseMuseumExhibit
{
    [Header("Scenario")] 
    [Header("Light")]
    [SerializeField] private Light _redLight;
    [SerializeField] private float _animateDuration = 1f;
    [SerializeField] private float _lightMaxIntensity = 50f;
    [SerializeField] private float _lightMinIntensity = 1f;
    private Sequence _lightSequence;
    
    [Header("Text")]
    [SerializeField] private TMP_Text _errorTMP;
    [SerializeField] private string _errorText = "1202";

    [Header("Audio")]
    [SerializeField] private AudioSource _alarmSource;

    [SerializeField] private float _microPauseTime = 0.2f;
    [SerializeField] private float _pauseTime = 1.2f;
    private Sequence _audioSequence;
    
    private bool _isErrorActive = false;

    private void Awake()
    {
        _errorTMP.text = "0000";
    }
    
    public override void ActivateScenario()
    {
        if (IsScenarioActive) return;

        IsScenarioActive = true;
        
        _isErrorActive = true;
        
        _errorTMP.text = _errorText;

        _redLight.gameObject.SetActive(true);
        
        LightSequence();
        AudioSequence();
        
        Debug.Log("ВНИМАНИЕ, СИСТЕМА ПЕРЕГРУЖЕНА ОШИБКА 1202");
    }

    protected override void OnButtonPressedLogic()
    {
        if (!_isErrorActive) return;
        
        _isErrorActive = false;
        
        _errorTMP.text = "0000";
        
        _audioSequence?.Kill(); 
        _alarmSource.Stop();
        
        _lightSequence?.Kill();
        _redLight.gameObject.SetActive(false);
        
        ResetScenario();
        
        Debug.Log("СИСТЕМА ПЕРЕЗАГРУЖЕНА");
    }

    private void AudioSequence()
    {
        _audioSequence?.Kill();
        
        _audioSequence = DOTween.Sequence();

        _audioSequence.AppendCallback(() => _alarmSource.Play())
            .AppendInterval(0.3f)
            .AppendCallback(() => _alarmSource.Stop())
            
            .AppendInterval(_microPauseTime)

            .AppendCallback(() => _alarmSource.Play())
            .AppendInterval(0.3f)
            .AppendCallback(() => _alarmSource.Stop())
            
            .AppendInterval(_pauseTime)
            
            .SetLoops(-1);
    }

    private void LightSequence()
    {
        _lightSequence?.Kill();
        
        _lightSequence = DOTween.Sequence();

        _lightSequence.Append(_redLight.DOIntensity(_lightMaxIntensity, _animateDuration))
            .AppendInterval(0.4f)
            .Append(_redLight.DOIntensity(_lightMinIntensity, _animateDuration))
            .SetLoops(-1);
    }
}
