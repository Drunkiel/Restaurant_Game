using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform[] targets;
    [SerializeField] private Vector3[] relativePositions;

    [SerializeField] private CinemachineVirtualCamera _camera;

    public void ChangeCameraTarget(int i)
    {
        if (i > targets.Length)
        {
            Debug.LogError("There is no target on index: " + i);
            return;
        }

        _camera.m_LookAt = targets[i];
        _camera.transform.position = relativePositions[i];
    }
}
