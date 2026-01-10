using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ApolloErrorPanel : BaseMuseumPanel
{
    [Header("Scenario")] 
    [Header("Light")]
    [SerializeField] private Light _redLight;
    
    [Header("Text")]
    [SerializeField] private TMP_Text _errorTMP;
    [SerializeField] private string _errorText = "1202";

    [Header("Audio")]
    [SerializeField] private AudioSource _alarmSource;

    private bool _isErrorActive = false;

    private void Awake()
    {
        _errorTMP.text = "0000";
    }
    
    public override void ActivateScenario()
    {
        _isErrorActive = true;
        
        _errorTMP.text = _errorText;
        _redLight.gameObject.SetActive(true);
        
        if(!_alarmSource.isPlaying) _alarmSource.Play();
        
        Debug.Log("ВНИМАНИЕ, СИСТЕМА ПЕРЕГРУЖЕНА ОШИБКА 1202");
    }

    protected override void OnButtonPressedLogic()
    {
        if (!_isErrorActive) return;
        
        _isErrorActive = false;
        
        _errorTMP.text = "0000";
        _redLight.gameObject.SetActive(false);
        
        _alarmSource.Stop();
        
        Debug.Log("СИСТЕМА ПЕРЕЗАГРУЖЕНА");
    }
    
}
