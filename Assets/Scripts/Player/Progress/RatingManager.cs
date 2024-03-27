using System.Collections.Generic;

[System.Serializable]
public class RatingManager
{
    private int ratingLevel;
    public int currentRating;

    private List<int> ratingForLevel = new() { 20, 40, 60, 80, 100 };

    public int GetRating()
    {
        return ratingLevel;
    }

    public void UpdateRating()
    {
        for (int i = 0; i < ratingForLevel.Count; i++)
        {
            if (currentRating >= ratingForLevel[i])
                ratingLevel = i + 1;
            else
                return;
        }
    }
}
