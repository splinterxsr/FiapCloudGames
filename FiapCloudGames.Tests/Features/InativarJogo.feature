#language: pt-br
Funcionalidade: Inativar Jogo

Contexto:
	Dado que eu estou autenticado como "Administrador"

Cenário: Inativar jogo com sucesso
	Dado que existe um jogo cadastrado com ID 1
	Quando eu solicitar a inativação do jogo 1
	Então o sistema deve retornar o status 200 OK

Cenário: Erro ao tentar inativar jogo inexistente
	Dado que não existe um jogo cadastrado com ID 999
	Quando eu solicitar a inativação do jogo 999
	Então o sistema deve retornar o status 404 NotFound