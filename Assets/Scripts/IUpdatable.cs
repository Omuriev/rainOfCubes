using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IUpdatable<T> where T : class
{
    public event Action<T> Update;
}