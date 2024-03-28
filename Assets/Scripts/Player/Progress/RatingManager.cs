using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RatingManager
{
    [SerializeField] private int ratingLevel;
    public int currentRating;

    private readonly List<int> ratingForLevel = new() { 20, 40, 60, 80, 100 };
    public readonly List<int> ratingPoints = new() { 8, 5, 2, 0, -5 };

    public int GetRating()
    {
        return ratingLevel;
    }

    public void UpdateRating()
    {
        if (currentRating < 0)
            currentRating = 0;

        for (int i = 0; i < ratingForLevel.Count; i++)
        {
            if (currentRating >= ratingForLevel[i])
                ratingLevel = i + 1;
            else
                return;
        }
    }
}
