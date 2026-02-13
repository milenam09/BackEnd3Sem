using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicio6
{
    public string Nome { get; set; }

    public Pessoa(string nome)
    {
        Nome = nome;
    }

    public void Apresentar()
    {
        Console.WriteLine($"Olá! Meu nome é {Nome}.");
    }

    public void Apresentar(string sobrenome)
    {
        Console.WriteLine($"Olá! Meu nome é {Nome} {sobrenome}.");
    }
}