using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationManager : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonHolder;
    public ApperanceHolder apperanceHolder;

    [SerializeField] private LocalPlayer localPlayer;

    [SerializeField] private ColorPicker colorPicker;
    [SerializeField] private GameObject[] customizationButtons; // Für untermenü
    [SerializeField] private Texture2D deselectIcon;
    [SerializeField] private MeshRenderer[] categoryButtons;

    private bool init;

    private int selectedMenuIndex = 0;

    void Start()
    {
        init = true;
    }

    private void FixedUpdate()
    {
        if (init)
        {
            if (!localPlayer)
            {
                localPlayer = LocalPlayer.localPlayer;
            }
            else
            {
                SelectMenuButton(0);
                init = false;
            }
        }
    }

    public void SelectMenuButton(int id)
    {
        //if (localPlayer.GetComponent<NetworkIdentity>().isServer)
        //{
        //    //return;
        //}
        selectedMenuIndex = id;

        for (int i = 0; i < categoryButtons.Length; i++)
        {
            categoryButtons[i].material.SetFloat("_Selected", (i == selectedMenuIndex)?1:0);
        }

        int customizationNumber = localPlayer.apperance.customizations[id].customizations.Length;

        if (localPlayer.apperance.customizations[id].zeroIsDisabled)
        {
            customizationNumber++;
        }

        colorPicker.SetPresetButtons(localPlayer.apperance.customizations[id].presetColors);

        for (int i = 0; i < customizationButtons.Length; i++)
        {
            customizationButtons[i].SetActive(i < customizationNumber);
            if (i < customizationNumber)
            {
                Texture2D icon = localPlayer.apperance.getCustomizationIcon(id, i);
                if (icon == null)
                    icon = deselectIcon;

                MeshRenderer buttonRenderer = customizationButtons[i].transform.GetChild(0).GetComponent<MeshRenderer>();
                buttonRenderer.material.SetTexture("_MainTex", icon);
                buttonRenderer.material.SetTexture("_EmissionMap", icon);
            }
        }

        GetComponent<AudioSource>().Play();
    }

    public void onColorChanged(Color color)
    {
        if (!localPlayer) { return; }
        localPlayer.apperance.SetColor(selectedMenuIndex, color);
        GetComponent<AudioSource>().Play();

        apperanceHolder.SaveColor(localPlayer.apperance.GetColors());
    }

    public void UIPresetButtonColor(int id)
    {
        localPlayer.apperance.SetColor(selectedMenuIndex, colorPicker.presetButtons[id].transform.GetChild(0).GetComponent<MeshRenderer>().material.color);
        GetComponent<AudioSource>().Play();

        apperanceHolder.SaveColor(localPlayer.apperance.GetColors());
    }

    int currentButton = -1;
    public void SelectCustomization(int id)
    {
        if (currentButton == -1)
        {
            localPlayer.apperance.SetApperance(selectedMenuIndex, id);
            currentButton = id;

            apperanceHolder.SaveApperance(localPlayer.apperance.GetApperiances());
        }
        GetComponent<AudioSource>().Play();
    }

    public void UIReleaseButton(int id)
    {
        if (currentButton == id)
        {
            currentButton = -1;
        }
    }

    [ContextMenu("GenerateButtons")]
    void GenerateButtons()
    {
        customizationButtons = new GameObject[40]; // größe muss an anzahl angepasst werden
        for (int i = 0; i < 40; i++)
        {
            GameObject btn = Instantiate(buttonPrefab, buttonHolder);
            btn.name = "Button" + i;
            btn.transform.localPosition = new Vector3(0, -i%5 * 0.15F, (i / 5) * 0.15F);
            int tmp = i;
            btn.GetComponent<BoxButton>().Id = tmp;
            customizationButtons[i] = btn;
        }
    }
}