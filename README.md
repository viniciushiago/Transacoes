# 🖥️ Backend API - .NET 8

Este projeto é uma API desenvolvida com **.NET 8** seguindo boas práticas de arquitetura e padrões de projeto.  
Utiliza **Repository Pattern**, **DTOs**, **Entity Framework Core** e **SQL Server** como banco de dados.

---

## 🚀 Tecnologias Utilizadas

- **.NET 8** (C#)
- **Entity Framework Core**
- **Repository Pattern**
- **DTOs (Data Transfer Objects)**
- **SQL Server**
- **Dependency Injection**
- **Swagger** para documentação de endpoints
- **Migrations** para versionamento do banco de dados

---

## 📂 Estrutura do Projeto

```plaintext
src/
├── Aplicação/       # Lógica de aplicação, DTOs e casos de uso
├── Dominio/            # Entidades e interfaces
├── Infraestrutura/    # Acesso a dados, contextos e repositórios
├── Transacao/               # Controllers e endpoints

### 2️⃣ Clonar o repositório
```bash
git clone https://github.com/seu-usuario/seu-projeto.git
cd seu-projeto

3️⃣ Configurar o appsettings.json
json
Copiar
Editar
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MinhaAPI;User Id=sa;Password=SuaSenha;"
  }

4️⃣ Criar as tabelas
CREATE TABLE [dbo].[Categorias](
  [Id] BIGINT IDENTITY(1,1) NOT NULL,
  [Nome] NVARCHAR(MAX) NOT NULL,
  [Tipo] INT NOT NULL,
  [Ativo] BIT NOT NULL,
  CONSTRAINT [PK_Categorias] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[Transacoes](
  [Id] BIGINT IDENTITY(1,1) NOT NULL,
  [Descricao] NVARCHAR(MAX) NOT NULL,
  [Valor] DECIMAL(18,2) NOT NULL,
  [Data] DATETIME2 NOT NULL,
  [CategoriaId] BIGINT NOT NULL,
  [Observacoes] NVARCHAR(MAX) NULL,
  [DataCriacao] DATETIME2 NOT NULL,
  CONSTRAINT [PK_Transacoes] PRIMARY KEY CLUSTERED ([Id] ASC),
  CONSTRAINT [FK_Transacoes_Categorias_CategoriaId] FOREIGN KEY ([CategoriaId]) REFERENCES [dbo].[Categorias] ([Id]) ON DELETE CASCADE
);
GO

5️⃣ Rodar o projeto
bash
Copiar
Editar
dotnet run --project API
