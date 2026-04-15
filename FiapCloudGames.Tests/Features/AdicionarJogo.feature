#language: pt-br
Funcionalidade: Adicionar Jogo

Contexto:
	Dado que eu estou autenticado como "Administrador"

Cenário: Adicionar novo jogo com sucesso
	Dado que eu preencho os dados do jogo:
		| Nome              |
		| The Wolf Among Us |
	Quando eu solicitar a adição do jogo
	Então o sistema deve retornar o status 201 Created

Cenário: Erro ao tentar adicionar jogo com dados inválidos
	Dado que eu tento adicionar um jogo com os seguintes dados:
		| Nome   |
		| <nome> |
	Quando eu solicitar a adição do jogo
	Então o sistema deve retornar o status 400 BadRequest
	E a mensagem de erro deve ser "<mensagem>"

Exemplos:
	| nome | mensagem               |
	|      | Insira o nome do jogo. |