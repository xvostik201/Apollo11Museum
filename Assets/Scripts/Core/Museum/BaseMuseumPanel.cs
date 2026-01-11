using System;
using UnityEngine;

public abstract class BaseMuseumPanel : MonoBehaviour
{
    [Header("Museum Info")]
    [SerializeField, TextArea(2,2)] protected string _panelName;
    [SerializeField, TextArea(3,10)] protected string _historyDescription;
    
    [Header("Interectable Settings")]
    [SerializeField] protected bool _interectable = false;
    [SerializeField] protected Button _mainButton;


    public bool IsScenarioActive { get; protected set; }
    
    public string Name => _panelName;
    public string HistoryDescription => _historyDescription;
    
    public bool Interectable => _interectable;

    public event Action OnScenarioComplete;
    
    public virtual void ShowInfo()
    {
        Debug.Log($"Экспонат: {_panelName}. История: {_historyDescription}");
    }
    
    protected virtual void ResetScenario()
    {
        IsScenarioActive = false;
        OnScenarioComplete?.Invoke();
    }
    
    public abstract void ActivateScenario();
    
    protected abstract void OnButtonPressedLogic();

    protected virtual void OnEnable()
    {
        if (_mainButton != null) _mainButton.OnButtonPressed += OnButtonPressedLogic;
    }
    
    protected virtual void OnDisable()
    {
        if (_mainButton != null) _mainButton.OnButtonPressed -= OnButtonPressedLogic;
    }
}
