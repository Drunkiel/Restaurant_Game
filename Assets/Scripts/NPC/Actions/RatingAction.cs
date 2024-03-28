using System.Collections.Generic;

[System.Serializable]
public class RatingAction
{
    public int GiveRating(float waitingTime, int maxWaitingTime)
    {
        List<int> ratingPoints = ProgressMetricController.instance._ratingManager.ratingPoints;
        float percent = (waitingTime / maxWaitingTime) * 100;
        int points;

        if (percent >= 0 && percent <= 25) //100% -> 75%
            points = ratingPoints[0];
        else if (percent > 25 && percent <= 50) //75% -> 50%
            points = ratingPoints[1];
        else if (percent > 50 && percent <= 75) //50% -> 25%
            points = ratingPoints[2];
        else if (percent > 75 && percent <= 100) //25% -> 0%
            points = ratingPoints[3];
        else
            points = ratingPoints[4];

        SummaryController.instance.rating += points;
        return points;
    }
}
