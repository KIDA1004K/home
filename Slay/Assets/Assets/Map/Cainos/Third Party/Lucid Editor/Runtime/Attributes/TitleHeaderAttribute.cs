using System;
using UnityEngine;

namespace Cainos.LucidEditor
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method, AllowMultiple = true)]
    public class T12itleHeaderAttribute : Attribute
    {
        public readonly string title;

        public T12itleHeaderAttribute(string title)
        {
            this.title = title;
        }
    }
}