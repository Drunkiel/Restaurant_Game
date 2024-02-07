using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform[] targets;
    [SerializeField] private List<Vector3> cameraToPlayer = new();
    [SerializeField] private List<Vector3> cameraToMap = new();
    private int state;

    [SerializeField] private CinemachineVirtualCamera _camera;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ChangeState(0);
    }

    public void ChangeCameraTarget(int i)
    {
        if (i > targets.Length)
        {
            Debug.LogError("There is no target on index: " + i);
            return;
        }

        _camera.m_LookAt = targets[i];
    }

    private void ChangeState(int i)
    {
        switch (i)
        {
            case 0:
                state += 1;
                if (state >= cameraToPlayer.Count) state = 0;
                _camera.transform.position = cameraToPlayer[state];
                break;

            case 1:
                state += 1;
                if (state >= cameraToMap.Count) state = 0;
                _camera.transform.position = cameraToMap[state];
                break;
        }
    }
}
