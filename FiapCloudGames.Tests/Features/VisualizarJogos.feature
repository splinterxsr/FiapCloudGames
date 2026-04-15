#language: pt-br
Funcionalidade: Visualizar Jogos

Contexto:
	Dado que eu estou autenticado

Cenário: Usuário visualiza informações dos jogos
	Dado que existe um jogo cadastrado com nome "The Wolf Among Us" e ID 1
	Quando eu solicitar <acao>
	Então o sistema deve retornar o status 200 OK

Exemplos:
	| acao                      |
	| a lista de todos os jogos |
	| os detalhes do jogo 1     |