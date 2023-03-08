using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TEMP
public class Launcher : MonoBehaviour, INetworkRunnerCallbacks
{
    public static Launcher Instance { get; private set; }

    [HideInInspector] public static int SelectedSkin;

    [SerializeField] private NetworkPrefabRef _playerPrefab;
    [SerializeField] private GameMaster _GM;
    [SerializeField] private WeaponSpawner _weapon;

    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();
    public static List<Transform> Chars = new List<Transform>();

    private NetworkRunner _runner;

    private void Awake()
    {
        _runner = GetComponent<NetworkRunner>();
        _spawnedCharacters.Clear();
        Chars.Clear();
        Instance = this;
    }

    public void Start()
    {
        LoadingScreen.EnableLoading(true);

        if (SessionInfo.isConnect)
        {
            JoinGame(SessionInfo.SessionName);
        }
        else
        {
            CreateGame(SessionInfo.SessionName);
        }
    }

    public async void CreateGame(string session)
    {
        _runner.ProvideInput = true;
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Host,
            SessionName = session,
            Scene = SceneManager.GetSceneAt(0).buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        }) ;
        LoadingScreen.EnableLoading(false);
        //_GM.StartGame();
    }

    public async void JoinGame(string session)
    {
        _runner.ProvideInput = true;
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Client,
            SessionName = session,
            Scene = SceneManager.GetSceneAt(0).buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
        LoadingScreen.EnableLoading(false);
    }

    public static void DisconnectAllServer()
    {
        foreach (var item in Chars)
        {
            if(item.TryGetComponent<Player>(out var data))
            {
                Instance.RPC_DisconnectPlayer(data);
            }
        }
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            // Create a unique position for the player
            Vector3 spawnPosition = Vector3.zero;
            NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
            // Keep track of the player avatars so we can remove it when they disconnect
            _spawnedCharacters.Add(player, networkPlayerObject);
            Chars.Add(networkPlayerObject.transform);

            _weapon.GivePlayerWeapon(networkPlayerObject.GetComponent<Player>());
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            _spawnedCharacters.Remove(player);
            Chars.Remove(networkObject.transform);
        }
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) 
    {
        DisconnectAllServer();
    }

    [Rpc]
    private void RPC_DisconnectPlayer(Player p)
    {
        p.OnDisconnect();
    }

    #region UNUSED
    public void OnInput(NetworkRunner runner, NetworkInput input) { }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }


    public void OnConnectedToServer(NetworkRunner runner) { }

    public void OnDisconnectedFromServer(NetworkRunner runner) { }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }

    public void OnSceneLoadDone(NetworkRunner runner) { }

    public void OnSceneLoadStart(NetworkRunner runner) { }

    public void OnSessionListUpdated(NetworkRunner runner, List<Fusion.SessionInfo> sessionList) { }
    #endregion
}
