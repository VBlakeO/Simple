using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    public float distance = 0;
    public Transform[] objects;

    public bool vertical = true;
    public bool m_using = true;
    public bool setName = false;
    public string m_name;

    private void OnValidate()
    {
        if (!m_using)
            return;

        if(setName)
        {
            for (int i = 1; i < objects.Length; i++)
            {
                objects[i].name = m_name + "_0" + i;
            }
        }

        if (vertical)
        {
            for (int i = 1; i < objects.Length; i++)
            {
                objects[i].position = new Vector3(objects[i - 1].position.x + distance, objects[i].position.y, objects[i].position.z);
            }
        }
        else
        {
            for (int i = 1; i < objects.Length; i++)
            {
                objects[i].position = new Vector3(objects[i].position.x, objects[i - 1].position.y + distance, objects[i].position.z);
            }
        }
    }

}
