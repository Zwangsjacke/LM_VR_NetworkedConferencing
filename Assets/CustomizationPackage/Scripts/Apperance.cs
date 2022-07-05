using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Apperance : MonoBehaviour
{
    [SerializeField] private GameObject[] armBoneDisplayObjects;
    public TextMesh armLengthText;
    [Header("Bei Körpermeshed 2 renders plätze frei lassen für die Hände. Werden automatisch zugewiesen Customization")]
    public AvatarCustomizations[] customizations;
    public SkinnedMeshRenderer[] arms;

    public SkinnedMeshRenderer headSkinnedMesh, eyeLidSkinnedMesh;

    private void Update()
    {
        eyeLidSkinnedMesh.SetBlendShapeWeight(0, headSkinnedMesh.GetBlendShapeWeight(1));
    }

    public void SetArmBoneDisplay(bool enabled)
    {
        for (int i = 0; i < armBoneDisplayObjects.Length; i++)
        {
            armBoneDisplayObjects[i].SetActive(enabled);
        }
    }

    public void SetArms(MeshRendererGroup.armState armstate)
    {
        if (armstate != MeshRendererGroup.armState.ignore)
        {
            for (int i = 0; i < arms.Length; i++)
            {
                arms[i].enabled = armstate == MeshRendererGroup.armState.enable;
            }
        }
    }

    public void SetApperance(int id, int value)
    {
        MeshRendererGroup.armState arms = customizations[id].Set(value);
        if (id == 5)
        {
            SetArms(arms);
        }
    }

    public int[] GetApperiances()
    {
        int[] apperances = new int[customizations.Length];
        for (int i = 0; i < customizations.Length; i++)
        {
            apperances[i] = customizations[i].GetEnabledIndex();
        }

        return apperances;
    }

    public Color[] GetColors()
    {
        Color[] colors = new Color[customizations.Length];
        for (int i = 0; i < customizations.Length; i++)
        {
            colors[i] = customizations[i].GetColor();
        }

        return colors;
    }

    public void SetColor(int id, Color c)
    {
        customizations[id].Set(c);
    }

    public void AddHands(SkinnedMeshRenderer leftHand, SkinnedMeshRenderer rightHand, int index)
    {
        leftHand.material = customizations[4].customizations[0].colorRenderers[0].material;
        rightHand.material = customizations[4].customizations[0].colorRenderers[0].material;

        int length = customizations[4].customizations[0].colorRenderers.Length;
        customizations[4].customizations[0].colorRenderers[length - index] = leftHand;
        customizations[4].customizations[0].colorRenderers[length - (index+1)] = rightHand;
    }

    public Texture2D getCustomizationIcon(int type, int id)
    {
        if (type < customizations.Length)
        {
            if (id < customizations[type].customizations.Length)
            {
                return customizations[type].customizations[id].icon;
            }
            else
            {
                if (customizations[type].zeroIsDisabled)
                {
                    return null;
                }
            }
        }

        return null;
    }

    [System.Serializable]
    public struct AvatarCustomizations
    {
        public string name;
        [Header("Settings")]
        public bool zeroIsDisabled;
        public Color[] presetColors;
        [Header("Renderers for customization")]
        public MeshRendererGroup[] customizations;

        public MeshRendererGroup.armState Set(int id) // returns if arms should be enabled
        {
            if (!zeroIsDisabled && id == -1)
                id = 0;

            if (zeroIsDisabled && id >= customizations.Length)
            {
                id = -1;
            }

            MeshRendererGroup.armState arms = MeshRendererGroup.armState.ignore;
            for (int i = 0; i < customizations.Length; i++)
            {
                if (id != i)
                {
                    if((zeroIsDisabled && id == -1) || !zeroIsDisabled || (zeroIsDisabled && id!=i))
                        customizations[i].Set(false);
                }
            }

            if (id!=-1)
                arms = customizations[id].Set(true);

            return arms;
        }

        public int GetEnabledIndex()
        {
            for (int i = 0; i < customizations.Length; i++)
            {
                if (customizations[i].GetEnabled())
                {
                    return i;
                }
            }
            return -1;
        }

        public void Set(Color c)
        {
            for (int i = 0; i < customizations.Length; i++)
            {
                customizations[i].Set(c);
            }
        }

        public Color GetColor()
        {
            return customizations[0].GetColor();
        }
    }

    [System.Serializable]
    public class MeshRendererGroup
    {
        public Texture2D icon;
        public armState targetArmState;
        public Renderer[] justEnableRenderers;
        public Renderer[] colorRenderers;

        public enum armState
        { 
        ignore,
        enable,
        disable
        }

        public armState Set(bool active) // returns if arms should be enabled
        {
            for (int i = 0; i < justEnableRenderers.Length; i++)
            {
                if(justEnableRenderers[i])
                    justEnableRenderers[i].enabled = active;
            }

            for (int i = 0; i < colorRenderers.Length; i++)
            {
                if(colorRenderers[i])
                    colorRenderers[i].enabled = active;
            }

            if (active)
            {
                return targetArmState;
            }
            return armState.ignore;
        }

        public bool GetEnabled()
        {
            if (colorRenderers.Length == 0) {
                return false;
            }
            if (colorRenderers[0] != null)
            {
                return colorRenderers[0].enabled;
            }
            else
            {
                return true;
            }
        }

        public void Set(Color c)
        {
            for (int i = 0; i < colorRenderers.Length; i++)
            {
                if (colorRenderers[i])
                {
                    colorRenderers[i].material.color = c;

                    for (int a = 0; a < colorRenderers[i].materials.Length; a++)
                    {
                        colorRenderers[i].materials[a].color = c;
                    }
                }
            }
        }

        public Color GetColor()
        {
            if (colorRenderers.Length > 0)
            {
                if (colorRenderers[0] != null)
                {
                    return colorRenderers[0].materials[0].color;
                }
            }
            return Color.magenta;
        }
    }
}
