import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cliente, PessoaFisica, PessoaJuridica } from '../models/cliente.model';
import { ClienteViewModel } from '../models/cliente-viewmodel';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  private apiUrl = 'http://localhost:5000';

  constructor(private http: HttpClient) { }

  getClientes(): Observable<ClienteViewModel[]> {
    return this.http.get<ClienteViewModel[]>(`${this.apiUrl}/clientes`);
  }

  getPessoaFisica(id: string): Observable<PessoaFisica> {
    return this.http.get<PessoaFisica>(`${this.apiUrl}/pessoas-fisicas/${id}`);
  }

  getPessoaJuridica(id: string): Observable<PessoaJuridica> {
    return this.http.get<PessoaJuridica>(`${this.apiUrl}/pessoas-juridicas/${id}`);
  }

  createPessoaFisica(cliente: PessoaFisica): Observable<PessoaFisica> {
    return this.http.post<PessoaFisica>(`${this.apiUrl}/pessoas-fisicas`, cliente);
  }

  createPessoaJuridica(cliente: PessoaJuridica): Observable<PessoaJuridica> {
    return this.http.post<PessoaJuridica>(`${this.apiUrl}/pessoas-juridicas`, cliente);
  }

  updatePessoaFisica(id: string, cliente: PessoaFisica): Observable<PessoaFisica> {
    return this.http.put<PessoaFisica>(`${this.apiUrl}/pessoas-fisicas/${id}`, cliente);
  }

  updatePessoaJuridica(id: string, cliente: PessoaJuridica): Observable<PessoaJuridica> {
    return this.http.put<PessoaJuridica>(`${this.apiUrl}/pessoas-juridicas/${id}`, cliente);
  }

  deletePessoaFisica(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/pessoas-fisicas/${id}`);
  }

  deletePessoaJuridica(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/pessoas-juridicas/${id}`);
  }
}