using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [SerializeField] private Transform[] targets;
    [HideInInspector] public int currentTarget = 0;
    public int state;
    [SerializeField] private List<Vector3> cameraToPlayer = new();
    [SerializeField] private List<Vector3> cameraToMap = new();
    private Transform player;

    [SerializeField] private CinemachineVirtualCamera _camera;

    private void Awake()
    {
        player = ProgressMetricController.instance.transform;
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
        _camera.transform.position = player.position + cameraToPlayer[state];
    }

    public void ChangeCameraTarget(int i)
    {
        if (i > targets.Length)
        {
            Debug.LogError("There is no target on index: " + i);
            return;
        }

        currentTarget = i;
        ChangeState(currentTarget);
        _camera.m_LookAt = targets[currentTarget];
    }

    private void ChangeState(int i)
    {
        switch (i)
        {
            case 0:
                state += 1;
                if (state >= cameraToPlayer.Count) state = 0;
                UpdateCameraPosition();
                break;

            case 1:
                state += 1;
                if (state >= cameraToMap.Count) state = 0;
                UpdateCameraPosition();
                break;
        }

        RestaurantManager.instance.ChangeWallsVisibility();
    }
}
