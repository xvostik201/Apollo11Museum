using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MuseumData")]
public class MuseumData : ScriptableObject
{
    [Header("Interactable name")]
    [SerializeField] private string _dataName = "Video of apollo";
    
    [Header("Data type")]
    [SerializeField] private VideoClip _videoClip;
    [SerializeField] private Image _image;
    
    public string DataName => _dataName;
    
    public VideoClip VideoClip => _videoClip;
    public Image Image => _image;
}
