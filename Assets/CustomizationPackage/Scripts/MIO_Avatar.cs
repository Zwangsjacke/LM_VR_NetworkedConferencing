using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIO_Avatar : MonoBehaviour
{
    [SerializeField] private Transform headTarget, leftHandTarget, rightHandTarget, feetTarget;
    public SkinnedMeshRenderer headSkin;

    private void Start()
    {
        feetTarget.parent = null;
    }

    public void SetCollider(bool islocalPlayer)
    {
        //GetComponent<RASCALSkinnedMeshCollider>().CleanUpMeshes();
        //GetComponent<RASCALSkinnedMeshCollider>().MenuGenerate();

        if (islocalPlayer)
        {
            SetLayerRecursivly(transform, LayerMask.NameToLayer("LocalPlayer"));
        }
    }

    private void SetLayerRecursivly(Transform t, int layer)
    {
        t.gameObject.layer = layer;
        for (int i = 0; i < t.childCount; i++)
        {
            SetLayerRecursivly(t.GetChild(i), layer);
        }
    }

    private void Update()
    {
        feetTarget.position = headTarget.position + Vector3.down * 3;
    }

    public void SetIKtargets(Transform head, Transform leftHand, Transform rightHand)
    {
        headTarget.parent = head;
        headTarget.localPosition = Vector3.zero;
        headTarget.localRotation = Quaternion.identity;

        leftHandTarget.parent = leftHand;
        leftHandTarget.localPosition = Vector3.zero;
        leftHandTarget.localRotation = Quaternion.identity;

        rightHandTarget.parent = rightHand;
        rightHandTarget.localPosition = Vector3.zero;
        rightHandTarget.localRotation = Quaternion.identity;
    }
}
