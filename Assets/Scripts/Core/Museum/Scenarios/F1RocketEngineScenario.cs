using System.Collections;
using UnityEngine;

public class F1RocketEngineScenario : BaseMuseumPanel
{
    [Header("Audio")]
    [SerializeField] private AudioSource _engineSource;
    
    public override void ActivateScenario()
    {
        if (IsScenarioActive) return; 

        IsScenarioActive = true;

        if (_engineSource != null)
        {
            _engineSource.Play();
            StartCoroutine(WaitForEngineEnd());
        }
        else
        {
            Debug.LogWarning($"AudioSource missing on {gameObject.name}");
            ResetScenario();
        }
    }

    private IEnumerator WaitForEngineEnd()
    {
        yield return new WaitForEndOfFrame();

        while (_engineSource != null && _engineSource.isPlaying)
        {
            yield return null;
        }
        
        ResetScenario();
    }

    protected override void OnButtonPressedLogic()
    {
        ActivateScenario();
    }
}