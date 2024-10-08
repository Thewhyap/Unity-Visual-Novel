using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class VariablesManager : MonoBehaviour
{
    private const char VARIABLE_CONTAINER_START = '{';
    private const char VARIABLE_CONTAINER_END = '}';
    private const string VARIABLE_REGEX = @"(?<!\\)\{(\w+(\.\w+)*)\}";
    private const char SUB_VARIABLE_SEPARATOR = '.';

    public static VariablesManager instance { get; private set; }

    private Dictionary<string, Lazy<object>> variableDatabase = new Dictionary<string, Lazy<object>>();
    private Dictionary<string, Lazy<Func<object>>> referenceDatabase = new Dictionary<string, Lazy<Func<object>>>();

    private void Awake()
    {
        instance = this;
    }

    public void AddVariable<T>(string name, T value)
    {
        variableDatabase[name.ToLower()] = new Lazy<object>(value);
    }

    public void AddReferenceVariable(string name, Func<object> reference)
    {
        referenceDatabase[name.ToLower()] = new Lazy<Func<object>>(() => reference);
    }

    public object GetVariable(string name)
    {
        name = name.ToLower();

        string[] parts = name.Split(SUB_VARIABLE_SEPARATOR);

        if (parts.Length == 0) throw new KeyNotFoundException($"Variable '{name}' not found.");

        if (variableDatabase.TryGetValue(parts[0], out Lazy<object> value))
        {
            object currentValue = value.Value;
            foreach (string part in parts)
            {
                currentValue = GetSubVariable(currentValue, part);
            }
            return currentValue;
        }
        else if (referenceDatabase.TryGetValue(parts[0], out Lazy<Func<object>> reference))
        {
            object currentValue = reference.Value();
            foreach (string part in parts)
            {
                currentValue = GetSubVariable(currentValue, part);
            }
            return currentValue;
        }

        throw new KeyNotFoundException($"Variable '{name}' not found.");
    }

    private object GetSubVariable(object currentValue, string part)
    {
        Type type = currentValue.GetType();
        PropertyInfo propInfo = type.GetProperty(part);
        FieldInfo fieldInfo = type.GetField(part);

        if (propInfo != null) currentValue = propInfo.GetValue(currentValue);
        else if (fieldInfo != null) currentValue = fieldInfo.GetValue(currentValue);
        else throw new KeyNotFoundException($"Property or field '{part}' not found on type '{type.Name}'.");

        return currentValue;
    }

    public string ParseVariablesInText(string text)
    {
        return System.Text.RegularExpressions.Regex.Replace(text, VARIABLE_REGEX, match =>
        {
            string varName = match.Groups[1].Value;
            try
            {
                object value = GetVariable(varName);
                return value?.ToString() ?? match.Value;
            }
            catch (KeyNotFoundException)
            {
                return match.Value;
            }
        });
    }

    public object ProcessWord(string word)
    {
        if (word.StartsWith(VARIABLE_CONTAINER_START) && word.EndsWith(VARIABLE_CONTAINER_END))
        {
            try
            {
                object variable = GetVariable(word.Substring(1, word.Length - 2));
                return variable;
            }
            catch (KeyNotFoundException)
            {
                return word;
            }

        }
        return word;
    }
}

