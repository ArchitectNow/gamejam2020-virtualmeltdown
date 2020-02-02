using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colyseus;
using NativeWebSocket;
using UnityEngine;
using Random = System.Random;

public class ColyseusManager : MonoBehaviour
{

    private Client client = null;
    private string lastRoomId = "";
    
    public Room<GameState> room = null;

    // Start is called before the first frame update
    async Task Start()
    {

    }

    public void ConnectToServer()
    {
        try
        {
            var serverUrl = PlayerPrefs.GetString("serverUrl");

            if (string.IsNullOrEmpty(serverUrl))
            {
                PlayerPrefs.SetString("serverUrl", "ws://localhost:3000");
                serverUrl = "ws://localhost:3000";
            }

            // serverUrl = "ws://12c5439f.ngrok.io";
            
            client = new Colyseus.Client (serverUrl);
        }
        catch (Exception e)
        {
            Debug.Log("Error connecting to server: " + e.Message);
            client = null;
        }
    }

    private Dictionary<string, object> GetRoomOptions(GameRoomOptions options)
    {
        var optionsDictionary = new Dictionary<string, object>();
        
        if (options != null)
        {
            optionsDictionary.Add("isHost", options.isHost);
            optionsDictionary.Add("maxClients", options.maxClients);    
        }
        else
        {
            optionsDictionary.Add("isHost", true);
            optionsDictionary.Add("maxClients", 5);   
        }

        return optionsDictionary;
    }

    public string GetRoomId()
    {
        if (room == null || room.Id.Length != 9)
        {
            return "[empty]";
        }
        
        lastRoomId = room.Id.Substring(5, 4);
        return lastRoomId;
    }
    
    public async Task CreateRoom(string RoomName = "", GameRoomOptions options = null)
    {
        if (client == null)
        {
            Debug.Log("Not connected to server!");
            throw new Exception("Not connected to server");
        }
        
        room = await client.Create<GameState>(RoomName, GetRoomOptions(options));
        
        AttachRoomEvents();
        
        Debug.Log($"Created Room: " + GetRoomId());
    }

    private void AttachRoomEvents()
    {
        // room.State.OnChange += (x) => Debug.Log("Room State Changed: " + x);
        room.OnLeave += OnLeftRoom;
        room.OnError += (message) => Debug.LogError(message);
        room.OnStateChange += OnStateChangeHandler;
        room.OnMessage += OnMessageReceived;
        room.State.players.OnChange += OnPlayerUpdated;
        room.State.players.OnAdd += OnPlayerAdded;
        room.State.players.OnRemove += OnPlayerRemoved;
    }
    
    public async Task JoinOrCreateRoom(string RoomName = "", GameRoomOptions options = null)
    {
        if (client == null)
        {
            Debug.Log("Not connected to server!");
            throw new Exception("Not connected to server");
        }
        
        room = await client.JoinOrCreate<GameState>(RoomName, GetRoomOptions(options));
        
        Debug.Log($"Created or Joined Room [{RoomName}]!");
        
        AttachRoomEvents();
    }
    
    public async Task Reconnect()
    {
        if (client == null)
        {
            Debug.Log("Not connected to server!");
            throw new Exception("Not connected to server");
        }
        
        room = await client.JoinById<GameState>(lastRoomId);
        
        Debug.Log($"Reconnected to room [{lastRoomId}]!");
        
        AttachRoomEvents();
    }

    public async void JoinRoom(string RoomName = "", GameRoomOptions options = null)
    {
        room = await client.Join<GameState>(RoomName, GetRoomOptions(options));
        
        Debug.Log($"Joined Room [{RoomName}]!");
        
        AttachRoomEvents();
    }

    public void Send(object Data)
    {
        if (room != null)
        {
            room.Send(Data);
        }
        else
        {
            Debug.Log("Room is not connected!");
        }
    }

    public async void LeaveRoom()
    {
        if (room != null)
        {
            await room.Leave(false);

            room = null;
            
            Debug.Log($"Left room!");
        }
    }
    
    public event ColyseusCloseEventHandler LeftRoom;

    private void OnLeftRoom(WebSocketCloseCode CloseCode)
    {
        if (LeftRoom != null)
        {
            LeftRoom(CloseCode);
            
            room = null;
            
            Debug.Log($"Left room!");
        }
    }
    
    public event EventHandler<Player> PlayerAdded;

    void OnPlayerAdded(Player Player, string Data)
    {
        if (PlayerAdded != null)
        {
            PlayerAdded(this, Player);
        }
    }
    
    public event EventHandler<Player> PlayerRemoved;
    
    void OnPlayerRemoved(Player Player, string Data)
    {
        if (PlayerRemoved != null)
        {
            PlayerRemoved(this, Player);
        }
    }

    public event EventHandler<Player> PlayerUpdated;
    
    void OnPlayerUpdated(Player Player, string Data)
    {
        if (PlayerUpdated != null)
        {
            PlayerUpdated(this, Player);
        }
    }

    public EventHandler<object> MessageReceived; 

    void OnMessageReceived(object msg)
    {
        if (MessageReceived != null)
        {
            MessageReceived(this, msg);
        }
    }
    
    void OnStateChangeHandler (GameState state, bool isFirstState)
    {
        if (StateChanged != null)
        {
            StateChanged(this, state);
        }
    }

    public event EventHandler<GameState> StateChanged;


}
