using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "Objects/Object")]
public class ObjectData : ScriptableObject
{
    [Header("Superficial name of the object")]
    public string ObjectName = "Name";

    [Tooltip("The model to be shown for this object")]
    public EntityList entityModel = EntityList.NULL;

    public ObjectBlockingStruct objectBlocking;
}
