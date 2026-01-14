using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class VideoDisplay : Display
{
    [Header("Settings")]
    [SerializeField] private VideoPlayer _videoPlayer;
    
    [Header("Video Settings")]
    [SerializeField] private VideoClip _noiseVideoClip;

    [Header("Text")]
    [SerializeField] private TMP_Text _cassetteEnterTextComponent;
    [SerializeField] private string _cassetteEnterText = "Press 'Play' button to play";
    
    [Header("Buttons")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _stopButton;
    
    private VideoClip _videoClip;

    protected override void Awake()
    {
        base.Awake();
        
        if(_videoPlayer == null)
            _videoPlayer = GetComponent<VideoPlayer>();
        
        _videoPlayer.clip = _noiseVideoClip;
        _videoPlayer.Play();
    }

    private void Start()
    {
        _cassetteEnterTextComponent.text = _cassetteEnterText;
        _cassetteEnterTextComponent.enabled = false;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _playButton.OnButtonPressed += PlayContent;
        _stopButton.OnButtonPressed += StopContent;
    }
    protected override void OnDisable()
    {
        base.OnDisable();

        _playButton.OnButtonPressed -= PlayContent;
        _stopButton.OnButtonPressed -= StopContent;
    }

    protected override void OnCassetteEnter(bool state)
    {
        base.OnCassetteEnter(state);

        if (state)
        {
            _videoPlayer.clip = _cassetteReceiver.GetCassetteVideoClip();
            _videoPlayer.Prepare();
            _cassetteEnterTextComponent.enabled = true;
        }
        else if (!state)
        {
            _videoPlayer.clip = _noiseVideoClip;
            _videoPlayer.Play();
        }
        else
        {
            Debug.LogWarning("Video clip is empty");
        }
    }

    public override void PlayContent()
    {
        if (!_canPressButtons) return;
        
        if (!_videoPlayer.isPlaying)
        {
            _cassetteEnterTextComponent.enabled = false;
            _videoPlayer.Play();
        }
    }

    public override void StopContent()
    {
        if (!_canPressButtons) return;

        if (_videoPlayer.isPlaying)
        {
            _videoPlayer.Stop();
        }
        else
        {
            _cassetteReceiver.RejectCassette();
        }
    }
}
