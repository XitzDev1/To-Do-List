namespace Program;

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;

class ProgramToDoList
{
    static void Main(string[] args)
    {
        Console.WriteLine("Bem-vindo à To-Do List!");

        List<Login> usuarios = new();

        if (File.Exists("usuarios.json"))
        {
            string json = File.ReadAllText("usuarios.json");
            usuarios = JsonSerializer.Deserialize<List<Login>>(json) ?? new();
        }

        ViewMain.Run(usuarios);
    }
}