using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EventHelper;
using UnityEngine.UI;

public class BoxButton : MonoBehaviour
{
    [SerializeField] private UnityIntEvent _onButtonClick = new UnityIntEvent();
    [SerializeField] private UnityIntEvent _onButtonRelease = new UnityIntEvent();
    [SerializeField] private bool changeMaterialSelectedState;

    private bool _lock;
    private MeshRenderer _meshRenderer;

    public UnityIntEvent OnButtonClick
    {
        get { return _onButtonClick; }
        set { _onButtonClick = value; }
    }

    public UnityIntEvent OnButtonRelease
    {
        get { return _onButtonRelease; }
        set { _onButtonRelease = value; }
    }

    public int Id;

    private void OnTriggerEnter(Collider other)
    {
        if (!_lock)
        {
            if (other.gameObject.tag == "Finger")
            {
                
                Button b = GetComponent<Button>();
                if (b)
                {
                    GetComponent<Button>().interactable = false;
                }

                if (changeMaterialSelectedState)
                {
                    GetComponent<MeshRenderer>().material.SetFloat("_Selected", 1);
                }

                _lock = true;
                OnButtonClick.Invoke(Id);
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        _lock = false;
        Button b = GetComponent<Button>();

        if (b != null)
        {
            GetComponent<Button>().interactable = true;
        }

        if (changeMaterialSelectedState)
        {
            GetComponent<MeshRenderer>().material.SetFloat("_Selected", 0);
        }

        OnButtonRelease.Invoke(Id);
    }


    public void ToggleHighlight(bool on)
    {
        if (on)
        {
            _meshRenderer.material.SetFloat("_Selected", 1);
        }
        else
        {
            _meshRenderer.material.SetFloat("_Selected", 0);
        }
    }

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
}
