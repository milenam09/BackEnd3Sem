using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicio3
{
    public class Pessoa
    {
        public string Nome;

        public int Idade = 1;
    };

    public void ExibirDados()
        {
            if (Idade <= 0)
            {
                Console.WriteLine($"Nao e possivel ter essa Idade");
                
            }
            else
            {
                Console.WriteLine($"Seu nome e {Nome}, e sua idade e {Idade}");
                
            }
            
        }
}