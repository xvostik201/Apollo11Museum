using UnityEngine;

public abstract class BaseMuseumPanel : MonoBehaviour
{
    [Header("Museum Info")]
    [SerializeField, TextArea(2,2)] protected string _panelName;
    [SerializeField, TextArea(3,10)] protected string _historyDescription;
    
    [Header("Interectable Settings")]
    [SerializeField] protected Button _mainButton;

    public virtual void ShowInfo()
    {
        Debug.Log($"Экспонат: {_panelName}. История: {_historyDescription}");
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
