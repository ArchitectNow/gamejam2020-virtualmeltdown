using System;
using System.Collections.Generic;
using GameDevWare.Serialization;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class PlayerController : MonoBehaviour
    {
        private ColyseusManager colyseusManager;
        private GameManager gameManager;
        private AudioManager audioManager;
        
        public Player player;
        public int speed = 10;
        public int force = 70;
        private Vector3? lastMove;
        private Rigidbody rb;
        public Animator animator;
        private string nearbyBattery;
        
        public void Start()
        {
            colyseusManager = GameObject.FindObjectOfType<ColyseusManager>();
            gameManager = GameObject.FindObjectOfType<GameManager>();
            audioManager = GameObject.FindObjectOfType<AudioManager>();

            animator = this.GetComponentInChildren<Animator>();

            rb = GetComponentInChildren<Rigidbody>();

            if (animator == null)
            {
                Debug.Log("Animator not found");
            }
            else
            {
                animator.SetFloat("speed", 0.0f);
            }
            
            colyseusManager.PlayerUpdated += OnPlayerUpdated;
            colyseusManager.MessageReceived += (sender, o) =>
            {
                if (o is IndexedDictionary<string, object>)
                {
                    var data = (IndexedDictionary<string, object>) o;

                    var playerId = "";

                    if (data.ContainsKey("playerId"))
                    {
                        playerId = data["playerId"].ToString();
                    }

                    if (player.id == playerId)
                    {
                        if (data.ContainsKey("eventType") && data["eventType"].ToString() == "deposit")
                        {
                            gameManager.Deposit(data["color"].ToString(),1);
                            
                            audioManager.PlaySoundEffect(SoundEffectType.Deposit);
                        }
                    }
                }
                
                Debug.Log("Received message");

            };
        }
        private void OnPlayerUpdated(object sender, Player args)
        {
            if (player.name == args.name)
            {
                if (player.horizontal != 0 || player.vertical != 0)
                {
                    lastMove = new Vector3(player.vertical * force,
                        0, player.horizontal * force);
                }
                else
                {
                    lastMove = null;
                }
            }
            else
            {
                // Debug.Log("Listened to event for another player");
            }
        }

        public void Update()
        {
            if (lastMove.HasValue)
            {
                // this.transform.position = new Vector3(lastMove.Value.x * speed * Time.deltaTime, 0,
                //     lastMove.Value.y * speed * Time.deltaTime);

                rb.AddForce(lastMove.Value * speed * Time.deltaTime);
                
                // transform.Translate(lastMove.Value  * speed * Time.deltaTime, Space.World);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lastMove.Value), 0.15F);
                
                if (animator != null)
                {
                    animator.SetFloat("speed", lastMove.Value.magnitude);
                }

                lastMove = null;
            }
            else
            {
                var xLocal = Input.GetAxis("Horizontal");
                var yLocal = Input.GetAxis("Vertical");

                xLocal *= force;
                yLocal *= force;
                if (xLocal == 0.0f && yLocal == 0.0f)
                {
                    animator.SetFloat("speed", 0.0f);
                    return;
                }
                var move =  new Vector3(xLocal,
                    0, yLocal);
                
                rb.AddForce(move * speed * Time.deltaTime);
                
                // this.transform.position = new Vector3(xLocal * speed * Time.deltaTime, 0,
                //     yLocal * speed * Time.deltaTime);

                
                // transform.Translate(lastMove.Value  * speed * Time.deltaTime, Space.World);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), 0.15F);

                if (animator != null)
                {
                    animator.SetFloat("speed", move.magnitude);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var battery = other.gameObject.GetComponent<BatteryController>();

            if (battery != null)
            {
                Debug.Log($"Player: " + player.name + " left the " + battery.batteryColor + " battery.");

                nearbyBattery = "";
                
                var outOfRangeMessage = new OutOfRangeMessage();

                outOfRangeMessage.username = player.name;
                outOfRangeMessage.playerId = player.id;
                outOfRangeMessage.eventType = "outOfRange";
                
                colyseusManager.Send(outOfRangeMessage);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var pickup = other.gameObject.GetComponent<PickupController>();

            if (pickup != null)
            {
                var lootMessage = new LootMessage();
                lootMessage.loot.blue = pickup.TotalBlue;
                lootMessage.loot.green = pickup.TotalGreen;
                lootMessage.loot.red = pickup.TotalRed;
                lootMessage.loot.yellow = pickup.TotalYellow;
                lootMessage.eventType = "loot";
                lootMessage.username = player.name;
                lootMessage.playerId = player.id;
                
                colyseusManager.Send(lootMessage);

                Debug.Log("Pickup complete!");

                audioManager.PlaySoundEffect(SoundEffectType.PickupSound);
                
                Destroy(other.gameObject);

                return;
            }

            var battery = other.gameObject.GetComponent<BatteryController>();

            if (battery != null)
            {
                Debug.Log($"Player: " + player.name + " approached the " + battery.batteryColor + " battery.");

                nearbyBattery = battery.batteryColor;
                
                var inRangeMessage = new InRangeMessage();
                inRangeMessage.color = nearbyBattery;
                inRangeMessage.username = player.name;
                inRangeMessage.playerId = player.id;
                inRangeMessage.eventType = "inRange";
                
                colyseusManager.Send(inRangeMessage);
            }
        }
    }
}