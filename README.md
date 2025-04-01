# Cadastro de Clientes

## 📋 Sobre o Projeto
Sistema de cadastro de clientes desenvolvido com AngularJS e .NET 8.

## 🔧 Funcionalidades
- Cadastro completo de clientes (Pessoa Física e Jurídica)
- Validações específicas por tipo de cliente
- Gestão de dados cadastrais

### Campos do Cadastro
- Nome/Razão Social
- CPF/CNPJ
- Data de Nascimento
- Telefone
- E-mail
- Endereço:
  - CEP
  - Endereço
  - Número
  - Bairro
  - Cidade
  - Estado

## ⚠️ Regras de Negócio
### Gerais
- CPF/CNPJ e E-mail devem ser únicos por cliente
- Validação completa de todos os campos

### Pessoa Física
- Idade mínima: 18 anos

### Pessoa Jurídica
- Inscrição Estadual (IE) obrigatória ou declaração de isenção

## 🏗️ Arquitetura e Tecnologias
- **Frontend:** AngularJS com Material Design
- **Backend:** .NET 8
- **Padrões Arquiteturais:**
  - Domain-Driven Design (DDD)
  - CQRS (Command Query Responsibility Segregation)
- **Validação:** FluentValidation
- **Testes:** Unitários, com xUnit e Moq
- **Containerização:** Docker e Docker Compose
- **Minimal APIs** - Abordagem simplificada do .NET para criar APIs HTTP com sintaxe mais concisa e menor cerimônia, ideal para microserviços e aplicações menores
- **Entity Framework Core** - ORM para acesso e manipulação do banco de dados
- **SQL Server** - Sistema de gerenciamento de banco de dados relacional
- **ErrorOr** - Biblioteca para tratamento de erros e validações de forma funcional, evitando o uso de exceções
- **Code First** - Abordagem de desenvolvimento onde as entidades são criadas primeiro, e o banco de dados é criado a partir das configurações das entidades
- **Migrations** - Controle de versionamento do banco de dados, permitindo rastrear, aplicar e reverter alterações no esquema
- **SonarQube** - Ferramenta de análise estática de código que ajuda a manter a qualidade do código, identificando bugs, vulnerabilidades de segurança, code smells e cobertura de testes

## 🚀 Como Executar

### Pré-requisitos
- Docker
- Docker Compose

### Passos para Execução

1. Clonar o projeto:
```bash
git clone https://github.com/gustavocesar/cadastro-clientes

cd ./cadastro-clientes
```

2. Construir a imagem:
```bash
docker build -t cadastro-cliente-api ./backend
```

3. Iniciar os containers:
```bash
docker compose -f ./backend/docker-compose.yml up -d
```

4. Inicializar a aplicação (frontend)
```bash
cd frontend

npm install

npm start
```

5. Abrir o endereço http://localhost:4200 no navegador

## Próximos Passos
 - Implementação de Event Sourcing
 - Refatoração das Entidades, para evitar Primitive Obsession
