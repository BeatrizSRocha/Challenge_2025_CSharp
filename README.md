# Challenge_2025_CSharp

## Integrantes

- Beatriz Silva Pinheiro Rocha | RM553455
- Isabelle Toricelli da Silva | RM552806
- Luis Alberto Rocha Filho | RM553507
- Rafael Alves do Nascimento | RM553117 

## O que o programa faz

O aplicativo console em C# para registrar e acompanhar atividades de saúde (minutos de exercício, litros de água ou horas de sono).

- Armazena registros em arrays internos (string[], double[], DateTime[]) com redimensionamento automático via `Array.Resize`;
- Lista registros cadastrados de forma organizada;
- Calcula e exibe, por tipo de atividade, a soma total e a média dos valores;
- Validações: tipo não pode ser vazio; valor precisa ser numérico e não negativo; data aceita formatos comuns.

## Como usar a interface
Ao executar o programa aparecerá um menu numerado:

Menu:
1 - Adicionar registro  
2 - Listar registros  
3 - Exibir estatísticas  
4 - Sair

- Escolha a opção digitando o número e pressionando Enter.
- Ao adicionar registro, informe:
  - Tipo: texto (ex.: Exercício, Água, Sono) — obrigatório;
  - Data: aceite `dd/MM/yyyy` ou `yyyy-MM-dd`;
  - Valor: número (não negativo), ex.: minutos, litros ou horas.

## Observações
- Os dados são mantidos apenas em memória durante a execução (não há persistência em arquivo).


