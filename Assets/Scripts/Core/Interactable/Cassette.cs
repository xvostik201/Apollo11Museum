using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cassette : MonoBehaviour, IInteractable 
{
    [Header("Data")] 
    [SerializeField] private MuseumData _museumData;

    public string GetInteractText()
    {
        return _museumData.DataName;
    }

    public MuseumData GetMuseumData()
    {
        return _museumData;
    }
    
}
