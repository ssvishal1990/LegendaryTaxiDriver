using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace characterProperties
public class Character : MonoBehaviour
{
    protected Movement movement;
    protected LegendaryTaxiDriver legendaryTaxiDriver; 
    // Start is called before the first frame update
    protected virtual void Start()
    {
        movement = GetComponent<Movement>();
        legendaryTaxiDriver = new LegendaryTaxiDriver();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
