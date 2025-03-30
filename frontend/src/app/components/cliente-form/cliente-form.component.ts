import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClienteService } from '../../services/cliente.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Cliente } from 'src/app/models/cliente.model';
import { ESTADOS } from './estados.constants';

@Component({
  selector: 'app-cliente-form',
  templateUrl: './cliente-form.component.html',
  styleUrls: ['./cliente-form.component.css']
})
export class ClienteFormComponent implements OnInit {
  clienteForm: FormGroup;
  tipoCliente: 'PessoaFisica' | 'PessoaJuridica' = 'PessoaFisica';
  clienteId: string = '';

  clienteEditando: Cliente | null = null;
  isEditando = false;

  estados = ESTADOS;

  constructor(
    private fb: FormBuilder,
    private clienteService: ClienteService,
    private router: Router,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar
  ) {
    this.clienteForm = this.fb.group({
      tipo: ['PessoaFisica'],
      email: ['', Validators.required],
      telefone: ['', Validators.required],
      dataNascimento: ['', Validators.required],
      cpf: [''],
      nome: [''],
      cnpj: [''],
      razaoSocial: [''],
      inscricaoEstadual: [''],
      isento: [false],
      endereco: this.fb.group({
        cep: [''],
        logradouro: [''],
        numero: [''],
        bairro: [''],
        cidade: [''],
        estado: [''],
      })
    });
  }

  ngOnInit(): void {
    this.clienteId = this.route.snapshot.paramMap.get('id') || '';
    this.tipoCliente = this.route.snapshot.paramMap.get('tipo') as 'PessoaFisica' | 'PessoaJuridica' || 'PessoaFisica';

    if (this.clienteId) {
      this.isEditando = true;
      this.carregarCliente();
    }

    this.clienteForm.get('tipo')?.valueChanges.subscribe(tipo => {
      this.tipoCliente = tipo;
      this.atualizarValidadores();
    });

    // Adicionar listener para o checkbox de isenção
    this.clienteForm.get('isento')?.valueChanges.subscribe(isento => {
      const inscricaoEstadual = this.clienteForm.get('inscricaoEstadual');
      if (isento) {
        inscricaoEstadual?.clearValidators();
        inscricaoEstadual?.setValue('');
      } else {
        inscricaoEstadual?.setValidators([Validators.required]);
      }
      inscricaoEstadual?.updateValueAndValidity({ emitEvent: false });
    });
  }

  private atualizarValidadores(): void {
    this.clienteForm.get('cpf')?.clearValidators();
    this.clienteForm.get('nome')?.clearValidators();
    this.clienteForm.get('cnpj')?.clearValidators();
    this.clienteForm.get('razaoSocial')?.clearValidators();
    this.clienteForm.get('inscricaoEstadual')?.clearValidators();
    this.clienteForm.get('isento')?.clearValidators();

    if (this.tipoCliente === 'PessoaFisica') {
      this.clienteForm.get('cpf')?.setValidators([Validators.required]);
      this.clienteForm.get('nome')?.setValidators([Validators.required]);
    } else {
      this.clienteForm.get('cnpj')?.setValidators([Validators.required]);
      this.clienteForm.get('razaoSocial')?.setValidators([Validators.required]);
    }

    this.clienteForm.get('cpf')?.updateValueAndValidity({ emitEvent: false });
    this.clienteForm.get('nome')?.updateValueAndValidity({ emitEvent: false });
    this.clienteForm.get('cnpj')?.updateValueAndValidity({ emitEvent: false });
    this.clienteForm.get('razaoSocial')?.updateValueAndValidity({ emitEvent: false });
    this.clienteForm.get('inscricaoEstadual')?.updateValueAndValidity({ emitEvent: false });
    this.clienteForm.get('isento')?.updateValueAndValidity({ emitEvent: false });
  }

  private carregarCliente(): void {
    if (this.tipoCliente === 'PessoaFisica') {
      this.clienteService.getPessoaFisica(this.clienteId!).subscribe(cliente => {
        this.clienteForm.patchValue(cliente);
        this.clienteEditando = cliente;
      });
    } else {
      this.clienteService.getPessoaJuridica(this.clienteId!).subscribe(cliente => {
        this.clienteForm.patchValue(cliente);
        this.clienteEditando = cliente;
      });
    }
  }

  onSubmit(): void {
    if (this.clienteForm.valid) {
      const formData = this.clienteForm.value;

      if (this.tipoCliente === 'PessoaFisica') {
        if (this.isEditando) {
          this.clienteService.updatePessoaFisica(this.clienteId!, formData).subscribe(() => {
            this.snackBar.open('Cliente atualizado com sucesso!', 'Fechar', { duration: 3000 });
            this.router.navigate(['/clientes']);
          });
        } else {
          this.clienteService.createPessoaFisica(formData).subscribe(() => {
            this.snackBar.open('Cliente cadastrado com sucesso!', 'Fechar', { duration: 3000 });
            this.router.navigate(['/clientes']);
          });
        }
      } else {
        if (this.isEditando) {
          this.clienteService.updatePessoaJuridica(this.clienteId!, formData).subscribe(() => {
            this.snackBar.open('Cliente atualizado com sucesso!', 'Fechar', { duration: 3000 });
            this.router.navigate(['/clientes']);
          });
        } else {
          this.clienteService.createPessoaJuridica(formData).subscribe(() => {
            this.snackBar.open('Cliente cadastrado com sucesso!', 'Fechar', { duration: 3000 });
            this.router.navigate(['/clientes']);
          });
        }
      }
    }
  }
}