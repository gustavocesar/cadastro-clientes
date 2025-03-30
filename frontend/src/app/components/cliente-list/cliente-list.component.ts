import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ClienteService } from '../../services/cliente.service';
import { ClienteViewModel } from 'src/app/models/cliente-viewmodel';

@Component({
  selector: 'app-cliente-list',
  templateUrl: './cliente-list.component.html',
  styleUrls: ['./cliente-list.component.css']
})
export class ClienteListComponent implements OnInit {
  displayedColumns: string[] = ['nome', 'documento', 'email', 'telefone', 'tipo', 'acoes'];
  dataSource: MatTableDataSource<ClienteViewModel>;

  constructor(
    private clienteService: ClienteService,
    private snackBar: MatSnackBar
  ) {
    this.dataSource = new MatTableDataSource();
  }

  ngOnInit(): void {
    this.carregarClientes();
  }

  carregarClientes(): void {
    this.clienteService.getClientes().subscribe(
      clientes => this.dataSource.data = clientes
    );
  }

  excluirCliente(id: string, tipo: 'PessoaFisica' | 'PessoaJuridica'): void {
    if (confirm('Tem certeza que deseja excluir este cliente?')) {
      const excluir = tipo === 'PessoaFisica' ?
        this.clienteService.deletePessoaFisica(id) :
        this.clienteService.deletePessoaJuridica(id);


      excluir.subscribe(() => {
        this.carregarClientes();
        this.snackBar.open('Cliente excluído com sucesso!', 'Fechar', {
          duration: 3000
        });
      });
    }
  }
}