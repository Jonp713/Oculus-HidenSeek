using UnityEngine;
using System.Collections;




public class NetworkManager : MonoBehaviour {


	private int playernumber;

	public GameObject toadPrefab;
	public GameObject marioPrefab;


	public Light marioLight;

	public Light flashlight;
	
	private const string typeName = "xxx333";
	private const string gameName = "bingshot";
	
	public bool customNetwork = false;

	public bool flashlightOn = false;

	void Start(){

		//StartServer();

		DontDestroyOnLoad(transform.gameObject);

		
	}
	
	private void StartServer()
	{
		Application.LoadLevel("corescene");

		if (customNetwork) {

			MasterServer.ipAddress = "127.0.0.1";
			MasterServer.port = 23466;
			Network.natFacilitatorIP = "127.0.0.1";
			Network.natFacilitatorPort = 50005;

		}

		//MasterServer.ipAddress = "127.0.0.1";
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);

	}
	
	void OnServerInitialized()
	{

		Debug.Log("Server Initialized");

		SpawnPlayer(false);
	}
	
	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");

		SpawnPlayer(true);
	}
	
	private void SpawnPlayer(bool mario)
	{

		Vector3 startloc;

		switch (playernumber)
		{
		case 0:
			startloc = new Vector3 (0f, 2f, 0f);

			break;
		case 1:
			startloc = new Vector3 (1f, 2f, 1f);
			break;
		case 2:
			startloc = new Vector3 (-1f, 2f, 1f);
			break;
		case 3:
			startloc = new Vector3 (-1f, 2f, -1f);
			break;
		case 4:
			startloc = new Vector3 (1f, 2f, -1f);
			break;
		default:
			startloc = new Vector3 (0f, 2f, 0f);
			print("playertnumber above5");
			break;
		}


		print(startloc);

		if (mario) {

			GameObject player = (GameObject)Network.Instantiate (marioPrefab, startloc, Quaternion.identity, 0);
			GameObject.Find ("Main Camera").GetComponent<PlayerFollow> ().player = player;
			player.name = "Me";

			DontDestroyOnLoad (player);

			
			//Instantiate(marioLight);


		} else {

			GameObject player = (GameObject)Network.Instantiate (toadPrefab, startloc, Quaternion.identity, 0);
			GameObject.Find ("Main Camera").GetComponent<PlayerFollow> ().player = player;
			player.name = "Me";

			DontDestroyOnLoad (player);

			
			if(flashlightOn){

				Light theFlashlight = (Light)Network.Instantiate (flashlight, player.transform.position, player.transform.rotation, 0);
				GameObject.Find ("Me").GetComponent<Flashlight> ().flashlight = theFlashlight;
				
			}else{

				GameObject.Find ("Me").GetComponent<Flashlight> ().enabled = false;
			}
		}


		
		
	}
	
	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer)
		{
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
				StartServer();

			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
				RefreshHostList();
			
			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
						JoinServer(hostList[i]);
				}
			}
		}
	}
	

	private HostData[] hostList;
	private HostData OurHostData;

	
	private void RefreshHostList()
	{

		if (customNetwork) {
			
			MasterServer.ipAddress = "127.0.0.1";
			MasterServer.port = 23466;
			Network.natFacilitatorIP = "127.0.0.1";
			Network.natFacilitatorPort = 50005;
			
		}


		MasterServer.RequestHostList(typeName);
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}
	
	private void JoinServer(HostData hostData)
	{


		Network.Connect(hostData);

		OurHostData = hostData;
		playernumber = hostData.connectedPlayers;

		Application.LoadLevel("corescene");

	}





}
