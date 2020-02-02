using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class ClampName : MonoBehaviour
{
    public Text nameLabel;
    public GameObject clampSphere;
    
    private Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        var playerController = GetComponent<PlayerController>();

        if (playerController != null)
        {
            player = playerController.player;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main != null && nameLabel != null && player != null && clampSphere != null)
        {
            Vector3 namePos = Camera.main.WorldToScreenPoint(clampSphere.transform.position);
            nameLabel.transform.position = namePos;
            nameLabel.text = player.name;
            nameLabel.gameObject.SetActive(true);
        }
        else if (nameLabel != null)
        {
            nameLabel.gameObject.SetActive(false);
        }

    }
}
