# JobTrack — Modelo inicial da base de dados

## 1. Objetivo

Este documento descreve o modelo inicial da base de dados do JobTrack, uma aplicação web para gerir candidaturas a emprego, acompanhar processos de recrutamento e analisar ofertas através de inteligência artificial.

A base de dados será PostgreSQL e o acesso aos dados será implementado com Entity Framework Core.

O modelo poderá evoluir durante o desenvolvimento do projeto.

## 2. Entidades

### 2.1 Users — Utilizadores

Representa os utilizadores registados na aplicação.

Campos principais:
- `Id` — identificador único.
- `FullName` — nome completo.
- `Email` — endereço de email único.
- `CreatedAt` — data de criação da conta.

A autenticação e a gestão segura das palavras-passe serão implementadas com ASP.NET Core Identity.

### 2.2 UserSkills — Competências

Representa as competências registadas por cada utilizador.

Campos principais:
- `Id` — identificador único.
- `UserId` — referência ao utilizador.
- `Name` — nome da competência ou tecnologia.
- `Level` — nível de conhecimento.
- `YearsOfExperience` — anos de experiência, quando conhecidos.

Cada competência pertence a um utilizador.

### 2.3 Companies — Empresas

Guarda informação sobre as empresas associadas às candidaturas.

Campos principais:
- `Id` — identificador único.
- `Name` — nome da empresa.
- `Website` — site oficial, quando conhecido.
- `Location` — localização, quando conhecida.

Uma empresa pode estar associada a várias candidaturas.

### 2.4 JobApplications — Candidaturas

Representa uma candidatura a uma oferta de emprego.

Campos principais:
- `Id` — identificador único.
- `UserId` — referência ao utilizador proprietário.
- `CompanyId` — referência à empresa, quando identificada.
- `JobTitle` — título da oferta.
- `Location` — localização da vaga.
- `WorkMode` — modalidade presencial, remota ou híbrida.
- `Salary` — salário, quando conhecido.
- `JobUrl` — endereço da oferta, quando disponível.
- `JobDescription` — descrição da oferta, quando disponível.
- `Status` — estado atual da candidatura.
- `AppliedAt` — data da candidatura, quando aplicável.
- `Notes` — notas pessoais.
- `CreatedAt` — data de criação do registo.
- `UpdatedAt` — data da última atualização.

O estado poderá assumir valores como `Interested`, `Applied`, `Interview`, `TechnicalTest`, `Offer` e `Rejected`.

Cada candidatura pertence a um utilizador e pode estar associada a uma empresa.

### 2.5 ApplicationStatusHistory — Histórico de estados

Regista as alterações de estado das candidaturas.

Campos principais:
- `Id` — identificador único.
- `JobApplicationId` — referência à candidatura.
- `PreviousStatus` — estado anterior, quando existente.
- `NewStatus` — novo estado.
- `ChangedAt` — data e hora da alteração.

Cada registo representa uma alteração de estado de uma candidatura.

### 2.6 JobAnalyses — Análises de ofertas

Guarda os resultados das análises de compatibilidade entre uma oferta e as competências de um utilizador.

Campos principais:
- `Id` — identificador único.
- `JobApplicationId` — referência à candidatura.
- `MatchPercentage` — percentagem de compatibilidade calculada pela aplicação.
- `MatchedSkills` — competências identificadas como correspondentes.
- `MissingSkills` — competências exigidas que não constam do perfil do utilizador.
- `Summary` — resumo dos resultados.
- `CreatedAt` — data e hora da análise.

As listas de competências poderão ser guardadas em formato JSON no PostgreSQL.

A lógica da aplicação calculará a percentagem de compatibilidade com base em critérios definidos. A IA ajudará a identificar e classificar os requisitos da oferta.

## 3. Relações entre entidades

- Um utilizador pode ter várias competências.
- Um utilizador pode ter várias candidaturas.
- Uma empresa pode estar associada a várias candidaturas.
- Uma candidatura pode ter várias alterações de estado.
- Uma candidatura pode ter várias análises de oferta.

## 4. Regras de integridade e segurança

- Cada utilizador terá um identificador único.
- O email será único entre as contas.
- Cada competência estará associada a um utilizador.
- Cada candidatura terá um utilizador proprietário.
- Os utilizadores só poderão consultar e modificar os seus próprios dados privados.
- As relações entre tabelas serão protegidas por chaves estrangeiras.
- As palavras-passe não serão guardadas em texto simples.
- Os campos obrigatórios e opcionais serão definidos na implementação.

## 5. Decisões técnicas

- Base de dados: PostgreSQL.
- ORM: Entity Framework Core.
- Autenticação: ASP.NET Core Identity.
- Competências: cada utilizador mantém a sua própria lista.
- Histórico: as alterações de estado serão registadas.
- IA: as análises serão guardadas para consulta posterior.

## 6. Evolução futura

O modelo poderá ser alterado através de migrations do Entity Framework Core.

Antes de implementar a base de dados, serão revistas as entidades, as relações, os tipos de dados, as restrições e as regras de eliminação de registos.