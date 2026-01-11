using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScenarioTrigger : MonoBehaviour
{
    [Header("Scenario")]
    [SerializeField] private BaseMuseumPanel _museumPanel;
    
    [Header("Activate on enter")] 
    [SerializeField] private GameObject[] _activateOnEnter;

    [Header("Text")]
    [SerializeField] private Transform _textParent;

    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _completeScenarioText;

    private bool _inTrigger = false;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
        _name.text = _museumPanel.Name;
        _description.text = _museumPanel.HistoryDescription;
        
        ToggleUI(false);

        if(_completeScenarioText != null)
            _completeScenarioText.alpha = 0f;
        
        enabled = false;
    }

    private void OnEnable()
    {
        _museumPanel.OnScenarioComplete += AnimateActiveText;
    }private void OnDisable()
    {
        _museumPanel.OnScenarioComplete -= AnimateActiveText;
    }

    private void Update()
    {
        if (_inTrigger)
        {
            Vector3 direction = _textParent.position - _mainCamera.transform.position;
            _textParent.rotation = Quaternion.LookRotation(direction);
        }
        
        if (_inTrigger && Input.GetKeyDown(KeyCode.E) && _museumPanel.Interectable)
        {
            if (!_museumPanel.IsScenarioActive)
            {
                _museumPanel.ActivateScenario();

                ToggleUI(false);
            }
            else
            {
                Debug.Log("Сценарий выполняется");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        enabled = true;
        
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player == null) return;
        
        _inTrigger = true;
        
        ToggleUI(true);
        
        Debug.Log($"Зашли в сценарий: {_museumPanel.Name}. Hажмите 'E', чтобы начать");
    }

    private void OnTriggerExit(Collider other)
    {
        enabled = false;
        
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player == null) return;
        
        _inTrigger = false;

        ToggleUI(false);
        
        Debug.Log($"Вышли из сценария: {_museumPanel.Name}");
    }

    private void ToggleUI(bool state)
    {
        _name.gameObject.SetActive(state);
        _description.gameObject.SetActive(state);
        if(_activateOnEnter != null)
        foreach (GameObject go in _activateOnEnter)
            go.SetActive(state);
    }

    private void AnimateActiveText()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(_completeScenarioText.DOFade(1f, 0.5f))
            .AppendInterval(2f)
            .Append(_completeScenarioText.DOFade(0f, 0.5f))
            .OnComplete(() =>
            {
                if (_inTrigger)
                {
                    ToggleUI(true);
                }
            });

    }
}
