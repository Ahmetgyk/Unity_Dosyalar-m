using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour
{

    public GameObject[] positions;

    private Holder holder;

    private int x;
    public int y;
    public int z;

    // Start is called before the first frame update
    void Start()
    {
        holder = GameObject.Find("Holder").GetComponent<Holder>();

        if (holder.array.Count == 0)
        {
            holder.Add(gameObject);
            holder.AddX(gameObject.transform.position.x);
            holder.AddY(gameObject.transform.position.y);
        }

        if (holder.array.Count <= holder.howMany)
        {

            if (z == 0)
            {

                x = Random.Range(0, 4);
                y = 0;

                int something = Check(x);

                while (Check(x) == 1)
                {
                    x = Random.Range(0, 4);
                    something = Check(x);
                }

                if (something == 0)
                {
                    GameObject newObject = Instantiate(gameObject, positions[x].transform.position, Quaternion.identity);
                    holder.Add(newObject);
                    holder.AddX(newObject.transform.position.x);
                    holder.AddY(newObject.transform.position.y);

                    newObject.GetComponent<Town>().z = 1;
                    newObject.GetComponent<Town>().y = x;
                }
                else
                {
                    holder.x = 1;
                }
            }
            else
            {
                GameObject newObject = Instantiate(gameObject, positions[y].transform.position, Quaternion.identity);
                holder.Add(newObject);
                holder.AddX(newObject.transform.position.x);
                holder.AddY(newObject.transform.position.y);

                newObject.GetComponent<Town>().z = 0;
            }
        }


    }

    int Check(int x)
    {
        List<int> numbers = new List<int>();

        for (int i = 0; i < holder.array.Count; i++)
        {
            if (holder.array[i].transform.position == positions[0].transform.position)
            {
                numbers.Add(1);
                break;
            }
        }
        for (int i = 0; i < holder.array.Count; i++)
        {
            if (holder.array[i].transform.position == positions[1].transform.position)
            {
                numbers.Add(1);
                break;
            }
        }
        for (int i = 0; i < holder.array.Count; i++)
        {
            if (holder.array[i].transform.position == positions[2].transform.position)
            {
                numbers.Add(1);
                break;
            }
        }
        for (int i = 0; i < holder.array.Count; i++)
        {
            if (holder.array[i].transform.position == positions[3].transform.position)
            {
                numbers.Add(1);
                break;
            }
        }

        if (numbers.Count == 4)
        {
            return 2;
        }

        for (int i = 0; i < holder.array.Count; i++)
        {
            if (holder.array[i].transform.position == positions[x].transform.position)
            {
                return 1;
            }
        }

        return 0;

    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
