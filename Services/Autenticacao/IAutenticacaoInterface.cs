﻿namespace ProjetoEmprestimoLivroCurso.Services.Autenticacao
{
    public interface IAutenticacaoInterface
    {
        public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
        public bool VerificaLogin(string senha, byte[] senhaHash, byte[] senhaSalt);
    }
}
