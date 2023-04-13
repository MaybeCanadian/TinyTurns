using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    private void Awake()
    {
        ConnectEvents();
    }

    private void ConnectEvents()
    {
        GridNode.OnNodeMouseDown += OnNodeDown;
    }
    private void DisconnectEvents()
    {
        GridNode.OnNodeMouseDown -= OnNodeDown;
    }

    private void OnNodeDown(GridNode node, int button)
    {
        Debug.Log("test");
    }
}
