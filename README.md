# Meu Projeto CRUD - C# .NET + Angular + ASP.NET Core

Este é um projeto CRUD básico para o gerenciamento de tarefas, composto por uma aplicação **frontend** em **Angular 18** com **Bootstrap 5.0** e um **backend** em **ASP.NET Core 8.0** utilizando o Entity Framework Core para gerenciamento do banco de dados.

## Tecnologias Utilizadas

- **Frontend:** Angular 18, Bootstrap 5.0  
- **Backend:**.NET 8, ASP.NET Core 8.0, Entity Framework Core  
- **.NET SDK:** 8.0.403  
- **Runtime do ASP.NET Core:** 8.0.10  
- **.NET Core Runtime:** Versões 6.0.x e 8.0.x (o runtime ativo é 8.0.11, conforme especificado no ambiente)

## Pré-requisitos

Antes de iniciar, certifique-se de ter instalado em sua máquina:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js e npm](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli) (caso não esteja instalado globalmente, utilize `npm install -g @angular/cli`)
- [Visual Studio](https://visualstudio.microsoft.com/) (para rodar o backend com IIS Express)

## Estrutura do Projeto

- **Frontend:**  
  Localizado em `C:\Cloud\Angular\MeuProjetoCrudAngular`  
  O código fonte principal encontra-se na pasta `src/app`.

- **Backend:**  
  Localizado em `C:\Cloud\ASPNet\MeuProjetoCrudAPI`  
  Contém pastas para Controllers, Data, Migrations, Models, Properties, além do arquivo de solução (`.sln`).  
  Nesta parte do projeto estão configuradas as migrações do Entity Framework para a criação e atualização do banco de dados.

## Como Rodar o Projeto

### Backend (ASP.NET Core via IIS Express)

1. **Abra o projeto no Visual Studio:**

   - Navegue até a pasta `C:\Cloud\ASPNet\MeuProjetoCrudAPI` e abra o arquivo de solução (`.sln`) com o Visual Studio.

2. **Restaure os pacotes e configure as migrações:**

   - Se necessário, utilize o **Package Manager Console** do Visual Studio para restaurar os pacotes:
     
     ```powershell
     C:\Cloud\ASPNet\MeuProjetoCrudAPI> Update-Package -reinstall
     ```
     
   - Para atualizar o banco de dados com as migrações existentes, abra o **Package Manager Console** e execute:
     
     ```powershell
     C:\Cloud\ASPNet\MeuProjetoCrudAPI> Update-Database
     ```
     
   - *Caso as migrações não estejam incluídas no repositório, será necessário gerá-las:*
     
     ```powershell
     Add-Migration InitialCreate
     Update-Database
     ```

3. **Execute o projeto:**

   - Selecione **IIS Express** como configuração de inicialização e pressione **F5** ou clique em **Iniciar**.
   - O Visual Studio abrirá o navegador com a URL definida no arquivo *launchSettings.json*.

### Frontend (Angular 18 + Bootstrap 5.0)

1. **Navegue até a pasta do projeto frontend:**

   *cd C:\Cloud\Angular\MeuProjetoCrudAngular*

2. **Instale as dependências:**

   npm install

3. **Inicialize o servidor de desenvolvimento:**
   ng serve --open

  Este comando compila o projeto e inicia um servidor de desenvolvimento local, que abre automaticamente o projeto no seu navegador.



 ## Desenvolvido por Bruno 
 ## linkedin.com/in/051bruno/

