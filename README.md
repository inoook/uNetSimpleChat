# uNetSimpleChat
Unity3d uNet chat sample.

Unity 5.1.1p2

UnityでuNetを使用した簡単なchatのようなもの。  
アプリケーション２つ（実行ファイルと、editor上など）を起動後、  
LAN Host(H) でサーバー起動し、サーバーのClientがSpawnInfoのPrefabがSpawn。  
もう一つのアプリで LAN Client(C)でサーバーへ接続、SpawnInfoのPrefabがSpawn。  
※このUIは Network Manager HUD で表示されている。


**NetworkManagerDebug**  
NetworkManager をデバッグ用にカスタマイズしたもの  
Debug.Log を表示するのみ。

**ChatPlayer**  
NetworkManagerDebugでSpawnInfo内で設定する。 
Client接続時に生成される。 生成後、自分のものは isLocalPlayer=true となっている。

**MainUI**  
メッセージ用のUI。  
ChatPlayerと参照関係にある。// TODO

##Attribute について
[SyncVar]  
サーバーの値をすべてのクライアントに対して同期する。  
クライアント側から値を変更するときは、[Command]経由で行う。

[Command]  
クライアント(SpawnInfoでSpawnしたもの)からサーバーへ。  
Spawnしたものからでないと、warning (Trying to send command for non-local player.)がでて機能しない。 
isLocalPlayer=true のときだけ使うのが一般的か？

[ClientRpc]
サーバーからクライアントへ。
クライアント(!isServer)から使用すると error (RPC Function RpcAddMsg called on client.) となる。

[Client] [Server]  
どっちで使うか明示的に示したいときにつかう？ 異なる場合は warning 表示される。


##isLocalPlayer=true となる状況
サーバーにつながったプレイヤーはSpawnInfoで設定したprefabから自動でSpawnされる。  
自分のプレイヤーとなるものが isLocalPlayer=true となるようだ。  


##疑問など
・大体の場合において isClient は true となる。これが false となる状況は？
・NetworkIdentity の LocalPlayerAuthority はどういう時に使用するか？
・NetworkIdentity を付けたGameObjectは起動時に不可視となるが、この処理はどこで行われているか？
