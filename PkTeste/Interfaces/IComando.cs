using System;
using System.Collections.Generic;
using System.Text;
using PkTeste.Interfaces;

namespace PkTeste
{
    public interface IComando
    {
        string id { get; }
        string desc { get; }
        string[] args { get; }

        /// <summary>
        /// Recebe uma string com o id deste comando e mais entradas. Pega os parametros que cabem a este comando e retorna a string com o que sobrar.
        /// </summary>
        /// <param name="argumentoPassado">String com este comando e possíves argumentos para ele.</param>
        string getMeusParametros(string comandosEArgumentosPassados);

        /// <summary>
        /// Verifica se o comando tem o necessário para rodar
        /// </summary>
        /// <returns>Tem o necessário ou não</returns>
        bool Validar();
    }
}
