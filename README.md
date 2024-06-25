# XP Case
Essa aplicação (API) simula um ambiente de uma corretora para investimentos em ações.

## Como executar

<details>
<summary><strong> Visual Studio</strong></summary><br  />
 Defina o projeto <b>XpCase.API</b> como principal e pressione Ctrl+F5 para executar ou clique com botão direito em cima do projeto e navegue até <code>View > View in Browser</code> conforme a imagem abaixo:
  <br> <br>
<img src="XpCase.API/Images/print_vs-studio.jpg">
  <br> <br>
  Acesse pelo navegador o endereço: <b>https://localhost:7060/swagger/index.html</b> para visualizar os endpoint da aplicação conforme a imagem abaixo: <br> <br>
  <img src="XpCase.API/Images/print_swagger.jpg">
</details>
<details>
<summary><strong> Visual Code</strong></summary><br  />
No terminal acesse o projeto <b>XpCase.API</b> e execute o comando <code>dotnet run</code> conforme a imagem abaixo:  <br> <br>
<img src="XpCase.API/Images/print_vs-code.jpg">
  <br> <br>
  Acesse pelo navegador o endereço: <b>http://localhost:5175/swagger/index.html</b> para visualizar os endpoint da aplicação conforme a imagem abaixo:  <br> <br>
  <img src="XpCase.API/Images/print_swagger.jpg">
</details>

## Como utilizar
Já há dados previamente cadastrados nas tabelas que apoiam a compra e venda de uma ação. Obtenha os dados em:

<b>Assets (Ativos)</b><br>
Em <code>GET/api/Assets/assets-with-positive-value-and-valid-date</code> obtenha a lista dos ativos disponíveis para negociação. <br>

<b>Customers (Clientes)</b><br>
Em <code>GET/api/Customers</code> obtenha a lista de clientes. <br>

<b>Orders (Ordens)</b><br>
Com os dados de ativos e clientes:<br><br>
Execute uma ordem de compra em <code>POST/api/Orders/buy</code>. <br>
Execute uma ordem de venda em <code>POST/api/Orders/sell</code>. <br><br>
Consulte as ordens pendentes para processamento em <code>GET/api/Orders</code> <br>

<b>Matching Engine</b><br>
Essa chamada fará o casamento das ordens, para processar as ordens pendentes ou parciais execute em <code>/api/Orders/execute-batch</code><br>

<b>Transactions (Transações)</b><br>
Em <code>GET/api/Transactions</code> obtenha a lista de transações de compra, venda, depósito ou saque.<br>

<b>Wallets (Carteiras)</b><br>
Em <code>GET/api/Wallets</code> obtenha a lista de carteiras com os dados dos ativos negociados em Ordens.<br>

> **Atenção:** Todos os endpoints estão liberados sem o uso do TOKEN para que os consumo/teste fosse feito de forma desburocratizada.
