using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Movementv2_0_1 : MonoBehaviour
{
    private float timerPlayerMove = 0;

    private float timerEnemySpawn = 0;

    private float timerMax = 0;

    public float cx = 0, cz = 0, x, z;

    public Vector3[] walls;

    public int numberOfEnemies = 0;

    public Vector3[] posArray;

    public int lengthArray = 0;

    public double spawnTime = 4d;

    public GameObject enemy;

    //public GameObject[] enemyClones;

    public bool[,] enemyPositions;/// <summary>
                                  /// inainte era de la 0 la 9 pe ambele umm coordonate cand de fapt era de la -4.5 la 4.5
                                  /// acum este de la uhhhh
                                  /// boneless chicken
                                  /// x e de la 0.5 la 49.5
                                  /// z e de la 0.5 la 48.5
                                  /// scad 0.5
                                  /// </summary>

    GameObject destroyedClone;

    List<GameObject> enemyClones = new List<GameObject>();

    public int lengthOfPlayer = 1;



    void Start()
    {
        posArray = new Vector3[2600];

        for (float i = 0.5f; i <= 49.5f; i++) ///for x
            for (float j = 0.5f; j <= 48.5f; j++) ///for z, y=0.25
            {
                lengthArray++;
                posArray[lengthArray].x = (float)i;
                posArray[lengthArray].y = 0.25f;
                posArray[lengthArray].z = (float)j;
            }
        enemyPositions = new bool[55, 55];


    }
    void Update()
    {
        timerPlayerMove += Time.deltaTime;

        timerEnemySpawn += Time.deltaTime;

        if (Waited(0.35f) == true)
        {
            Move();
        }

        GetNewPosition();

        if (timerEnemySpawn >= spawnTime)
        {
            Spawn();
            timerEnemySpawn = 0;
        }
    }

    private void initWalls()
    {
        walls = new Vector3[6];

        walls[0] = GameObject.Find("Margin_Left_Big").transform.position;

        walls[1] = GameObject.Find("Margin_Right_Big").transform.position;

        walls[2] = GameObject.Find("Margin_Up_Big").transform.position;

        walls[3] = GameObject.Find("Margin_Down_Big").transform.position;
    }

    private bool Waited(float seconds)
    {
        timerMax = seconds;

        if (timerPlayerMove >= timerMax)
        {
            return true; //max reached - waited x - seconds
        }

        return false;
    }
    private void Move()
    {
        x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
        z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
        if (x > 0) x = 0.1f;
        else if (x < 0) x = -0.1f;
        if (z > 0) z = 0.1f;
        else if (z < 0) z = -0.1f;
        if (x != 0 || z != 0)
        {
            cx = 10 * x;
            cz = 10 * z;
            if (Movement_Is_Valid(cx, cz) == true)
            {
                if (HitsEnemy(cx, cz) == 2)
                {
                    findAndDestroyClone(cx, cz);
                    lengthOfPlayer += 1;
                    //numberOfEnemies = numberOfEnemies - 1;
                }
                transform.Translate(cx, 0, 0);
                transform.Translate(0, 0, cz);
            }
        }
        else if ((cx != 0 || cz != 0) && Movement_Is_Valid(cx, cz) == true)
        {

            if (HitsEnemy(cx, cz) == 2)
            {
                findAndDestroyClone(cx, cz);
                lengthOfPlayer += 1;
                //numberOfEnemies = numberOfEnemies - 1;
            }
            transform.Translate(cx, 0, 0);
            transform.Translate(0, 0, cz);
        }
        timerPlayerMove = 0;
    }
    private void GetNewPosition()
    {
        x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
        z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
        if (x > 0) x = 0.1f;
        else if (x < 0) x = -0.1f;
        if (z > 0) z = 0.1f;
        else if (z < 0) z = -0.1f;
        if (x != 0 || z != 0)
        {
            cx = 10 * x;
            cz = 10 * z;
        }
    }
    private bool Movement_Is_Valid(float Comp_x, float Comp_z)
    {
        if (HitsWall(Comp_x, Comp_z) == false && HitsEnemy(Comp_x, Comp_z) % 2 == 0)
            return true;
        return false;
    }
    private bool HitsWall(float Comp_x, float Comp_z)///1 if it will hit wall
    {
        Vector3 playerNextPosition = transform.position;

        playerNextPosition.x += Comp_x;

        playerNextPosition.z += Comp_z;

        if (playerNextPosition.x == GameObject.Find("Margin_Left_Big").transform.position.x)
            return true;
        if (playerNextPosition.x == GameObject.Find("Margin_Right_Big").transform.position.x)
            return true;
        if (playerNextPosition.z == GameObject.Find("Margin_Up_Big").transform.position.z)
            return true;
        if (playerNextPosition.z == GameObject.Find("Margin_Down_Big").transform.position.z)
            return true;
        return false;
    }
    private int HitsEnemy(float Comp_x, float Comp_z)///0=nu loveste  1=loveste gresit   2=loveste corect
    {
        if (numberOfEnemies == 0)
            return 0;

        Vector3 playerNextPosition = transform.position;

        playerNextPosition.x += Comp_x;

        playerNextPosition.z += Comp_z;

        /*VERSION 1
         * 
         * 
         * Vector3 enemyPosition = GameObject.Find("Enemy").transform.position;

        if (playerNextPosition == enemyPosition)
        {
            if (playerNextPosition.z - Comp_z > enemyPosition.z)
                return 2;
            else return 1;
        }*/
        ///check to see if hits enemy clone:
        ///in matrice pun pozitiile de la 0 la 9 si adaug 4.5 pt ca de fapt e de la -4.5 la 4.5
        ///acum e de la 0 la 49 si la z e de la 0 la 48
        ///
        if (enemyPositions
            [Mathf.RoundToInt(playerNextPosition.x - 0.5f)
            , Mathf.RoundToInt(playerNextPosition.z - 0.5f)]
            == true)
        {
            if (Comp_z < 0)
                return 2;
            else return 1;
        }



        return 0;
    }
    private int DistanceBetweenTwoPoints(float x1, float z1, float x2, float z2)
    {
        return Mathf.Abs((int)x1 - (int)x2) + Mathf.Abs((int)z2 - (int)z1);
    }
    private void Spawn()
    {
        int spawnPoint;
        float spawnPointX, spawnPointZ;
        do
        {
            spawnPoint = Random.Range(1, lengthArray);
            spawnPointX = posArray[spawnPoint].x;
            spawnPointZ = posArray[spawnPoint].z;
        } while (enemyPositions[Mathf.RoundToInt(spawnPointX - 0.5f), Mathf.RoundToInt(spawnPointZ - 0.5f)] == true
        ||
        DistanceBetweenTwoPoints(spawnPointX, spawnPointZ, transform.position.x, transform.position.z) > 12);


        GameObject clone = Instantiate(enemy, posArray[spawnPoint], Quaternion.identity);

        enemyPositions[Mathf.RoundToInt(spawnPointX - 0.5f), Mathf.RoundToInt(spawnPointZ - 0.5f)] = true;

        numberOfEnemies += 1;

        //enemyClones[numberOfEnemies] = clone;

        enemyClones.Add(clone);
    }
    private void findAndDestroyClone(float Comp_x, float Comp_z)///PROBLEMO
    {
        Vector3 playerNextPosition = transform.position;

        playerNextPosition.x += Comp_x;

        playerNextPosition.z += Comp_z;

        for (int i = 0; i <= enemyClones.Count; i++)
        {
            if (playerNextPosition == enemyClones[i].transform.position)
            {
                destroyedClone = enemyClones[i];
                //System.Array.Resize<>(ref [] enemyClones, numberOfEnemies - 1);
                break;
            }
        }
        enemyClones.Remove(destroyedClone);

        Destroy(destroyedClone);

        numberOfEnemies -= 1;

        enemyPositions
            [Mathf.RoundToInt(playerNextPosition.x - 0.5f)
            , Mathf.RoundToInt(playerNextPosition.z - 0.5f)] = false;
    }
}