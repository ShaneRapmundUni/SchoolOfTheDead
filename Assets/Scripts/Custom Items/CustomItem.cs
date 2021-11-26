using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomItem : MonoBehaviour
{
    public string ItemName = "DefaultName";

    public void Start()
    {
        this.enabled = false;
    }
    public virtual void OnEnable()
    {

    }
    public virtual void OnDisable()
    {

    }

    public virtual void Update()
    {

    }
}
