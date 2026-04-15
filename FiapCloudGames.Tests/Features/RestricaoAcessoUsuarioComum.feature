#language: pt-br
Funcionalidade: Restrição de Acesso

Esquema do Cenário: Erro ao tentar realizar operação proibida sendo usuário comum
	Dado que eu estou autenticado como "Usuário Comum"
	Quando eu tentar realizar a operação <operacao>
	Então o sistema deve retornar o status 403 Forbidden

Exemplos:
	| operacao                  |
	| listar todos os usuários  |
	| editar o usuário 2        |
	| inativar o usuário 2      |
	| buscar o usuário por ID 2 |
	| adicionar jogo novo       |
	| editar o jogo 1           |
	| inativar o jogo 1         |