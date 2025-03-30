export interface ClienteViewModel {
  id: string;
  nome: string;
  documento: string;
  email: string;
  telefone: string;
  dataNascimento: Date;
  tipo: 'PessoaFisica' | 'PessoaJuridica';
}
