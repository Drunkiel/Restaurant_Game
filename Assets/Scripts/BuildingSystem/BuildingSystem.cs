using UnityEngine;
using UnityEngine.UI;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem instance;
    public static bool inBuildingMode;

    [SerializeField] private Grid grid;
    public Vector2 mapSize;

    public GameObject buildingMaterial;
    [SerializeField] private Material[] materials;

    [SerializeField] private GameObject UI;
   // public BuildingUI _buildingUI;
    public PlacableObject _objectToPlace;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!_objectToPlace) return;

        ChangeMaterial(CanBePlaced());
        if (Input.GetKeyDown(KeyCode.R)) _objectToPlace.Rotate();

/*        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaceButton();
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) Destroy(_objectToPlace.gameObject);*/
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) return hit.point;
        return hit.point;
        //else return Vector3.zero;
    }

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPosition = grid.WorldToCell(position);

        //Checking if object is out of bounds
        if (position.x > mapSize.x ||
            position.x < -mapSize.x ||
            position.z > mapSize.y ||
            position.z < -mapSize.y
            ) return SnapCoordinateToGrid(Vector3.zero);

        position = grid.GetCellCenterWorld(cellPosition);
        return position;
    }

    public void InitializeWithObject(GameObject prefab)
    {
        if (inBuildingMode) return;

        inBuildingMode = true;
        Vector3 position = SnapCoordinateToGrid(Vector3.zero);

        GameObject newObject = Instantiate(prefab, position, Quaternion.identity);
        _objectToPlace = newObject.GetComponent<PlacableObject>();
        Instantiate(buildingMaterial, newObject.transform);
        newObject.AddComponent<ObjectDrag>();

        OpenUI(true);
    }

    public void OpenUI(bool destroy)
    {
        //UI
        UI.SetActive(true);

        //Removing listeners
        UI.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        UI.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();

        //Adding new listeners
        UI.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => PlaceButton());

        if (destroy) UI.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { Destroy(_objectToPlace.gameObject); UI.SetActive(false); inBuildingMode = false; });
        else
        {
            Vector3 oldPosition = _objectToPlace.transform.position;
            UI.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { _objectToPlace.transform.position = oldPosition; PlaceButton(); });
        }
    }

    public void PlaceButton()
    {
        if (CanBePlaced()) _objectToPlace.Place();
        else Destroy(_objectToPlace.gameObject);
        UI.SetActive(false);
        inBuildingMode = false;
    }

    private bool CanBePlaced()
    {
        if (_objectToPlace == null) return false;
        // return !_objectToPlace.transform.GetChild(1).GetComponent<MultiTriggerController>().isTriggered;
        return false;
    }

    private void ChangeMaterial(bool itCanBePlaced)
    {
        MeshRenderer meshRenderer = _objectToPlace.transform.GetChild(_objectToPlace.transform.childCount - 1).GetComponent<MeshRenderer>();
        meshRenderer.material = itCanBePlaced ? materials[0] : materials[1];
    }
}
