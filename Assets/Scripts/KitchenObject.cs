using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParents kitchenObjectParents;

    public KitchenObjectSO GetKitchenObjectSO() 
    { 
        return kitchenObjectSO; 
    }

    public void SetKitchenObjectParents(IKitchenObjectParents kitchenObjectParents)
    {
        if (this.kitchenObjectParents != null)
        {
            this.kitchenObjectParents.ClearKitchenObject();
        }
        this.kitchenObjectParents = kitchenObjectParents;

        if (kitchenObjectParents.HasKitchenObject())
        {
            Debug.Log("IKitchenObjectParents already has the kitchen object!");
        }

        kitchenObjectParents.SetKitchenObject(this);

        transform.parent = kitchenObjectParents.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParents GetKitchenObjectParents() { return kitchenObjectParents; }

    public void DestroySelf()
    {
        kitchenObjectParents.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParents kitchenObjectParents)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParents(kitchenObjectParents);

        return kitchenObject;
    }
}
