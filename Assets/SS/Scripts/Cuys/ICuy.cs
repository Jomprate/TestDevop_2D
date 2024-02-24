using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICuy
{
    string CuyName { get; set; }
    float InitSpeed { get; set; }
    float Fatigue { get; set; }
    Sprite Image { get; set; }

    void SetCuyName(string newName);
    void SetInitSpeed(float newSpeed);
    void SetFatigue(float newFatigue);


}
public interface ICuyStats
{
    float InitSpeed { get; set; }
    float Fatigue { get; set; }
}

public interface ICuyAppearance
{
    string CuyName { get; set; }
    Sprite Image { get; set; }
}

public interface ICuyBehavior
{
    
    void SetCuyName(string newName);
    void SetInitSpeed(float newSpeed);
    void SetFatigue(float newFatigue);
}
