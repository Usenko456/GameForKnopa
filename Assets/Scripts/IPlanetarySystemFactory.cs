using UnityEngine;
public interface IPlanetarySystemFactory
{
    IPlanetarySystem Create(double mass);
}