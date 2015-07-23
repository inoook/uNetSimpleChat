using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ChatPlayer : NetworkBehaviour {

	[SyncVar]
	public string userName = "user";
	
	private MainUI mainUI;
	
	// Use this for initialization
	void Awake () {
		Debug.Log("Awake");
	}

	public override void PreStartClient()
	{
		Debug.Log("PreStartClient");
		mainUI = GameObject.FindObjectOfType<MainUI>();
	}

	public override void OnStartLocalPlayer()
	{
		Debug.Log("OnStartLocalPlayer");
		string nameStr = isServer ? "user_Server" : "user_Client";

		mainUI.SetChatPlayer(this, nameStr);

		SendName(nameStr);
		CmdSendToServer("connected.");
	}

	public override void OnNetworkDestroy()
	{
		Debug.Log("OnNetworkDestroy "+userName);
		SendMsg("disconnected.");
	}

	public void SendMsg(string str)
	{
		CmdSendToServer(str);
	}

	public void SendName(string str)
	{
		CmdSendName(str);
	}

	//[Client]
	public void ChangeName(string str)
	{
		if( mainUI.CheckSameName(str) ){
			SendMsg("same name.");
			return;
		}
		string currentName = userName;

		SendName(str);
		SendMsg(currentName +" > "+ str + " renamed.");
	}
	
	// client player -> server
	[Command]
	void CmdSendToServer(string str)
	{
		mainUI.AddNameAndMsg(userName, str);
	}

	[Command]
	void CmdSendName(string name)
	{
		userName = name;
	}

}
