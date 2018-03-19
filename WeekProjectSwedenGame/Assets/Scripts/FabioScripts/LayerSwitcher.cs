using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSwitcher : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> m_LayerPositions;

    [SerializeField]
    private GameObject m_LayerParentPoint;

    private Vector3 m_CurrentLayer;

    private int m_LayerIndex;
    private int m_MovingToLayer;

    private float m_SwitchLayerValue;

    private bool m_SwitchingLayer;

    private void Start()
    {
        m_SwitchingLayer = false;
        m_SwitchLayerValue = 0;
    }

    private void Update()
    {
        if (!m_SwitchingLayer)
        {
            //Move layers down
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (m_LayerIndex >= 0 && m_LayerIndex <= 3)
                {
                    Debug.Log("Move Down");
                    SwitchLayer(-1);
                }
            }
            //Move layers up
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if (m_LayerIndex >= 0 && m_LayerIndex <= 3)
                {
                    SwitchLayer(1);
                }
            }
        }

        if (m_SwitchingLayer)
        {
            m_SwitchLayerValue += Time.deltaTime / 2f;
            if (m_SwitchLayerValue > 1f)
            {
                m_SwitchLayerValue = 0f;
                m_SwitchingLayer = false;
            }
            else
            {
                Transform layerTransform = m_LayerParentPoint.transform;
                layerTransform.position = Vector3.Lerp(m_CurrentLayer, m_LayerPositions[m_LayerIndex], m_SwitchLayerValue);
            }
        }
    }

    private void SwitchLayer(int upOrDown)
    {
        m_SwitchingLayer = true;
        m_CurrentLayer = m_LayerPositions[m_LayerIndex];
        m_LayerIndex += upOrDown;
    }

}
