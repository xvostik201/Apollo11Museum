using UnityEngine;

public abstract class Display : MonoBehaviour
{
    [Header("Display Settings")] 
    [SerializeField] protected CassetteReceiver _cassetteReceiver;

    protected bool _canPressButtons = false;

    protected virtual void Awake()
    {
    }

    protected virtual void OnEnable()
    {
        _cassetteReceiver.OnCassetteChanged += OnCassetteChanged;
        _cassetteReceiver.OnCassetteEnter += OnCassetteEnter;
    }

    protected virtual void OnDisable()
    {
        _cassetteReceiver.OnCassetteChanged -= OnCassetteChanged;
        _cassetteReceiver.OnCassetteEnter -= OnCassetteEnter;
    }

    protected virtual void OnCassetteChanged(bool state)
    {
        
    }
    
    protected virtual void OnCassetteEnter(bool state)
    {
        _canPressButtons = state;
    }

    public abstract void PlayContent();
    public abstract void StopContent();
}

