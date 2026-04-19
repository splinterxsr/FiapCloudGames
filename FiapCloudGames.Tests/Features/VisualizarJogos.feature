#language: pt-br
Funcionalidade: Visualizar Jogos

Cenário: Usuário e Administrador visualizam informações dos jogos
	Dado que eu estou autenticado como "<perfil>"
	E que existe um jogo cadastrado com nome "The Wolf Among Us" e ID 1
	Quando eu solicitar <acao>
	Então o sistema deve retornar o status 200 OK

Exemplos:
	| perfil        | acao                      |
	| Usuario       | a lista de todos os jogos |
	| Usuario       | os detalhes do jogo 1     |
	| Administrador | a lista de todos os jogos |
	| Administrador | os detalhes do jogo 1     |