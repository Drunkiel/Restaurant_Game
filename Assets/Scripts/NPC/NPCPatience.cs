using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NPCPatience
{
    public float waitingTime;
    public readonly List<int> maxWaitingTime = new() { 45, 60, 75, 105, 150, 180 };
    public Slider waitingSlider;
    public GameObject review;
}
