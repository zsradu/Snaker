using UnityEngine;
/// <summary>
/// v1.0.0
/// + Nu trece prin ziduri
/// </summary>

public class Player_Movementv1_0_1 : MonoBehaviour
{
    private float timer = 0;

    private float timerMax = 0;

    public float cx = 0, cz = 0, x, z;

    public Vector3[] walls;

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
            if (HitsWall(cx, cz) == false)
            {
                transform.Translate(cx, 0, 0);
                transform.Translate(0, 0, cz);
            }
        }
        else if ((cx != 0 || cz != 0) && HitsWall(cx, cz) == false)
        {
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
        if (HitsWall(Comp_x, Comp_z) == false && HitsEnemy(Comp_x, Comp_z) == false)
            return true;
        return false;
    }
    private bool HitsWall(float Comp_x, float Comp_z)///1 if it will hit wall
    {
        initWalls();

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
    private bool HitsEnemy(float Comp_x, float Comp_z)
    {
        return true;
    }
}