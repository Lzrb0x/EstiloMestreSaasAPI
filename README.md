# 💈 EstiloMestre API

### A solução completa para a gestão da sua barbearia.

## 💡 Sobre o Projeto

**EstiloMestre** é uma plataforma SaaS (*Software as a Service*) projetada para modernizar e simplificar a gestão de barbearias de todos os tamanhos. A API robusta serve como a espinha dorsal do sistema, permitindo que donos de estabelecimentos, funcionários e clientes interajam de forma eficiente e segura.

O objetivo é centralizar todas as operações do dia a dia de uma barbearia em um único lugar, oferecendo funcionalidades como:

* **Gestão Completa:** Cadastre sua barbearia, gerencie seus dados e adicione múltiplos funcionários.
* **Catálogo de Serviços Personalizável:** Escolha a partir de uma lista de serviços globais e defina preços e durações específicas para o seu estabelecimento.
* **Gerenciamento de Equipe:** Associe funcionários aos serviços que eles são qualificados para realizar, otimizando a agenda.
* **Agendamentos Inteligentes (Em Desenvolvimento):** Um sistema completo para que clientes possam agendar horários com seus profissionais preferidos, incluindo diferentes preços e tempo de serviço para cada funcionário
* **Autenticação Segura:** Sistema de login e permissões baseado em JWT para garantir que donos, funcionários e clientes acessem apenas o que lhes é permitido.

## 🚀 Tecnologias e Arquitetura

Este projeto foi construído utilizando tecnologias modernas e padrões de design que garantem um software escalável, seguro e de fácil manutenção.

* **Linguagem e Framework Principal**
    * **C# 12**
    * **ASP.NET Core 9:** Para a construção de uma API RESTful de alta performance.

* **Banco de Dados e Acesso a Dados**
    * **Entity Framework Core:** Como ORM para o mapeamento objeto-relacional.
    * **FluentMigrator:** Para controle total e versionamento do schema do banco de dados.

* **Arquitetura e Padrões de Design**
    * **Arquitetura Limpa (Onion Architecture):** Com uma clara separação entre as camadas de Domínio, Aplicação e Infraestrutura.
    * **Domain-Driven Design (DDD):** O modelo de negócio é o centro da aplicação, com entidades ricas e regras de negócio bem definidas.
    * **SOLID:** Os cinco princípios de design de software são seguidos para garantir um código coeso e desacoplado.
    * **Injeção de Dependência:** Utilizada extensivamente em todo o projeto.

* **Segurança**
    * **JWT (JSON Web Tokens):** JWT (JSON Web Tokens) com filtros de autorização customizados.

* **Tecnologias Auxiliares**
    * **AutoMapper:** Para mapeamentos eficientes entre DTOs e Entidades.
 
* **Ferramentas Utilizadas:**
    * **JetBrains Rider:** IDE para o desenvolvimento em C# e .NET, focada em produtividade, refatoração e análise de código em tempo real.
    * **JetBrains DataGrip:** Ferramenta de banco de dados utilizada para gerenciar, consultar e visualizar o schema do banco de dados durante o desenvolvimento e testes.
    * **Postman / Insomnia:** Utilizados para testar os endpoints da API, criar e enviar requisições HTTP complexas, e validar as respostas e o comportamento do sistema.
    * **Git:** Sistema de controle de versão distribuído para gerenciar o histórico do código-fonte e facilitar o trabalho em equipe.
---
