using COMMANDS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TESTING
{
    public class CMD_DatabaseExtension_Exemples : CMD_DatabaseExtension
    {
        new public static void Extend(CommandDatabase database)
        {
            database.AddCommand("print", new Action(Print));
            database.AddCommand("print_1p", new Action<string>(PrintMessage));

            database.AddCommand("lambda", new Action(() => { Debug.Log("lambda"); }));
            database.AddCommand("lambda_1p", new Action<string>((arg) => { Debug.Log($"lambda' {arg}"); }));

            database.AddCommand("process", new Func<IEnumerator>(SimpleProcess));
            database.AddCommand("process_1p", new Func<string, IEnumerator>(Process));

        }

        private static void Print()
        {
            Debug.Log("test");
        }

        private static void PrintMessage(string message)
        {
            Debug.Log($"User Message : {message}");
        }

        private static IEnumerator SimpleProcess()
        {
            for (int i = 0; i < 10; i++)
            {
                Debug.Log($"Process Running... [{i}]");
                yield return new WaitForSeconds(1);
            }
        }

        private static IEnumerator Process(string data)
        {
            for (int i = 0; i < 10; i++)
            {
                Debug.Log($"Process Running {data}... [{i}]");
                yield return new WaitForSeconds(1);
            }
        }
    }
}