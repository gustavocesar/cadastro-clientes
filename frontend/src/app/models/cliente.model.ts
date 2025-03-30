import { Endereco } from "./endereco.model";

export interface Cliente {
  id: string;
  email: string;
  telefone: string;
  dataNascimento: Date;
  tipo: 'PessoaFisica' | 'PessoaJuridica';
  endereco?: Endereco;
}

export interface PessoaFisica extends Cliente {
  cpf: string;
  nome: string;
}

export interface PessoaJuridica extends Cliente {
  cnpj: string;
  razaoSocial: string;
  inscricaoEstadual: string;
  isento: boolean;
}