# SSCadastroClientes

## Descrição
SSCadastroClientes é um sistema para cadastro de clientes Pessoa Jurídica (PJ) e Pessoa Física (PF). O projeto utiliza uma combinação de tecnologias modernas para fornecer uma solução robusta e eficiente.

## Tecnologias Utilizadas
- **HTML**
- **CSS**
- **jQuery**
- **.NET 8** com arquitetura MVC
- **Entity Framework** com Code First
- **AutoMapper**
- **Bootstrap**

## Execução do Projeto
Para executar o projeto localmente, siga os passos abaixo:

1. Clone o repositório:
    ```bash
    git clone https://github.com/MatheusBelussi-dev/SSCadastroClientes.git
    ```

2. Abra o projeto no Visual Studio.

3. Certifique-se de que o banco de dados `mssqllocaldb` está configurado corretamente.

4. Compile e execute o projeto no Visual Studio (Executar o "RodeEsseProfile").

5. Caso ocorrer algum erro, execute as migrações do Entity Framework para criar o banco de dados:
    ```bash
    Update-Database
    ```



## Funcionalidades
- Cadastro de clientes PJ e PF.
- Visualização, edição e exclusão de clientes cadastrados.
- Interface amigável utilizando Bootstrap.

## Estrutura do Projeto
- **Controllers**: Contém os controladores MVC.
- **Models**: Contém as classes de modelo do Entity Framework.
- **Views**: Contém as views em HTML e Razor.
- **ViewsModel**: Camada onde contem as regras e validações de negócios são inseridas para refletir nas Views
- **Data**: Onde foi criado o contexto do banco de dados
- **Repositories**: Criado para tratar e coletar os dados do banco de dados
- **wwwroot**: Contém arquivos estáticos como CSS, JavaScript e imagens.

## Licença
Este projeto está licenciado sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.
