using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MainUI : NetworkBehaviour {

	public string messages;

	[SerializeField]
	private string inputMsg;
	[SerializeField]
	private string tmpName;

	private ChatPlayer player;

	// Use this for initialization
	void Start () {

	}
	
	public void SetChatPlayer(ChatPlayer p, string name)
	{
		player = p;
		tmpName = name;
	}

	public void AddNameAndMsg(string name, string msg)
	{
		string str = name + ": "+msg;

		if(isServer){
			RpcAddMsg(str);
		}
	}

	// server -> client
	[ClientRpc]
	void RpcAddMsg(string str)
	{
		messages += str + "\n";
	}

	[Command]
	void CmdTest(string str)
	{
		Debug.Log("CmdSendToServer: "+str);
		messages += str;
	}
	
	void OnGUI()
	{
		GUILayout.BeginArea(new Rect(300,10,400,600));
		GUILayout.TextArea(messages, GUILayout.Height(250));

		tmpName = GUILayout.TextField(tmpName);
		if( GUILayout.Button("ChangeName") ){
			player.ChangeName(tmpName);
		}
		inputMsg = GUILayout.TextField(inputMsg);
		if( GUILayout.Button("SEND") ){
			player.SendMsg(inputMsg);
			inputMsg = "";
		}
		
//		GUILayout.Label("NetworkServer.connections.Count: "+NetworkServer.connections.Count.ToString() );
//		GUILayout.Label("NetworkServer.localConnections.Count: "+NetworkServer.localConnections.Count.ToString() );
//		GUILayout.Label("NetworkClient.allClients.Count: "+NetworkClient.allClients.Count.ToString() );

		GUILayout.Space(20);
		GUILayout.Label("--- user List ----");
		
		ChatPlayer[] players = GameObject.FindObjectsOfType<ChatPlayer>();
		for(int i = 0; i < players.Length; i++){
			ChatPlayer p = players[i];
			GUILayout.Label(p.userName +" > isLocalPlayer: "+ p.isLocalPlayer + " / hasAuthority: "+p.hasAuthority);
		}

		if(isServer){
			// RPC fucntionはサーバーからのみ使用可
			if( GUILayout.Button("RPC_Test") ){
				RpcAddMsg("RPC_Test");
			}
		}

		GUILayout.EndArea();
	}

	public bool CheckSameName(string name)
	{
		ChatPlayer[] players = GameObject.FindObjectsOfType<ChatPlayer>();
		for(int i = 0; i < players.Length; i++){
			if(players[i].userName == name){
				return true;
			}
		}
		return false;
	}

	public override void OnNetworkDestroy()
	{
		// disconnect
		Debug.Log("MainUI OnNetworkDestroy");
		NetworkServer.DisconnectAll();
	}
}
