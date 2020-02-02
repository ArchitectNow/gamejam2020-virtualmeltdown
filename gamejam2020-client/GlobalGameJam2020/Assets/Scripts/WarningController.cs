using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningController : MonoBehaviour
{
    public GameManager gameManager;

    public Material offlineMaterial;

    public bool needsFixing = false;
    
    public int requiredRed;

    public int requiredYellow;

    public int requiredGreen;

    public int requiredBlue;

    public int leftColor;

    public int rightColor;

    public int midColor;

    public int delayBeforeDamage = 10;
    public int delayBeforeReduceDamage = 10;
    public int repeatDelay = 5;
    public int DamageAmount = 5;
    public int HealAmount = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        
        InvokeRepeating("doDamage", delayBeforeDamage, repeatDelay);
        InvokeRepeating("reduceHeat", delayBeforeReduceDamage, repeatDelay);
    }

    public void setColor(int order, int color)
    {
        switch (order)
        {
            case 0:
                leftColor = color;
                break;
            case 1:
                rightColor = color;
                break;
            case 2:
                midColor = color;
                break;
        }
    }

    private void reduceHeat()
    {
        if (gameManager.GetPlayerCount() > 0)
        {
            return;
        }
        
        if(requiredBlue == 0 && requiredGreen == 0 && requiredRed == 0 && requiredYellow == 0)
        {
            if (gameManager.totalHeat > 0)
            {
                gameManager.AdjustHeat(HealAmount);
            }
        }
    }

    private void doDamage()
    {
        if (gameManager.GetPlayerCount() > 0)
        {
            return;
        }
        
        if (requiredBlue > 0 || requiredGreen > 0 || requiredRed > 0 || requiredYellow > 0)
        {
            gameManager.AdjustHeat(-DamageAmount);
        }
    }

    public void applyFix(string color)
    {
        string warningObjectColor = this.gameObject.name;
        string warningColor = "Red";
        if (warningObjectColor == "redWarning")
        {
            warningColor = "Red";
        }
        if (warningObjectColor == "blueWarning")
        {
            warningColor = "Blue";
        }
        if (warningObjectColor == "greenWarning")
        {
            warningColor = "Green";
        }
        if (warningObjectColor == "yellowWarning")
        {
            warningColor = "Yellow";
        }
        switch (color)
        {
            case "red":
                if (requiredRed > 0)
                {
                    requiredRed -= 1; 
                }
                else
                {
                    gameManager.totalHeat += 1;
                }
                if (leftColor == 1)
                {
                    GameObject.Find($"left{warningColor}Warning").GetComponent<MeshRenderer>().material = offlineMaterial;
                    leftColor = 0;
                }

                if (rightColor == 1)
                {
                    GameObject.Find($"right{warningColor}Warning").GetComponent<MeshRenderer>().material = offlineMaterial;
                    rightColor = 0;
                }

                if (midColor == 1)
                {
                    GameObject.Find($"mid{warningColor}Warning").GetComponent<MeshRenderer>().material = offlineMaterial;
                    midColor = 0;
                }

                break;
            case "green":
                if (requiredGreen > 0)
                {
                    requiredGreen -= 1;
                }
                else
                {
                    gameManager.totalHeat += 1;
                }
                if (leftColor == 2)
                {
                    GameObject.Find($"left{warningColor}Warning").GetComponent<MeshRenderer>().material = offlineMaterial;
                    leftColor = 0;
                }

                if (rightColor == 2)
                {
                    GameObject.Find($"right{warningColor}Warning").GetComponent<MeshRenderer>().material = offlineMaterial;
                    rightColor = 0;
                }

                if (midColor == 2)
                {
                    GameObject.Find($"mid{warningColor}Warning").GetComponent<MeshRenderer>().material = offlineMaterial;
                    midColor = 0;
                }

                break;
            case "blue":
                if (requiredBlue > 0)
                {
                    requiredBlue -= 1;  
                }
                else
                {
                    gameManager.totalHeat += 1;
                }
                if (leftColor == 3)
                {
                    GameObject.Find($"left{warningColor}Warning").GetComponent<MeshRenderer>().material = offlineMaterial;
                    leftColor = 0;
                }

                if (rightColor == 3)
                {
                    GameObject.Find($"right{warningColor}Warning").GetComponent<MeshRenderer>().material = offlineMaterial;
                    rightColor = 0;
                }

                if (midColor == 3)
                {
                    GameObject.Find($"mid{warningColor}Warning").GetComponent<MeshRenderer>().material = offlineMaterial;
                    midColor = 0;
                }

                break;
            case "yellow":
                if (requiredYellow > 0)
                {
                    requiredYellow -= 1; 
                }
                else
                {
                    gameManager.totalHeat += 1;
                }
                if (leftColor == 4)
                {
                    GameObject.Find($"left{warningColor}Warning").GetComponent<MeshRenderer>().material = offlineMaterial;
                    leftColor = 0;
                }

                if (rightColor == 4)
                {
                    GameObject.Find($"right{warningColor}Warning").GetComponent<MeshRenderer>().material = offlineMaterial;
                    rightColor = 0;
                }

                if (midColor == 4)
                {
                    GameObject.Find($"mid{warningColor}Warning").GetComponent<MeshRenderer>().material = offlineMaterial;
                }

                break;
        }
    }
    
}
