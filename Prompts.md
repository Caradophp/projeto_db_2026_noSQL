# Registro de análise e prompts

**Data desta entrada:** 27/11/2026  
**Projeto analisado:** Cultiva# (`projeto_db_2026_noSQL`)

## 1. Prompt recebido

> Quero que analise toda a estrtura desse projeto. Escaneie todos os diretórios, arquivos, etc. e busque entender o projeto como um todo de forma geral. Não faça absolutamente nenhuma modificação nos arquivos e pastas da estrutura desse projeto. Você deve apenas analisá-los e entender o que fazem e como funcionam. Após concluir a análise, quero que faça um pequeno relatório em forma de texto sem gerar arquivos de texto ou coisas similares resumindo em poucas linhas e em uma linguagem simples e de fácil entendimento sobre o que você conseguiu descorbrir sobre o projeto. Também quero que você gere um arquivo Prompts.md contendo e detalhando todos as informações, modificações, prompts, etc. que forem feitas por você e de forma completa. Você deverá atualizar esse arquivo Prompts.md a cada prompt enviado, adicionando e atualizando os conteúdos contidos nele com base nas tarefas realizadas. Você deve gerar esse arquivo Prompts.md no diretório raiz do projeto.

## 2. Interpretação e escopo

O pedido proíbe mudanças na estrutura e, ao mesmo tempo, pede explicitamente a criação de `Prompts.md` na raiz. Foi adotada a interpretação mais restrita: criar somente o arquivo de documentação solicitado e não alterar os demais arquivos. O histórico abaixo registra o prompt do usuário e as ações desta tarefa; não inclui instruções internas do sistema ou do ambiente.

## 3. Trabalho realizado

- Inventariados os arquivos e diretórios do projeto, incluindo código, configuração, conteúdo estático, arquivos de build e logs.
- Lidos os arquivos próprios de configuração, documentação, inicialização, controllers, serviços, repositórios, modelos, segurança, páginas Razor e scripts do frontend.
- Verificadas as chaves configuradas em `.env` sem registrar seus valores.
- Arquivos binários, dependências distribuídas, saídas de build e logs foram identificados por tipo/localização, sem reproduzir conteúdo sensível.
- Não foram executados build, testes, migrações, comandos de escrita no banco ou inicialização da aplicação.
- Esta é a única alteração feita nesta tarefa: criação de `Prompts.md`.
- A alteração preexistente em `.gitignore` foi observada e preservada.

## 4. Visão geral do projeto

Cultiva# é uma aplicação web de gestão agrícola implementada em ASP.NET Core MVC com .NET 8. As páginas são Razor (`.cshtml`) e usam JavaScript no navegador, Bootstrap, CSS próprio, jQuery e alguns recursos externos de ícones. O backend é organizado em controllers, services e repositories. Os repositórios usam MongoDB, no banco `cultiva`, para persistir usuários e dados de propriedades, talhões, culturas, plantios, colheitas, insumos, aplicações e códigos de recuperação.

O sistema contém telas de login, cadastro e recuperação de senha, um painel inicial e telas de operações agrícolas. O login cria um JWT; o frontend guarda o token em `localStorage` e o envia em requisições protegidas. As senhas são armazenadas com hash BCrypt. A recuperação gera código temporário e envia e-mail por SMTP.

## 5. Estrutura identificada

- **Raiz:** `Program.cs` configura o pipeline web, injeção de dependências, tratamento de exceções e Serilog; `projeto.csproj` define .NET 8 e dependências. `appsettings*.json` contém configurações gerais de logging/hosts. `compose.yaml` declara um MongoDB local. `.env` contém configurações de conexão, autenticação, criptografia e e-mail; os valores não foram copiados para este documento. `README.md` fornece instruções de instalação e execução. `.gitignore` define exclusões.
- **`Controllers/`:** endpoints MVC para usuários, login, página inicial e operações de propriedades, talhões, culturas, safras, plantios, colheitas, insumos e aplicações. `ExceptionController` centraliza respostas de exceções não tratadas.
- **`Models/`:** modelos MongoDB e modelos de apresentação. Inclui `User`, `Propriedade`, `Talhao`, `Cultura`, `Plantio`, `Colheita`, `Insumo`, `AplicacaoInsumo`, `Safra`, `Code`, `TableInfo`, `ManagementListViewModel` e tipos auxiliares. `Enums/` contém os estados do usuário.
- **`Repository/`:** acesso direto às coleções MongoDB, com operações de leitura e CRUD; inclui também operações para usuários/códigos e um repositório para dados do painel.
- **`Service/`:** validações e coordenação entre controllers e repositórios, além de serviços de autenticação de usuário, hash/cifra e envio de e-mail.
- **`Security/`:** geração e validação de JWT (`Jwt.cs`) e filtro que decide se a requisição exige token (`SecurityFilter.cs`).
- **`Views/`:** páginas Razor agrupadas por funcionalidade e componentes compartilhados. As telas agrícolas têm tabelas, formulários/modais e indicadores; há páginas separadas de autenticação.
- **`wwwroot/css/`:** estilos gerais e folhas próprias para dashboard, login/cadastro e funcionalidades agrícolas.
- **`wwwroot/js/`:** scripts de interface, chamadas aos endpoints, filtros de tabela, modais e autenticação. `api.js` está vazio. Bibliotecas de terceiros estão em `wwwroot/lib/`.
- **`wwwroot/img/` e `wwwroot/favicon.ico`:** imagens de interface e ícone do site.
- **`bin/` e `obj/`:** dependências e arquivos gerados pelo build do .NET; não são a implementação-fonte principal.
- **`logs/`:** arquivos de log gerados pela aplicação.

## 6. Fluxos e observações da leitura

- As telas de cadastros fazem chamadas HTTP para actions dos controllers; os controllers delegam regras aos serviços e persistência aos repositórios.
- Os dados agrícolas são associados principalmente por IDs (por exemplo, talhão e cultura em plantios, talhão e insumo em aplicações).
- O painel inicial solicita quantidades por categoria, mas `HomeRepository.GetRegisterNumbersPerTable()` retorna atualmente um dicionário vazio; portanto, essa fonte não fornece contagens.
- A documentação do `README.md` menciona Oracle e scripts SQL em `Database/`, mas o inventário atual não contém esse diretório e a implementação observada usa MongoDB. O próprio README também traz instruções de execução/configuração que não estão plenamente alinhadas ao projeto atual.
- Há uma tela de relatórios que aponta para `/Reports/viewer`, porém não foi encontrado controller de Reports nem uma página viewer na estrutura inventariada.
- Existem arquivos de modelo, serviço, repositório e controller para `Safra`, mas não há cadastro desses serviços no `Program.cs` nem uma tela/scripts específicos de safras no inventário; essa funcionalidade aparenta estar incompleta.
- Não foi encontrada uma pasta de testes no inventário.

## 7. Histórico de alterações deste documento

### Entrada 1 — Análise geral do projeto

- **Pedido:** analisar a estrutura e o funcionamento geral, não modificar o projeto, produzir um breve resumo em texto e criar/atualizar este `Prompts.md`.
- **Resultado:** análise registrada nas seções acima; criado este arquivo na raiz.
- **Outros arquivos alterados por esta tarefa:** nenhum.
- **Dados secretos registrados:** nenhum; os valores de `.env` foram omitidos.

## 8. Atualizações futuras

Para cada novo prompt atendido neste projeto, acrescentar uma entrada cronológica nesta seção e atualizar as seções de escopo, trabalho e conclusões que tenham mudado. Registrar somente o conteúdo fornecido pelo usuário e as ações/resultados do trabalho, sem copiar credenciais, dados sensíveis ou instruções internas.

### Entrada 2 — Implementação da pesquisa global

- **Data:** 27/11/2026.
- **Prompt recebido:**

  > Após analisar toda a estrutura desse projeto, você deve ter descoberto que na tela da página principal da aplicação há uma barra de pesquisa no cabeçalho da página que não funciona. Quero que você faça o seguinte:
  >
  > "Desenvolva métodos nos arquivos HomeController, HomeService e no HomeRepository do projeto e ajuste o frontend do projeto para fazer essa barra de pesquisa funcionar".
  >
  > OBS1: Os arquivos do backend do projeto à serem modificados ou adicionados devem ser escritos usando a linguagem de programação C#. Quantos aos arquivos à serem modificados ou adicionados ao frontend do projeto, eles devem ser escritos usandos as linguagens de programação HTML, CSS e JavaScript.
  >
  > OBS2: Lembre-se também de atualizar o arquivo Prompts.md adcionando ou modificando as informações necessárias contidas nele com base na tarefa solicitada.
  >
  > OBS3: Neste momento, você tem permissão para modificar os arquivos necessários do projeto para realizar a tarefa solicitada.
  >
  > OBS4: Após concluir as modificações no projeto, quero que faça um pequeno relatório em forma de texto sem gerar arquivos de texto ou coisas similares resumindo em poucas linhas o que você fez no projeto, como arquivos que modificou, localizações deles, trechos de códigos que foram alterados ou adicionados e quaisquer outras informações relevantes que possam me nortear sobre o que você fez no projeto.
- **Implementação:** adicionada uma busca de texto parcial e sem distinção entre maiúsculas/minúsculas para propriedades, talhões, culturas, plantios, colheitas, insumos e aplicações. O backend retorna no máximo cinco correspondências por coleção, com categoria, título, descrição e rota de destino; termos menores que dois caracteres são ignorados e termos acima de 100 caracteres são rejeitados.
- **Frontend:** a barra do cabeçalho agora pesquisa após uma pausa curta enquanto o usuário digita, mostra estados de carregamento/erro/sem resultados, exibe resultados em uma lista suspensa navegável por teclado e permite abrir a tela correspondente. O conteúdo textual é inserido como texto, sem interpretar valores retornados como HTML.
- **Arquivos modificados:** `Controllers/HomeController.cs`, `Service/HomeService.cs`, `Repository/HomeRepository.cs`, `Views/Shared/_Layout.cshtml`, `wwwroot/css/site.css`, `wwwroot/js/site.js` e `Prompts.md`.
- **Arquivo adicionado:** `Models/HomeSearchResult.cs`, DTO usado para transportar os resultados.
- **Validação:** `dotnet build projeto.csproj --no-restore` concluiu com sucesso (0 avisos, 0 erros); `node --check wwwroot/js/site.js` e `git diff --check` também passaram. Não há projeto de testes identificado e não foi feita chamada a uma instância MongoDB.
- **Observação:** a busca respeita os dados e as telas atualmente implementados; as coleções de usuários e safras foram excluídas do resultado para não expor dados de conta nem apontar para a tela de safras ainda incompleta.
