using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineManager : Singleton<CinemachineManager>
{
    public CinemachineVirtualCamera playCam;
    public CinemachineVirtualCamera winCam;

    private int currentPriority;

    private void Start()
    {
        currentPriority = 10;
        SwitchToPlayCam();
    }

    public void SwitchToPlayCam()
    {
        playCam.Priority = currentPriority;
        currentPriority += 1;
    }

    public void SwitchToWinCam()
    {
        winCam.Priority = currentPriority;
        currentPriority += 1;
    }
}
