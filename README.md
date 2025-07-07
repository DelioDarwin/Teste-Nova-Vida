# Teste-Nova-Vida
Teste Nova Vida

# Dev
Delio Darwin

# Criação do Ambiente de Desenvolvimento
  - Fazer o downlod ou o clone deste repositório do código fonte
  - Atualizar os pacotes Nugets
  - Restaurar o banco SQL Sever que está no diretório raiz: ScriptsBackupBanco
  - Ou poderá também executar o script de criação da estrutura de banco (opção 2)
  - Aterar a string de conexão na key "ConnectionStrings" localizada no arquivo: appsettings.jso, de acordo com o nome do servidor e o tipo de autenticação do ambiente de testes do SQL Server
  - Os arquivos de importação TXT e CSV estão no diretório raiz: ArquivosParaImportacao
  - Ao importar o arquivo pela funcionalidade da aplicação, gerado uma cópia do mesmo na pasta de arquivos estáticos: wwwroot/uploads e em seguida é feita a leitura deste arquivo via stream e convertidas em objetos do model Aluno e gravadas via entity framework no SQL Server

# Tecnologias Utilizadas
  - .Net 9
  - .Net MVC Core
  - Razor
  - Jquery
  - Bootstrap
  - ORM - Entity Framework Core
  - Javascript / CSS / HTML
  - SQL Server
  - Scripts javascript (Jquery.Validation, Jquery.Mask, SweetAlert)

# Recursos adicionais implementados
  = Substituição do Jquery-Valition, pela versão pt-BR alterando o bundles para o padrão regional do Brasil, para validações de data e moedas nos formatos adequados
  - Ajustes nas injeções de depências do Entity Framework Core - implementação da deleção em cascata para (Professores e Alunos associados respectivamente)
  - Configuração do Gloalization na inicialização do program.cs - para que toda solução possa considerar os padrões regionais pt-BR
  - Importação de alunos via Arquivos TXT e CSS, configuração e implementação dos bloqueios para novas importações, crição de uma tabela extra que regista a ultima importação e leitura do appsettings.json para ler o limite de intervalo de tempo entre uma importação e outra (configurada em minutos);
  - Tramento de exeções redirecionamentos das rotas implmentações das actions dos controlores todar a navegação fluida e intuitiva
  - Testes realizados por horas prevendo todos os tipos falhas possíveis
  - Exclusão reativa através do Ajax Jquery sem reload na pagina

# DER Banco SQL Server
![image](https://github.com/user-attachments/assets/004d4270-15eb-4845-be6d-70a800459bb3)

# Arquivo de configuração (Alterar o limite de intervalo de tempo de uma importação ou outra em minutos) / Alterar a string de conexão
![image](https://github.com/user-attachments/assets/a41226d1-a1d4-4be5-9c6a-512f15163e7c)

# Diretórios Extras detalhos acima
![image](https://github.com/user-attachments/assets/8551d74b-e570-4fbf-a7e3-09ce7a41c68a)
![image](https://github.com/user-attachments/assets/dd149963-6221-4d68-9c01-4c23eabc3b8a)

#Screenshots
![image](https://github.com/user-attachments/assets/07486c5d-3f19-439e-ac51-08f44d6a8864)
![image](https://github.com/user-attachments/assets/75ac6529-b514-4fbb-a40f-dbab1b833d65)
![image](https://github.com/user-attachments/assets/4961b78b-7854-4845-918f-05cec6a531df)
![image](https://github.com/user-attachments/assets/75513387-3a04-40e4-88f6-4830f6d5f9f3)
![image](https://github.com/user-attachments/assets/f9a0fe49-ec01-4b87-8ca2-b804458e514e)
![image](https://github.com/user-attachments/assets/51b92a5a-0221-4626-b87c-2525cf2a7417)
![image](https://github.com/user-attachments/assets/4a370000-01f8-4fb2-a960-4e38efe40753)
![image](https://github.com/user-attachments/assets/744c4377-5cd5-419a-9a9a-3f23fb453d77)
![image](https://github.com/user-attachments/assets/5b0f5493-d1f3-4802-bbea-831f3bb1aba4)
![image](https://github.com/user-attachments/assets/44ca4c06-0291-407c-8c26-2988e55dc3ff)
![image](https://github.com/user-attachments/assets/be35de95-bc59-4654-a3de-59c9d665506a)
![image](https://github.com/user-attachments/assets/89dda168-ff8c-4f86-9b2e-2712c85a5f2d)
![image](https://github.com/user-attachments/assets/b85cc923-f5fe-487b-bee6-aa442f5c3e82)
![image](https://github.com/user-attachments/assets/9b5a8546-f676-4ee4-860c-1bbf21b4632e)
![image](https://github.com/user-attachments/assets/b153f007-78ec-4594-b5c0-3c07ab64801b)

-------------------------------------------------------------------------------------------

# Escopo
Criar um sistema para cadastro de professores e alunos de uma instituição de ensino seguindo os requisitos definidos abaixo:

Requisitos Funcionais
 
RN01 - A página inicial deve exibir a lista de professores da instituição;

RN02 - Na lista de professores, disponibilizar opção para visualizar a "lista de alunos" associadas ao professor (a lista deve ser uma página diferente);

RN03 - A página inicial deve exibir uma opção para cadastrar, somente inclusão, professores (essa operação deverá ser realizada em outra página);

RN04 - A página da "lista de alunos" deverá exibir os alunos cadastrados - campos: "Nome", "Mensalidade" (formato R$ 0,00) e "Data de Vencimento" (formato DD/MM/AAAA) -
    e o nome do professor;

RN05 - A página da "lista de alunos" deverá permitir a exclusão de alunos cadastrados, porém, o sistema não deverá re-carregar a página ao
realizar a exclusão do aluno, ou seja, o registro do aluno deverá sumir da tela após emissão do comando;

RN06 - A página da "lista de alunos" deverá permitir que novos alunos sejam cadastrados a partir da importação de um arquivo texto no padrão:

    NomeAluno||ValorMensalidade||DataVencimento
    
RN07 - Ao importar a lista de alunos, para um professor específico, o sistema deverá bloquear a importação por tempo definido no arquivo de configuração appsettings.json (a configuração do banco de dados também deverá estar neste arquivo);

RN08 - As páginas de cadastro de professores e lista de alunos deverão ter opção para que o usuário retorne à página inicial;
