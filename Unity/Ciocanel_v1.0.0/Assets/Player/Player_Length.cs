using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 
/// </summary>

public class Player_Length : MonoBehaviour {

    public float timer=0f;

    public int lastPlayerLength;

    public int newPlayerLength;

    public Vector3 lastPlayerPosition;

    public Vector3 newPlayerPosition;

    public List<GameObject> playerPieces;

    public GameObject player;

    public GameObject clone;

    void Start()
    {
        lastPlayerLength = GameObject.Find("Player_Dragon").GetComponent<Player_Movement>().lengthOfPlayer;

        lastPlayerPosition = GameObject.Find("Player_Dragon").GetComponent<Player_Movement>().transform.position;

        playerPieces = new List<GameObject>();
    }

    private Player_Movement playerMovement;
    // The following getter will try to retrieve it at runtime
    private Player_Movement PlayerMovement
    {
        get
        {
            if (playerMovement == null)
            {
                GameObject playerDragon = GameObject.Find("Player_Dragon");
                if (playerDragon != null)
                    playerMovement = playerDragon.GetComponent<Player_Movement>();
                else
                    Debug.LogError("Can't find Player_Dragon object. Is it enabled ?");
            }
            return playerMovement;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.35f) 
        {
            newPlayerLength = PlayerMovement.lengthOfPlayer;

            newPlayerPosition = PlayerMovement.transform.position;

            lastPlayerLength = newPlayerLength;

            lastPlayerPosition = newPlayerPosition;

            comparePlayerLengths(lastPlayerLength, newPlayerLength);

            movePlayerPieces();

            timer = 0f;
        }
        
    }
    private void comparePlayerLengths(int lastLength,int newLength)
    {
        if(newLength<lastLength)
        {
            cutLastNPieces(lastLength - newLength);
        }

        if (newLength == lastLength) ;

        if (newLength == lastLength + 1)
        {
            createNewPiece();
        }

        if (newLength >= lastLength + 2)
            print("huh?...weird...");
    }
    private void cutLastNPieces(int n)
    {

    }
    private void createNewPiece()
    {
        clone = Instantiate(player, lastPlayerPosition, Quaternion.identity);

        playerPieces.Add(clone);
    }
    private void movePlayerPieces()
    {
        for (int i = playerPieces.Count; i >= 1; i -= 1)
        {
            if (playerPieces[i] != null)
                playerPieces[i].transform.position = playerPieces[i - 1].transform.position;

            else Debug.Log("uhmm"+ i);

        }

        if (playerPieces.Count >= 0 && playerPieces[0]!=null) 
            playerPieces[0].transform.position = lastPlayerPosition;
    }
}
