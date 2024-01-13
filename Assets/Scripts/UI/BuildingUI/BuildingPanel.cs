using System.Collections.Generic;
using UnityEngine;

public class BuildingPanel : MonoBehaviour
{
    [SerializeField] private BuildingObjectData _objectsData;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject cardPrefab;

    public void SpawnCards(int i)
    {
        List<BuildingID> list = GetList(i);

        for (int j = 0; j < list.Count; j++)
        {
            GameObject newCard = Instantiate(cardPrefab, content);
            newCard.GetComponent<BuildingCard>().SetData(list[j]);
        }
    }

    private List<BuildingID> GetList(int i)
    {
        return i switch
        {
            0 => _objectsData._furnituresIDList,
            1 => _objectsData._wallsIDList,
            2 => _objectsData._floorsIDList,
            3 => _objectsData._decorationsIDList,
            _ => new(),
        };
    }
}
