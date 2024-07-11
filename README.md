# 📚 Projeto de Estudo em .NET 8 com Clean Architecture

Bem-vindo ao repositório do meu projeto de estudo! Este projeto foi desenvolvido com o objetivo de aprender e aplicar os princípios de Clean Architecture utilizando .NET 8. 🚀

## 📂 Estrutura do Projeto

A estrutura do projeto é organizada em várias camadas para garantir a separação de responsabilidades e facilitar a manutenção e evolução do código. As camadas são as seguintes:

- **Infra.Data**: Camada responsável pelo acesso aos dados e implementação do Entity Framework Core.
- **Infra.Ioc**: Camada de injeção de dependências e configuração dos serviços.
- **Domain**: Camada que contém as entidades e interfaces principais do domínio.
- **Application**: Camada que contém a lógica de aplicação, incluindo serviços e comandos.
- **Domain.Testes**: Camada de testes de unidade para o domínio, utilizando xUnit.
- **API**: Camada de API para expor os endpoints HTTP.
- **WebUI**: Interface web do projeto.

## 🛠️ Tecnologias Utilizadas

O projeto foi desenvolvido utilizando as seguintes tecnologias e bibliotecas:

- **.NET 8**: Framework principal do projeto.
- **AutoMapper**: Biblioteca para mapeamento de objetos.
- **MediatR**: Biblioteca para implementação do padrão Mediator.
- **Identity**: Biblioteca para autenticação e autorização.
- **EntityFrameworkCore**: ORM para acesso ao banco de dados.
- **Swagger**: Ferramenta para documentação da API.
- **xUnit**: Framework de testes de unidade.
- **MSSQL Server**: Banco de dados utilizado no projeto.

## 🎓 Aprendizados

Este projeto proporcionou diversos aprendizados importantes, incluindo:

- **Clean Architecture**: Aplicação dos princípios de Clean Architecture para criar uma estrutura de código limpa, escalável e fácil de manter.
- **Injeção de Dependência**: Uso de IoC para gerenciar dependências e melhorar a testabilidade e flexibilidade do código.
- **Mapeamento de Objetos**: Utilização do AutoMapper para simplificar o mapeamento entre objetos de domínio e DTOs.
- **MediatR**: Implementação do padrão Mediator para reduzir o acoplamento entre componentes e facilitar a implementação de lógica de negócios complexa.
- **Autenticação e Autorização**: Configuração do Identity para gerenciar usuários, funções e políticas de segurança.
- **Entity Framework Core**: Uso do EF Core para interagir com o banco de dados de forma eficiente e segura.
- **Documentação de API**: Integração do Swagger para gerar automaticamente a documentação da API, facilitando a comunicação e uso por outras equipes.
- **Testes de Unidade**: Criação de testes de unidade com xUnit para garantir a qualidade e confiabilidade do código.

## 🤝 Contribuições

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests.
