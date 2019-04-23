using UnityEngine;
/// <summary>
/// v1.0.0
/// Doar merge ca in snake. 
/// Trece prin pereti si inamici.
/// </summary>


public class Player_Movementv1_0_0 : MonoBehaviour
{
    private float timer = 0;

    private float timerMax = 0;

    public float cx=0, cz=0, x, z;

    void Update()
    {
        timer += Time.deltaTime;

        if (Waited(0.35f) == true)
        {
            Move();
        }
        
        GetNewPosition();
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
            transform.Translate(cx, 0, 0);
            transform.Translate(0, 0, cz);
        }
        else if (cx != 0 || cz != 0)
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
    //private bool Movement_Is_Valid(float )
    private bool HitsWall(float Comp_x, float Comp_z)///1 if it will hit wall
    {
        /*Vector3 playerNextPosition = transform.position;
        playerNextPosition.x += Comp_x;
        playerNextPosition.z += Comp_z;
        Vector3 enemyPosition;
        enemy*/
        return true;
    }
}