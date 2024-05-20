using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using Cinemachine;

public class Zoom : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera camera;
    [SerializeField] float minZoom=5, maxZoom=7;

    private void Awake()
    {
        camera.m_Lens.OrthographicSize = 5.4f;
    }

    void Update()
    {

        if (Input.GetAxis("Mouse ScrollWheel") > 0 &&
            camera.m_Lens.OrthographicSize > minZoom && camera.m_Lens.OrthographicSize < maxZoom)
        {
            camera.m_Lens.OrthographicSize -= .5f;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 &&
            camera.m_Lens.OrthographicSize > minZoom && camera.m_Lens.OrthographicSize < maxZoom)
        {
            camera.m_Lens.OrthographicSize += .5f;
        }

        if(camera.m_Lens.OrthographicSize <= minZoom)
        {
            camera.m_Lens.OrthographicSize = minZoom+0.1f;
        }
        if (camera.m_Lens.OrthographicSize >= maxZoom)
        {
            camera.m_Lens.OrthographicSize = maxZoom-0.1f;
        }
    }
}