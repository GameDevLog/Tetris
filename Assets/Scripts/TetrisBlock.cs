using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;

    private float previousTime;
    private float fallTime = 0.8f;
    private static int width = 10;
    private static int height = 20;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);

            if (!ValidMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);

            if (!ValidMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // local position -> global position
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90f);

            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90f);
            }
        }


        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);

            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);

            }

            previousTime = Time.time;
        }
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                return false;
            }
        }

        return true;
    }
}