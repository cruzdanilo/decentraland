using DCL.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildModeState : MonoBehaviour
{

    [Header("Snap variables")]
    public float snapFactor = 1f;
    public float snapRotationDegresFactor = 15f;
    public float snapScaleFactor = 0.5f;

    public float snapDistanceToActivateMovement = 10f;

    public System.Action OnInputDone;

    protected GameObject gameObjectToEdit, undoGO, snapGO, freeMovementGO;

    protected bool isSnapActive = false, isMultiSelectionActive = false;
    protected List<DecentralandEntityToEdit> selectedEntities;
    public virtual void Init(GameObject _goToEdit,GameObject _undoGo,GameObject _snapGO,GameObject _freeMovementGO,List<DecentralandEntityToEdit> _selectedEntities)
    {
        gameObjectToEdit = _goToEdit;
        undoGO = _undoGo;
        snapGO = _snapGO;
        freeMovementGO = _freeMovementGO;

        selectedEntities = _selectedEntities;
        gameObject.SetActive(false);

    }
    public virtual void Activate(ParcelScene scene)
    {
        gameObject.SetActive(true);
    }

    public virtual void Desactivate()
    {
        gameObject.SetActive(false);
    }
    public virtual void SetSnapActive(bool isActive)
    {
        isSnapActive = isActive;
    }

    public virtual void StartMultiSelection()
    {
        isMultiSelectionActive = true;

    }

    public virtual void EndMultiSelection()
    {
        isMultiSelectionActive = false;

    }

    public virtual void SelectedEntity(DecentralandEntityToEdit selectedEntity)
    {
        foreach (DecentralandEntityToEdit entity in selectedEntities)
        {
            entity.rootEntity.gameObject.transform.SetParent(null);
        }
        gameObjectToEdit.transform.position = GetCenterPointOfSelectedObjects();
        gameObjectToEdit.transform.rotation = Quaternion.Euler(0, 0, 0);
        gameObjectToEdit.transform.localScale = Vector3.one;
        foreach (DecentralandEntityToEdit entity in selectedEntities)
        {
            entity.rootEntity.gameObject.transform.SetParent(gameObjectToEdit.transform);
        }

        BuildModeUtils.CopyGameObjectStatus(gameObjectToEdit, undoGO, false, false);
    }

    public virtual void CreatedEntity(DecentralandEntityToEdit createdEntity)
    {

    }
    public virtual void DeselectedEntities()
    {

    }
    public virtual void CheckInput()
    {

    }
    public virtual void CheckInputSelectedEntities()
    {

    }

    public virtual void InputDone()
    {
        OnInputDone?.Invoke();
    }

    public virtual void ResetScaleAndRotation()
    {
        gameObjectToEdit.transform.localScale = Vector3.one;
        snapGO.transform.localScale = Vector3.one;
        freeMovementGO.transform.localScale = Vector3.one;

        Quaternion zeroAnglesQuaternion = Quaternion.Euler(Vector3.zero);

        snapGO.transform.rotation = zeroAnglesQuaternion;
        freeMovementGO.transform.rotation = zeroAnglesQuaternion;
        gameObjectToEdit.transform.rotation = zeroAnglesQuaternion;

        foreach(DecentralandEntityToEdit decentralandEntityToEdit in selectedEntities)
        {
            decentralandEntityToEdit.rootEntity.gameObject.transform.eulerAngles = Vector3.zero;
        }

    }

    public virtual Vector3 GetCreatedEntityPoint()
    {
        return Vector3.zero;
    }

    protected Vector3 GetCenterPointOfSelectedObjects()
    {
        float totalX = 0f;
        float totalY = 0f;
        float totalZ = 0f;
        foreach (DecentralandEntityToEdit entity in selectedEntities)
        {
            totalX += entity.rootEntity.gameObject.transform.position.x;
            totalY += entity.rootEntity.gameObject.transform.position.y;
            totalZ += entity.rootEntity.gameObject.transform.position.z;
        }
        float centerX = totalX / selectedEntities.Count;
        float centerY = totalY / selectedEntities.Count;
        float centerZ = totalZ / selectedEntities.Count;
        return new Vector3(centerX, centerY, centerZ);
    }
}
