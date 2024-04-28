using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [SerializeField] private Transform[] targets;
    public int currentTarget = 0;
    public int playerState;
    public int cameraState;
    [SerializeField] private List<Vector3> cameraToPlayer = new();
    [SerializeField] private List<Vector3> cameraToMap = new();

    [SerializeField] private CinemachineVirtualCamera _camera;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ChangeState(currentTarget);

        UpdateCameraPosition();
    }

    public void UpdateCameraPosition()
    {
        _camera.transform.position = targets[currentTarget].position + (currentTarget == 0 ? cameraToPlayer[playerState] : cameraToMap[cameraState]);
    }

    public void ChangeCameraTarget(int i)
    {
        if (i > targets.Length)
        {
            Debug.LogError("There is no target on index: " + i);
            return;
        }

        currentTarget = i;
        ChangeState(currentTarget + 1);

        _camera.m_LookAt = targets[currentTarget];
    }

    private void ChangeState(int i)
    {
        switch (i)
        {
            case 0:
                playerState++;
                if (playerState >= cameraToPlayer.Count)
                    playerState = 0;
                break;

            case 1:
                cameraState++;
                if (cameraState >= cameraToMap.Count)
                    cameraState = 0;
                break;
        }

        RestaurantManager.instance.ChangeWallsVisibility();
    }
}
