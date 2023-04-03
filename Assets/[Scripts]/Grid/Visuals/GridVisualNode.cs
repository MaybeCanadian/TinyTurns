using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class GridVisualNode : MonoBehaviour
{
    public SpriteRenderer sr;
    public GridNode connectedNode = null;

    #region Init Functions
    private void Start()
    {
        sr = gameObject.AddComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        if(connectedNode != null)
        {
            ConnectEvents();
        }
    }
    private void OnDisable()
    {
        if (connectedNode != null)
        {
            DisconnectEvents();
        } 
    }
    private void ConnectEvents()
    {
        if(connectedNode == null)
        {
            Debug.LogError("ERROR - Could not connect events as the connected grid node was null.");
            return;
        }

        connectedNode.OnGridTileTypeChanged += OnGridTileTypeChangedEvent;
    }
    private void DisconnectEvents()
    {
        if (connectedNode == null)
        {
            Debug.LogError("ERROR - Could not disconnect events as the connected grid node was null.");
            return;
        }

        connectedNode.OnGridTileTypeChanged -= OnGridTileTypeChangedEvent;
    }
    #endregion

    #region Node Control
    /// <summary>
    /// Connects the visual node to the given node. This must be called after the node is created. Needed for most functionality.
    /// </summary>
    /// <param name="node"></param>
    public void ConnectToGridNode(GridNode node)
    {
        connectedNode = node;

        ConnectEvents();
    }
    /// <summary>
    /// Moves the visual node to the world position stored in the connected node. Will fail if connected node is null.
    /// </summary>
    /// <param name="node"></param>
    public void MoveToGridPos()
    {
        if(connectedNode == null)
        {
            Debug.LogError("ERROR - Could not move to grid position as the connected grid node is null.");
            return;
        }

        transform.position = connectedNode.worldPos;
    }
    /// <summary>
    /// Sets the visuals of the tile to the given tile key found in the connected node. Will fail if no node is connected.
    /// </summary>
    public void SetTileVisuals()
    {
        if(connectedNode == null)
        {
            Debug.LogError("ERROR - Could not set tile visuals as connected node is null.");
            return;
        }

        Sprite sprite = GridVisuals.GetTileAsset(connectedNode.tileType);

        if(sprite == null)
        {
            Debug.LogError("ERROR - Could not set the tile visuals as given tile key returns null.");
            return;
        }

        sr.sprite = sprite;
    }
    #endregion

    #region Event Recievers
    private void OnGridTileTypeChangedEvent()
    {
        SetTileVisuals();
    }
    #endregion
}
