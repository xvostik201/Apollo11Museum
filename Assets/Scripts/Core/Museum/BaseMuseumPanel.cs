using UnityEngine;

public abstract class BaseMuseumPanel : MonoBehaviour
{
    [Header("Museum Info")]
    [SerializeField] protected string _panelName;
    [SerializeField, TextArea] protected string _historyDescription;
    
    [Header("Interectable Settings")]
    [SerializeField] protected Button _mainButton;

    public virtual void ShowInfo()
    {
        Debug.Log($"Экспонат: {_panelName}. История: {_historyDescription}");
    }

    public abstract void ActivateScenario();
    
    public abstract void OnButtonPressedLogic();

    protected virtual void OnEnable()
    {
        if (_mainButton != null) _mainButton.OnButtonPressed += OnButtonPressedLogic;
    }
    
    protected virtual void OnDisable()
    {
        if (_mainButton != null) _mainButton.OnButtonPressed -= OnButtonPressedLogic;
    }
}
