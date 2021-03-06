using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager a;

    private void Start()
    {
        a = GameManager.FindObjectOfType<GameManager>();
    }

    public bool Move(Vector2 direction) //Habilita movimiento diagonal
    {

        if (Mathf.Abs(direction.x) < 0.5) // Setear siempre la coordenada 0
        {
            direction.x = 0;
        }
        else
        {
            direction.y = 0;
        }
        direction.Normalize(); // Hacer que x o y sea = 1
        if (Blocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            transform.Translate(direction);
            a.ScoreUp();
            return true;
        }
    }

    bool Blocked(Vector3 position, Vector2 direction)
    {

        Vector2 newPos = new Vector2(position.x, position.y) + direction;
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (var wall in walls)
        {
            if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
            {
                return true;
            }
        }
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach (var box in boxes)
        {
            if (box.transform.position.x == newPos.x && box.transform.position.y == newPos.y)
            {
                Box bx = box.GetComponent<Box>();
                if (bx && bx.Move(direction))
                {
                    return false;
                }
                else
                {                    
                    return true;
                }
            }
        }
        return false;

    }

}
