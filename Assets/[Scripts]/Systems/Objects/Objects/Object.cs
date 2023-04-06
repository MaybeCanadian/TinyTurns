using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Object
{
    #region Event Dispatchers
    public delegate void ObjectEvent();
    public ObjectEvent OnObjectMoved;
    public ObjectEvent OnObjectRemoved;
    #endregion

    public Vector3 worldPos = Vector3.zero;
    public Vector2Int gridPos = Vector2Int.zero;

    public GridNode currentGridNode = null;

    public ObjectData data = null;
    public GameObject objectOBJ = null;

    #region Init Functions
    public Object(ObjectData data)
    {
        this.data = data;

        //ConnectEvents(); Base Object doesn't need events

        //Debug.Log("Created Object");
    }
    protected void ConnectEvents()
    {
        GameController.OnUpdate += Update;
        GameController.OnFixedUpdate += FixedUpdate;
        GameController.OnLateUpdate += LateUpdate;
    }
    protected void DisconnectEvents()
    {
        GameController.OnUpdate -= Update;
        GameController.OnFixedUpdate -= FixedUpdate;
        GameController.OnLateUpdate -= LateUpdate;
    }
    #endregion

    #region Update Functions
    protected virtual void Update(float delta) { }
    protected virtual void FixedUpdate(float fixedDelta) { }
    protected virtual void LateUpdate(float delta) { }
    #endregion

    #region Movement
    public void PlaceObjectAtGridPos(Vector2Int gridPos) 
    {
        this.gridPos = gridPos;

        currentGridNode = GridManager.GetGridNode(gridPos.x, gridPos.y);

        if(!GridManager.GetWorldPosFromGridPos(gridPos, out this.worldPos))
        {

        }

        if(objectOBJ != null)
        {
            MoveObjToPosition();
        }
    }
    #endregion

    #region Visuals
    public void CreateVisuals()
    {
        GameObject model = EntityModelDataBase.GetModel(data.entityModel);

        if(model == null)
        {
            Debug.LogError("ERROR - Could not make object visuals as the model returned null.");
            return;
        }

        objectOBJ = GameObject.Instantiate(model);

        if(objectOBJ == null)
        {
            Debug.LogError("ERROR - Could not create visuals as object visuals did not instantiate.");
            return;
        }

        ObjectManager.ConnectParent(objectOBJ);
        objectOBJ.name = data.ObjectName;

        MoveObjToPosition();
    }
    public void DestroyVisuals() 
    {
        if(objectOBJ == null)
        {
            return;
        }

        GameObject.Destroy(objectOBJ);
        objectOBJ = null;
    }
    protected void MoveObjToPosition()
    {
        if(objectOBJ == null)
        {
            Debug.LogError("ERROR - Could not move object visuals to position as the object visuals is null.");
            return;
        }

        objectOBJ.transform.position = worldPos;
    }
    #endregion

    #region Lifecycle
    public void DestroyObject()
    {
        DestroyVisuals();
        OnObjectRemoved?.Invoke();
    }
    #endregion
}
