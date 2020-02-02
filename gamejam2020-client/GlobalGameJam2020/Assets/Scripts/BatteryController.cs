using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : MonoBehaviour
{
    public string batteryColor;

    public GameObject warningObject;

    private WarningController fixMeController;
    
    // Start is called before the first frame update
    void Start()
    {
        if (warningObject != null)
        {
            fixMeController = warningObject.GetComponent<WarningController>();
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

}
