﻿using ProjetoEmprestimoLivroCurso.Data;
using System.Security.Cryptography;

namespace ProjetoEmprestimoLivroCurso.Services.Autenticacao
{
    public class AutenticacaoService : IAutenticacaoInterface
    {
        public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }
        }

        public bool VerificaLogin(string senha, byte[] senhaHash, byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512(senhaSalt)) 
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                return computeHash.SequenceEqual(senhaHash);
            }
        }
    }
}

