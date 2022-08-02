using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public AreaEffector2D area;
    public BoxCollider2D colider;

    // Start is called before the first frame update
    void Start()
    {
        area = GetComponent<AreaEffector2D>();
        colider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.instance.isInWater)
        {
            if (PlayerController.instance.isAir)
            {
                colider.offset = new Vector2(colider.offset.x, 3.5f);
                colider.size = new Vector2(colider.size.x, 8);
                area.forceMagnitude = 30;
                area.drag = 5;
                area.angularDrag = 5;
            }
            else if (PlayerController.instance.isMetal)
            {
                colider.offset = new Vector2(colider.offset.x, 2);
                colider.size = new Vector2(colider.size.x, 5);
                area.forceMagnitude = 100;
                area.drag = 1.5f;
                area.angularDrag = 1.5f;
            }
            else
            {
                colider.offset = new Vector2(colider.offset.x, 2);
                colider.size = new Vector2(colider.size.x, 5);
                area.forceMagnitude = 140;
                area.drag = 1.5f;
                area.angularDrag = 1.5f;
            }
        }
        else
        {
            if (PlayerController.instance.isAir)
            {
                colider.offset = new Vector2(colider.offset.x, 3.5f);
                colider.size = new Vector2(colider.size.x, 8);
                area.forceMagnitude = 30;
                area.drag = 5;
                area.angularDrag = 5;
            }
            else if (PlayerController.instance.isMetal)
            {
                colider.offset = new Vector2(colider.offset.x, 2);
                colider.size = new Vector2(colider.size.x, 5);
                area.forceMagnitude = 100;
                area.drag = 1.5f;
                area.angularDrag = 1.5f;
            }
            else
            {
                colider.offset = new Vector2(colider.offset.x, 2);
                colider.size = new Vector2(colider.size.x, 5);
                area.forceMagnitude = 100;
                area.drag = 1.5f;
                area.angularDrag = 1.5f;
            }
        }
    }
}
