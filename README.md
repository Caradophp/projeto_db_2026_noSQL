# Project Setup Guide

[🇺🇸 English](#project-setup-guide) | [🇧🇷 Português](#guia-de-configuração-do-projeto)

## Prerequisites

- Docker and Docker Compose installed.
- .NET SDK installed.
- A web browser.

## Creating an Oracle Database Instance Using Docker

### `compose.yml` Example

The project already contains a `compose.yml` file; it is the same as the one below.

```yaml
services:
  mongodb:
    image: mongo
    ports:
      - "27017:27017"
    environment: 
      - MONGO_INITDB_ROOT_USERNAME=admin
      - MONGO_INITDB_ROOT_PASSWORD=123
    volumes:
      - data:/data/db

volumes:
  data:
```

## Configuring the `.env` File

The application requires environment variables for email configuration, API security, and encryption.

### 1. Create the `.env` File

Create a `.env` file in the project root directory.

On Linux or macOS, you can run:

```bash
touch .env
```

On Windows, you can create the file manually in the project root directory.

### 2. Define the Environment Variables

Add the following variables to your `.env` file:

```dotenv
EMAIL_FROM=<your_email>
EMAIL_PASSWORD=<your_email_password>
API_SECRECT_KEY=<your_secret_key>
ENCRYPTION_KEY=<your_encryption_key>
MONGO_CONNECTION_STRING="mongodb://admin:123@localhost:27017/?authSource=admin"
```

Replace the placeholders with your actual configuration values.

**Important:** Make sure the environment variable names match those expected by the application. In particular, verify whether `API_SECRECT_KEY` is the intended name or whether it should be `API_SECRET_KEY`.

Keep your `.env` file private and do not commit it to version control. Add `.env` to your `.gitignore` file to prevent accidentally exposing credentials and secrets.

## Before run the project

Before you go to next steps, you should get the `00-create-migrations-table.sql` SQL script and execute it manually in your database
The file is at directory `Database/`

## Running the Project

### 1. Start the Oracle Database

Start the database container using Docker Compose:

```bash
docker-compose up -d
```

### 2. Restore Dependencies

Restore the project's .NET dependencies:

```bash
dotnet restore
```

### 3. Run the Application

Start the application on port `8081`:

```bash
dotnet run --urls "http://0.0.0.0:8081"
```

### 4. Open the Application

Open your web browser and navigate to:

http://localhost:8081/login

## Notes

- The default MongoDB password in the Docker Compose example is `123` and user is `admin`.
- The Mongo database is accessible on port `27017`.
- The application runs on port `8081`.
- Make sure Docker is running before starting the database.
- Ensure that the database connection string and environment variables are correctly configured before running the application.
- The example credentials are intended for local development only. Use secure credentials in production.

---

# Guia de Configuração do Projeto

[🇺🇸 English](#project-setup-guide) | [🇧🇷 Português](#guia-de-configuração-do-projeto)

## Pré-requisitos

- Docker e Docker Compose instalados.
- .NET SDK instalado.
- Um navegador web.

## Criando uma Instância do Oracle Database com Docker

### Exemplo de `docker-compose.yml`

### Exemplo de `compose.yml`

O projeto já contém arquivo `compose.yml`, ele é o mesmo mostrado abaixo.

```yaml
services:
  mongodb:
    image: mongo
    ports:
      - "27017:27017"
    environment: 
      - MONGO_INITDB_ROOT_USERNAME=admin
      - MONGO_INITDB_ROOT_PASSWORD=123
    volumes:
      - data:/data/db

volumes:
  data:
```

## Configurando o Arquivo `.env`

A aplicação utiliza variáveis de ambiente para configurar o envio de e-mails, a segurança da API e a criptografia.

### 1. Crie o Arquivo `.env`

Crie um arquivo `.env` no diretório raiz do projeto.

No Linux ou macOS, você pode executar:

```bash
touch .env
```

No Windows, você pode criar o arquivo manualmente no diretório raiz do projeto.

### 2. Defina as Variáveis de Ambiente

Adicione as seguintes variáveis ao arquivo `.env`:

```dotenv
EMAIL_FROM=<seu_email>
EMAIL_PASSWORD=<sua_senha_de_email>
API_SECRECT_KEY=<sua_chave_secreta>
ENCRYPTION_KEY=<sua_chave_de_criptografia>
MONGO_CONNECTION_STRING="mongodb://admin:123@localhost:27017/?authSource=admin"
```

Substitua os valores de exemplo pelas suas configurações reais.

**Importante:** Os nomes das variáveis de ambiente devem corresponder aos nomes esperados pela aplicação. Em particular, verifique se `API_SECRECT_KEY` é o nome correto ou se deveria ser `API_SECRET_KEY`.

Mantenha o arquivo `.env` privado e não o envie para o repositório. Adicione `.env` ao arquivo `.gitignore` para evitar a exposição acidental de credenciais e informações sensíveis.

## ANtes de executar o projeto

Antes de prosseguir para as próximas etapas, você deve obter o script SQL `00-create-migrations-table` e executá-lo manualmente em seu banco de dados.
O arquivo encontra-se no diretório `Database/`.

## Executando o Projeto

### 1. Inicie o Banco de Dados Oracle

Inicie o contêiner do banco de dados usando o Docker Compose:

```bash
docker-compose up -d
```

### 2. Restaure as Dependências

Restaure as dependências .NET do projeto:

```bash
dotnet restore
```

### 3. Execute a Aplicação

Inicie a aplicação na porta `8081`:

```bash
dotnet run --urls "http://0.0.0.0:8081"
```

### 4. Acesse a Aplicação

Abra o navegador e acesse:

http://localhost:8081/login

## Observações

- A senha padrão do MongoDB  no exemplo do Docker Compose é `123` e o usuário pe `admin`.
- O banco de dados Mongo estará acessível na porta `27017`.
- A aplicação será executada na porta `8081`.
- Certifique-se de que o Docker esteja em execução antes de iniciar o banco de dados.
- Verifique se a string de conexão e as variáveis de ambiente estão configuradas corretamente antes de executar a aplicação.
- As credenciais de exemplo são destinadas apenas ao ambiente de desenvolvimento local. Utilize credenciais seguras em produção.
