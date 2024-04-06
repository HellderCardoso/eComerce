API seguindo boas práticas do SOLID e Design Patterns, feita com .NET 8.0 e Entity Framework. Também fizemos testes unitários usando XUnit. Como usamos o Entity Framework com migrações.
Estrutura e Camadas

1.	commerce.API:  esta camada é responsável por receber requisições, encaminhá-las para a camada de Application e retornar as respostas das requisições
2.	comerce.aplication: Aqui são tratados os dados e aplicadas as regras de negócio.
3.	comerce.data: Cuida de criar, ler, atualizar e excluir os dados no banco de dados.
4.	comerce.domain: Aqui é onde guardamos as entidades e os modelos de visualização (viewModels) do projeto.
5.	comerce.Application.Teste: Esta parte é toda dedicada aos testes da camada comerce.Application. 
6.	comerce.moce: Guardamos os mocks de teste para as camadas comecer.application e comerce.api.

Api e-comerce

- Instalar Postgree SQL Versão 14 ou superior;
- Versão SDK .Net 8.0;

•	clonar repositório:  ` https://github.com/HellderCardoso/eComerceApi.git`
•	configurar AppSettings para estância desejada (localhost; user; password;)
