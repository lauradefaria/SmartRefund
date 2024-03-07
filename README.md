# ProjetoFinalDiverseDEV

## ⚡ Desafio 1 - Inteligência Artificial
- No cenário empresarial, uma das tarefas que mais causam contratempos é o processo de lançamento de notas fiscais e cupons para reembolso. Muitas pessoas encontram dificuldades em preencher de forma correta, resultando em uma série de problemas, tanto para os colaboradores quanto para a equipe financeira da empresa, consumindo um tempo valioso de todos.

## 📋 Detalhe técnico
- Para resolver esse desafio, gostaríamos de utilizar o processamento de imagem para preenchermos os dados da despesa a partir do comprovante.

- Para extrairmos essas informações da despesa através de uma imagem, iremos utilizar a API de Imagem do ChatGPT-4 que será disponibilizada para vocês.

## 🚀 Proposta de solução
- Criar uma API de OCR para o lançamento de despesas através de uma imagem de comprovante.

- Essa API deverá enviar a imagem para o ChatGPT-4 e realizar alguns prompts para extrair as informações necessárias para o preenchimento.

- O primeiro passo seria validar o comprovante `prompt: essa imagem e algum comprovante de comprovante fiscal? responda com SIM ou NAO` Caso o comprovante seja inválido, devemos retornar a seguinte informação:

```
HTTP/1.1 400 Bad Request
{
  "message": "Comprovante Inválido"
}
```

- Em seguida, poderíamos extrair as informações da despesa.
  
  - Categoria da despesa `prompt: que categoria de despesa é essa, entre: hospedagem, transporte, viagem, alimentação ou Outros`.
  - Valor total da despesa `prompt: qual o valor dessa despesa`
  - Descricao da despesa `prompt: descreva sobre a despesa`
    
- Após extrair todas as informações, devemos gravar em um banco de dados qualquer a despesa na seguinte estrutura de exemplo:

```
{
  "Total": 99.00
  "Categoria": "HOSPEDAGEM",
  "Descricao": "Descricao do comprovante",
  "Status": "SUBMETIDO"
}
```

- Criar endpoint de consulta de despesas para que o Financeiro consiga visualizar todas as notas com status em SUBMETIDO.

- Criar endpoint para que o financeiro consiga mudar o status da despesa para PAGA.