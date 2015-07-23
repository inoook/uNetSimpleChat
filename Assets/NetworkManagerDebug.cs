using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkManagerDebug : NetworkManager {

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		Debug.Log(">> OnServerAddPlayer: "+conn + " / "+playerControllerId);
		base.OnServerAddPlayer(conn, playerControllerId);
	}

	public override void OnClientConnect(NetworkConnection conn)
	{
		Debug.Log(">> OnClientConnect: "+conn);
		base.OnClientConnect(conn);
	}

	public override void OnClientDisconnect(NetworkConnection conn)
	{
		Debug.Log(">> OnClientDisconnect: "+conn);
		base.OnClientDisconnect(conn);
	}
	
	public override void OnServerDisconnect(NetworkConnection conn)
	{
		Debug.Log(">> OnServerDisconnect: "+conn);
		base.OnServerDisconnect(conn);
	}

	public override void OnClientError(NetworkConnection conn, int errorCode)
	{
		Debug.Log(">> OnClientError: "+conn);
		base.OnClientError(conn, errorCode);
	}

	public override void OnServerError(NetworkConnection conn, int errorCode)
	{
		Debug.Log(">> OnServerError: "+conn);
		base.OnServerError(conn, errorCode);

	}
}
