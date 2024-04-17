using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class NPCPatience
{
    public float waitingTime;
    public readonly List<int> maxWaitingTime = new() { 15, 20, 25, 35, 50, 60 };
    public Slider waitingSlider;
}
