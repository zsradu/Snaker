using UnityEngine;
/// <summary>
/// v1.0.1 
/// + Trece prin inamic doar de sus si il omoara astfel, din alta parte nu trece prin el
/// </summary>

public class Player_Movementv1_0_2 : MonoBehaviour
{
    private float timer = 0;

    private float timerMax = 0;

    public float cx = 0, cz = 0, x, z;

    public Vector3[] walls;

    public int numberOfEnemies = 1;

    void Update()
    {
        timer += Time.deltaTime;

        if (Waited(0.35f) == true)
        {
            Move();
        }

        GetNewPosition();
    }

    private void initWalls()
    {
        walls = new Vector3[6];

        walls[0] = GameObject.Find("Margin_Left").transform.position;

        walls[1] = GameObject.Find("Margin_Right").transform.position;

        walls[2] = GameObject.Find("Margin_Up").transform.position;

        walls[3] = GameObject.Find("Margin_Down").transform.position;
    }

    private bool Waited(float seconds)
    {
        timerMax = seconds;

        if (timer >= timerMax)
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
                    Destroy(GameObject.Find("Enemy"));
                    numberOfEnemies = numberOfEnemies - 1;
                }
                transform.Translate(cx, 0, 0);
                transform.Translate(0, 0, cz);
            }
        }
        else if ((cx != 0 || cz != 0) && Movement_Is_Valid(cx, cz) == true)
        {

            if (HitsEnemy(cx, cz) == 2)
            {
                Destroy(GameObject.Find("Enemy"));
                numberOfEnemies = numberOfEnemies - 1;
            }
            transform.Translate(cx, 0, 0);
            transform.Translate(0, 0, cz);
        }
        timer = 0;
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

        if (playerNextPosition.x == GameObject.Find("Margin_Left").transform.position.x)
            return true;
        if (playerNextPosition.x == GameObject.Find("Margin_Right").transform.position.x)
            return true;
        if (playerNextPosition.z == GameObject.Find("Margin_Up").transform.position.z)
            return true;
        if (playerNextPosition.z == GameObject.Find("Margin_Down").transform.position.z)
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

        Vector3 enemyPosition = GameObject.Find("Enemy").transform.position;

        if (playerNextPosition == enemyPosition)
        {
            if (playerNextPosition.z - Comp_z > enemyPosition.z)
                return 2;
            else return 1;
        }
        else return 0;
    }
}