using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RatingManager
{
    [SerializeField] private int ratingLevel;
    public int currentRating;

    private readonly List<int> ratingForLevel = new() { 0, 100, 400, 1200, 2000, 8000 };
    public readonly List<int> ratingPoints = new() { 10, 5, 2, 0, -5 };

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
                ratingLevel = i;
            else
                break;
        }
    }
}
