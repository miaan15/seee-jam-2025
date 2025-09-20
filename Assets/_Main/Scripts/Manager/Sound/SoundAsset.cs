using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundAsset", menuName = "Data/SoundAsset")]
public class SoundAsset : ScriptableObject
{
    [SerializeField]
    public List<SoundData> Sounds;
}