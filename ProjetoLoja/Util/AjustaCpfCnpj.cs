using System;
using System.Collections.Generic;
using System.Text;
using ProjetoLoja.Enums;
using ProjetoLoja.Mdl;

namespace ProjetoLoja.Util
{
    internal class AjustaCpfCnpj
    {
        public static string FormataCpfCnpj(Cliente cliente)
        {
            if (cliente == null || string.IsNullOrEmpty(cliente.CpfCnpj))
                return string.Empty;
            switch (cliente.TipoPessoa)
            {
                case EnTipoPessoa.Fisica:
                    return FormataCpf(cliente.CpfCnpj);

                case EnTipoPessoa.Juridica:
                    return FormataCnpj(cliente.CpfCnpj);

                default:
                    return string.Empty;
            }
        }

        private static string FormataCnpj(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
                return string.Empty;
            cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length == 14) // CNPJ
            {
                return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
            }
            else
            {
                return cnpj; // Retorna como está se não for CPF ou CNPJ válido
            }
        }

        private static string FormataCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return string.Empty;
            cpf = cpf.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
            if (cpf.Length == 11) // CPF
            {
                return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
            }
            else
            {
                return cpf; // Retorna como está se não for CPF ou CNPJ válido
            }
        }

    }
}
